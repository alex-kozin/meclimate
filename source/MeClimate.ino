// ****************************************************
// Регулятор температуры и освещения (курсовая работа)
//
// ****************************************************
// Arduino --- GSM-module
// 5V >--------> 5V
// GND >-------> GND
// GND >-------> BOOT
// D9 >--------> TXD
// D8 >--------> RXD
#include <EEPROM.h>
#include <SoftwareSerial.h>
SoftwareSerial ESPport(10, 11);
//-------------------- RX, TX
#define BUFFER_SIZE 128    //размер буфера Wi-Fi
char buffer[BUFFER_SIZE];  //буфер обмена Wi-Fi
int reset_pin = 12;  //пин кнопки возврата к заводским настройкам
int speak_pin = 7;   //пин льезокерамического излучателя
int red_pin   = 6;   //пин превышения верхней границы температуры (красный с/д)
int yel_pin   = 5;   //желтый с/д аварии - превышение разумных пределов температуры
int gre_pin   = 4;   //зелёный с/д индикации состояния регулятора температуры
int blu_pin   = 3;   //пин занижения нижней границы температуры (синий с/д)
int whi_pin   = 2;   //выходной пин регулятора освещения (белый светодиод)
int t_pin     = 5;   //аналоговый пин датчика температуры
int light_pin = 4;   //аналоговый пин датчика освещённости

//Объявление переменных датчика ТМР36 при напряжении 5V tk=(t+50)*2.048
int tk_max    = 200; //температура: максимальная 50
int tk_high   = 170; //температура: верхняя_граница 35
int tk_now;          //текущее значение температуры
int tk_low    = 130; //температура: нижняя_граница 15
int tk_min    = 100; //температура: минимальная 0
int t_counter = 300; //интервал нормализации температуры (сек)

//Объявление и инициализация переменных Фоторезистора VT90N2 при напряжении 5V
int light_low; //освещённость: нижняя_граница (темно)
int light_now;       //освещённость в настоящий момент

//Объявление прочих переменных
long t_begin;        //стартовое значение мсек
bool S_or_W;         //переключатель канала связи с регулятором
char pusk;           //команда Разрешения/Запрета работы регулятора
int yel_ind;         //управление желтым светодиодом
int gre_ind;         //управление зелёным светодиодом
bool sendSms;        //разрешение на отправку смс
char telnum[12];          //номер телефона для отправки СМС

void setup() //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
{
  //EEPROM.write(0,255);
 if (EEPROM.read(0) == 255)
  {
   //начальная инициализация
   tk_max    = 200; //температура: максимальная 50
   EEPROM_intWrite(tk_max,1);
   tk_high   = 170; //температура: верхняя_граница 35
   EEPROM_intWrite(tk_high,3);
   tk_low    = 130; //температура: нижняя_граница 15
   EEPROM_intWrite(tk_low,5);
   tk_min    = 100; //температура: минимальная 0
   EEPROM_intWrite(tk_min,7);
   t_counter = 300; //интервал нормализации температуры (сек)
   EEPROM_intWrite(t_counter,9);
   light_low = 600; //освещённость: нижняя_граница (темно)
   EEPROM_intWrite(light_low,11);
   pusk = 'K';      //команда Разрешения(N)/Запрета(K) работы регулятора
   EEPROM.write(13,0);
   EEPROM.write(14,1);
   String temp = "380989617943";
   for(int i = 0; i<12; i++)
   {
    telnum[i] = temp[i];
    EEPROM.write(15+i, temp[i]);
   }   
   EEPROM.write(0, 0);
   void(* resetFunc) (void) = 0; // объявляем функцию reset
         resetFunc(); //вызываем reset
  }
 else
  {
    tk_max = EEPROM_intRead(1);
    tk_high = EEPROM_intRead(3);
    tk_low = EEPROM_intRead(5);
    tk_min = EEPROM_intRead(7);
    t_counter = EEPROM_intRead(9);
    light_low = EEPROM_intRead(11);
    if(EEPROM.read(13)==0)
      pusk = 'K';
    else
      pusk = 'N';
    sendSms = EEPROM.read(14);
    for(int i = 0; i < 12; i++)
    {
      telnum[i] = EEPROM.read(15+i);
    }
  }
  
  //Инициализация переменных
  yel_ind = -1;    //>0 - зажечь; <0 - потушить; 0 - не трогать
  gre_ind = -1;    //>0 - зажечь; <0 - потушить; 0 - не трогать
  t_begin = 0;     //сбросить интервал
  S_or_W = false;  //1(true)=Serial или 0(false)=Wi-Fi канал связи с регулятором

  //Инициализация портов и пинов
  pinMode(reset_pin, INPUT);  //режим пина возврата заводских настроек
  pinMode(red_pin,OUTPUT);   //режим пина охлаждателя (красный с/д)
  pinMode(yel_pin,OUTPUT);   //режим пина аварии (желтый с/д)
  pinMode(gre_pin,OUTPUT);   //режим пина регулятора температуры (зеленый с/д)
  pinMode(blu_pin,OUTPUT);   //режим пина нагревателя (голубой с/д)
  pinMode(whi_pin,OUTPUT);   //режим пина управления освещённостью (белый с/д)
  pinMode(speak_pin,OUTPUT); //режим пина пьезокерамического излучателя
  digitalWrite(reset_pin, HIGH); //слушаем кнопку рестарта
  digitalWrite(red_pin,LOW); //выключить охлаждатель
  digitalWrite(yel_pin,LOW); //выключить желтый с/д
  digitalWrite(gre_pin,LOW); //выключить зеленый с/д
  digitalWrite(blu_pin,LOW); //выключить нагреватель
  digitalWrite(whi_pin,LOW); //выключить освещение
  Serial.begin(9600);        //инициализация COM-порта
  while (!Serial) {;}        //ожидание подключения родного USB-СОМ-порта
  Serial.println("Please wait some seconds ...");

  //---------------------------------------------------
  //Настройка связи с ESP-иодулем и инициализация Wi-Fi
  //---------------------------------------------------
  Serial.println(Get_Resp("AT+RESTORE",3000));         //перезагрузка ESP
  Serial.println(Get_Resp("AT+CIOBAUD=9600",300));     //установка скорости 
  ESPport.begin(9600);                                 // ESP8266
  Serial.println(Get_Resp("AT+CWMODE=3",300));         //смешаный режим
  Serial.println(Get_Resp("AT+CIPSERVER=0", 300));     //остановить сервер (если он был запущен)
  Serial.println(Get_Resp("AT+CIPMUX=0",300));         //single connection
  Serial.println(Get_Resp("AT+CIPMODE=0",300));        //сквозной режим передачи данных  
  Serial.println(Get_Resp("AT+CIPMUX=1",300));         // multiple connection
  Serial.println(Get_Resp("AT+CWSAP=\"MeClimate\",\"\",2,0",1000)); //идентификатор сети,пароль,канал,режим шифрования
  Serial.println(Get_Resp("AT+CIPAP=\"192.168.0.222\"", 300));   //IP-адрес
  Serial.println(Get_Resp("AT+CIPSERVER=1,888", 300)); //запустить сервер на порту 888
  Serial.println(Get_Resp("AT+CIPSTO=7200", 300));     // таймаут сервера в сек
   for(int i = 0; i < 40; i++)
            {
              Serial.println(EEPROM.read(i));
            }
  Clear_Soft_Port();                                   //очистить буфер Soft-порта  
  Clear_Serial_Port();                                 //очистить буфер Serial-порта  
  Play_Start();                                        //проиграть стартовую мелодию
}

void loop() //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
{
  //Обработка перезапуска
  if(digitalRead(reset_pin)==LOW)
  {
    EEPROM.write(0,255);
       void(* resetFunc) (void) = 0; // объявляем функцию reset
         resetFunc(); //вызываем reset
  }
  
  //~~~~~~ Опрос датчиков
  tk_now = analogRead(t_pin);         //читаем код сигнала датчика температуры
  light_now = analogRead(light_pin);  //читаем код сигнала датчика освещения

  if (S_or_W)
  { //связь с регулятором по ПРОВОДАМ
    Get_Serial(); //ввод команд и параметров регулирования через Serial
  }
  else
  { //связь с регулятором через WI-FI
    Get_Soft();   //ввод команд и параметров регулирования через SoftwareSerial
  }

  //~~~~~~ Основной исполнительный блок регулятора
  if (pusk == 'N') //если есть разрешение на работу
  {  
     //обработка показаний датчика освещённости
     if (light_now < light_low) digitalWrite(whi_pin,HIGH); //включаем освещение
     else digitalWrite(whi_pin,LOW);  //отключаем освещение

     //обработка показаний датчика температуры

     if (tk_now > tk_max || tk_now < tk_min) //авария - температура вне разумных границ
     { //ВЫКЛЮЧИТЬ ВСЁ И БИТЬ ТРЕВОГУ
       digitalWrite(red_pin,LOW); //вык.охлаждатель
       digitalWrite(blu_pin,LOW); //вык.нагреватель
       t_begin = 0;
       if(sendSms)
        GsmSend("Warning! Your home temperature is out of limits!");
     }

     if (tk_now > tk_high || tk_now < tk_low) //тек.температура требует коррекции 
     {
        if (tk_now > tk_high) //надо включать охлаждатель
        { 
          digitalWrite(blu_pin,LOW);  //выключить нагреватель
          digitalWrite(red_pin,HIGH); //включить охлаждатель
          //проверить не превышен ли интервал нормализации температуры
          if (t_begin == 0) //начало процесса охлаждения
          { 
            t_begin = millis();
            EEPROM.write(14,1);
            sendSms = true;
          }
          else if (abs(millis() - t_begin)/1000 > t_counter) //превышен интервал
          { 
            gre_ind *= (-1);  //начать мигать
            delay(500); 
            if(sendSms)
              GsmSend("Warning! Your home temperature is out of limits!");
            playNote('C', 500); //и подавать звук
          }
        }
        if (tk_now < tk_low)  //надо включать нагреватель
        { 
          digitalWrite(red_pin,LOW);  //выключить охлаждатель
          digitalWrite(blu_pin,HIGH); //включить нагреватель
          //проверить не превышен ли интервал нормализации температуры
          if (t_begin == 0) //начало процесса нагревания
          { 
            t_begin = millis();
            EEPROM.write(14,1);
            sendSms = true;
          }
          else if (abs(millis() - t_begin)/1000 > t_counter) //превышен интервал
          { 
            gre_ind *= (-1);    //начать мигать
            delay(500); 
            if(sendSms)
              GsmSend("Warning! Your home temperature is out of limits!");
            playNote('c', 500); //и подавать звук
          }
        }
     }
     else //температура в норме
     { 
       digitalWrite(red_pin,LOW); //выключить охлаждатель
       digitalWrite(blu_pin,LOW); //выключить нагреватель
       t_begin = 0;               //сбросить интервал
       gre_ind = 1;               //установить признак нормальной температуры 
       yel_ind = -1;
       EEPROM.write(14,1);
       sendSms = true;
     }
  }
  else if (pusk == 'K') //~~~~~~~~~~~~~~~~~~~~ команда прекратить работу регулятора
  {
    if(sendSms == false)
    {
       yel_ind *= (-1);    //начать мигать
       playNote('C', 100); //подать звук
       playNote('b', 100); //подать звук
       playNote('a', 100); //подать звук
       delay(200);
    }
    else
    {
      digitalWrite(red_pin,LOW); //вык.охлаждатель
      digitalWrite(blu_pin,LOW); //вык.нагреватель
      digitalWrite(whi_pin,LOW); //вык.освещение
      digitalWrite(yel_pin,LOW); //вык.желтый индик-р
      digitalWrite(gre_pin,LOW); //вык.зеленый индик-р
      yel_ind = -1;              //признак = погасить с/д
      gre_ind = -1;              //признак = погасить с/д
      t_begin = 0;               //сбросить интервал
      EEPROM.write(14,1);
      sendSms = true;
    }
  }

  //индикация параметров
  if (gre_ind > 0) digitalWrite(gre_pin,HIGH); //зажечь индикатор нормы
  if (gre_ind < 0) digitalWrite(gre_pin,LOW);  //погасить индикатор нормы
  if (yel_ind > 0) digitalWrite(yel_pin,HIGH); //зажечь индикатор аварии
  if (yel_ind < 0) digitalWrite(yel_pin,LOW);  //погасить индикатор аварии

  if (S_or_W) delay(200); //задержка цикла опроса при канале связи Serial

} //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//~~~ Ввод параметров регулирования через SoftSerial
void Get_Soft()
{ 
  int ch_id, packet_len;
  char *pb, inp_v, rrr[70] = "", ccc[8] = "";
    
  ESPport.readBytesUntil('\n', buffer, BUFFER_SIZE); //читаем порт до 0x0A
  if(strncmp(buffer, "+IPD,", 5)==0) //если есть идентификатор принятых данных
  {
    sscanf(buffer+5, "%d,%d", &ch_id, &packet_len); //№_канала_WiFi, длина_данных
    if (packet_len > 0)      //если длина принятых данных > 0
    {
      pb = buffer+5;         //пропускаем №_канала + длину и ...
      while(*pb != ':') pb++;//ищем начало принятых данных
      pb++;                  //становимся в их начало
      inp_v = pb[0];         //запоминаем команду 

      if (inp_v == 'S')      //переход на работу через Serial (по проводам)
      { 
        Clear_Soft_Port();   //очистить буфер Soft-порта 
        Clear_Serial_Port(); //очистить буфер Serial-порта  
        S_or_W = true;       //изменить режим обмена
        playNote('C', 100);  //подать звук
      }

      //пуск/останов регулятора
      if (inp_v == 'N' || inp_v == 'K') 
      {
         pusk = inp_v; //запомнить команду
         if(inp_v == 'N')
          EEPROM.write(13,1);
         else
          EEPROM.write(13,0);
         playNote('c', 100);  //подать звук
      }

      if(inp_v == 'R'){
         void(* resetFunc) (void) = 0; // объявляем функцию reset
         resetFunc(); //вызываем reset
      }

      if(inp_v == 'P'){
        playNote('c', 100);  //подать звук
        for(int i = 0; i < 12; i++)
        {
          pb++;
          telnum[i] = pb[0];
          EEPROM.write(15+i, telnum[i]);
        }
        delay(200);
      }

      if(inp_v == 'Q'){
        ESPport.print("AT+CIPSEND="); // ответ клиенту
        ESPport.print(ch_id); //номер конекта клиента
        ESPport.print(",");
        ESPport.println(12); //длина передаваемых данных
        delay(20);
        if(ESPport.find(">")) //есть приглашение на передачу
         {
          for(int i = 0; i < 12; i++)
            {
              telnum[i] = EEPROM.read(15+i);
            }
            ESPport.print(telnum);
            Serial.println(telnum);
            for(int i = 0; i < 40; i++)
            {
              Serial.println(EEPROM.read(i));
            }
            playNote('c', 100);  //подать звук
         }
         delay(200);
      }

      if(inp_v=='O'){
        ESPport.print("AT+CIPSEND="); // ответ клиенту
        ESPport.print(ch_id); //номер конекта клиента
        ESPport.print(",");
        ESPport.println(1); //длина передаваемых данных
        delay(20);
        if(ESPport.find(">")) //есть приглашение на передачу
         {
           int val = EEPROM.read(13);
           if(val == 0)
           {
              ESPport.print('K'); //передать данные через Wi-Fi
           }
           else
           {
              ESPport.print('N'); //передать данные через Wi-Fi
           }
           delay(200); 
           playNote('a', 100);  //подать звук
         }
      }

      //установить параметры регулировки температуры/освещения
      if (inp_v == 'A' || inp_v == 'L' || inp_v == 'H' || inp_v == 'Z' || inp_v == 'X' || inp_v == 'C') 
      { int val = 0;
 
        while (true)
        { 
          if (pb[0] >= '0' && pb[0] <= '9') //формирование значения параметра
          { val *= 10;   
            val += (pb[0] & 15);
          }

          if (pb[0] == '.') //запомнить значение сформированного параметра
          { 
            if (inp_v == 'Z')
            {
              tk_max = val;
              EEPROM_intWrite(val,1);
            }
            if (inp_v == 'H')
            {
              tk_high = val;
              EEPROM_intWrite(val,3);
            }
            if (inp_v == 'L')
            {
              tk_low = val;
              EEPROM_intWrite(val,5);
            }
            if (inp_v == 'A')
            {
              tk_min = val;
              EEPROM_intWrite(val,7);
            }
            if (inp_v == 'C')
            {
              t_counter = val;
              EEPROM_intWrite(val,9);
            }
            if (inp_v == 'X')
            {
              light_low = val;
              EEPROM_intWrite(val,11);
            }
            pb++;
            val = 0;
            inp_v = pb[0];
            playNote('e', 100);  //подать звук
          }

          if (pb[0] == '\n') //закончить обработку строки параметров
          { inp_v = ' ';
            break;
          }
          pb++;
        }
      }

      //получить состояние/данные от регулятора
      if (inp_v == 'I')
      {
        strcat(rrr,"A"); itoa(tk_min,ccc,10);    strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"L"); itoa(tk_low,ccc,10);    strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"T"); itoa(tk_now,ccc,10);    strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"H"); itoa(tk_high,ccc,10);   strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"Z"); itoa(tk_max,ccc,10);    strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"X"); itoa(light_low,ccc,10); strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"Y"); itoa(light_now,ccc,10); strcat(rrr,ccc);  strcat(rrr,".");  
        strcat(rrr,"C"); itoa(t_counter,ccc,10); strcat(rrr,ccc);  strcat(rrr,"."); 
        strcat(rrr,"\r\n");
        ESPport.print("AT+CIPSEND="); // ответ клиенту
        ESPport.print(ch_id); //номер конекта клиента
        ESPport.print(",");
        ESPport.println(strlen(rrr)); //длина передаваемых данных
        delay(20);
        if(ESPport.find(">")) //есть приглашение на передачу
         {
           ESPport.print(rrr); //передать данные через Wi-Fi
           delay(200); 
           playNote('a', 100);  //подать звук
         }
      }
    }
  }
  Clear_Buffer();    //очистить буфер и порт
  Clear_Soft_Port();
}

//~~~ Oтправка АТ-команд через ESPport и чтение ответа
String Get_Resp(String AT_Command, int wait)
{ String tmpData;
  ESPport.println(AT_Command);
  delay(wait);
  while (ESPport.available() >0 )  
  { char c = ESPport.read();
    tmpData += c;
    tmpData.trim();       
  }
  return tmpData;
}

//~~~ Oчистка буфера Soft-порта (ESPport)
void Clear_Soft_Port() 
{ while ( ESPport.available() > 0 ) 
  { ESPport.read();
  }
}

//~~~ Oчистка буфера Setial-порта
void Clear_Serial_Port() 
{ while ( Serial.available() > 0 ) 
  { Serial.read();
  }
}

//~~~ Очистка буфера Wi-Fi
void Clear_Buffer() {
  for (int i =0;i<BUFFER_SIZE;i++ ) 
  { buffer[i]=0;
  }
}

//~~~ Ввод команд и параметров регулирования через SerialPort
void Get_Serial()
{ char inp_v; //принятый символ 
  int tkk;    //принятый параметр
  if (Serial.available())  //если есть принятый символ,
  { 
    inp_v = Serial.read(); //то читаем его и сохраняем в inp_v

    if (inp_v == 'W') //переход на работу по Wi-Fi
    { 
      Clear_Soft_Port(); //очистить буфер Soft-порта  
      Clear_Serial_Port(); //очистить буфер Serial-порта  
      S_or_W = false; //изменить режим обмена
      playNote('C', 100); //подать звук
    }

    //пуск-останов регулятора
    if (inp_v == 'N' || inp_v == 'K')
    {
      pusk = inp_v; //запомнить команду
      playNote('c', 100);  //подать звук
    }

    //установить параметры регулировки температуры/освещения
    if (inp_v == 'A' || inp_v == 'L' || inp_v == 'H' || inp_v == 'Z' || inp_v == 'X' || inp_v == 'C') 
    { tkk = inp_v; //запомним тип параметра
      int val = 0; //здесь будет сформировано значение параметра
      while (true) //читаем значение параметра до появления точки
      { if (Serial.available())
        { inp_v = Serial.read();
          if (inp_v == '.') break;  //конец ввода значения параметра
          val *= 10;
          val += (inp_v & 15);    //формирования численного значения параметра
        }
      }
      if (tkk == 'A') tk_min = val; //запоминание значения параметра
      if (tkk == 'L') tk_low = val; //в соответствующей переменной
      if (tkk == 'H') tk_high = val;//запоминание значения параметра
      if (tkk == 'Z') tk_max = val; //в соответствующей переменной
      if (tkk == 'X') light_low = val; //запоминание значения параметра
      if (tkk == 'C') t_counter = val; //запоминание значения параметра
      playNote('e', 100);  //подать звук
    }

    //получить состояние/данные от регулятора
    if (inp_v == 'I')
    {
       Serial.print("A"); Serial.print(tk_min); Serial.print(".");
       Serial.print("L"); Serial.print(tk_low); Serial.print(".");
       Serial.print("T"); Serial.print(tk_now); Serial.print(".");
       Serial.print("H"); Serial.print(tk_high); Serial.print(".");
       Serial.print("Z"); Serial.print(tk_max); Serial.print(".");
       Serial.print("X"); Serial.print(light_low); Serial.print(".");
       Serial.print("Y"); Serial.print(light_now); Serial.print(".");
       Serial.print("C"); Serial.print(t_counter); Serial.print(".");
       Serial.println();
       playNote('a', 100);  //подать звук
    }
  }
}

//~~~ Вывод звука заданной частоты (tone) и длительности (duration)
void playTone(int tone, int duration) 
{
for (long i = 0; i < duration * 1000L; i += tone * 2) 
   {
    digitalWrite(speak_pin, HIGH);
    delayMicroseconds(tone);
    digitalWrite(speak_pin, LOW);
    delayMicroseconds(tone);
   }
}

//~~~ Озвучивание ноты (note) заданной длительности (duration)
void playNote(char note, int duration) 
{
 char names[] = { 'c', 'd', 'e', 'f', 'g', 'a', 'b', 'C' };
 int tones[] = { 1915, 1700, 1519, 1432, 1275, 1136, 1014, 956 };
 //воспроизвести тон соответствующей ноты 
 for (int i = 0; i < 8; i++) 
   {
    if (names[i] == note) 
       {
        playTone(tones[i], duration);
       }
   }
}

void EEPROM_intWrite(int num, int cellnum)//Запись данных в постоянную память
{
  byte high = highByte(num); // старший байт
  byte low = lowByte(num); // младший байт
  EEPROM.write(cellnum, high);  // записываем в ячейку 1 старший байт
  EEPROM.write(cellnum+1, low); // записываем в ячейку 2 младший байт
}

int EEPROM_intRead(int cellnum)//Чтение данных из постоянной памяти
{
  byte high = EEPROM.read(cellnum); // читаем старший байт
  byte low = EEPROM.read(cellnum+1); // читаем младший байт
  int value = word(high,low);// объединяем их в значение
  return value;
}

void GsmSend(String message)
{
    EEPROM.write(14,0);
    EEPROM.write(13,0);
    SoftwareSerial gsm(9, 8); // на контроллере RX, TX
    gsm.begin(115200);               //скорость работы UART модема
    gsm.println("AT+CPIN=\"1111\""); //PIN-код SIM-карты
    while(true)                      //ждем подключение модема к GSM-сети
    { 
    gsm.println("AT+COPS?");
    if (gsm.find("0")) break;      //сеть найдена, модем подключен к ней
    delay(500);  
    }
       //~~~~~~~~~ Oтправка SMS ~~~~~~~~~
    gsm.println("AT+CMGF=1");                //текстовый режим (латиница)        
    delay(100);      
    gsm.println("AT+CSCS=\"GSM\"");          //кодировка текста - ASCII        
    delay(100);   
    char phone[12];
    for(int i =0; i< 12; i++)
    {
      phone[i] = EEPROM.read(15+i);
    }
    gsm.println("AT+CMGS=\"" + String(phone).substring(0,12) + "\""); //номер телефона
    Serial.println("AT+CMGS=\"" + String(phone).substring(0,12) + "\"");
    delay(100);    
    gsm.print(message);                      //текст SMS-сообщения
    gsm.print((char)26);                     //символ завершающий передачу    
    delay(300);
    void(* resetFunc) (void) = 0; // объявляем функцию reset
         resetFunc(); //вызываем reset
}

//~~~ Стартовая мелодия
void Play_Start()
{
  playNote('c', 50);
  playNote('d', 50);
  playNote('e', 50);
  playNote('f', 50);
  playNote('g', 50);
  playNote('a', 50);
  playNote('b', 50);
  playNote('C', 50);
}
