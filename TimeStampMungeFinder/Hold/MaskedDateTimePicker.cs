using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeStampMungeFinder
{
    public class MaskedDateTimePicker : MaskedTextBox
    {
        #region Data

        private const string _dateFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private const string _mask = "0000-00-00 00:00:00.000";

        #endregion Data

        #region Properties

        public bool UTC
        {
            get { return _UTC; }
            set
            {
                _UTC = value;

                if (Working)
                    return;

                try
                {
                    Working = true;
                    DateTime dateTime;
                    if (TryParseDateTime(Text, !UTC, out dateTime))
                        Text =
                            (UTC ? TimeZone.CurrentTimeZone.ToUniversalTime(dateTime) : TimeZone.CurrentTimeZone.ToLocalTime(dateTime)).ToString(_dateFormat);
                }
                finally
                {
                    Working = false;
                }

            }
        }
        private bool _UTC;

        public DateTime Value
        {
            get
            {
                DateTime dateTime;
                if (TryParseDateTime(Text, UTC, out dateTime))
                    return dateTime;

                throw new InvalidOperationException("Date is incomplete.");
            }

            set
            {
                UTC = value.Kind == DateTimeKind.Utc;
                Text = value.ToString(_dateFormat);
            }
        }

        private bool Working = false;

        #endregion Properties

        #region ctor

        public MaskedDateTimePicker()
            : base(_mask)
        { }

        #endregion ctor

        #region Events

        public event EventHandler ValueChanged;
        public void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            if (null != handler)
                handler(this, EventArgs.Empty);
        }

        #endregion Events

        protected override void OnTextChanged(EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;

                DateTime dateTime;
                if (Text.Length == _dateFormat.Length &&
                    TryParseDateTime(Text, UTC, out dateTime))
                {
                    OnValueChanged();
                }
            }
            finally
            {
                Working = false;
            }

            base.OnTextChanged(e);
        }

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
    }
}
