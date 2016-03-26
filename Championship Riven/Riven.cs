using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace Championship_Riven
{
    class Riven
    {
        public static int CountQ;
        public static int LastQ;
        public static int LastW;
        public static int LastE;

        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Active R;
        public static Spell.Skillshot R2;
        public static Spell.Skillshot Flash;

        public static Item Hydra;
        public static Item Tiamat;

        public static AIHeroClient FocusTarget;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 275, SkillShotType.Circular, 250, 2200, 100);
            W = new Spell.Active(SpellSlot.W, 260);
            E = new Spell.Skillshot(SpellSlot.E, 310, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R);
            R2 = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Cone, 250, 1600, 125);

            if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("summonerflash"))
            {
                Flash = new Spell.Skillshot(SpellSlot.Summoner1, 500, SkillShotType.Circular);
            }
            else if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("summonerflash"))
            {
                Flash = new Spell.Skillshot(SpellSlot.Summoner2, 500, SkillShotType.Circular);
            }

            Hydra = new Item((int)ItemId.Ravenous_Hydra, 350);
            Tiamat = new Item((int)ItemId.Tiamat, 350);

            DamageIndicator.Initialize(DamageTotal);
            Game.OnUpdate += Game_OnUpdate;
            Game.OnWndProc += Game_OnWndProc;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static bool CheckUlt()
        {
            if (Player.Instance.HasBuff("RivenFengShuiEngine"))
            {
                return true;
            }
            return false;
        }

        /*private static void Burst()
        {
            if(Flash.IsReady())
            {
                if(FocusTarget.IsValidTarget(E.Range + Flash.Range))
                {
                    if(E.IsReady())
                    {
                        Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                    }

                    Flash.Cast(Game.CursorPos);

                    if(W.IsReady() || R.IsReady() || FocusTarget.IsValidTarget(W.Range))
                    {
                        if(CheckUlt())
                        {
                            R.Cast();
                        }
                        Core.DelayAction(() => W.Cast(), 1);
                    }

                    if (CheckUlt())
                    {
                        if (FocusTarget.Health <= RDmg(FocusTarget))
                        {
                            var RPred = R2.GetPrediction(FocusTarget);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                R2.Cast(RPred.UnitPosition);
                            }
                        }
                    }

                }
            }
        }*/

        private static void Combo()
        {
            if (!RivenMenu.CheckBox(RivenMenu.Misc, "BrokenAnimations"))
            {
                if (E.IsReady() || RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo"))
                {
                    if (FocusTarget.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange()))
                    {
                        Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                    }
                }

                if (W.IsReady() || RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo"))
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.ChampionName))
                    {
                        if (FocusTarget.IsValidTarget(W.Range))
                        {
                            W.Cast();
                        }
                    }
                }

                if (R.IsReady() || !CheckUlt())
                {
                    if (FocusTarget.HealthPercent > RivenMenu.Slider(RivenMenu.Combo, "DontR1"))
                    {
                        R.Cast();
                    }
                }
            }
            else
            {
                #region UseR No Animation

                if (RivenMenu.CheckBox(RivenMenu.Combo, "UseRCombo"))
                {
                    if (W.IsReady() || R.IsReady())
                    {
                        if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo") || RivenMenu.CheckBox(RivenMenu.Combo, "UseRCombo"))
                        {
                            if (FocusTarget.IsValidTarget(W.Range))
                            {
                                if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.ChampionName))
                                {
                                    if (!CheckUlt())
                                    {
                                        if (FocusTarget.HealthPercent > RivenMenu.Slider(RivenMenu.Combo, "DontR1"))
                                        {
                                            R.Cast();
                                        }
                                        Core.DelayAction(() => W.Cast(), 1);
                                    }
                                }
                            }
                        }
                    }

                    if (E.IsReady() || R.IsReady())
                    {
                        if (RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo") || RivenMenu.CheckBox(RivenMenu.Combo, "UseRCombo"))
                        {
                            if (FocusTarget.IsValidTarget(E.Range))
                            {
                                if (!CheckUlt())
                                {
                                    if (FocusTarget.HealthPercent > RivenMenu.Slider(RivenMenu.Combo, "DontR1"))
                                    {
                                        R.Cast();
                                    }
                                    Core.DelayAction(() => Player.CastSpell(SpellSlot.E, FocusTarget.Position), 1);
                                }
                            }
                        }
                    }
                }
                #endregion

                if (E.IsReady() || W.IsReady())
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo") || RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo"))
                    {
                        if (FocusTarget.IsValidTarget(E.Range + W.Range))
                        {
                            Player.CastSpell(SpellSlot.E, FocusTarget.Position);

                            if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.ChampionName))
                            {
                                Core.DelayAction(() => W.Cast(), 1);
                            }
                        }
                    }
                }
                else
                {
                    if (E.IsReady())
                    {
                        if(RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo"))
                        {
                            if (FocusTarget.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange()))
                            {
                                Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                            }
                        }
                    }

                    if(W.IsReady())
                    {
                        if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.ChampionName))
                        {
                            if (FocusTarget.IsValidTarget(W.Range))
                            {
                                W.Cast();
                            }
                        }
                    }
                }
            }
        }

        private static void Flee()
        {
            if(Q.IsReady() || RivenMenu.CheckBox(RivenMenu.Flee, "UseQFlee"))
            {
                Q.Cast(Game.CursorPos.Distance(Player.Instance.Position) > Q.Range ? Player.Instance.Position.Extend(Game.CursorPos, Q.Range - 1).To3D() : Game.CursorPos);
            }

            if (E.IsReady() || RivenMenu.CheckBox(RivenMenu.Flee, "UseEFlee"))
            {
                E.Cast(Game.CursorPos.Distance(Player.Instance.Position) > Q.Range ? Player.Instance.Position.Extend(Game.CursorPos, Q.Range - 1).To3D() : Game.CursorPos);
            }
        }

        private static void Laneclear()
        {
            var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValid || x.IsDead).OrderBy(x => x.IsValidTarget(W.Range));
            var Minions = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(Minion, Q.Range, (int)Q.Range);

            if (Minion == null)
                return;

            if(RivenMenu.CheckBox(RivenMenu.Laneclear, "UseWLane"))
            {
                if (Minions.HitNumber >= RivenMenu.Slider(RivenMenu.Laneclear, "UseWLaneMin"))
                {
                    W.Cast();
                }
            }
        }

        private static void Jungleclear()
        {
            var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange()));

            if (Monsters == null)
                return;

            if (RivenMenu.CheckBox(RivenMenu.Jungleclear, "UseWJG"))
            {
                if(Monsters.IsValidTarget(W.Range))
                {
                    W.Cast();
                }
            }

            if (RivenMenu.CheckBox(RivenMenu.Jungleclear, "UseEJG"))
            {
                Player.CastSpell(SpellSlot.E, Monsters.Position);
            }
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != 0x202)
                return;

            FocusTarget = EntityManager.Heroes.Enemies.FindAll(x => x.IsValid || x.Distance(Game.CursorPos) < 3000 || x.IsVisible).OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault();
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (!target.IsEnemy)
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (Q.IsReady() || RivenMenu.CheckBox(RivenMenu.Combo, "UseQCombo"))
                {
                    if (CountQ == 0 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 1 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 2 || !Orbwalker.IsAutoAttacking)
                    {
                        if(target.IsValidTarget(Q.Range + Player.Instance.GetAutoAttackRange()))
                        {
                            Q.Cast(target.Position.Distance(Player.Instance.Position) > Q.Range ? Player.Instance.Position.Extend(target.Position, Q.Range - 1).To3D() : target.Position);
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (Q.IsReady() || RivenMenu.CheckBox(RivenMenu.Laneclear, "UseQLane"))
                {
                    if (CountQ == 0 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 1 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 2 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }
                }
            }


            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (Q.IsReady() || RivenMenu.CheckBox(RivenMenu.Jungleclear, "UseQJG"))
                {
                    if (CountQ == 0 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 1 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }

                    if (CountQ == 2 || !Orbwalker.IsAutoAttacking)
                    {
                        Q.Cast(target.Position);
                    }
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(RivenMenu.CheckBox(RivenMenu.Misc, "Skin"))
            {
                Player.Instance.SetSkinId(RivenMenu.Slider(RivenMenu.Misc, "SkinID"));
            }

            if(Player.Instance.CountEnemiesInRange(W.Range) > RivenMenu.Slider(RivenMenu.Combo, "W/Auto"))
            {
                W.Cast();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();

                if(CheckUlt() || RivenMenu.CheckBox(RivenMenu.Combo, "UseR2Combo"))
                {
                    if (!RivenMenu.CheckBox(RivenMenu.Combo, "R2/" + FocusTarget.BaseSkinName))
                        return;

                    if(FocusTarget.Health <= RDmg(FocusTarget))
                    {
                        var RPred = R2.GetPrediction(FocusTarget);

                        if(RPred.HitChance >= HitChance.High)
                        {
                            R2.Cast(RPred.UnitPosition);
                        }
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Flee();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Laneclear();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Jungleclear();
            }

            /*
            if(RivenMenu.Keybind(RivenMenu.Combo, "BurstFlash"))
            {
                Burst();
            }*/
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if (!sender.IsValidTarget(R2.Range))
                return;

            if(E.IsReady())
            {

                if (SpellSlot.Q == args.Slot)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "E/" + sender.BaseSkinName + "/Q"))
                    {
                        Player.CastSpell(SpellSlot.E, Game.CursorPos);
                    }
                }

                if (SpellSlot.W == args.Slot)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "E/" + sender.BaseSkinName + "/W"))
                    {
                        Player.CastSpell(SpellSlot.E, Game.CursorPos);
                    }
                }

                if (SpellSlot.E == args.Slot)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "E/" + sender.BaseSkinName + "/E"))
                    {
                        Player.CastSpell(SpellSlot.E, Game.CursorPos);
                    }
                }

                if (SpellSlot.R == args.Slot)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "E/" + sender.BaseSkinName + "/R"))
                    {
                        Player.CastSpell(SpellSlot.E, Game.CursorPos);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if(e.End == Player.Instance.Position)
            {
                if(sender.IsValidTarget(W.Range))
                {
                    W.Cast();
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if(sender.IsValidTarget(W.Range))
            {
                W.Cast();
            }
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (!sender.IsMe)
                return;

            switch(args.Animation)
            {
                case "Spell1a":

                    LastQ = Core.GameTickCount;
                    CountQ = 1;

                    break;

                case "Spell1b":

                    LastQ = Core.GameTickCount;
                    CountQ = 2;

                    break;

                case "Spell1c":

                    LastQ = 0;
                    CountQ = 0;

                    break;
            }
        }

        private static float QDmg(AIHeroClient Target)
        {
            if (Target == null)
                return 0f;

            return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, -10 + (Q.Level * 20) + (0.35f + (Q.Level * 0.05f)) * (Player.Instance.FlatPhysicalDamageMod + Player.Instance.BaseAttackDamage));
        }

        private static float WDmg(AIHeroClient Target)
        {
            if (Target == null)
                return 0f;

            return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, new float[] { 50, 80, 110, 150, 170 }[W.Level - 1] + 1 * Player.Instance.FlatPhysicalDamageMod + Player.Instance.BaseAttackDamage);
        }

        private static float RDmg(AIHeroClient Target)
        {
            if (Target == null)
                return 0f;

            var x = (Target.MaxHealth - Target.Health) / Target.MaxHealth > 0.75f ? 0.75f : (Target.MaxHealth - Target.Health) / Target.MaxHealth;
            var y = x * (8 / 3);
            var z = new float[] { 80, 120, 160 }[R.Level - 1] + 0.6f * Player.Instance.FlatPhysicalDamageMod;

            return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, z * (1 + y));
        }

        public static float DamageTotal(AIHeroClient Target)
        {
            float Damage = 0f;
            float Passive = 0f;
            
            if(Player.Instance.Level >= 18)
            {
                Passive = 0.50f;
            }else if(Player.Instance.Level >= 15)
            {
                Passive = 0.45f;
            }else if(Player.Instance.Level >= 12)
            {
                Passive = 0.40f;
            }else if(Player.Instance.Level >= 9)
            {
                Passive = 0.35f;
            }else if(Player.Instance.Level >= 6)
            {
                Passive = 0.30f;
            }else if(Player.Instance.Level >= 3)
            {
                Passive = 0.25f;
            }
            else
            {
                Passive = 0.20f;
            }

            if(Q.IsReady())
            {
                Damage += QDmg(Target);
            }

            if (W.IsReady())
            {
                Damage += WDmg(Target);
            }

            if (R.IsReady())
            {
                Damage += RDmg(Target);
            }

            Damage += Player.Instance.GetAutoAttackDamage(Target) * (1 + Passive);

            return Damage;
        }
    }
}