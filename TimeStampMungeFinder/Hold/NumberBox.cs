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

namespace TimeStampMungeFinder
{
    public class NumberBox : TextBox
    {
        #region Properties

        public bool HEX
        {
            get { return _HEX; }
            set
            {
                _HEX = value;

                if (Working)
                    return;

                try
                {
                    Working = true;
                    ulong number;
                    if (TryParse(Text, !HEX, out number))
                        Value = number;
                }
                finally
                {
                    Working = false;
                }
            }
        }
        public bool _HEX;

        public ulong Value
        {
            get
            {
                ulong value;
                if (TryParse(Text, HEX, out value))
                    return value;

                //throw new InvalidOperationException("The value is invalid.");
                return 0;
            }

            set
            {
                Text = value.ToString(_HEX ? "X16" : "");
            }
        }

        private bool Working = false;

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

        public NumberBox() :
            base()
        { }

        #endregion ctor

        #region Overrides

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // If it is a ctrl key, then let it through.
            if (e.KeyChar <= 0x20
                || (HEX && char.ToUpper(e.KeyChar) >= 'A' && char.ToUpper(e.KeyChar) <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                base.OnKeyPress(e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Working)
                return;

            base.OnTextChanged(e);

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

        #endregion Overrides

        private static bool TryParse(string text, bool hex, out ulong value)
        {
            if (ulong.TryParse(text, hex ? NumberStyles.HexNumber : NumberStyles.Integer, null, out value))
                return true;

            return false;
        }
    }
}
