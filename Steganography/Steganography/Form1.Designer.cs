namespace Steganography
{
    partial class MainMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Encode = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Decode = new System.Windows.Forms.TabPage();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.Encode.SuspendLayout();
            this.Decode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Encode);
            this.tabControl1.Controls.Add(this.Decode);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(243, 184);
            this.tabControl1.TabIndex = 0;
            // 
            // Encode
            // 
            this.Encode.Controls.Add(this.checkBox3);
            this.Encode.Controls.Add(this.button3);
            this.Encode.Controls.Add(this.button2);
            this.Encode.Controls.Add(this.button1);
            this.Encode.Controls.Add(this.checkBox2);
            this.Encode.Controls.Add(this.checkBox1);
            this.Encode.Location = new System.Drawing.Point(4, 22);
            this.Encode.Name = "Encode";
            this.Encode.Padding = new System.Windows.Forms.Padding(3);
            this.Encode.Size = new System.Drawing.Size(235, 158);
            this.Encode.TabIndex = 0;
            this.Encode.Text = "Encode";
            this.Encode.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(87, 123);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(61, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Готово";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(224, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Выполнить сокрытие";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Выбрать текст";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Выбрать изображение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(5, 44);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(5, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Decode
            // 
            this.Decode.Controls.Add(this.checkBox5);
            this.Decode.Controls.Add(this.button5);
            this.Decode.Controls.Add(this.button4);
            this.Decode.Controls.Add(this.checkBox4);
            this.Decode.Location = new System.Drawing.Point(4, 22);
            this.Decode.Name = "Decode";
            this.Decode.Padding = new System.Windows.Forms.Padding(3);
            this.Decode.Size = new System.Drawing.Size(235, 158);
            this.Decode.TabIndex = 1;
            this.Decode.Text = "Decode";
            this.Decode.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(87, 123);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(61, 17);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "Готово";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(5, 77);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(224, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Извлечь сообщение";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(26, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(203, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Выбрать изображение";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(5, 11);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 208);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(283, 247);
            this.MinimumSize = new System.Drawing.Size(283, 247);
            this.Name = "MainMenu";
            this.Text = "Steganography";
            this.tabControl1.ResumeLayout(false);
            this.Encode.ResumeLayout(false);
            this.Encode.PerformLayout();
            this.Decode.ResumeLayout(false);
            this.Decode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Encode;
        private System.Windows.Forms.TabPage Decode;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

