using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class RaidBoss : Pokemon
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        [DefaultValue(0)]
        public int raid_cp { get; set; }

        [XmlElement]
        public Pokemon Pokemon { get; set; }

        [XmlElement]
        public Common.PossibilitySet Regular { get; set; }

        [XmlElement]
        public Common.PossibilitySet Boosted { get; set; }

        #endregion Properties

        #region ctor

        public RaidBoss() { }

        public RaidBoss(RaidBosses._RaidBoss raidboss, Common.PossibilitySet.Possibility[] regular, Common.PossibilitySet.Possibility[] boosted)
        {
            last_updated = DateTime.Today;
            Pokemon = new Pokemon(raidboss.id, raidboss.name);
            raid_cp = raidboss.raid_cp;
            Regular = new Common.PossibilitySet(regular);
            Boosted = new Common.PossibilitySet(boosted);
        }

        #endregion ctor
    }
}