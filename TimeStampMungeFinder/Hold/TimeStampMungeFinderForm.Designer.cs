namespace TimeStampMungeFinder
{
    partial class TimeStampMungeFinderForm
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
            this.labelTimeStamp = new System.Windows.Forms.Label();
            this.checkBoxHEX = new System.Windows.Forms.CheckBox();
            this.labelNormal = new System.Windows.Forms.Label();
            this.numberBoxTimeStamp = new TimeStampMungeFinder.NumberBox();
            this.maskedDateTimePickerNormal = new TimeStampMungeFinder.MaskedDateTimePicker();
            this.labelDifference = new System.Windows.Forms.Label();
            this.textBoxTimePickerDifference = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTimeStamp
            // 
            this.labelTimeStamp.AutoSize = true;
            this.labelTimeStamp.Location = new System.Drawing.Point(11, 14);
            this.labelTimeStamp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTimeStamp.Name = "labelTimeStamp";
            this.labelTimeStamp.Size = new System.Drawing.Size(63, 13);
            this.labelTimeStamp.TabIndex = 1;
            this.labelTimeStamp.Text = "TimeStamp:";
            this.labelTimeStamp.Click += new System.EventHandler(this.labelTimeStamp_Click);
            // 
            // checkBoxHEX
            // 
            this.checkBoxHEX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHEX.AutoSize = true;
            this.checkBoxHEX.Location = new System.Drawing.Point(239, 13);
            this.checkBoxHEX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxHEX.Name = "checkBoxHEX";
            this.checkBoxHEX.Size = new System.Drawing.Size(48, 17);
            this.checkBoxHEX.TabIndex = 2;
            this.checkBoxHEX.Text = "HEX";
            this.checkBoxHEX.UseVisualStyleBackColor = true;
            this.checkBoxHEX.CheckedChanged += new System.EventHandler(this.checkBoxHEX_CheckedChanged);
            // 
            // labelNormal
            // 
            this.labelNormal.AutoSize = true;
            this.labelNormal.Location = new System.Drawing.Point(11, 38);
            this.labelNormal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(52, 13);
            this.labelNormal.TabIndex = 1;
            this.labelNormal.Text = "File Date:";
            // 
            // numberBoxTimeStamp
            // 
            this.numberBoxTimeStamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxTimeStamp.HEX = false;
            this.numberBoxTimeStamp.Location = new System.Drawing.Point(87, 11);
            this.numberBoxTimeStamp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numberBoxTimeStamp.Name = "numberBoxTimeStamp";
            this.numberBoxTimeStamp.Size = new System.Drawing.Size(148, 20);
            this.numberBoxTimeStamp.TabIndex = 1;
            this.numberBoxTimeStamp.Text = "0";
            this.numberBoxTimeStamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numberBoxTimeStamp.Value = ((ulong)(0ul));
            this.numberBoxTimeStamp.ValueChanged += new System.EventHandler(this.numberBoxTimeStamp_ValueChanged);
            this.numberBoxTimeStamp.TextChanged += new System.EventHandler(this.numberBoxTimeStamp_TextChanged);
            // 
            // maskedDateTimePickerNormal
            // 
            this.maskedDateTimePickerNormal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedDateTimePickerNormal.Location = new System.Drawing.Point(87, 35);
            this.maskedDateTimePickerNormal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.maskedDateTimePickerNormal.Mask = "0000-00-00 00:00:00.000";
            this.maskedDateTimePickerNormal.Name = "maskedDateTimePickerNormal";
            this.maskedDateTimePickerNormal.Size = new System.Drawing.Size(148, 20);
            this.maskedDateTimePickerNormal.TabIndex = 1;
            this.maskedDateTimePickerNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedDateTimePickerNormal.UTC = false;
            this.maskedDateTimePickerNormal.ValueChanged += new System.EventHandler(this.maskedDateTimePickerNormal_ValueChanged);
            this.maskedDateTimePickerNormal.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedDateTimePickerNormal_MaskInputRejected);
            // 
            // labelDifference
            // 
            this.labelDifference.AutoSize = true;
            this.labelDifference.Location = new System.Drawing.Point(11, 60);
            this.labelDifference.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDifference.Name = "labelDifference";
            this.labelDifference.Size = new System.Drawing.Size(56, 13);
            this.labelDifference.TabIndex = 1;
            this.labelDifference.Text = "Difference";
            // 
            // textBoxTimePickerDifference
            // 
            this.textBoxTimePickerDifference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTimePickerDifference.Location = new System.Drawing.Point(87, 60);
            this.textBoxTimePickerDifference.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxTimePickerDifference.Name = "textBoxTimePickerDifference";
            this.textBoxTimePickerDifference.ReadOnly = true;
            this.textBoxTimePickerDifference.Size = new System.Drawing.Size(148, 20);
            this.textBoxTimePickerDifference.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(239, 60);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 1;
            // 
            // TimeStampMungeFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 110);
            this.Controls.Add(this.labelDifference);
            this.Controls.Add(this.labelNormal);
            this.Controls.Add(this.checkBoxHEX);
            this.Controls.Add(this.labelTimeStamp);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBoxTimePickerDifference);
            this.Controls.Add(this.maskedDateTimePickerNormal);
            this.Controls.Add(this.numberBoxTimeStamp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TimeStampMungeFinderForm";
            this.Text = "TimeStamp Converter";
            this.Load += new System.EventHandler(this.TimeStampMungeFinderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTimeStamp;
        private System.Windows.Forms.CheckBox checkBoxHEX;
        private System.Windows.Forms.Label labelNormal;
        private TimeStampMungeFinder.NumberBox numberBoxTimeStamp;
        private TimeStampMungeFinder.MaskedDateTimePicker maskedDateTimePickerNormal;
        private System.Windows.Forms.Label labelDifference;
        private System.Windows.Forms.TextBox textBoxTimePickerDifference;
        private System.Windows.Forms.TextBox textBox1;
    }
}