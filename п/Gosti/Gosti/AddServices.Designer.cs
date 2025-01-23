namespace Gosti
{
    partial class AddServiceForm
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
            this.comboBoxRoomID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxServiceType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddService = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBookingID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxRoomID
            // 
            this.comboBoxRoomID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoomID.FormattingEnabled = true;
            this.comboBoxRoomID.Location = new System.Drawing.Point(189, 130);
            this.comboBoxRoomID.Name = "comboBoxRoomID";
            this.comboBoxRoomID.Size = new System.Drawing.Size(142, 21);
            this.comboBoxRoomID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(57, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер комнаты";
            // 
            // comboBoxServiceType
            // 
            this.comboBoxServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServiceType.FormattingEnabled = true;
            this.comboBoxServiceType.Location = new System.Drawing.Point(189, 181);
            this.comboBoxServiceType.Name = "comboBoxServiceType";
            this.comboBoxServiceType.Size = new System.Drawing.Size(142, 21);
            this.comboBoxServiceType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(57, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Тип услуги";
            // 
            // btnAddService
            // 
            this.btnAddService.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddService.Location = new System.Drawing.Point(146, 232);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(96, 29);
            this.btnAddService.TabIndex = 4;
            this.btnAddService.Text = "Добавить";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(77, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID ";
            // 
            // comboBoxBookingID
            // 
            this.comboBoxBookingID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBookingID.FormattingEnabled = true;
            this.comboBoxBookingID.Location = new System.Drawing.Point(189, 86);
            this.comboBoxBookingID.Name = "comboBoxBookingID";
            this.comboBoxBookingID.Size = new System.Drawing.Size(142, 21);
            this.comboBoxBookingID.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(110, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Добавить услугу";
            // 
            // AddServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 320);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxBookingID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddService);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxServiceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxRoomID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddServiceForm";
            this.Text = "Добавление";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxRoomID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxServiceType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBookingID;
        private System.Windows.Forms.Label label4;
    }
}