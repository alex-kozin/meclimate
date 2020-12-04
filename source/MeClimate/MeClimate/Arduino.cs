using System;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;

namespace MeClimate
{
    public class Arduino
    {
        private SerialPort Sport { get; set; }
        private string ESPip { get; set; } //IP-адрес регулятора (ESP8266)
        private int ESPport { get; set; } //Порт для связи с регулятором
        private NetworkStream ESPstream { get; set; } //поток ввода/вывода по Wi-Fi
        public bool S_or_W { get; private set; } //тип связи: 1 = Serial;  0 = Wi-Fi
        public int maxTemperature { get; private set; }
        public int minTemperature { get; private set; }
        public int normalizeTime { get; private set; }
        public string phoneNumber { get; private set; }
        public int lowTemperature { get; set; }
        public int highTemperature { get; set; }
        public int nowTemperature { get; private set; }

        public int lowLight { get; private set; }
        public int nowLight { get; private set;}

        public bool IsEnabled { get; private set; }
        
        public void Configure(double minTemp, double maxTemp, int secToNormalize)
        {
            minTemperature = this.TempToCode(minTemp);
            maxTemperature = this.TempToCode(maxTemp);
            normalizeTime = secToNormalize;
            this.SetValues(this.CodeToTemp(lowTemperature), this.CodeToTemp(highTemperature), this.CodeToLux(lowLight));
        }

        public void SetWireless(string ip, int port)
        {
            this.ESPip = ip;
            this.ESPport = port;
            this.S_or_W = false;
        }

        public void SetSport(SerialPort _sport)
        {
            this.Sport = _sport;
            this.S_or_W = true;
        }

        public Arduino()
        {
            Sport = new SerialPort("COM20", 9600, Parity.None, 8, StopBits.One);
            ESPip = "192.168.0.222";
            ESPport = 888;
            S_or_W = false;
        }

        //public bool SportConnect()
        //{
        //    bool result = true;
        //    if (this.S_or_W)
        //    {
        //        try
        //        {
        //            this.Sport.Open();
        //        }
        //        catch { result = false; }
        //    }
        //    return result;
        //}

        public bool WirelessConnect(TcpClient ESPclient)
        {
            bool result = true;
            if (!this.S_or_W)
            {
                try
                {
                    ESPclient.Connect(ESPip, ESPport);
                    this.ESPstream = ESPclient.GetStream();
                    this.ESPstream.ReadTimeout = 3000;
                }
                catch { result = false; }
            }

            return result;
        }

        public bool ReadPhoneNumber()
        {
            bool result = true;
            try
            {
                Byte[] phone = System.Text.Encoding.UTF8.GetBytes("Q");
                this.ESPstream.Write(phone, 0, phone.Length);
                phone = new Byte[12];
                Thread.Sleep(400);
                int l = this.ESPstream.Read(phone, 0, phone.Length);
                Thread.Sleep(400);
                string tmpNum = System.Text.Encoding.ASCII.GetString(phone, 0, l);
                this.phoneNumber = tmpNum;
            }
            catch { result = false; }
            return result;
        }

        public bool ReadValues()
        {
            bool result = true;
            string tmp1 = ""; //здесь будут сформированы прочитанные данные
            Thread.Sleep(400);
            try
            {
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("I");
                Thread.Sleep(400);
                this.ESPstream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(400);
                bytes = new Byte[256];
                ESPstream.ReadTimeout = 10000;
                int k = this.ESPstream.Read(bytes, 0, bytes.Length);
                Thread.Sleep(400);
                tmp1 = System.Text.Encoding.ASCII.GetString(bytes, 0, k);
                Thread.Sleep(400);
                string ESPotvet = tmp1.Trim();
            }
            catch { result = false; }
            // Обработка полученных параметров регулирования и данных о температуре и освещении
            string[] tmp2 = tmp1.Split('.');
            for (int j = 0; j < tmp2.Length-1; j++)
            {
                string command = tmp2[j].Substring(0, 1);
                int value = int.Parse(tmp2[j].Substring(1));
                if (command == "A") this.minTemperature = value;
                if (command == "L") this.lowTemperature = value;
                if (command == "T") this.nowTemperature = value;
                if (command == "H") this.highTemperature = value;
                if (command == "Z") this.maxTemperature = value;
                if (command == "X") this.lowLight = value;
                if (command == "Y") this.nowLight = value;
                if (command == "C") this.normalizeTime = value;
            }

            return result;
        }

        public bool SetValues(double lowT, double highT, double lowL)
        {
            bool result = true;
            string tmp1 = "";
            Thread.Sleep(400);
            tmp1 = "A" + this.minTemperature + ".L" + TempToCode(lowT) + ".H" + TempToCode(highT) + ".Z" + this.maxTemperature +
                   ".X" + LuxToCode(lowL) + ".C" + this.normalizeTime + ".";
            try
            {
                tmp1 += '\n';
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(tmp1);
                Thread.Sleep(400);
                ESPstream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(400);
            }
            catch { result = false; }

            return result;
        }

        public double CodeToTemp(int code)
        {
            double temp = (double)(Convert.ToDouble(code) / 2 - 50);
            return temp;
        }

        public int TempToCode(double temp)
        {
            int code = (int)Math.Round((temp + 50)*2);
            return code;
        }

        public double CodeToLux(int code)
        {
            //double lux = Math.Round((510- 10240.0/code)*10/476, 2);
            //double lux = Math.Round(476.0*(51 - 5 / (code * 5000 / (1000*1024))), 2);
            double lux = Math.Round((code - 24) * 8.0, 2);
            return lux;
        }

        public int LuxToCode(double lux)
        {
            int code = (int)Math.Round(lux/8.0 + 24);
            return code;
        }

        public bool CheckWork()
        {
            bool result = true;
            try
            {
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("O");
                this.ESPstream.Write(bytes, 0, bytes.Length);
                bytes = new Byte[1];
                int k = this.ESPstream.Read(bytes, 0, bytes.Length);
                string tmp1 = System.Text.Encoding.ASCII.GetString(bytes, 0, k);
                string ESPotvet = tmp1;
                if (tmp1 == "K")
                    this.IsEnabled = false;
                else
                    this.IsEnabled = true;
            }
            catch { result = false; }
            return result;
        }

        public bool Restart()
        {
            bool result = true;
            try
            {
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("R");
                this.ESPstream.Write(bytes, 0, bytes.Length);
            }
            catch { result = false; }
            return result;
        }

        public bool TurnRegulator(bool state)
        {
            bool result = true;
            try
                {
                    Thread.Sleep(400);
                    if (state)
                    {
                        Thread.Sleep(400);
                        Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("N");
                        Thread.Sleep(400);
                        ESPstream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(400);
                    }
                    else
                    {
                        Thread.Sleep(400);
                        Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("K");
                        Thread.Sleep(400);
                        ESPstream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(400);
                    }
                }
                catch { result = false; }
            return result;
        }

        public bool SetPhoneNumber(string telnum)
        {
            bool result = true;
            try
            {
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes("P" + telnum);
                Thread.Sleep(400);
                ESPstream.Write(bytes, 0, bytes.Length);
                this.phoneNumber = telnum;
            }
            catch {result = false;}
            return result;
        }
    }
}
