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

namespace JokerLib.Detectors
{
    public static class Stealth
    {
        public static List<SpellData> SpellList = new List<SpellData>();

        public static void LoadSpells()
        {
            SpellList.Add(new SpellData { Name = Champion.Akali, SName = "akalismokebomb", Slot = SpellSlot.W });
            SpellList.Add(new SpellData { Name = Champion.Shaco, SName = "deceive", Slot = SpellSlot.Q });
            SpellList.Add(new SpellData { Name = Champion.Khazix, SName = "khazixr", Slot = SpellSlot.R });
            SpellList.Add(new SpellData { Name = Champion.Khazix, SName = "khazixrlong", Slot = SpellSlot.R });
            SpellList.Add(new SpellData { Name = Champion.Talon, SName = "talonshadowassault", Slot = SpellSlot.R });
            SpellList.Add(new SpellData { Name = Champion.MonkeyKing, SName = "monkeykingdecoy", Slot = SpellSlot.W });
            SpellList.Add(new SpellData { Name = Champion.Vayne, SName = "vaynetumble", Slot = SpellSlot.Q });
            SpellList.Add(new SpellData { Name = Champion.Twitch, SName = "hideinshadows", Slot = SpellSlot.Q });
        }
    }

    public class SpellData
    {
        public Champion Name;
        public string SName;
        public SpellSlot Slot;

        public SpellData() { }

        public SpellData(Champion Name, string SName, SpellSlot Slot)
        {
            this.Name = Name;
            this.SName = SName;
            this.Slot = Slot;
        }
    }
}
