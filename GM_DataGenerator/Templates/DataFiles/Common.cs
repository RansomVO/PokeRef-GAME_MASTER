using System;
using System.Xml.Serialization;

using POGOProtos.Settings.Master.Pokemon;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Common
    {
        [Serializable]
        public class IVScore
        {
            [XmlAttribute]
            public int attack { get; set; }

            [XmlAttribute]
            public int defense { get; set; }

            [XmlAttribute]
            public int stamina { get; set; }

            public IVScore() { }

            public IVScore(int _attack, int _defense, int _stamina)
            {
                attack = _attack;
                defense = _defense;
                stamina = _stamina;
            }

            public IVScore(StatsAttributes stats)
            {
                attack = stats.base_attack;
                defense = stats.base_defense;
                stamina = stats.base_stamina;
            }
        }

        [Serializable]
        public class PossibilitySet
        {
            [XmlAttribute]
            public int columns { get; set; }

            [XmlElement]
            public Possibility[] Possibilities { get; set; }

            #region Internal classes

            [Serializable]
            public class Possibility
            {
                #region Properties 

                [XmlAttribute]
                public int cp { get; set; }

                [XmlAttribute]
                public int min_score { get; set; }

                [XmlAttribute]
                public int max_score { get; set; }

                [XmlElement]
                public IV[] IVs { get; set; }

                #endregion Properties 

                #region Internal classes

                [Serializable]
                public class IV : Common.IVScore
                {
                    #region Properties

                    [XmlAttribute]
                    public int hp { get; set; }

                    [XmlAttribute]
                    public int score { get { return (int)Math.Round((attack + defense + stamina) * 100.0 / (PokeConstants.Evaluation.IV.Max)); } set { } }

                    #endregion Properties

                    #region ctor

                    public IV() { }

                    public IV(Common.IVScore ivScore, int _hp)
                    {
                        attack = ivScore.attack;
                        defense = ivScore.defense;
                        stamina = ivScore.stamina;
                        hp = _hp;
                    }

                    #endregion ctor
                }

                #endregion Internal classes

                #region ctor

                public Possibility() { }

                public Possibility(int _cp, IV[] ivs)
                {
                    cp = _cp;
                    IVs = ivs;

                    // Calculate min/max score.
                    max_score = -1;
                    min_score = ivs.Length == 1 ? 0 : int.MaxValue;
                    foreach (IV iv in ivs)
                    {
                        if (iv.score > max_score)
                            max_score = iv.score;
                        if (iv.score < min_score)
                            min_score = iv.score;
                    }
                }

                #endregion ctor
            }

            #endregion Internal classes

            public PossibilitySet() { }

            public PossibilitySet(Possibility[] possibilities)
            {
                Possibilities = possibilities;

                foreach (var possibility in Possibilities)
                    if (possibility.IVs.Length > columns)
                        columns = possibility.IVs.Length;
            }
        }


    }
}
