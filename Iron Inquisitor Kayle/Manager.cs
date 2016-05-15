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

namespace Iron_Inquisitor_Kayle
{
    class Manager
    {
        public static void Init()
        {
            foreach (var Ally in EntityManager.Heroes.Allies.OrderByDescending(x => x.HealthPercent))
            {
                if (KayleMenu.ComboBox(KayleMenu.Manage, "Order") == 0)
                {
                    if (Kayle.W.IsReady())
                    {
                        if (Player.Instance.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinW"))
                        {

                            Kayle.W.Cast(Player.Instance);
                        }
                        else
                        {
                            if (Ally.Hero != Champion.Kayle)
                            {
                                if (Ally.Position.Distance(Player.Instance) <= Kayle.W.Range)
                                {
                                    if (KayleMenu.CheckBox(KayleMenu.Manage, Ally.ChampionName + "/W"))
                                    {

                                        if (Ally.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinWAlly"))
                                        {
                                            Kayle.W.Cast(Ally);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Kayle.R.IsReady())
                    {
                        if (Player.Instance.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinR"))
                        {
                            Kayle.R.Cast(Player.Instance);
                        }
                        else
                        {
                            if (Ally.Hero != Champion.Kayle)
                            {
                                if (Ally.Position.Distance(Player.Instance) <= Kayle.R.Range)
                                {
                                    if (KayleMenu.CheckBox(KayleMenu.Manage, Ally.ChampionName + "/R"))
                                    {
                                        if (Ally.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinRAlly"))
                                        {
                                            Kayle.R.Cast(Ally);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }else
                {
                    if (Kayle.W.IsReady())
                    {
                        if (KayleMenu.CheckBox(KayleMenu.Manage, Ally.ChampionName + "/W"))
                        {
                            if (Ally.Position.Distance(Player.Instance) <= Kayle.W.Range)
                            {
                                if (Ally.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinWAlly"))
                                {
                                    if (!Ally.IsRecalling())
                                    {
                                        Kayle.W.Cast(Ally);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Player.Instance.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinW"))
                            {
                                if (!Player.Instance.IsRecalling())
                                {
                                    Kayle.W.Cast(Player.Instance);
                                }
                            }
                        }
                    }

                    if (Kayle.R.IsReady())
                    {
                        if (KayleMenu.CheckBox(KayleMenu.Manage, Ally.ChampionName + "/R"))
                        {
                            if (Ally.Position.Distance(Player.Instance) <= Kayle.R.Range)
                            {
                                if (Ally.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinRAlly"))
                                {
                                    if (!Ally.IsRecalling())
                                    {
                                        Kayle.R.Cast(Ally);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Player.Instance.HealthPercent <= KayleMenu.Slider(KayleMenu.Manage, "MinR"))
                            {
                                if (!Ally.IsRecalling())
                                {
                                    Kayle.R.Cast(Player.Instance);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
