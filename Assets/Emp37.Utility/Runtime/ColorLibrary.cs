using System;
using static System.Byte;

using UnityEngine;
using Random = UnityEngine.Random;

namespace Emp37.Utility
{
      using static Shades;

      public readonly struct ColorLibrary
      {
            #region L I B R A R Y
            private static readonly Color32 AMARANTH = new(229, 043, 080, MaxValue);
            private static readonly Color32 AMETHYST = new(153, 102, 204, MaxValue);
            private static readonly Color32 APRICOT = new(251, 206, 177, MaxValue);
            private static readonly Color32 AQUAMARINE = new(127, 255, 212, MaxValue);
            private static readonly Color32 AZURE = new(000, 127, 255, MaxValue);
            private static readonly Color32 BEIGE = new(245, 245, 220, MaxValue);
            private static readonly Color32 BLACK = new(000, 000, 000, MaxValue);
            private static readonly Color32 BLOND = new(250, 240, 190, MaxValue);
            private static readonly Color32 BLUE = new(000, 000, 255, MaxValue);
            private static readonly Color32 BROWN = new(150, 075, 000, MaxValue);
            private static readonly Color32 CINNAMON = new(210, 105, 030, MaxValue);
            private static readonly Color32 CHERRY = new(222, 049, 099, MaxValue);
            private static readonly Color32 CHOCOLATE = new(123, 063, 000, MaxValue);
            private static readonly Color32 COBALT = new(000, 071, 171, MaxValue);
            private static readonly Color32 COFFEE = new(111, 078, 055, MaxValue);
            private static readonly Color32 CORAL = new(255, 127, 080, MaxValue);
            private static readonly Color32 COTTON_CANDY = new(255, 188, 217, MaxValue);
            private static readonly Color32 CRIMSON = new(220, 020, 060, MaxValue);
            private static readonly Color32 CYAN = new(000, 255, 255, MaxValue);
            private static readonly Color32 DANDELION = new(240, 225, 048, MaxValue);
            private static readonly Color32 DARK_GREY = new(090, 090, 090, MaxValue);
            private static readonly Color32 EDITOR_TEXT = new(185, 185, 185, MaxValue);
            private static readonly Color32 EGGPLANT = new(097, 064, 081, MaxValue);
            private static readonly Color32 EMERALD = new(080, 200, 120, MaxValue);
            private static readonly Color32 FOREST = new(034, 139, 034, MaxValue);
            private static readonly Color32 GOLD = new(255, 215, 000, MaxValue);
            private static readonly Color32 GREEN = new(000, 255, 000, MaxValue);
            private static readonly Color32 GREY = new(127, 127, 127, MaxValue);
            private static readonly Color32 HELIOTROPE = new(223, 115, 255, MaxValue);
            private static readonly Color32 HONEYDEW = new(240, 255, 240, MaxValue);
            private static readonly Color32 ICTERINE = new(252, 247, 094, MaxValue);
            private static readonly Color32 KHAKI = new(195, 176, 145, MaxValue);
            private static readonly Color32 LAVENDER = new(230, 230, 250, MaxValue);
            private static readonly Color32 LEMON = new(255, 247, 000, MaxValue);
            private static readonly Color32 LIME = new(191, 255, 000, MaxValue);
            private static readonly Color32 LINEN = new(250, 240, 230, MaxValue);
            private static readonly Color32 MAGENTA = new(255, 000, 255, MaxValue);
            private static readonly Color32 MAROON = new(127, 000, 000, MaxValue);
            private static readonly Color32 MINT = new(062, 180, 137, MaxValue);
            private static readonly Color32 MISTY_ROSE = new(255, 228, 225, MaxValue);
            private static readonly Color32 MUSTARD = new(255, 219, 088, MaxValue);
            private static readonly Color32 OLIVE = new(128, 128, 000, MaxValue);
            private static readonly Color32 ONYX = new(015, 015, 015, MaxValue);
            private static readonly Color32 ORANGE = new(255, 165, 000, MaxValue);
            private static readonly Color32 PEAR = new(209, 226, 049, MaxValue);
            private static readonly Color32 PINK = new(255, 192, 203, MaxValue);
            private static readonly Color32 PISTACHIO = new(147, 197, 114, MaxValue);
            private static readonly Color32 PLUM = new(221, 160, 221, MaxValue);
            private static readonly Color32 RASPBERRY = new(227, 011, 093, MaxValue);
            private static readonly Color32 RED = new(255, 000, 000, MaxValue);
            private static readonly Color32 RICH_BLACK = new(000, 064, 064, MaxValue);
            private static readonly Color32 ROSE = new(255, 000, 127, MaxValue);
            private static readonly Color32 RUBY = new(224, 017, 095, MaxValue);
            private static readonly Color32 SALMON = new(250, 128, 114, MaxValue);
            private static readonly Color32 SEA_GREEN = new(000, 087, 051, MaxValue);
            private static readonly Color32 SIENNA = new(136, 045, 023, MaxValue);
            private static readonly Color32 SILVER = new(192, 192, 192, MaxValue);
            private static readonly Color32 SKYBLUE = new(135, 206, 235, MaxValue);
            private static readonly Color32 TANGERINE = new(242, 133, 000, MaxValue);
            private static readonly Color32 TEAL = new(000, 127, 127, MaxValue);
            private static readonly Color32 TOMATO = new(255, 099, 071, MaxValue);
            private static readonly Color32 TURQUOISE = new(048, 213, 200, MaxValue);
            private static readonly Color32 VANILLA = new(243, 229, 171, MaxValue);
            private static readonly Color32 VIOLET = new(238, 130, 238, MaxValue);
            private static readonly Color32 WHITE = new(255, 255, 255, MaxValue);
            private static readonly Color32 WHITE_SMOKE = new(245, 245, 238, MaxValue);
            private static readonly Color32 WISTERIA = new(201, 160, 220, MaxValue);
            private static readonly Color32 YELLOW = new(255, 255, 000, MaxValue);
            #endregion

            private static readonly int length = Enum.GetNames(typeof(Shades)).Length;

            public static Color32 Random => Pick((Shades) UnityEngine.Random.Range(0, length));
            public static Color32 Pick(Shades shade) => shade switch
            {
                  Amaranth => AMARANTH,
                  Amethyst => AMETHYST,
                  Apricot => APRICOT,
                  Aquamarine => AQUAMARINE,
                  Azure => AZURE,
                  Beige => BEIGE,
                  Black => BLACK,
                  Blond => BLOND,
                  Blue => BLUE,
                  Brown => BROWN,
                  Cinnamon => CINNAMON,
                  Cherry => CHERRY,
                  Chocolate => CHOCOLATE,
                  Cobalt => COBALT,
                  Coffee => COFFEE,
                  Coral => CORAL,
                  CottonCandy => COTTON_CANDY,
                  Crimson => CRIMSON,
                  Cyan => CYAN,
                  Dandelion => DANDELION,
                  DarkGrey => DARK_GREY,
                  EditorText => EDITOR_TEXT,
                  Eggplant => EGGPLANT,
                  Emerald => EMERALD,
                  Forest => FOREST,
                  Gold => GOLD,
                  Green => GREEN,
                  Grey => GREY,
                  Heliotrope => HELIOTROPE,
                  Honeydew => HONEYDEW,
                  Icterine => ICTERINE,
                  Khaki => KHAKI,
                  Lavender => LAVENDER,
                  Lemon => LEMON,
                  Lime => LIME,
                  Linen => LINEN,
                  Magenta => MAGENTA,
                  Maroon => MAROON,
                  Mint => MINT,
                  MistyRose => MISTY_ROSE,
                  Mustard => MUSTARD,
                  Olive => OLIVE,
                  Onyx => ONYX,
                  Orange => ORANGE,
                  Pear => PEAR,
                  Pink => PINK,
                  Pistachio => PISTACHIO,
                  Plum => PLUM,
                  Raspberry => RASPBERRY,
                  Red => RED,
                  RichBlack => RICH_BLACK,
                  Rose => ROSE,
                  Ruby => RUBY,
                  Salmon => SALMON,
                  SeaGreen => SEA_GREEN,
                  Sienna => SIENNA,
                  Silver => SILVER,
                  Skyblue => SKYBLUE,
                  Tangerine => TANGERINE,
                  Teal => TEAL,
                  Tomato => TOMATO,
                  Turquoise => TURQUOISE,
                  Vanilla => VANILLA,
                  Violet => VIOLET,
                  White => WHITE,
                  WhiteSmoke => WHITE_SMOKE,
                  Wisteria => WISTERIA,
                  Yellow => YELLOW,
                  _ => throw new NotImplementedException()
            };
      }
}