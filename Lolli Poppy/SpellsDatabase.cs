using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace Lolli_Poppy
{
    class SpellsDatabase
    {
        public static List<SpellsCheck> Spells = new List<SpellsCheck>();

        static SpellsDatabase()
        {
            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.MonkeyKing,
                Name = "MonkeyKing",
                SpellName = "",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Nidalee,
                Name = "Nidalee",
                SpellName = "pounce",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Amumu,
                Name = "Amumu",
                SpellName = "BandageToss",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Aatrox,
                Name = "Aatrox",
                SpellName = "aatroxq",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Alistar,
                Name = "Alistar",
                SpellName = "headbutt",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Ahri,
                Name = "Ahri",
                SpellName = "ahritumble",
                SpellSlots = SpellSlot.R
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Akali,
                Name = "Akali",
                SpellName = "akalishadowdance",
                SpellSlots = SpellSlot.R
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Braum,
                Name = "Braum",
                SpellName = "braumw",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Corki,
                Name = "Corki",
                SpellName = "carpetbomb",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Diana,
                Name = "Diana",
                SpellName = "dianateleport",
                SpellSlots = SpellSlot.R
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Fiora,
                Name = "Fiora",
                SpellName = "fioraq",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Fizz,
                Name = "Fizz",
                SpellName = "fizzpiercingstrike",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Gnar,
                Name = "Gnar",
                SpellName = "GnarE",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Gragas,
                Name = "Gragas",
                SpellName = "aatroxq",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Graves,
                Name = "Graves",
                SpellName = "gravesmove",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Irelia,
                Name = "Irelia",
                SpellName = "ireliagatotsu",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Jax,
                Name = "Jax",
                SpellName = "jaxleapstrike",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Khazix,
                Name = "Kha'Zix",
                SpellName = "khazixe",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Khazix,
                Name = "Kha'Zix",
                SpellName = "khazixelong",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Leblanc,
                Name = "Leblanc",
                SpellName = "leblancslide",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Leblanc,
                Name = "Leblanc",
                SpellName = "leblancslidem",
                SpellSlots = SpellSlot.R
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.LeeSin,
                Name = "LeeSin",
                SpellName = "blindmonkqtwo",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Leona,
                Name = "Leona",
                SpellName = "leonazenithblademissle",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Lucian,
                Name = "Lucian",
                SpellName = "luciane",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Quinn,
                Name = "Quinn",
                SpellName = "quinne",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Caitlyn,
                Name = "Caitlyn",
                SpellName = "CaitlynEntrapment",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Riven,
                Name = "Riven",
                SpellName = "riventricleave",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Riven,
                Name = "Riven",
                SpellName = "riventricleave_03",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Riven,
                Name = "Riven",
                SpellName = "rivenfeint",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Sejuani,
                Name = "Sejuani",
                SpellName = "sejuaniarcticassault",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Shen,
                Name = "Shen",
                SpellName = "shenshadowdash",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Shyvana,
                Name = "Shyvana",
                SpellName = "shyvanatransformcast",
                SpellSlots = SpellSlot.R
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Tristana,
                Name = "Tristana",
                SpellName = "rocketjump",
                SpellSlots = SpellSlot.W
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Tryndamere,
                Name = "Tryndamere",
                SpellName = "slashcast",
                SpellSlots = SpellSlot.E
            });
            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Vi,
                Name = "Vi",
                SpellName = "viq",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.XinZhao,
                Name = "XinZhao",
                SpellName = "xenzhaosweep",
                SpellSlots = SpellSlot.E
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Nautilus,
                Name = "Nautilus",
                SpellName = "NautilusAnchorDrag",
                SpellSlots = SpellSlot.Q
            });

            Spells.Add(
            new SpellsCheck
            {
                Champion = Champion.Zac,
                Name = "Zac",
                SpellName = "zace",
                SpellSlots = SpellSlot.E
            });
        }
    }

    public class SpellsCheck
    {
        public Champion Champion;
        public string Name;
        public string SpellName;
        public SpellSlot SpellSlots;

        public SpellsCheck()
        {

        }

        public SpellsCheck(Champion Champion, string Name, string SpellName, SpellSlot SpellSlots)
        {
            this.Champion = Champion;
            this.Name = Name;
            this.SpellName = SpellName;
            this.SpellSlots = SpellSlots;
        }
    }
}
