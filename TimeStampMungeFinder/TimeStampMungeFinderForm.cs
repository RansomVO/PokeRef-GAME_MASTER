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

        private void Input_ValueChanged(object sender, EventArgs e)
        {
            if (Working)
                return;

            try
            {
                Working = true;

                DateTime gameMasterTimestamp = TimestampUtils.TicksToDateTime(numberBox.Value);
                DateTime fileTimestamp = maskedDateTimePicker.Value;
                TimeSpan difference = gameMasterTimestamp- fileTimestamp;

                StringBuilder stringBuilder = new StringBuilder("new Mangle(");
                stringBuilder.AppendFormat($"\"{numberBox.Text}\", ");
                stringBuilder.AppendFormat($"new DateTime({fileTimestamp.Year:0000}, {fileTimestamp.Month:00}, {fileTimestamp.Day:00}, {fileTimestamp.Hour:00}, {fileTimestamp.Minute:00}, 0, DateTimeKind.Utc)),");
                stringBuilder.AppendFormat($"    // {gameMasterTimestamp.Year:0000}-{gameMasterTimestamp.Month:00}-{gameMasterTimestamp.Day:00} {gameMasterTimestamp.Hour:00}:{gameMasterTimestamp.Minute:00}:{gameMasterTimestamp.Second:00}.{gameMasterTimestamp.Millisecond:000}");
                stringBuilder.AppendFormat($" - {difference.Days:00} days, {difference.Hours:00} hours, {difference.Minutes:00} minutes");

                textBoxMunge.Text = stringBuilder.ToString();
            }
            catch (Exception)
            {
                // One of the fields is invalid.
                textBoxMunge.Text = string.Empty;
            }
            finally
            {
                Working = false;
            }
        }

        private void File_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.ToString());
        }

        #endregion EventHandlers
    }
}
