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
        //Defensive

        public static Item Zhonya;
        public static Item Seraph;
        public static Item Solari;
        public static Item FaceMountain;
        public static Item Mikael;
        public static Item Qss;
        public static Item Qss2;

        //Offensive

        public static Item Youmu;
        public static Item Tiamat;
        public static Item Bilgewater;
        public static Item Muramana;
        public static Item HydraRavenous;
        public static Item HydraTitanic;
        public static Item BladeKing;

        //Consumable

        public static Item HealthPotion;
        public static Item Biscuit;
        public static Item RefillablePotion;
        public static Item CorruptingPotion;
        public static Item HuntersPotion;

        public static void Load()
        {
            Zhonya = new Item((int)ItemId.Zhonyas_Hourglass);
            Seraph = new Item((int)ItemId.Seraphs_Embrace);
            Solari = new Item((int)ItemId.Locket_of_the_Iron_Solari, 600);
            FaceMountain = new Item((int)ItemId.Face_of_the_Mountain, 700);
            Mikael = new Item((int)ItemId.Mikaels_Crucible, 750);
            Qss = new Item((int)ItemId.Quicksilver_Sash);
            Qss2 = new Item((int)ItemId.Mercurial_Scimitar);

            Youmu = new Item((int)ItemId.Youmuus_Ghostblade);
            Tiamat = new Item((int)ItemId.Tiamat, 300);
            Muramana = new Item((int)ItemId.Muramana, Player.Instance.GetAutoAttackRange());
            HydraRavenous = new Item((int)ItemId.Ravenous_Hydra, 300);
            HydraTitanic = new Item((int)ItemId.Titanic_Hydra, Player.Instance.GetAutoAttackRange());
            BladeKing = new Item((int)ItemId.Blade_of_the_Ruined_King, 450);

            HealthPotion = new Item((int)ItemId.Health_Potion);
            Biscuit = new Item((int)ItemId.Total_Biscuit_of_Rejuvenation);
            RefillablePotion = new Item((int)ItemId.Refillable_Potion);
            CorruptingPotion = new Item((int)ItemId.Corrupting_Potion);
            HuntersPotion = new Item((int)ItemId.Hunters_Potion);
        }

        public static void UseYoumu()
        {
            if (!Youmu.IsReady() || !Youmu.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Youmu"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T != null)
                {
                    if (T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Youmu/01") || T.IsValidTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck")))
                    {
                        Youmu.Cast();
                    }
                }
            }
        }

        public static void UseTiamat()
        {
            if (!Tiamat.IsReady() || !Tiamat.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Tiamat"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(Tiamat.Range) || !x.IsEnemy);

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, Tiamat.Range, (int)Tiamat.Range);

                if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Tiamat/02"))
                {
                    Tiamat.Cast();
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T.IsValidTarget(Tiamat.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Tiamat/01"))
                {
                    Tiamat.Cast();
                }
            }
        }

        public static void UseMuramana()
        {
            if (!Muramana.IsReady() || !Muramana.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Muramana"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Muramana/01"))
                {
                    if (Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck")) > ActivatorMenu.Slider(ActivatorMenu.Offensive, "Muramana0x03"))
                    {
                        Muramana.Cast();
                    }
                }
            }
        }

        public static void UseHydraRavenous()
        {
            if (!HydraRavenous.IsReady() || !HydraRavenous.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraRavenous"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(HydraRavenous.Range) || x.IsEnemy);

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, HydraRavenous.Range, (int)HydraRavenous.Range);

                if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraRavenous/02"))
                {
                    HydraRavenous.Cast();
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T.IsValidTarget(HydraRavenous.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraRavenous/01"))
                {
                    HydraRavenous.Cast();
                }
            }
        }

        public static void UseHydraTitanic()
        {
            if (!HydraTitanic.IsReady() || !HydraTitanic.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraTitanic"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValidTarget(HydraTitanic.Range) || !x.IsEnemy);

                if (Minion == null)
                    return;

                var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, HydraTitanic.Range, (int)HydraTitanic.Range);

                if (Minions.HitNumber >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraTitanic/02"))
                {
                    HydraTitanic.Cast();
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T.IsValidTarget(HydraTitanic.Range) || T.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraTitanic/01"))
                {
                    HydraTitanic.Cast();
                }
            }
        }

        public static void UseBladeKing()
        {
            if (!BladeKing.IsReady() || !BladeKing.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "BladeKing"))
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var T = TargetSelector.GetTarget(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck"), DamageType.Mixed);

                if (T == null)
                    return;

                if (T.IsValidTarget(BladeKing.Range) || T.Health >= ActivatorMenu.Slider(ActivatorMenu.Offensive, "BladeKing/01"))
                {
                    BladeKing.Cast(T);                    
                }
            }
        }

        public static void UseFaceMountain()
        {
            if (!FaceMountain.IsReady() || !FaceMountain.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "FaceMountain"))
                return;

            if (Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck")) < ActivatorMenu.Slider(ActivatorMenu.Defensive, "FaceMountain0x03"))
            {
                foreach (AIHeroClient Ally in EntityManager.Heroes.Allies.Where(x => x.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Defensive, "FaceMountain/01") || x.IsValidTarget(FaceMountain.Range)).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
                {
                    if (Ally == null)
                        return;

                    if (!Ally.IsRecalling())
                    {
                        FaceMountain.Cast(Ally);
                    }
                }
            }
        }

        public static void UseSolari()
        {
            if (!Solari.IsReady() || !Solari.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Solari"))
                return;

            if (Player.Instance.CountEnemiesInRange(ActivatorMenu.Slider(ActivatorMenu.Principal, "RangeCheck")) < ActivatorMenu.Slider(ActivatorMenu.Defensive, "Solari/03"))
            {
                foreach (AIHeroClient Ally in EntityManager.Heroes.Allies.Where(x => x.HealthPercent >= ActivatorMenu.Slider(ActivatorMenu.Defensive, "Solari/01") || x.IsValidTarget(Solari.Range)).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
                {
                    if (Ally == null)
                        return;

                    if (!Ally.IsRecalling())
                    {
                        Solari.Cast();
                    }
                }
            }
        }

        public static void UseMikael()
        {
            if (!Mikael.IsReady() || !Mikael.IsOwned())
                return;

            foreach (AIHeroClient Ally in EntityManager.Heroes.Allies.Where(z => z.IsValidTarget(Mikael.Range) || !z.IsDead || z.IsZombie).OrderByDescending(x => ActivatorMenu.Slider(ActivatorMenu.Priority, x.ChampionName)))
            {
                if (Ally != null || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael"))
                {
                    if (Ally.HasBuffOfType(BuffType.Charm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Charm"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Fear) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Fear"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Silence) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Silence"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Snare) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Snare"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Taunt) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Taunt"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Suppression) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Suppression"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Polymorph) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Polymorph"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Disarm) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Disarm"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.NearSight) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-NearSight"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Blind) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Blind"))
                    {
                        Mikael.Cast(Ally);
                    }

                    if (Ally.HasBuffOfType(BuffType.Sleep) || ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Mikael-Sleep"))
                    {
                        Mikael.Cast(Ally);
                    }
                }
            }
        }

        public static void UseQss()
        {
            if (!Qss.IsReady() || !Qss.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss"))
                return;

            if (Player.Instance.HasBuffOfType(BuffType.Charm))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Charm"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Fear))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Fear"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Silence))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Silence"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Snare))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Snare"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Taunt))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Taunt"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Suppression))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Suppression"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Polymorph))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Polymorph"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Disarm))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Disarm"))
                    return;

                Qss.Cast();
            }else if (Player.Instance.HasBuffOfType(BuffType.Blind))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Blind"))
                    return; 

                Qss.Cast();
            }
        }

        public static void UseQss2()
        {
            if (!Qss2.IsReady() || !Qss2.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss"))
                return;

            if (Player.Instance.HasBuffOfType(BuffType.Charm))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Charm"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Fear))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Fear"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Silence))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Silence"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Snare))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Snare"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Taunt))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Taunt"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Suppression))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Suppression"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Polymorph))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Polymorph"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Disarm))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Disarm"))
                    return;

                Qss2.Cast();
            }
            else if (Player.Instance.HasBuffOfType(BuffType.Blind))
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Qss-Blind"))
                    return;

                Qss2.Cast();
            }
        }

        public static void UseHealthPotion()
        {
            if (!HealthPotion.IsReady() || !HealthPotion.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Consumable, "HealthPotion"))
                return;

            if(Player.Instance.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Consumable, "HealthPotion/01"))
            {
                if(!Player.Instance.HasBuff("RegenerationPotion"))
                {
                    HealthPotion.Cast();
                }
            }
        }

        public static void UseBiscuit()
        {
            if (!Biscuit.IsReady() || !Biscuit.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Consumable, "Biscuit"))
                return;

            if (Player.Instance.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Consumable, "Biscuit/01"))
            {
                if (!Player.Instance.HasBuff("ItemMiniRegenPotion"))
                {
                    Biscuit.Cast();
                }
            }
        }

        public static void UseRefillablePotion()
        {
            if (!RefillablePotion.IsReady() || !RefillablePotion.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Consumable, "RefillablePotion"))
                return;

            if (Player.Instance.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Consumable, "RefillablePotion/01"))
            {
                if (!Player.Instance.HasBuff("ItemCrystalFlask"))
                {
                    RefillablePotion.Cast();
                }
            }
        }

        public static void UseHuntersPotion()
        {
            if (!HuntersPotion.IsReady() || !HuntersPotion.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Consumable, "HuntersPotion"))
                return;

            if (Player.Instance.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Consumable, "HuntersPotion/01"))
            {
                if (!Player.Instance.HasBuff("ItemCrystalFlaskJungle"))
                {
                    HuntersPotion.Cast();
                }
            }
        }

        public static void UseCorruptingPotion()
        {
            if (!CorruptingPotion.IsReady() || !CorruptingPotion.IsOwned())
                return;

            if (!ActivatorMenu.CheckBox(ActivatorMenu.Consumable, "CorruptingPotion"))
                return;

            if (Player.Instance.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Consumable, "CorruptingPotion/01"))
            {
                if (!Player.Instance.HasBuff("ItemDarkCrystalFlask"))
                {
                    CorruptingPotion.Cast();
                }
            }
        }
    }
}
