namespace Z13_F2
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Put_BUTTON = new System.Windows.Forms.Button();
            this.Delete_BUTTON = new System.Windows.Forms.Button();
            this.Replace_BUTTON = new System.Windows.Forms.Button();
            this.Clear_BUTTON = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 212);
            this.listBox1.TabIndex = 0;
            // 
            // Put_BUTTON
            // 
            this.Put_BUTTON.Location = new System.Drawing.Point(190, 12);
            this.Put_BUTTON.Name = "Put_BUTTON";
            this.Put_BUTTON.Size = new System.Drawing.Size(128, 36);
            this.Put_BUTTON.TabIndex = 1;
            this.Put_BUTTON.Text = "Поставить";
            this.Put_BUTTON.UseVisualStyleBackColor = true;
            this.Put_BUTTON.Click += new System.EventHandler(this.Put_BUTTON_Click);
            // 
            // Delete_BUTTON
            // 
            this.Delete_BUTTON.Location = new System.Drawing.Point(190, 54);
            this.Delete_BUTTON.Name = "Delete_BUTTON";
            this.Delete_BUTTON.Size = new System.Drawing.Size(128, 36);
            this.Delete_BUTTON.TabIndex = 2;
            this.Delete_BUTTON.Text = "Убрать";
            this.Delete_BUTTON.UseVisualStyleBackColor = true;
            this.Delete_BUTTON.Click += new System.EventHandler(this.Delete_BUTTON_Click);
            // 
            // Replace_BUTTON
            // 
            this.Replace_BUTTON.Location = new System.Drawing.Point(190, 96);
            this.Replace_BUTTON.Name = "Replace_BUTTON";
            this.Replace_BUTTON.Size = new System.Drawing.Size(128, 36);
            this.Replace_BUTTON.TabIndex = 3;
            this.Replace_BUTTON.Text = "Заменить";
            this.Replace_BUTTON.UseVisualStyleBackColor = true;
            this.Replace_BUTTON.Click += new System.EventHandler(this.Replace_BUTTON_Click);
            // 
            // Clear_BUTTON
            // 
            this.Clear_BUTTON.Location = new System.Drawing.Point(12, 230);
            this.Clear_BUTTON.Name = "Clear_BUTTON";
            this.Clear_BUTTON.Size = new System.Drawing.Size(172, 43);
            this.Clear_BUTTON.TabIndex = 4;
            this.Clear_BUTTON.Text = "Чистый";
            this.Clear_BUTTON.UseVisualStyleBackColor = true;
            this.Clear_BUTTON.Click += new System.EventHandler(this.Clear_BUTTON_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 294);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Clear_BUTTON);
            this.Controls.Add(this.Replace_BUTTON);
            this.Controls.Add(this.Delete_BUTTON);
            this.Controls.Add(this.Put_BUTTON);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Put_BUTTON;
        private System.Windows.Forms.Button Delete_BUTTON;
        private System.Windows.Forms.Button Replace_BUTTON;
        private System.Windows.Forms.Button Clear_BUTTON;
        private System.Windows.Forms.Label label1;
    }
}

