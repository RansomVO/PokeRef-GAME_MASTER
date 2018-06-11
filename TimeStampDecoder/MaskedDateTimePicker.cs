using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeStampDecoder
{
    public partial class MaskedDateTimePicker : Panel
    {
        #region Data

        private const string _dateFormat = "yyyy-MM-dd HH:mm:ss.fff";

        #endregion Data

        #region Properties

        private bool UTC { get { return _checkBoxUTC.Checked; } set { _checkBoxUTC.Checked = value; } }

        private string Mask { get { return _maskedTextBox.Mask; } set { _maskedTextBox.Mask = value; } }

        public DateTime Value
        {
            get
            {
                DateTime dateTime;
                if (TryParseDateTime(_maskedTextBox.Text, UTC, out dateTime))
                    return dateTime;

                throw new InvalidOperationException("Date is incomplete.");
            }

            set
            {
                _maskedTextBox.Text = value.ToString(_dateFormat);
                UTC = value.Kind == DateTimeKind.Utc;
            }
        }

        #endregion Properties

        #region ctor

        public MaskedDateTimePicker()
        {
            InitializeComponent();
        }

        #endregion ctor

        private static bool TryParseDateTime(string text, bool utc, out DateTime dateTime)
        {
            try
            {
                string[] dateTimeValues = text.Split('-', ' ', ':', '.');
                dateTime = new DateTime(
                    int.Parse(dateTimeValues[0]),
                    int.Parse(dateTimeValues[1]),
                    int.Parse(dateTimeValues[2]),
                    int.Parse(dateTimeValues[3]),
                    int.Parse(dateTimeValues[4]),
                    int.Parse(dateTimeValues[5]),
                    int.Parse(dateTimeValues[6]),
                    utc ? DateTimeKind.Utc : DateTimeKind.Local);
                return true;
            }
            catch (Exception)
            {
                dateTime = DateTime.Now;
                return false;
            }
        }

        #region EventHandlers

        private bool Working = false;

        private void MaskedDateTimePicker_Layout(object sender, EventArgs e)
        {
            _checkBoxUTC.Left = Width - _checkBoxUTC.Width;
            _maskedTextBox.Width = _checkBoxUTC.Left - 10;
        }

        private void checkBoxUTC_CheckedChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;
                DateTime dateTime;
                if (TryParseDateTime(_maskedTextBox.Text, !UTC, out dateTime))
                    _maskedTextBox.Text =
                        (UTC ? TimeZone.CurrentTimeZone.ToUniversalTime(dateTime) : TimeZone.CurrentTimeZone.ToLocalTime(dateTime)).ToString(_dateFormat);
            }
            finally
            {
                Working = false;
            }
        }

        private void maskedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;
                if (_maskedTextBox.Text.Length == _dateFormat.Length)
                    OnTextChanged(e);
            }
            finally
            {
                Working = false;
            }
        }

        #endregion EventHandlers
    }
}
