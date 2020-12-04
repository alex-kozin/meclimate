namespace MeClimate
{
    partial class frmWelcome
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelcome));
            this.msmWelcome = new MetroFramework.Components.MetroStyleManager(this.components);
            this.lstNetworks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBegin = new MetroFramework.Controls.MetroButton();
            this.btnConnect = new MetroFramework.Controls.MetroButton();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.tltpInfo = new MetroFramework.Drawing.Html.HtmlToolTip();
            this.tileGuide = new MetroFramework.Controls.MetroTile();
            ((System.ComponentModel.ISupportInitialize)(this.msmWelcome)).BeginInit();
            this.SuspendLayout();
            // 
            // msmWelcome
            // 
            this.msmWelcome.Owner = this;
            // 
            // lstNetworks
            // 
            this.lstNetworks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstNetworks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstNetworks.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lstNetworks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lstNetworks.FullRowSelect = true;
            this.lstNetworks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstNetworks.HideSelection = false;
            this.lstNetworks.Location = new System.Drawing.Point(116, 161);
            this.lstNetworks.MultiSelect = false;
            this.lstNetworks.Name = "lstNetworks";
            this.lstNetworks.Size = new System.Drawing.Size(495, 151);
            this.lstNetworks.TabIndex = 2;
            this.lstNetworks.UseCompatibleStateImageBehavior = false;
            this.lstNetworks.View = System.Windows.Forms.View.Details;
            this.lstNetworks.SelectedIndexChanged += new System.EventHandler(this.lstNetworks_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Wi-Fi SSID";
            this.columnHeader1.Width = 263;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Quality";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Security";
            this.columnHeader3.Width = 130;
            // 
            // btnBegin
            // 
            this.btnBegin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBegin.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnBegin.Location = new System.Drawing.Point(282, 60);
            this.btnBegin.Margin = new System.Windows.Forms.Padding(6);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(138, 44);
            this.btnBegin.TabIndex = 0;
            this.btnBegin.Text = "Begin";
            this.tltpInfo.SetToolTip(this.btnBegin, "Press to search for Wi-Fi networks available");
            this.btnBegin.UseSelectable = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnConnect.Location = new System.Drawing.Point(282, 316);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(138, 44);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.tltpInfo.SetToolTip(this.btnConnect, "Press to connect to chosen Wi-Fi network");
            this.btnConnect.UseSelectable = true;
            this.btnConnect.Visible = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(213, 112);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(283, 39);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Choose your MeClimate device Wi-Fi network.\r\nThen click \'Connect\' button to start" +
                ".\r\n";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Visible = false;
            // 
            // tltpInfo
            // 
            this.tltpInfo.IsBalloon = true;
            this.tltpInfo.OwnerDraw = true;
            // 
            // tileGuide
            // 
            this.tileGuide.ActiveControl = null;
            this.tileGuide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileGuide.Location = new System.Drawing.Point(9, 26);
            this.tileGuide.Name = "tileGuide";
            this.tileGuide.Size = new System.Drawing.Size(25, 25);
            this.tileGuide.TabIndex = 5;
            this.tileGuide.TileImage = ((System.Drawing.Image)(resources.GetObject("tileGuide.TileImage")));
            this.tltpInfo.SetToolTip(this.tileGuide, "User\'s guide");
            this.tileGuide.UseSelectable = true;
            this.tileGuide.UseTileImage = true;
            this.tileGuide.Click += new System.EventHandler(this.tileGuide_Click);
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 366);
            this.Controls.Add(this.tileGuide);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lstNetworks);
            this.Controls.Add(this.btnBegin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "frmWelcome";
            this.Padding = new System.Windows.Forms.Padding(37, 111, 37, 37);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.Text = "Welcome";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            ((System.ComponentModel.ISupportInitialize)(this.msmWelcome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager msmWelcome;
        private MetroFramework.Controls.MetroButton btnBegin;
        private MetroFramework.Controls.MetroButton btnConnect;
        private System.Windows.Forms.ListView lstNetworks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MetroFramework.Controls.MetroLabel lblInfo;
        private MetroFramework.Drawing.Html.HtmlToolTip tltpInfo;
        private MetroFramework.Controls.MetroTile tileGuide;
    }
}

