﻿namespace HaltmannLab
{
    partial class YAMLEditor
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
            this.yamlDataList = new System.Windows.Forms.ListBox();
            this.save = new System.Windows.Forms.Button();
            this.value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delAttribute = new System.Windows.Forms.Button();
            this.valueSelect = new System.Windows.Forms.ComboBox();
            this.addAttribute = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // yamlDataList
            // 
            this.yamlDataList.FormattingEnabled = true;
            this.yamlDataList.Location = new System.Drawing.Point(13, 13);
            this.yamlDataList.Name = "yamlDataList";
            this.yamlDataList.Size = new System.Drawing.Size(197, 277);
            this.yamlDataList.TabIndex = 0;
            this.yamlDataList.SelectedIndexChanged += new System.EventHandler(this.yamlDataList_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(178, 326);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(216, 28);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(201, 20);
            this.value.TabIndex = 2;
            this.value.TextChanged += new System.EventHandler(this.value_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value";
            // 
            // delAttribute
            // 
            this.delAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delAttribute.Location = new System.Drawing.Point(42, 296);
            this.delAttribute.Name = "delAttribute";
            this.delAttribute.Size = new System.Drawing.Size(23, 23);
            this.delAttribute.TabIndex = 5;
            this.delAttribute.Text = "-";
            this.delAttribute.UseVisualStyleBackColor = true;
            this.delAttribute.Click += new System.EventHandler(this.delAttribute_Click);
            // 
            // valueSelect
            // 
            this.valueSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueSelect.Enabled = false;
            this.valueSelect.FormattingEnabled = true;
            this.valueSelect.Location = new System.Drawing.Point(216, 55);
            this.valueSelect.Name = "valueSelect";
            this.valueSelect.Size = new System.Drawing.Size(201, 21);
            this.valueSelect.Sorted = true;
            this.valueSelect.TabIndex = 6;
            this.valueSelect.SelectedIndexChanged += new System.EventHandler(this.valueSelect_SelectedIndexChanged);
            // 
            // addAttribute
            // 
            this.addAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAttribute.Location = new System.Drawing.Point(13, 296);
            this.addAttribute.Name = "addAttribute";
            this.addAttribute.Size = new System.Drawing.Size(23, 23);
            this.addAttribute.TabIndex = 7;
            this.addAttribute.Text = "+";
            this.addAttribute.UseVisualStyleBackColor = true;
            this.addAttribute.Click += new System.EventHandler(this.addAttribute_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.richTextBox1.Location = new System.Drawing.Point(217, 83);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(200, 207);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // YAMLEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(430, 356);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.addAttribute);
            this.Controls.Add(this.valueSelect);
            this.Controls.Add(this.delAttribute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.value);
            this.Controls.Add(this.save);
            this.Controls.Add(this.yamlDataList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YAMLEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YAMLEditor";
            this.Load += new System.EventHandler(this.YAMLEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox yamlDataList;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delAttribute;
        private System.Windows.Forms.ComboBox valueSelect;
        private System.Windows.Forms.Button addAttribute;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}