namespace DentistDatabase
{
    partial class 患者建档
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(患者建档));
            this.label2 = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBirth = new System.Windows.Forms.DateTimePicker();
            this.textSex = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textVocation = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textAddress = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBirthPlace = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "姓名";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(191, 30);
            this.textName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(111, 25);
            this.textName.TabIndex = 3;
            this.textName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "性别";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "出生年月";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 115);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 34);
            this.button1.TabIndex = 24;
            this.button1.Text = "建档并返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(414, 232);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(232, 34);
            this.button2.TabIndex = 25;
            this.button2.Text = "取消并返回";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBirth
            // 
            this.textBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.textBirth.Location = new System.Drawing.Point(192, 168);
            this.textBirth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBirth.Name = "textBirth";
            this.textBirth.Size = new System.Drawing.Size(160, 25);
            this.textBirth.TabIndex = 26;
            this.textBirth.Value = new System.DateTime(2023, 1, 4, 0, 0, 0, 0);
            // 
            // textSex
            // 
            this.textSex.FormattingEnabled = true;
            this.textSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.textSex.Location = new System.Drawing.Point(192, 116);
            this.textSex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textSex.Name = "textSex";
            this.textSex.Size = new System.Drawing.Size(135, 23);
            this.textSex.TabIndex = 27;
            this.textSex.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "职业";
            // 
            // textVocation
            // 
            this.textVocation.Location = new System.Drawing.Point(192, 216);
            this.textVocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textVocation.Name = "textVocation";
            this.textVocation.Size = new System.Drawing.Size(106, 25);
            this.textVocation.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(76, 262);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 15);
            this.label13.TabIndex = 30;
            this.label13.Text = "住址";
            // 
            // textAddress
            // 
            this.textAddress.Location = new System.Drawing.Point(196, 262);
            this.textAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textAddress.Name = "textAddress";
            this.textAddress.Size = new System.Drawing.Size(106, 25);
            this.textAddress.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(76, 313);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 15);
            this.label14.TabIndex = 32;
            this.label14.Text = "出生地";
            // 
            // textBirthPlace
            // 
            this.textBirthPlace.Location = new System.Drawing.Point(197, 303);
            this.textBirthPlace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBirthPlace.Name = "textBirthPlace";
            this.textBirthPlace.Size = new System.Drawing.Size(106, 25);
            this.textBirthPlace.TabIndex = 33;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(414, 175);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(232, 34);
            this.button3.TabIndex = 41;
            this.button3.Text = "建档并编辑档案";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "身份证号码";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(190, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 25);
            this.textBox1.TabIndex = 43;
            // 
            // 患者建档
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(705, 416);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBirthPlace);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textAddress);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textVocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textSex);
            this.Controls.Add(this.textBirth);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "患者建档";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "患者建档";
            this.Load += new System.EventHandler(this.BuildArchive_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker textBirth;
        private System.Windows.Forms.ComboBox textSex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textVocation;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBirthPlace;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
    }
}