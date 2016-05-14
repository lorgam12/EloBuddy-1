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
        public static List<GameObject> Axes = new List<GameObject>();

        public static void Init()
        {
            foreach (var Axe in Axes)
            {
                if (Axe.IsValid || !Axe.IsDead)
                {
                    if (DravenMenu.ComboBox(DravenMenu.Axes, "Mode") == 0)
                    {
                        if (Axe.Distance(Game.CursorPos) <= DravenMenu.Slider(DravenMenu.Axes, "Range"))
                        {
                            if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 0)
                            {
                                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                                {
                                    Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                                }
                            }
                            else if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 1)
                            {
                                Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                            }
                        }
                    }
                    else if (DravenMenu.ComboBox(DravenMenu.Axes, "Mode") == 1)
                    {
                        if (Axe.Distance(Player.Instance.Position) <= DravenMenu.Slider(DravenMenu.Axes, "Range"))
                        {
                            if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 0)
                            {
                                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                                {
                                    Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                                }
                            }
                            else if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 1)
                            {
                                Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                            }
                        }
                    }
                    else
                    {
                        if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 0)
                        {
                            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                            {
                                Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                            }
                        }
                        else if (DravenMenu.ComboBox(DravenMenu.Axes, "Pick") == 1)
                        {
                            Core.DelayAction(() => Orbwalker.MoveTo(Axe.Position), DravenMenu.Slider(DravenMenu.Axes, "Delay"));
                        }
                    }

                    if (Player.Instance.Position.Distance(Axe.Position) == 0)
                    {
                        Orbwalker.DisableMovement = true;
                        Core.DelayAction(() => Orbwalker.DisableMovement = false, 50);
                    }
                }
            }
        }
    }
}