namespace TimeStampDecoder
{
    partial class TimeStampConverterForm
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
            this.numberBox = new TimeStampDecoder.NumberBox();
            this.maskedDateTimePicker = new TimeStampDecoder.MaskedDateTimePicker();
            this.checkBoxGameMaster = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // numberBox
            // 
            this.numberBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBox.Location = new System.Drawing.Point(12, 12);
            this.numberBox.Name = "numberBox";
            this.numberBox.Size = new System.Drawing.Size(337, 25);
            this.numberBox.TabIndex = 0;
            this.numberBox.Value = ((ulong)(0ul));
            this.numberBox.ValueChanged += new System.EventHandler(this.numberBox_ValueChanged);
            // 
            // maskedDateTimePicker
            // 
            this.maskedDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedDateTimePicker.Location = new System.Drawing.Point(12, 40);
            this.maskedDateTimePicker.Name = "maskedDateTimePicker";
            this.maskedDateTimePicker.Size = new System.Drawing.Size(337, 25);
            this.maskedDateTimePicker.TabIndex = 2;
            this.maskedDateTimePicker.TextChanged += new System.EventHandler(this.maskedDateTimePicker_ValueChanged);
            // 
            // checkBoxGameMaster
            // 
            this.checkBoxGameMaster.AutoSize = true;
            this.checkBoxGameMaster.Location = new System.Drawing.Point(12, 78);
            this.checkBoxGameMaster.Name = "checkBoxGameMaster";
            this.checkBoxGameMaster.Size = new System.Drawing.Size(135, 21);
            this.checkBoxGameMaster.TabIndex = 3;
            this.checkBoxGameMaster.Text = "GAME_MASTER";
            this.checkBoxGameMaster.UseVisualStyleBackColor = true;
            this.checkBoxGameMaster.CheckedChanged += new System.EventHandler(this.checkBoxGameMaster_CheckedChanged);
            // 
            // TimeStampConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 111);
            this.Controls.Add(this.checkBoxGameMaster);
            this.Controls.Add(this.numberBox);
            this.Controls.Add(this.maskedDateTimePicker);
            this.Name = "TimeStampConverterForm";
            this.Text = "TimeStamp Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumberBox numberBox;
        private MaskedDateTimePicker maskedDateTimePicker;
        private System.Windows.Forms.CheckBox checkBoxGameMaster;
    }
}

