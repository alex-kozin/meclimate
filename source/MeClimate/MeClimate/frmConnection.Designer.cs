namespace MeClimate
{
    partial class frmConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnection));
            this.prgsConnection = new MetroFramework.Controls.MetroProgressSpinner();
            this.msmConnection = new MetroFramework.Components.MetroStyleManager(this.components);
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.msmConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // prgsConnection
            // 
            this.prgsConnection.Location = new System.Drawing.Point(146, 68);
            this.prgsConnection.Maximum = 100;
            this.prgsConnection.Name = "prgsConnection";
            this.prgsConnection.Size = new System.Drawing.Size(23, 23);
            this.prgsConnection.TabIndex = 0;
            this.prgsConnection.UseSelectable = true;
            // 
            // msmConnection
            // 
            this.msmConnection.Owner = null;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(11, 100);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(317, 23);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Setting wireless connection...";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // frmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 126);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.prgsConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "frmConnection";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.Text = "Connection To MeClimate...";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConnection_FormClosed);
            this.Load += new System.EventHandler(this.frmConnection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.msmConnection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroProgressSpinner prgsConnection;
        private MetroFramework.Components.MetroStyleManager msmConnection;
        private MetroFramework.Controls.MetroLabel lblInfo;
    }
}