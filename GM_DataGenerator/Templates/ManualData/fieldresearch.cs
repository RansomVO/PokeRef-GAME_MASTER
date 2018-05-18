using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
	[Serializable]
	public class FieldResearch
	{
		[XmlAttribute(DataType = "date")]
		public DateTime last_updated { get; set; }

		[XmlElement]
		public _Category[] Category { get; set; }

        #region Internal classes

        [Serializable]
        public class _Category
        {
            [XmlAttribute]
            public string type { get; set; }

            [XmlElement]
            public _Research[] Research { get; set; }

            #region Internal classes

            [Serializable]
            public class _Research
            {
                [XmlAttribute]
                public string task { get; set; }

                [XmlElement]
                public Pokemon[] Encounter { get; set; }

                [XmlElement]
                public _Item[] Item { get; set; }

                #region Internal classes

                [Serializable]
                public class _Item
                {
                    [XmlAttribute]
                    public string id { get; set; }

                    [XmlAttribute]
                    public string amount { get; set; }
                }

                #endregion Internal classes
            }
            #endregion Internal classes
        }
        #endregion Internal classes
    }
}