using TimeStampDecoder;

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
            this.textBoxMunge = new System.Windows.Forms.TextBox();
            this.numberBox = new TimeStampDecoder.NumberBox();
            this.maskedDateTimePicker = new TimeStampDecoder.MaskedDateTimePicker();
            this.SuspendLayout();
            // 
            // textBoxMunge
            // 
            this.textBoxMunge.AllowDrop = true;
            this.textBoxMunge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMunge.Location = new System.Drawing.Point(12, 44);
            this.textBoxMunge.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMunge.Multiline = true;
            this.textBoxMunge.Name = "textBoxMunge";
            this.textBoxMunge.ReadOnly = true;
            this.textBoxMunge.Size = new System.Drawing.Size(547, 77);
            this.textBoxMunge.TabIndex = 3;
            this.textBoxMunge.DragDrop += new System.Windows.Forms.DragEventHandler(this.File_DragDrop);
            // 
            // numberBox
            // 
            this.numberBox.HEX = true;
            this.numberBox.Location = new System.Drawing.Point(12, 12);
            this.numberBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numberBox.Name = "numberBox";
            this.numberBox.Size = new System.Drawing.Size(272, 25);
            this.numberBox.TabIndex = 0;
            this.numberBox.Text = "0000000000000000";
            this.numberBox.Value = ((ulong)(0ul));
            this.numberBox.ValueChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // maskedDateTimePicker
            // 
            this.maskedDateTimePicker.Location = new System.Drawing.Point(289, 14);
            this.maskedDateTimePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maskedDateTimePicker.Name = "maskedDateTimePicker";
            this.maskedDateTimePicker.Size = new System.Drawing.Size(272, 25);
            this.maskedDateTimePicker.TabIndex = 2;
            this.maskedDateTimePicker.TextChanged += new System.EventHandler(this.Input_ValueChanged);
            // 
            // TimeStampMungeFinderForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 137);
            this.Controls.Add(this.textBoxMunge);
            this.Controls.Add(this.numberBox);
            this.Controls.Add(this.maskedDateTimePicker);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(594, 174);
            this.Name = "TimeStampMungeFinderForm";
            this.Text = "GAME_MASTER TimeStamp Munge Finder";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.File_DragDrop);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumberBox numberBox;
        private MaskedDateTimePicker maskedDateTimePicker;
        private System.Windows.Forms.TextBox textBoxMunge;
    }
}