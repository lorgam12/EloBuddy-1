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
    class ActivatorItems
    {
        //Defensive

        public static Item Zhonya;
        public static Item Seraph;
        public static Item Solari;
        public static Item FaceMountain;
        public static Item Mikael;
        public static Item Qss;

        //Offensive

        public static Item Youmu;
        public static Item Tiamat;
        public static Item Bilgewater;
        public static Item Muramana;
        public static Item HydraRavenous;
        public static Item HydraTitanic;
        public static Item BladeKing;
            
        public static void Load()
        {
            Zhonya = new Item((int)ItemId.Zhonyas_Hourglass);
            Seraph = new Item((int)ItemId.Seraphs_Embrace);
            Solari = new Item((int)ItemId.Locket_of_the_Iron_Solari, 600);
            FaceMountain = new Item((int)ItemId.Face_of_the_Mountain, 700);
            Mikael = new Item((int)ItemId.Mikaels_Crucible, 750);
            Qss = new Item((int)ItemId.Quicksilver_Sash);

            Youmu = new Item((int)ItemId.Youmuus_Ghostblade);
            Tiamat = new Item((int)ItemId.Tiamat, 300);
            Muramana = new Item((int)ItemId.Muramana, Player.Instance.GetAutoAttackRange());
            HydraRavenous = new Item((int)ItemId.Ravenous_Hydra, 300);
            HydraTitanic = new Item((int)ItemId.Titanic_Hydra, Player.Instance.GetAutoAttackRange());
            BladeKing = new Item((int)ItemId.Blade_of_the_Ruined_King, 450);
        }

        public static bool Check(Item x)
        {
            return x.IsOwned();
        }

        public static bool IsReady(Item x)
        {
            return x.IsReady();
        }
    }
}
