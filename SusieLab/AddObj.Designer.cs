namespace HaltmannLab
{
    partial class AddObj
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
            this.objectDropDown = new System.Windows.Forms.ComboBox();
            this.save = new System.Windows.Forms.Button();
            this.enemyDropDown = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // objectDropDown
            // 
            this.objectDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectDropDown.FormattingEnabled = true;
            this.objectDropDown.Location = new System.Drawing.Point(12, 12);
            this.objectDropDown.Name = "objectDropDown";
            this.objectDropDown.Size = new System.Drawing.Size(256, 21);
            this.objectDropDown.TabIndex = 0;
            this.objectDropDown.SelectedIndexChanged += new System.EventHandler(this.objectDropDown_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(97, 68);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // enemyDropDown
            // 
            this.enemyDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enemyDropDown.FormattingEnabled = true;
            this.enemyDropDown.Location = new System.Drawing.Point(12, 39);
            this.enemyDropDown.Name = "enemyDropDown";
            this.enemyDropDown.Size = new System.Drawing.Size(256, 21);
            this.enemyDropDown.TabIndex = 2;
            this.enemyDropDown.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "isLong";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AddObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 99);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.enemyDropDown);
            this.Controls.Add(this.save);
            this.Controls.Add(this.objectDropDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddObj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Object";
            this.Load += new System.EventHandler(this.AddObj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox objectDropDown;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.ComboBox enemyDropDown;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}