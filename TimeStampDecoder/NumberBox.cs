using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace TimeStampDecoder
{
    public partial class NumberBox : Panel
    {
        #region Properties

        public bool HEX { get { return _checkBoxHEX.Checked; } set { _checkBoxHEX.Checked = value; } }

        public ulong Value
        {
            get
            {
                ulong value;
                if (TryParse(_textBox.Text, HEX, out value))
                    return value;

                //throw new InvalidOperationException("The value is invalid.");
                return 0;
            }

            set
            {
                _textBox.Text = value.ToString(_checkBoxHEX.Checked ? "X16" : "");
            }
        }

        public override string Text { get { return _textBox.Text; } set { _textBox.Text = value; } }

        #endregion Properties

        #region Events

        public event EventHandler ValueChanged;
        public void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        #endregion Events

        #region ctor

        public NumberBox()
        {
            InitializeComponent();
        }

        #endregion ctor

        private static bool TryParse(string text, bool hex, out ulong value)
        {
            if (ulong.TryParse(text, hex ? NumberStyles.HexNumber : NumberStyles.Integer, null, out value))
                return true;

            return false;
        }

        #region EventHandlers

        private bool Working = false;

        private void NumberBox_Layout(object sender, EventArgs e)
        {
            _checkBoxHEX.Left = Width - _checkBoxHEX.Width;
            _textBox.Width = _checkBoxHEX.Left - 10;
        }

        private void checkBoxHEX_CheckedChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;
                ulong value;
                if (TryParse(_textBox.Text, !HEX, out value))
                    Value = value;
            }
            finally
            {
                Working = false;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;
                OnValueChanged();
            }
            finally
            {
                Working = false;
            }
        }

        #endregion EventHandlers

        private void _textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If it is a ctrl key, then let it through.
            if (e.KeyChar <= 0x20)
                return;

            // If it is a HEX value, and the key is a letter between A and F, then it is valid so just return.
            if (HEX && char.ToUpper(e.KeyChar) >= 'A' && char.ToUpper(e.KeyChar) <= 'F')
                return;

            // Regardless of whether it is a HEX value or not if the key is a digit between 0 and 9, then it is valid so just return.
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                return;

            // If we made it here, it is an invalid key, so just ignore.
            e.Handled = true;
        }
    }
}
