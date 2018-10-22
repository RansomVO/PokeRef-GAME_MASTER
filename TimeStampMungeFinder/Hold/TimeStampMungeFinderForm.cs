using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VanOrman.PokemonGO.GAME_MASTER;
using VanOrman.Utils;

namespace TimeStampMungeFinder
{
    public partial class TimeStampMungeFinderForm : Form
    {
        #region Fields

        private bool Working = false;

        #endregion Fields

        #region ctor

        public TimeStampMungeFinderForm()
        {
            InitializeComponent();
        }

        #endregion ctor

        #region EventHandlers

        private void checkBoxHEX_CheckedChanged(object sender, EventArgs e)
        {
            numberBoxTimeStamp.HEX = checkBoxHEX.Checked;
        }

        private void checkBoxUTC_CheckedChanged(object sender, EventArgs e)
        {
            maskedDateTimePickerNormal.UTC =
                maskedDateTimePickerGameMaster.UTC =
                checkBoxHEX.Checked;
        }

        private void numberBoxTimeStamp_ValueChanged(object sender, EventArgs e)
        {
            // Make sure we aren't in the middle of doing work.
            if (Working)
                return;

            try
            {
                Working = true;

                // Convert TimeStamp to DateTime for each.
                maskedDateTimePickerNormal.Value = TimestampUtils.TicksToDateTime(numberBoxTimeStamp.Value);
                maskedDateTimePickerGameMaster.Value = GameMasterTimestampUtils.TicksToDateTime(numberBoxTimeStamp.Value);
            }
            finally
            {
                Working = false;
            }
        }

        private void maskedDateTimePickerNormal_ValueChanged(object sender, EventArgs e)
        {
            numberBoxTimeStamp.Value = TimestampUtils.DateTimeToTicks(maskedDateTimePickerNormal.Value);
        }

        private void maskedDateTimePickerGameMaster_ValueChanged(object sender, EventArgs e)
        {
            numberBoxTimeStamp.Value = GameMasterTimestampUtils.DateTimeToTicks(maskedDateTimePickerNormal.Value);
        }

        #endregion EventHandlers

        private void maskedDateTimePickerGameMaster_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TimeStampMungeFinderForm_Load(object sender, EventArgs e)
        {

        }

        private void labelGameMaster_Click(object sender, EventArgs e)
        {

        }

        private void labelTimeStamp_Click(object sender, EventArgs e)
        {

        }

        private void maskedDateTimePickerNormal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void numberBoxTimeStamp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
