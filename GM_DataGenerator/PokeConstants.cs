using System;
using System.Collections.Generic;
using System.Reflection;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    static class PokeConstants
    {
        public readonly static DateTime LastModified = DateTime.Parse("2018-06-14");

        #region const Values

        public const int MaxPokemonLevel = 40;
        public const int MinRaidBossIV = 10;

        public const string DateFormat = "dd-MMM-yyyy";
        public const string DateTimeFormat = DateFormat + " HH:mm:ss.fff";

        #endregion const Values

        #region enums

        #region constants.xml

        public static class Availability
        {
            public const string Unreleased = "Unreleased";
            public const string General = "General";
            public const string Regional = "Regional";
            public const string RaidBossOnly = "Raid Boss Only";
            public const string EXRaidBossOnly = "EX Raid Boss Only";
            public const string HatchOnly_2K = "Hatch Only: 2K";
            public const string HatchOnly_5K = "Hatch Only: 5K";
            public const string HatchOnly_10K = "Hatch Only: 10K";

            public static class Region
            {
                public const string Asia = "Asia";
                public const string Australia = "Australia";
                public const string Europe = "Europe";
                public const string UnitedStates = "United States";
                public const string Equatorial = "Equatorial";
                public const string SouthernHemisphere = "Southern Hemisphere";
                public const string Mexico = "Mexico";
                public const string Americas = "Americas";
                public const string India = "India";
                public const string Thailand = "Thailand";
                public const string Rotating = "Rotating";
                public const string NewZealand = "New Zealand";
                public const string Fiji = "Fiji";
                public const string Pacific = "Pacific";
                public const string Unknown = "?";
            }
        }

        public static class Rarity
        {
            public const string Legendary = "Legendary";
            public const string Mythic = "Mythic";
            public const string UltraBeast = "Ultra Beast";
        }

        public static class NumericChars
        {
            public const char _00 = '⓪';
            public const char _01 = '①';
            public const char _02 = '②';
            public const char _03 = '③';
            public const char _04 = '④';
            public const char _05 = '⑤';
            public const char _06 = '⑥';
            public const char _07 = '⑦';
            public const char _08 = '⑧';
            public const char _09 = '⑨';
            public const char _10 = '⑩';
            public const char _11 = '⑪';
            public const char _12 = '⑫';
            public const char _13 = '⑬';
            public const char _14 = '⑭';
            public const char _15 = '⑮';
            public const char _16 = '⑯';
            public const char _17 = '⑰';
            public const char _18 = '⑱';
            public const char _19 = '⑲';
            public const char _20 = '⑳';
            public const char _21 = '㉑';
            public const char _22 = '㉒';
            public const char _23 = '㉓';
            public const char _24 = '㉔';
            public const char _25 = '㉕';
            public const char _26 = '㉖';
            public const char _27 = '㉗';
            public const char _28 = '㉘';
            public const char _29 = '㉙';
            public const char _30 = '㉚';
            public const char _31 = '㉛';
            public const char _32 = '㉜';
            public const char _33 = '㉝';
            public const char _34 = '㉞';
            public const char _35 = '㉟';
            public const char _36 = '㊱';
            public const char _37 = '㊲';
            public const char _38 = '㊳';
            public const char _39 = '㊴';
            public const char _40 = '㊵';
            public const char _41 = '㊶';
            public const char _42 = '㊷';
            public const char _43 = '㊸';
            public const char _44 = '㊹';
            public const char _45 = '㊺';
            public const char _46 = '㊻';
            public const char _47 = '㊼';
            public const char _48 = '㊽';
            public const char _49 = '㊾';
            public const char _50 = '㊿';
        }

        public static class Evaluation
        {
            public class EvaluationValue
            {
                public EvaluationValue(int min, int max)
                {
                    Min = min;
                    Max = max;
                }

                public int Min { get; private set; }
                public int Max { get; private set; }
            }

            public static class IV
            {
                public const int Max = 45;
                public static readonly EvaluationValue NotGreat = new EvaluationValue(0, 22);
                public static readonly EvaluationValue Decent = new EvaluationValue(23, 29);
                public static readonly EvaluationValue Strong = new EvaluationValue(30, 36);
                public static readonly EvaluationValue Amazes = new EvaluationValue(37, 45);
            }
            public static class Attribute
            {
                public const int Max = 15;
                public static readonly EvaluationValue NotGreatness = new EvaluationValue(0, 7);
                public static readonly EvaluationValue JobDone = new EvaluationValue(8, 12);
                public static readonly EvaluationValue Excellent = new EvaluationValue(13, 14);
                public static readonly EvaluationValue Wow = new EvaluationValue(15, 15);
            }
		}

		public static class Gender
		{
			public const string Male = "♂";
			public const string Female = "♀";
			public const string Neutral = "⚲";
		}

		#endregion constants.xml

		public enum PokeType
        {
            Other = -1,
            Bug = 0,
            Dark,
            Dragon,
            Electric,
            Fairy,
            Fighting,
            Fire,
            Flying,
            Ghost,
            Grass,
            Ground,
            Ice,
            Normal,
            Poison,
            Psychic,
            Rock,
            Steel,
            Water,
        };

        public static class Weather
        {
            public const string Unknown = "Unknown";
            public const string Rainy = "Rainy";
            public const string Fog = "Fog";
            public const string Windy = "Windy";
            public const string Cloudy = "Cloudy";
            public const string Sunny = "Sunny";
            public const string Snow = "Snow";
            public const string PartlyCloudy = "Partly Cloudy";
        }

        public enum Effectiveness
        {
            SuperEffective,
            Neutral,
            NotVeryEffective,
            Immune,
        };

        #endregion enums

        #region Regions

        public static string[] Regions = new[]
        {
            "",         // dummy so indexing matches
            "Kanto",    // Gen1
            "Johto",    // Gen2
            "Hoenn",    // Gen3
            "Sinnoh",   // Gen4
            "Unova",    // Gen5
            "Kalos",    // Gen6
            "Alola",    // Gen7
        };

        #endregion Regions

        #region Effectiveness

        public class EffectivnessKeyEntry
        {
            public Effectiveness Key { get; private set; }
            public char Symbol { get; private set; }
            public double Multiplier { get; private set; }

            public EffectivnessKeyEntry(Effectiveness key, char symbol, double multiplier)
            {
                Key = key;
                Symbol = symbol;
                Multiplier = multiplier;
            }
        }

        public static readonly EffectivnessKeyEntry[] EffectivnessKey = new[]
        {
            new EffectivnessKeyEntry(Effectiveness.SuperEffective, '+', 1.4),
            new EffectivnessKeyEntry(Effectiveness.Neutral, '○', 1),
            new EffectivnessKeyEntry(Effectiveness.NotVeryEffective, '-', 0.714),
            new EffectivnessKeyEntry(Effectiveness.Immune, '×', 0.51),
        };

        public static readonly char[][] Effectivness = new char[][]
        {
            new[] {'○', '+', '○', '○', '-', '-', '-', '-', '-', '+', '○', '○', '○', '-', '+', '○', '-', '○', }, // Bug 
            new[] {'○', '-', '○', '○', '-', '-', '○', '○', '+', '○', '○', '○', '○', '○', '+', '○', '○', '○', }, // Dark 
            new[] {'○', '○', '+', '○', '×', '○', '○', '○', '○', '○', '○', '○', '○', '○', '○', '○', '-', '○', }, // Dragon 
            new[] {'○', '○', '-', '-', '○', '○', '○', '+', '○', '-', '×', '○', '○', '○', '○', '○', '○', '+', }, // Electric 
            new[] {'○', '+', '+', '○', '○', '+', '-', '○', '○', '○', '○', '○', '○', '-', '○', '○', '-', '○', }, // Fairy 
            new[] {'-', '+', '○', '○', '-', '○', '○', '-', '×', '○', '○', '+', '+', '-', '-', '+', '+', '○', }, // Fighting 
            new[] {'+', '○', '○', '-', '○', '○', '○', '○', '○', '+', '○', '+', '○', '○', '○', '○', '+', '○', }, // Fire 
            new[] {'+', '○', '○', '○', '○', '+', '○', '○', '○', '+', '○', '○', '○', '○', '○', '-', '-', '○', }, // Flying 
            new[] {'○', '-', '○', '○', '○', '○', '○', '○', '+', '○', '○', '○', '-', '○', '+', '○', '○', '○', }, // Ghost 
            new[] {'-', '○', '○', '-', '○', '○', '-', '-', '○', '-', '+', '○', '○', '-', '○', '+', '-', '+', }, // Grass 
            new[] {'○', '○', '○', '+', '○', '○', '+', '×', '○', '○', '-', '-', '○', '+', '○', '+', '+', '○', }, // Ground 
            new[] {'○', '○', '+', '○', '○', '○', '-', '+', '○', '+', '+', '-', '○', '○', '○', '○', '-', '-', }, // Ice 
            new[] {'○', '○', '○', '○', '○', '○', '○', '○', '×', '○', '○', '○', '○', '○', '○', '-', '-', '○', }, // Normal 
            new[] {'○', '○', '○', '○', '+', '○', '○', '○', '-', '+', '-', '○', '○', '-', '○', '-', '×', '○', }, // Poison 
            new[] {'○', '×', '○', '○', '○', '+', '○', '○', '○', '○', '○', '○', '○', '+', '-', '○', '-', '○', }, // Psychic 
            new[] {'+', '○', '○', '○', '○', '-', '+', '+', '○', '○', '-', '+', '○', '○', '○', '○', '-', '○', }, // Rock 
            new[] {'○', '○', '○', '-', '+', '○', '-', '○', '○', '○', '○', '+', '○', '○', '○', '+', '-', '-', }, // Steel 
            new[] {'○', '○', '○', '-', '○', '○', '+', '○', '○', '-', '+', '○', '○', '○', '○', '+', '○', '-', }, // Water 
        };

        #endregion Effectiveness

        #region Mappings

        public static readonly KeyValuePair<PokeType, string>[] WeatherBoosts = new[]
        {
            new KeyValuePair<PokeType, string>(PokeType.Bug, Weather.Rainy),
            new KeyValuePair<PokeType, string>(PokeType.Dark, Weather.Fog),
            new KeyValuePair<PokeType, string>(PokeType.Dragon, Weather.Windy),
            new KeyValuePair<PokeType, string>(PokeType.Electric, Weather.Rainy),
            new KeyValuePair<PokeType, string>(PokeType.Fairy, Weather.Cloudy),
            new KeyValuePair<PokeType, string>(PokeType.Fighting, Weather.Cloudy),
            new KeyValuePair<PokeType, string>(PokeType.Fire, Weather.Sunny),
            new KeyValuePair<PokeType, string>(PokeType.Flying, Weather.Windy),
            new KeyValuePair<PokeType, string>(PokeType.Ghost, Weather.Fog),
            new KeyValuePair<PokeType, string>(PokeType.Grass, Weather.Sunny),
            new KeyValuePair<PokeType, string>(PokeType.Ground, Weather.Sunny),
            new KeyValuePair<PokeType, string>(PokeType.Ice, Weather.Snow),
            new KeyValuePair<PokeType, string>(PokeType.Normal, Weather.PartlyCloudy),
            new KeyValuePair<PokeType, string>(PokeType.Poison, Weather.Cloudy),
            new KeyValuePair<PokeType, string>(PokeType.Psychic, Weather.Windy),
            new KeyValuePair<PokeType, string>(PokeType.Rock, Weather.PartlyCloudy),
            new KeyValuePair<PokeType, string>(PokeType.Steel, Weather.Snow),
            new KeyValuePair<PokeType, string>(PokeType.Water, Weather.Rainy),
            new KeyValuePair<PokeType, string>(PokeType.Other, Weather.Unknown),
        };

        #endregion Mappings

        public static string[] GetValues(Type type)
        {
            List<string> values = new List<string>();

            foreach (var field in type.GetTypeInfo().DeclaredFields)
                if (!field.IsSpecialName)
                    values.Add(field.Name);

            return values.ToArray();
        }
    }
}
