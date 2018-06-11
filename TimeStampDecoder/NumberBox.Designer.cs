using System;

namespace TimeStampDecoder
{
    partial class NumberBox
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
            this._textBox = new System.Windows.Forms.TextBox();
            this._checkBoxHEX = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Location = new System.Drawing.Point(0, 0);
            this._textBox.MaxLength = 16;
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(100, 22);
            this._textBox.TabIndex = 0;
            this._textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._textBox.WordWrap = false;
            this._textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this._textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_KeyPress);
            // 
            // _checkBoxHEX
            // 
            this._checkBoxHEX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkBoxHEX.AutoSize = true;
            this._checkBoxHEX.Location = new System.Drawing.Point(-10, 2);
            this._checkBoxHEX.Name = "_checkBoxHEX";
            this._checkBoxHEX.Size = new System.Drawing.Size(58, 21);
            this._checkBoxHEX.TabIndex = 1;
            this._checkBoxHEX.Text = "HEX";
            this._checkBoxHEX.UseVisualStyleBackColor = true;
            this._checkBoxHEX.CheckedChanged += new System.EventHandler(this.checkBoxHEX_CheckedChanged);
            this._checkBoxHEX.SizeChanged += new System.EventHandler(this.NumberBox_Layout);
            // 
            // NumberBox
            // 
            this.Controls.Add(this._textBox);
            this.Controls.Add(this._checkBoxHEX);
            this.Size = new System.Drawing.Size(200, 25);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.NumberBox_Layout);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.CheckBox _checkBoxHEX;
    }
}
