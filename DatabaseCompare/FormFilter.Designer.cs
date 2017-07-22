namespace DatabaseCompare
{
    partial class FormFilter
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.CBxSchema = new System.Windows.Forms.ComboBox();
            this.CBxTable = new System.Windows.Forms.ComboBox();
            this.CBxField = new System.Windows.Forms.ComboBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CBxSchema
            // 
            this.CBxSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBxSchema.FormattingEnabled = true;
            this.CBxSchema.Location = new System.Drawing.Point(15, 25);
            this.CBxSchema.Name = "CBxSchema";
            this.CBxSchema.Size = new System.Drawing.Size(121, 21);
            this.CBxSchema.TabIndex = 0;
            this.CBxSchema.SelectedIndexChanged += new System.EventHandler(this.CBxSchema_SelectedIndexChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 13);
            label1.TabIndex = 1;
            label1.Text = "Schema:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(139, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 13);
            label2.TabIndex = 2;
            label2.Text = "Table:";
            // 
            // CBxTable
            // 
            this.CBxTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBxTable.Enabled = false;
            this.CBxTable.FormattingEnabled = true;
            this.CBxTable.Location = new System.Drawing.Point(142, 25);
            this.CBxTable.Name = "CBxTable";
            this.CBxTable.Size = new System.Drawing.Size(121, 21);
            this.CBxTable.TabIndex = 3;
            this.CBxTable.SelectedIndexChanged += new System.EventHandler(this.CBxTable_SelectedIndexChanged);
            // 
            // CBxField
            // 
            this.CBxField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBxField.Enabled = false;
            this.CBxField.FormattingEnabled = true;
            this.CBxField.Location = new System.Drawing.Point(269, 25);
            this.CBxField.Name = "CBxField";
            this.CBxField.Size = new System.Drawing.Size(121, 21);
            this.CBxField.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(266, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(32, 13);
            label3.TabIndex = 4;
            label3.Text = "Field:";
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(215, 64);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(94, 23);
            this.BtnOK.TabIndex = 6;
            this.BtnOK.Text = "Apply Filter";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(315, 64);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // FormFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 98);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.CBxField);
            this.Controls.Add(label3);
            this.Controls.Add(this.CBxTable);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.CBxSchema);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormFilter";
            this.Text = "Setup Filter";
            this.Load += new System.EventHandler(this.FormFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBxSchema;
        private System.Windows.Forms.ComboBox CBxTable;
        private System.Windows.Forms.ComboBox CBxField;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
    }
}