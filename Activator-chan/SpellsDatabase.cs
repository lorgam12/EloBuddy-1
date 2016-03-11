using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Activator_chan
{
    class SpellsDatabase
    {
        public static List<SpellsCheck> CheckSpells = new List<SpellsCheck>();

        static SpellsDatabase()
        {
            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Azir,
                    Name = "AzirR",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Karthus,
                    Name = "FallenOne",
                    Slot = SpellSlot.R,
                    DelayX = 2.5,
                    DelayZ = 2.5
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Vi,
                    Name = "ViR",
                    Slot = SpellSlot.R,
                    DelayX = 0.2,
                    DelayZ = 1.0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Syndra,
                    Name = "SyndraR",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Veigar,
                    Name = "VeigarPrimordialBurst",
                    Slot = SpellSlot.R,
                    DelayX = 0.1,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Morgana,
                    Name = "SoulShackles",
                    Slot = SpellSlot.R,
                    DelayX = 2.4,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Vladimir,
                    Name = "VladimirHemoplague",
                    Slot = SpellSlot.R,
                    DelayX = 2.5,
                    DelayZ = 5
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Caitlyn,
                    Name = "CaitlynAceintheHole",
                    Slot = SpellSlot.R,
                    DelayX = 0.2,
                    DelayZ = 0.9
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Velkoz,
                    Name = "VelkozR",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Brand,
                    Name = "BrandWildfire",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Zyra,
                    Name = "ZyraBrambleZone",
                    Slot = SpellSlot.R,
                    DelayX = 1.85,
                    DelayZ = 1.9
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Rumble,
                    Name = "RumbleCarpetBomb",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Lux,
                    Name = "LuxMaliceCannon",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0.1
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Lissandra,
                    Name = "LissandraR",
                    Slot = SpellSlot.R,
                    DelayX = 0.1,
                    DelayZ = 1
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Kennen,
                    Name = "KennenShurikenStorm",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.FiddleSticks,
                    Name = "Crowstorm",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.MonkeyKing,
                    Name = "MonkeyKingSpinToWin",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Zed,
                    Name = "zedult",
                    Slot = SpellSlot.R,
                    DelayX = 1.5,
                    DelayZ = 2.8
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Garen,
                    Name = "GarenR",
                    Slot = SpellSlot.R,
                    DelayX = 0.1,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Nautilus,
                    Name = "nautilusgrandline",
                    Slot = SpellSlot.R,
                    DelayX = 0.1,
                    DelayZ = 0.1
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.JarvanIV,
                    Name = "JarvanIVCataclysm",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Sejuani,
                    Name = "SejuaniGlacialPrisonStart",
                    Slot = SpellSlot.R,
                    DelayX = 0.1,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Katarina,
                    Name = "KatarinaR",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Nocturne,
                    Name = "NocturneParanoia",
                    Slot = SpellSlot.R,
                    DelayX = 0.15,
                    DelayZ = 1.0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Orianna,
                    Name = "OrianaDetonateCommand",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 1.0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Riven,
                    Name = "RivenFengShuiEngine",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 1.0
                });

            CheckSpells.Add(
                new SpellsCheck
                {
                    Champ = Champion.Malphite,
                    Name = "MalphiteR",
                    Slot = SpellSlot.R,
                    DelayX = 0,
                    DelayZ = 0
                });
        }


        public class SpellsCheck
        {
            public Champion Champ;
            public string Name;
            public SpellSlot Slot;
            public double DelayX;
            public double DelayZ;

            public SpellsCheck()
            {

            }

            public SpellsCheck(Champion Champ, string Name, SpellSlot Slot, double DelayX, double DelayZ)
            {
                this.Champ = Champ;
                this.Name = Name;
                this.Slot = Slot;
                this.DelayX = DelayX;
                this.DelayZ = DelayZ;
            }
        }
    }
}
