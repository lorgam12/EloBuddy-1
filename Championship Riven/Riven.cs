using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Championship_Riven
{
    class Riven : Extensions
    {
        public static int CountQ = 0;
        public static Obj_AI_Base TT;
        public static bool TTs;

        public static void LoadModules()
        {
            Menu.Load();
            LoadSpells();
            LoadEvents();
        }

        private static void LoadSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 260, SkillShotType.Circular, 250, 2200, 100);
            W = new Spell.Active(SpellSlot.W, 250);
            E = new Spell.Skillshot(SpellSlot.E, 310, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R);
            R2 = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Cone, 250, 1600, (int)(45 * 0.5));
            Ignite = new Spell.Targeted(Player.Instance.GetSpellSlotFromName("summonerdot"), 550);
            Flash = new Spell.Targeted(Player.Instance.GetSpellSlotFromName("summonerflash"), 425);      
        }

        private static void LoadEvents()
        {
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Menu.CheckBox(Menu.E, "Shield") && !sender.IsAlly)
            {
                var s = sender as AIHeroClient;

                if (E.IsReady() && args.SData.TargettingType == SpellDataTargetType.Unit)
                {
                    if (args.Target.NetworkId == Player.Instance.NetworkId)
                    {
                        if (Menu.CheckBox(Menu.E, s.ChampionName + "/" + args.Slot))
                        {
                            E.Cast(Player.Instance.Position.Extend(Game.CursorPos, E.Range).To3D());
                        }
                    }            
                }
            }
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (sender.IsMe)
            {
                int T = 0;

                switch (args.Animation)
                {
                    case "Spell1a":
                        CountQ = 1;
                        T = 291 + Menu.Slider(Menu.Humanizer, "Q2");
                        break;

                    case "Spell1b":
                        CountQ = 2;
                        T = 291 + Menu.Slider(Menu.Humanizer, "Q3");
                        break;

                    case "Spell1c":
                        CountQ = 3;
                        T = 393;
                        break;

                    case "Spell2":
                        T = 170;
                        TTs = true;
                        break;

                    case "Spell3":
                        break;

                    case "Spell4a":
                        T = 0;
                        break;

                    case "Spell4b":
                        T = 150;
                        break;
                }

                if (T != 0)
                {
                    if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                    {
                        if (Menu.CheckBox(Menu.Humanizer, "Ping"))
                            Core.DelayAction(CancelAnimation, T - Game.Ping);

                        Core.DelayAction(CancelAnimation, T);
                    }
                }
            }
        }

        private static void CancelAnimation() { if (Menu.CheckBox(Menu.Humanizer, "Emotes")) { Player.DoEmote(Emote.Laugh); } Orbwalker.ResetAutoAttack(); }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (target == null) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (Q.IsReady())
                    Q.Cast(Target.Position);
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) && Menu.CheckBox(Menu.Q, "Any"))
            {
                if (Q.IsReady())
                    Q.Cast(target.Position);
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) && Menu.CheckBox(Menu.Q, "Any"))
            {
                if (Q.IsReady())
                    Q.Cast(target.Position);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Ignite.Slot != SpellSlot.Unknown)
            {
                if (Ignite.IsReady() && Menu.CheckBox(Menu.Item, "Ignite"))
                {
                    foreach(var Hero in EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(Ignite.Range)).OrderBy(x => x.Health))
                    {
                        if (Hero.Health <= Player.Instance.GetSummonerSpellDamage(Hero, DamageLibrary.SummonerSpells.Ignite))
                        {
                            Ignite.Cast(Hero);
                        }
                    }
                }
            }

            if (!W.IsReady())
                TTs = false;

            if (TTs)
            {
                foreach (var Hero in EntityManager.Heroes.Enemies.Where(X => X.Position.Distance(Player.Instance.Position) <= Player.Instance.GetAutoAttackRange()).OrderBy(x => x.Health))
                {
                    TT = Hero;
                }

                foreach (var Minions in EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Position.Distance(Player.Instance.Position) <= Player.Instance.GetAutoAttackRange()).OrderBy(x => x.Health))
                {
                    TT = Minions;
                }

                foreach (var Mob in EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.Position.Distance(Player.Instance.Position) <= Player.Instance.GetAutoAttackRange()).OrderBy(x => x.Health))
                {
                    TT = Mob;
                }

                if (TT != null)
                {
                    Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackUnit, TT), 50);
                }
            }

            if (Jungle())
            {
                var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(X => X.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange())).OrderBy(x => x.MaxHealth).FirstOrDefault();

                if (Monsters != null)
                {
                    if (W.IsReady() && Menu.CheckBox(Menu.W, "Jungle"))
                    {
                        if (Monsters.IsValidTarget(RealW()))
                        {
                            W.Cast();
                        }
                    }

                    if (E.IsReady() && Menu.CheckBox(Menu.E, "Jungle"))
                    {
                        if (Monsters.IsValidTarget(E.Range))
                        {
                            E.Cast(Monsters.Position);
                        }
                    }
                }
            }

            if (Lane())
            {
                var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, RealW());
                var Circle = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minions, 200, RealW());

                if (Q.IsReady() && CountQ == 2)
                {
                    Q.Cast(Circle.CastPosition);
                }

                if (Menu.CheckBox(Menu.W, "Lane"))
                {
                    if (Circle.HitNumber >= Menu.Slider(Menu.W, "LaneMin") && W.IsReady())
                    {
                        W.Cast();
                    }
                }
            }

            if (Combo() && Target != null)
            {
                if (Menu.CheckBox(Menu.Item, "Youmuu"))
                {
                    if (Target.IsValidTarget(E.Range))
                    {
                        Use();
                    }
                }

                if (R.IsReady() || R2.IsReady())
                {
                    if (!IsActive && CheckR1(Target))
                    {
                        if (Target.IsValidTarget(RealW(), true) && W.IsReady())
                        {
                            Reset();
                            W.Cast();
                            R.Cast();
                        }
                        else if (Target.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange(), true) && E.IsReady())
                        {
                            E.Cast(Target.Position);
                            R.Cast();
                        }else if (Target.IsValidTarget(RealQ(), true) && Q.IsReady())
                        {
                            Q.Cast(Target.Position);
                            R.Cast();
                        }
                        else
                        {
                            R.Cast();
                        }
                    }
                    else if (CheckR2(Target))
                    {
                        var Pred = R2.GetPrediction(Target);

                        if (Pred.HitChance >= HitChance.High)
                        {
                            if (Target.IsValidTarget(R2.Range))
                            {
                                R2.Cast(Pred.UnitPosition);
                            }
                        }
                    }
                }

                if (Q.IsReady() && CountQ == 2)
                {
                    if (Target.IsValidTarget(RealQ(), true) && !Target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                    {
                        Q.Cast(Target.Position);
                    }

                    if (Target.IsValidTarget(RealW(), true) && W.IsReady())
                    {
                        Reset();
                        W.Cast();
                    }
                }

                if (E.IsReady())
                {
                    if (Target.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange(), true))
                    {
                        if (!Target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                            E.Cast(Target.Position);
                    }

                    if (W.IsReady() && Target.IsValidTarget(RealW()))
                    {
                        Reset();
                        W.Cast();
                    }
                }

                if (W.IsReady() && Target.IsValidTarget(RealW()))
                {
                    Reset();
                    W.Cast();
                }
            }

            if (Menu.Keybind(Menu.Misc, "Burst") && !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                if (Target.IsValidTarget(E.Range + Flash.Range))
                {
                    var Pred = R2.GetPrediction(Target);
                    E.Cast(Player.Instance.Position.Extend(Target.Position, E.Range).To3D());

                    if (!IsActive)
                        R.Cast();
                    
                    if (Flash.IsReady() && Target.Position.Distance(Player.Instance.Position ) <= 650)
                    {
                        Flash.Cast(Player.Instance.Position.Extend(Target.Position, Flash.Range).To3D());
                    }

                    if (W.IsReady() && Target.IsValidTarget(RealW()))
                    {
                        Reset();
                        W.Cast();
                    }

                    if (Q.IsReady())
                    {
                        if (IsActive)
                        {
                            R2.Cast(Target.Position);
                        }

                        Q.Cast(Target.Position);
                    }
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsEnemy && sender != null)
            {
                if (Menu.CheckBox(Menu.Misc, "WInt") && W.IsReady())
                {
                    if (sender.IsValidTarget(RealW()))
                    {
                        Reset();
                        W.Cast();
                    }
                }

                if (Menu.CheckBox(Menu.Misc, "QInt") && Q.IsReady())
                {
                    if (sender.IsValidTarget(RealQ()) && CountQ == 2)
                    {
                        Q.Cast(sender.Position);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender != null)
            {
                if (Menu.CheckBox(Menu.Misc, "WGap") && W.IsReady())
                {
                    if (sender.IsValidTarget(RealW()))
                    {
                        if (W.IsInRange(e.End) || W.IsInRange(e.Start))
                        {
                            Reset();
                            W.Cast();
                        }
                    }
                }

                if (Menu.CheckBox(Menu.Misc, "QGap") && Q.IsReady() && CountQ == 2)
                {
                    if (sender.IsValidTarget(RealQ()))
                    {
                        if (Q.IsInRange(e.End) || Q.IsInRange(e.Start))
                        {
                            Q.Cast(sender.Position);
                        }
                    }
                }
            }
        }

        private static bool CheckR1(Obj_AI_Base T)
        {
            if (Menu.Keybind(Menu.R, "Force"))
            {
                if (Target.IsValidTarget(E.Range))
                    return true;
            }

            return false;
        }

        private static bool CheckR2(Obj_AI_Base T)
        {
            if (Menu.CheckBox(Menu.R2, "Save"))
            {
                if (T.IsValidTarget(Player.Instance.GetAutoAttackRange()) && Player.Instance.GetAutoAttackDamage(T) * 2 >= T.Health)
                {
                    return false;
                }
            }

            if (Menu.ComboBox(Menu.R2, "Mode") == 0)
            {
                if (RDMG(T) - T.Health >= 0)
                {
                    return true;
                }
            }else if (Menu.ComboBox(Menu.R2, "Mode") == 1)
            {
                if (Target.Health / T.MaxHealth <= 0.45)
                {
                    return true;
                }
            }

            return false;
        }
    }
}