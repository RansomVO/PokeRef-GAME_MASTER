using System;
using System.Windows.Forms;

using VanOrman.PokemonGO.GAME_MASTER;
using VanOrman.Utils;

namespace TimeStampDecoder
{
    public partial class TimeStampConverterForm : Form
    {
        #region ctor

        public TimeStampConverterForm()
        {
            InitializeComponent();
            maskedDateTimePicker.Value = DateTime.Now;
        }

        #endregion ctor

        #region EventHandlers

        private bool Working = false;

        private void maskedDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;
                numberBox.Value = GameMasterTimestampUtils.DateTimeToTimestamp(maskedDateTimePicker.Value);
            }
            finally
            {
                Working = false;
            }
        }

        private void numberBox_ValueChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;

                maskedDateTimePicker.Value = checkBoxGameMaster.Checked ?
                    GameMasterTimestampUtils.TimestampToDateTime(numberBox.Value) :
                    TimestampUtils.TimestampToDateTime(numberBox.Value);
            }
            finally
            {
                Working = false;
            }
        }

        private void checkBoxGameMaster_CheckedChanged(object sender, EventArgs e)
        {
            numberBox_ValueChanged(null, null);
        }

        #endregion EventHandlers
    }
}
