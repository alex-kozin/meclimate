namespace MeClimate
{
    partial class frmAdvancedSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtPass = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.nmcTempMax = new System.Windows.Forms.NumericUpDown();
            this.nmcTempMin = new System.Windows.Forms.NumericUpDown();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnApply = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.msmAdvSettings = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.lblC1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nmcTempMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcTempMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.msmAdvSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPass.CustomButton.Image = null;
            this.txtPass.CustomButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPass.CustomButton.Location = new System.Drawing.Point(133, 1);
            this.txtPass.CustomButton.Name = "";
            this.txtPass.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPass.CustomButton.TabIndex = 1;
            this.txtPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPass.CustomButton.UseSelectable = true;
            this.txtPass.CustomButton.Visible = false;
            this.txtPass.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPass.Lines = new string[0];
            this.txtPass.Location = new System.Drawing.Point(44, 37);
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '\0';
            this.txtPass.PromptText = "380...";
            this.txtPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPass.SelectedText = "";
            this.txtPass.SelectionLength = 0;
            this.txtPass.SelectionStart = 0;
            this.txtPass.ShortcutsEnabled = true;
            this.txtPass.ShowClearButton = true;
            this.txtPass.Size = new System.Drawing.Size(155, 23);
            this.txtPass.TabIndex = 3;
            this.txtPass.UseCustomBackColor = true;
            this.txtPass.UseSelectable = true;
            this.txtPass.WaterMark = "380...";
            this.txtPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(8, 99);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(154, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Max. Temperature:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(8, 123);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(151, 25);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Min. Temperature:";
            // 
            // nmcTempMax
            // 
            this.nmcTempMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nmcTempMax.DecimalPlaces = 1;
            this.nmcTempMax.Location = new System.Drawing.Point(163, 104);
            this.nmcTempMax.Name = "nmcTempMax";
            this.nmcTempMax.Size = new System.Drawing.Size(44, 20);
            this.nmcTempMax.TabIndex = 7;
            this.nmcTempMax.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // nmcTempMin
            // 
            this.nmcTempMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nmcTempMin.DecimalPlaces = 1;
            this.nmcTempMin.Location = new System.Drawing.Point(163, 128);
            this.nmcTempMin.Name = "nmcTempMin";
            this.nmcTempMin.Size = new System.Drawing.Size(44, 20);
            this.nmcTempMin.TabIndex = 7;
            // 
            // metroLabel3
            // 
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.Location = new System.Drawing.Point(26, 158);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(192, 61);
            this.metroLabel3.TabIndex = 8;
            this.metroLabel3.Text = "Maximal and minimal temperature\r\nvalues depend on sensor`s\r\nspecifications. Chang" +
                "ing these\r\noptions is NOT RECOMMENDED.";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel3.WrapToLine = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnCancel.Location = new System.Drawing.Point(128, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnApply.Location = new System.Drawing.Point(48, 227);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(74, 33);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Apply";
            this.btnApply.UseSelectable = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.Location = new System.Drawing.Point(40, 6);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(161, 25);
            this.metroLabel4.TabIndex = 11;
            this.metroLabel4.Text = "Emergency number";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 60);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape4,
            this.lineShape2,
            this.lineShape1,
            this.lineShape3});
            this.shapeContainer1.Size = new System.Drawing.Size(238, 184);
            this.shapeContainer1.TabIndex = 12;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineShape4.BorderWidth = 2;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.SelectionColor = System.Drawing.Color.DarkGray;
            this.lineShape4.X1 = -137;
            this.lineShape4.X2 = 355;
            this.lineShape4.Y1 = 93;
            this.lineShape4.Y2 = 92;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineShape2.BorderWidth = 2;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.SelectionColor = System.Drawing.Color.DarkGray;
            this.lineShape2.X1 = -137;
            this.lineShape2.X2 = 355;
            this.lineShape2.Y1 = 36;
            this.lineShape2.Y2 = 35;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.SelectionColor = System.Drawing.Color.DarkGray;
            this.lineShape1.X1 = -144;
            this.lineShape1.X2 = 348;
            this.lineShape1.Y1 = 161;
            this.lineShape1.Y2 = 160;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineShape3.BorderWidth = 2;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.SelectionColor = System.Drawing.Color.DarkGray;
            this.lineShape3.X1 = -142;
            this.lineShape3.X2 = 350;
            this.lineShape3.Y1 = 199;
            this.lineShape3.Y2 = 198;
            // 
            // msmAdvSettings
            // 
            this.msmAdvSettings.Owner = this;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel5.Location = new System.Drawing.Point(49, 63);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(142, 30);
            this.metroLabel5.TabIndex = 13;
            this.metroLabel5.Text = "Telephone number to send\r\nSMS in case of emergency";
            this.metroLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel5.WrapToLine = true;
            // 
            // lblC1
            // 
            this.lblC1.AutoSize = true;
            this.lblC1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblC1.Location = new System.Drawing.Point(208, 99);
            this.lblC1.Name = "lblC1";
            this.lblC1.Size = new System.Drawing.Size(30, 25);
            this.lblC1.TabIndex = 14;
            this.lblC1.Text = "ºC";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel6.Location = new System.Drawing.Point(207, 124);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(30, 25);
            this.metroLabel6.TabIndex = 14;
            this.metroLabel6.Text = "ºC";
            // 
            // frmAdvancedSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(238, 264);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.lblC1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.nmcTempMax);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.nmcTempMin);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "frmAdvancedSettings";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmAdvancedSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmcTempMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcTempMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.msmAdvSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox txtPass;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.NumericUpDown nmcTempMax;
        private System.Windows.Forms.NumericUpDown nmcTempMin;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton btnCancel;
        private MetroFramework.Controls.MetroButton btnApply;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private MetroFramework.Components.MetroStyleManager msmAdvSettings;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel lblC1;
    }
}