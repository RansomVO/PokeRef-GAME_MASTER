using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Moves
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        public string category { get; set; }

        [XmlElement]
        public _Move[] Move { get; set; }

        #region Internal classes

        [Serializable]
        public class _Move
        {

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public string type { get; set; }

            [XmlAttribute]
            public int energy { get; set; }

            [XmlAttribute]
            public int power { get; set; }

            [XmlAttribute]
            public double duration { get; set; }

            [XmlAttribute]
            public int damage_window_start { get; set; }

            [XmlAttribute]
            public int damage_window_end { get; set; }

            public _Move() { }

            public _Move(string _name, string _type, int _energy, int _power, double _duration, int _damage_window_start, int _damage_window_end)
            {
                name = _name;
                type = _type;
                energy = _energy;
                power = _power;
                duration = _duration;
                damage_window_start = _damage_window_start;
                damage_window_end = _damage_window_end;
            }
        }

        #endregion Internal classes

        #region Writers

        private static string FastFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "moves.fast.xml"); } }
        private static string ChargedFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "moves.charged.xml"); } }

        /// <summary>
        /// Write out the Moves that are available in the game.
        /// </summary>
        public static void Write(IEnumerable<MoveTranslator> moves, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            DateTime updateDateTime = gameMasterStatsCalculator.GameMasterStats.last_updated.Date;

            if (!File.Exists(FastFilePath) || Utils.GetLastUpdated(FastFilePath) < updateDateTime ||
                !File.Exists(ChargedFilePath) || Utils.GetLastUpdated(ChargedFilePath) < updateDateTime)
            {
                List<Moves._Move> movesFast = new List<Moves._Move>();
                List<Moves._Move> movesCharged = new List<Moves._Move>();
                foreach (var move in moves)
                {
                    (move.IsFast ? movesFast : movesCharged).Add(
                        new Moves._Move(move.Name, move.Type, move.Energy, move.Power, move.Duration, move.DamageWindowStart, move.DamageWindowEnd));
                }

                if (!File.Exists(FastFilePath) || Utils.GetLastUpdated(FastFilePath) < updateDateTime)
                    Utils.WriteXML(new Moves()
                    {
                        last_updated = updateDateTime,
                        category = "Fast",
                        Move = movesFast.ToArray(),
                    }, FastFilePath);

                if (!File.Exists(ChargedFilePath) || Utils.GetLastUpdated(ChargedFilePath) < updateDateTime)
                    Utils.WriteXML(new Moves()
                    {
                        last_updated = updateDateTime,
                        category = "Charged",
                        Move = movesCharged.ToArray(),
                    }, ChargedFilePath);
            }
        }

        #endregion Writers
    }
}