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

namespace Hextech_Annie
{
    class Tibbers
    {
        public static Obj_AI_Base Tibber;

        public static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.IsAlly)
            {
                if (sender.Name.ToLower().Equals("tibbers"))
                {
                    Tibber = null;
                }
            }
        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if (sender.Name.ToLower().Equals("tibbers"))
                {
                    Tibber = (Obj_AI_Base)sender;
                }
            }
        }

        public static void Init()
        {
            if(Has())
            {
                switch(AnnieMenu.ComboBox(AnnieMenu.Combo, "Mode"))
                {
                    case 0:

                        foreach(var Enemy in EntityManager.Heroes.Enemies.Where(x => x.Distance(Tibber.Position) <= 1100 && x.IsValid && !x.IsDead))
                        {
                            Player.IssueOrder(GameObjectOrder.MovePet, Enemy);
                        }

                        break;

                    case 1:

                        foreach(var Enemy in EntityManager.Heroes.Enemies.Where(x => x.Distance(Tibber.Position) <= 1100 && x.IsValid && !x.IsDead).OrderBy(x => x.Health))
                        {
                            Player.IssueOrder(GameObjectOrder.MovePet, Enemy);
                        }

                        break;
                }
            }
        }

        public static bool Has()
        {
            if (Player.Instance.HasBuff("infernalguardiantime"))
                return true;

            return false;
        }
    }
}
