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
    class Items
    {
        #region Solari
        public static void Solari(float Range, bool Ready)
        {
            if(ActivatorItems.Check(ActivatorItems.Solari))
            {
                if(Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck")) < ActivatorMenu.Slider(ActivatorMenu.Defensive, "Solari0x03"))
                {
                    foreach(AIHeroClient Ally in EntityManager.Heroes.Allies.Where(x => x.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Defensive, "Solari0x01") || x.IsValidTarget(ActivatorItems.Solari.Range)).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
                    {
                        if (Ally == null)
                            return;

                        if(!Ally.IsRecalling())
                        {
                            ActivatorItems.Solari.Cast();
                        }
                    }
                }
            }
        }
        #endregion

        #region FaceMountain
        public static void FaceMountain(float Range, bool Ready)
        {
            if(ActivatorItems.Check(ActivatorItems.FaceMountain))
            {
                if (Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck")) < ActivatorMenu.Slider(ActivatorMenu.Defensive, "FaceMountain0x03"))
                {
                    foreach (AIHeroClient Ally in EntityManager.Heroes.Allies.Where(x => x.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Defensive, "FaceMountain0x01") || x.IsValidTarget(ActivatorItems.Solari.Range)).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
                    {
                        if (Ally == null)
                            return;

                        if (!Ally.IsRecalling())
                        {
                            ActivatorItems.FaceMountain.Cast(Ally);
                        }
                    }
                }
            }
        }
        #endregion

        #region Youmu
        public static void Youmu(bool Ready)
        {
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

                if (T == null)
                    return;

                if (Ready == true)
                {
                    if (T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Youmu0x01") || T.IsValidTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck")))
                    {
                        ActivatorItems.Youmu.Cast();
                    }
                }
            }
        }
        #endregion

        #region Tiamat
        public static void Tiamat(bool Ready)
        {
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(ActivatorItems.Tiamat.Range) || !x.IsEnemy);

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, ActivatorItems.Tiamat.Range, (int)ActivatorItems.Tiamat.Range);

                if (Ready == true)
                {
                    if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Tiamat0x02"))
                    {
                        ActivatorItems.Tiamat.Cast();
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

                if (T.IsValidTarget(ActivatorItems.Tiamat.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Tiamat0x01"))
                {
                    if (Ready == true)
                    {
                        ActivatorItems.Tiamat.Cast();
                    }
                }
            }
        }
#endregion

        #region Muramana
        public static void Muramana(bool Ready)
        {
            var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

            if(T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Muramana0x01"))
            {
                if (Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck")) > ActivatorMenu.Slider(ActivatorMenu.Offensive, "Muramana0x03"))
                {
                    if(Ready == true)
                    {
                        ActivatorItems.Muramana.Cast();
                    }
                }
            }
        }
        #endregion

        #region HydraRavenous
        public static void HydraRavenous(bool Ready)
        {
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(ActivatorItems.HydraRavenous.Range) || x.IsEnemy) ;

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, ActivatorItems.HydraRavenous.Range, (int)ActivatorItems.HydraRavenous.Range);

                if (Ready == true)
                {
                    if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraRavenous0x02"))
                    {
                        ActivatorItems.HydraRavenous.Cast();
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

                if(T.IsValidTarget(ActivatorItems.HydraRavenous.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraRavenous0x01"))
                {
                    if(Ready == true)
                    {
                        ActivatorItems.HydraRavenous.Cast();
                    }
                }
            }
        }
        #endregion

        #region HydraTitanic
        public static void HydraTitanic(bool Ready)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(ActivatorItems.HydraTitanic.Range) || !x.IsEnemy);

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, ActivatorItems.HydraTitanic.Range, (int)ActivatorItems.HydraTitanic.Range);

                if (Ready == true)
                {
                    if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraTitanic0x02"))
                    {
                        ActivatorItems.HydraTitanic.Cast();
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

                if (T.IsValidTarget(ActivatorItems.HydraTitanic.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraTitanic0x01"))
                {
                    if (Ready == true)
                    {
                        ActivatorItems.HydraTitanic.Cast();
                    }
                }
            }
        }
        #endregion

        #region BladeKing
        public static void BladeKing(bool Ready)
        {
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Misc, "RangeCheck"), DamageType.Mixed);

                if (T == null)
                    return;

                if (T.IsValidTarget(ActivatorItems.BladeKing.Range) || T.Health >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "BladeKing0x01"))
                {
                    if (Ready == true)
                    {
                        ActivatorItems.BladeKing.Cast(T);
                    }
                }
            }
        }
        #endregion
    }
}
