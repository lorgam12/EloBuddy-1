using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace JokerLib.Items
{
    public static class Offensive
    {
        public static Item Hydra = new Item((int)ItemId.Ravenous_Hydra, 250);
        public static Item HydraTitanic = new Item((int)ItemId.Titanic_Hydra, 250);
        public static Item Tiamat = new Item((int)ItemId.Tiamat, 250);
        public static Item Cutlass = new Item((int)ItemId.Bilgewater_Cutlass, 550);
        public static Item Botrk = new Item((int)ItemId.Blade_of_the_Ruined_King, 550);
        public static Item Youmuu = new Item((int)ItemId.Youmuus_Ghostblade);
        public static Item Gunblade = new Item((int)ItemId.Hextech_Gunblade, 700);
        public static Item Protobelt = new Item((int)ItemId.Will_of_the_Ancients, 600);
        public static Item GLP = new Item(3030, 600);
    }
}
