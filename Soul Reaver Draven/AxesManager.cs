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

namespace Soul_Reaver_Draven
{
    class AxesManager
    {
        public static List<AxesC> Axes = new List<AxesC>();

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Equals("Draven_Base_Q_reticle_self.troy") && !sender.IsDead)
            {
                Axes.Add(new AxesC { Axe = sender, Expire = Game.Time + 1800});
                Core.DelayAction(() => AxesManager.Axes.RemoveAll(x => x.Axe.NetworkId == sender.NetworkId), 1800);
            }
        }

        public static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name.Equals("Draven_Base_Q_reticle_self.troy"))
            {
                Axes.RemoveAll(x => x.Axe.NetworkId == sender.NetworkId);
            }
        }

        public static void Init()
        {
            switch (DravenMenu.ComboBox(DravenMenu.Axes, "Mode"))
            {
                case 0:

                    var Axe1 = Axes.Where(x => x.Axe.Distance(Game.CursorPos) <= DravenMenu.Slider(DravenMenu.Axes, "Range")).OrderBy(x => x.Axe.Position.Distance(Player.Instance.ServerPosition)).ThenBy(x => x.Axe.Distance(Game.CursorPos)).ThenBy(x => x.Expire).FirstOrDefault();

                    if (Axe1 != null && Axe1.Axe.Position.Distance(Player.Instance.ServerPosition) > 80)
                    {
                        if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 0)
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Core.DelayAction(() => Orbwalker.MoveTo(Axe1.Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                            }

                        }
                        else if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 1)
                        {
                            Core.DelayAction(() => Orbwalker.MoveTo(Axe1.Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                        }
                    }

                    break;

                case 1:

                    var Axe2 = Axes.Where(x => x.Axe.Distance(Player.Instance.ServerPosition) <= DravenMenu.Slider(DravenMenu.Axes, "Range")).OrderBy(x => x.Axe.Position.Distance(Player.Instance.ServerPosition)).ThenBy(x => x.Axe.Distance(Game.CursorPos)).ThenBy(x => x.Expire).FirstOrDefault();

                    if (Axe2 != null && Axe2.Axe.Position.Distance(Player.Instance.ServerPosition) > 80)
                    {
                        if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 0)
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Core.DelayAction(() => Orbwalker.MoveTo(Axe2.Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                            }

                        }
                        else if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 1)
                        {
                            Core.DelayAction(() => Orbwalker.MoveTo(Axe2.Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                        }
                    }

                    break;
            }
        }

        public class AxesC
        {
            public GameObject Axe;
            public float Expire;

            public AxesC()
            {

            }

            public AxesC(GameObject axe, float expire)
            {
                Axe = axe;
                Expire = expire;
            }
        }
    }
}