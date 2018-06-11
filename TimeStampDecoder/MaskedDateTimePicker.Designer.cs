using System;

namespace TimeStampDecoder
{
    partial class MaskedDateTimePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this._checkBoxUTC = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _maskedTextBox
            // 
            this._maskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._maskedTextBox.Location = new System.Drawing.Point(0, 0);
            this._maskedTextBox.Mask = "0000-00-00 00:00:00.000";
            this._maskedTextBox.Name = "_maskedTextBox";
            this._maskedTextBox.Size = new System.Drawing.Size(100, 22);
            this._maskedTextBox.TabIndex = 0;
            this._maskedTextBox.TextChanged += new System.EventHandler(this.maskedTextBox_TextChanged);
            // 
            // _checkBoxUTC
            // 
            this._checkBoxUTC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkBoxUTC.AutoSize = true;
            this._checkBoxUTC.Location = new System.Drawing.Point(-10, 2);
            this._checkBoxUTC.Name = "_checkBoxUTC";
            this._checkBoxUTC.Size = new System.Drawing.Size(58, 21);
            this._checkBoxUTC.TabIndex = 1;
            this._checkBoxUTC.Text = "UTC";
            this._checkBoxUTC.UseVisualStyleBackColor = true;
            this._checkBoxUTC.CheckedChanged += new System.EventHandler(this.checkBoxUTC_CheckedChanged);
            this._checkBoxUTC.SizeChanged += new System.EventHandler(this.MaskedDateTimePicker_Layout);
            // 
            // MaskedDateTimePicker
            // 
            this.Controls.Add(this._maskedTextBox);
            this.Controls.Add(this._checkBoxUTC);
            this.Size = new System.Drawing.Size(200, 25);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MaskedDateTimePicker_Layout);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox _maskedTextBox;
        private System.Windows.Forms.CheckBox _checkBoxUTC;
    }
}
