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
    class Activator
    {
        public static void Load()
        {
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (sender.IsAlly || !args.Target.IsMe || sender.IsMe)
                return;

            var Enemy = (AIHeroClient)sender;

            foreach (var x in SpellsDatabase.CheckSpells)
            {
                if (Enemy.Hero == x.Champ)
                {
                    if (args.SData.Name == x.Name)
                    {
                        if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Zhonya/" + x.Slot) && ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Seraph/" + x.Slot))
                        {
                            var Delay = (int)x.DelayX * 1000;

                            if (ActivatorItems.IsReady(ActivatorItems.Zhonya) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Zhonya"))
                            {
                                Core.DelayAction(() => ActivatorItems.Zhonya.Cast(), Delay);
                            }
                            else if (ActivatorItems.IsReady(ActivatorItems.Seraph) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Seraph"))
                            {
                                Core.DelayAction(() => ActivatorItems.Seraph.Cast(), Delay);
                            }
                        }
                    }
                    else
                    {
                        if (args.Slot == x.Slot)
                        {
                            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Zhonya/" + x.Slot))
                            {
                                var Delay = (int)x.DelayX * 1000;

                                if (ActivatorItems.IsReady(ActivatorItems.Zhonya))
                                {
                                    Core.DelayAction(() => ActivatorItems.Zhonya.Cast(), Delay);
                                }
                                else if (ActivatorItems.IsReady(ActivatorItems.Seraph))
                                {
                                    Core.DelayAction(() => ActivatorItems.Seraph.Cast(), Delay);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (ActivatorMenu.CheckBox(ActivatorMenu.Misc, "Skin"))
            {
                try
                {
                    Player.Instance.SetSkinId(ActivatorMenu.Slider(ActivatorMenu.Misc, "SkinID"));
                }
                catch (Exception)
                {
                    Chat.Print("Error Skin");
                }
            }

            #region BladeKing
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "BladeKing"))
            {
                try
                {
                    Items.BladeKing(ActivatorItems.IsReady(ActivatorItems.BladeKing));
                }
                catch (Exception)
                {
                    Chat.Print("Error BladeKing");
                }
            }
            #endregion

            #region Youmu
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Youmu"))
            {
                try
                {
                    Items.Youmu(ActivatorItems.IsReady(ActivatorItems.Youmu));
                }
                catch (Exception)
                {
                    Chat.Print("Error Youmu");
                }
            }
            #endregion

            #region Tiamat
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Tiamat"))
            {
                try
                {
                    Items.Tiamat(ActivatorItems.IsReady(ActivatorItems.Tiamat));
                }
                catch (Exception)
                {
                    Chat.Print("Error Tiamat");
                }
            }
            #endregion

            #region Muramana
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Muramana"))
            {
                try
                {
                    Items.Muramana(ActivatorItems.IsReady(ActivatorItems.Muramana));
                }
                catch (Exception)
                {
                    Chat.Print("Error Muramana");
                }
            }
            #endregion

            #region HydraRavenous
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraRavenous"))
            {
                try
                {
                    Items.HydraRavenous(ActivatorItems.IsReady(ActivatorItems.HydraRavenous));
                }
                catch (Exception)
                {
                    Chat.Print("Error HydraRavenous");
                }
            }
            #endregion

            #region HydraTitanic
            if (ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraTitanic"))
            {
                try
                {
                    Items.HydraTitanic(ActivatorItems.IsReady(ActivatorItems.HydraTitanic));
                }
                catch (Exception)
                {
                    Chat.Print("Error HydraTitanic");
                }
            }
            #endregion

            #region Solari
            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Solari"))
            {
                try
                {
                    Items.Solari(ActivatorItems.Solari.Range, ActivatorItems.IsReady(ActivatorItems.Solari));
                }
                catch (Exception)
                {
                    Chat.Print("Error Solari");
                }
            }
            #endregion

            #region FaceMountain
            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "FaceMountain"))
            {
                try
                {
                    Items.FaceMountain(ActivatorItems.FaceMountain.Range, ActivatorItems.IsReady(ActivatorItems.FaceMountain));
                }
                catch (Exception)
                {
                    Chat.Print("Error FaceMountain");
                }
            }
            #endregion

            #region Qss
            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss"))
            {
                try
                {
                    if (!ActivatorItems.Check(ActivatorItems.Qss) || !ActivatorItems.IsReady(ActivatorItems.Qss))
                        return;

                    if (Player.Instance.HasBuffOfType(BuffType.Charm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Charm"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Fear) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Fear"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Silence) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Silence"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Snare) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Snare"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Taunt) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Taunt"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Suppression) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Suppression"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Polymorph) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Polymorph"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Disarm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Disarm"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.NearSight) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-NearSight"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Blind) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Blind"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }

                    if (Player.Instance.HasBuffOfType(BuffType.Sleep) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Sleep"))
                    {
                        ActivatorItems.Qss.Cast(Player.Instance);
                    }
                }
                catch (Exception)
                {
                    Chat.Print("Error Qss");
                }
            }
            #endregion

            #region Mikael
            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael"))
            {
                try
                {
                    if (!ActivatorItems.Check(ActivatorItems.Mikael) || !ActivatorItems.IsReady(ActivatorItems.Mikael))
                        return;

                    foreach (AIHeroClient Ally in EntityManager.Heroes.Allies.Where(z => z.IsValidTarget(ActivatorItems.Mikael.Range) || !z.IsDead || z.IsZombie).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
                    {
                        if (Ally == null)
                            return;

                        if (Ally.HasBuffOfType(BuffType.Charm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Charm"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Fear) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Fear"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Silence) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Silence"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Snare) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Snare"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Taunt) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Taunt"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Suppression) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Suppression"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Polymorph) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Polymorph"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Disarm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Disarm"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.NearSight) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-NearSight"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Blind) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Blind"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }

                        if (Ally.HasBuffOfType(BuffType.Sleep) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Sleep"))
                        {
                            ActivatorItems.Mikael.Cast(Ally);
                        }
                    }
                }
                catch (Exception)
                {
                    Chat.Print("Error Mikael");
                }                
            }
            #endregion
        }
    }
}