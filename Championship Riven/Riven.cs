using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using SharpDX;

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
        public static Spell.Targeted Flash;

        public static Item Hydra;
        public static Item Tiamat;
        public static Item Youmuu;

        public static AIHeroClient FocusTarget;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 275, SkillShotType.Circular, 250, 2200, 100);
            W = new Spell.Active(SpellSlot.W, 250);
            E = new Spell.Skillshot(SpellSlot.E, 310, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R);
            R2 = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Cone, 250, 1600, 125);

            if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name == "SummonerFlash")
            {
                Flash = new Spell.Targeted(SpellSlot.Summoner1, 425);
            }
            else if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name == "SummonerFlash")
            {
                Flash = new Spell.Targeted(SpellSlot.Summoner2, 425);
            }

            Hydra = new Item((int)ItemId.Ravenous_Hydra, 350);
            Tiamat = new Item((int)ItemId.Tiamat, 350);
            Youmuu = new Item((int)ItemId.Youmuus_Ghostblade, 0);

            DamageIndicator.Initialize(DamageTotal);
            Game.OnUpdate += Game_OnUpdate;
            Game.OnWndProc += Game_OnWndProc;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static bool HasHydra()
        {
            if (!RivenMenu.CheckBox(RivenMenu.Combo, "UseHydra"))
                return false;

            if (Hydra.IsOwned() || Hydra.IsReady())
            {
                return true;
            }

            return false;
        }

        private static bool HasTiamat()
        {
            if (!RivenMenu.CheckBox(RivenMenu.Combo, "UseTiamat"))
                return false;

            if (Tiamat.IsOwned() || Tiamat.IsReady())
            {
                return true;
            }

            return false;
        }

        private static bool CheckUlt()
        {
            if (Player.Instance.HasBuff("RivenFengShuiEngine"))
            {
                return true;
            }
            return false;
        }

        private static void Burst()
        {
            if(RivenMenu.ComboBox(RivenMenu.Burst, "BurstType") == 0)
            {
                if (DamageTotal(FocusTarget) >= FocusTarget.Health)
                {
                    if (FocusTarget.Distance(Player.Instance.Position) < Flash.Range + W.Range)
                    {
                        if (W.IsReady() || R.IsReady() || E.IsReady())
                        {
                            E.Cast(FocusTarget.Position);

                            if (Flash.IsReady())
                            {
                                Flash.Cast(FocusTarget.Position);
                            }

                            if (FocusTarget.IsValidTarget(W.Range))
                            {
                                R.Cast();
                                W.Cast();

                                if (HasHydra())
                                {
                                    Hydra.Cast();
                                }
                                else if (HasTiamat())
                                {
                                    Tiamat.Cast();
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private static void Flee()
        {
            if(RivenMenu.CheckBox(RivenMenu.Flee, "UseQFlee"))
            {
                Q.Cast((Game.CursorPos.Distance(Player.Instance) > Q.Range ? Player.Instance.Position.Extend(Game.CursorPos, Q.Range - 1).To3D() : Game.CursorPos));
            }

            if(RivenMenu.CheckBox(RivenMenu.Flee, "UseEFlee"))
            {
                E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos));
            }
        }

        private static void Combo()
        {
            if (!R.IsReady())
            {
                if (RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo") || E.IsReady())
                {
                    if (FocusTarget.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange()))
                    {
                        Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                    }
                }

                if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo") || W.IsReady())
                {
                    if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.BaseSkinName) || FocusTarget.IsValidTarget(W.Range))
                    {
                        W.Cast();

                        if (HasHydra())
                        {
                            Hydra.Cast();
                        }
                        else if (HasTiamat())
                        {
                            Tiamat.Cast();
                        }
                    }
                }
            }
            else
            {
                if (Player.Instance.HealthPercent >= RivenMenu.Slider(RivenMenu.Combo, "DontR1"))
                {
                    if (CheckUlt())
                        return;

                    if (RivenMenu.ComboBox(RivenMenu.Combo, "UseRType") == 1)
                    {
                        if (DamageTotal(FocusTarget) >= FocusTarget.Health)
                        {
                            if (RivenMenu.CheckBox(RivenMenu.Misc, "BrokenAnimations"))
                            {
                                if (FocusTarget.IsValidTarget(W.Range))
                                {
                                    if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo"))
                                    {
                                        if (W.IsReady())
                                        {
                                            if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.BaseSkinName))
                                            {
                                                R.Cast();
                                                W.Cast();
                                            }
                                        }
                                    }
                                }
                                else if (FocusTarget.IsValidTarget(E.Range))
                                {
                                    if (RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo"))
                                    {
                                        if (E.IsReady())
                                        {
                                            R.Cast();
                                            Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                                        }
                                    }
                                }
                                else if (CountQ == 2)
                                {
                                    if (RivenMenu.CheckBox(RivenMenu.Combo, "UseQCombo"))
                                    {
                                        if (Q.IsReady())
                                        {
                                            Q.Cast(FocusTarget.Position);
                                            R.Cast();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                R.Cast();
                            }
                        }
                    }
                    else if (RivenMenu.ComboBox(RivenMenu.Combo, "UseRType") == 0)
                    {
                        if (FocusTarget.HealthPercent <= 40)
                        {
                            if (RivenMenu.CheckBox(RivenMenu.Misc, "BrokenAnimations"))
                            {
                                if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo") || W.IsReady())
                                {
                                    if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.BaseSkinName) || FocusTarget.IsValidTarget(W.Range))
                                    {
                                        R.Cast();
                                        W.Cast();
                                    }
                                }
                                else if (RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo") || E.IsReady())
                                {
                                    if (FocusTarget.IsValidTarget(E.Range))
                                    {
                                        R.Cast();
                                        Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                                    }
                                }
                            }
                            else
                            {
                                R.Cast();
                            }
                        }
                    }
                    else
                    {
                        if (RivenMenu.CheckBox(RivenMenu.Misc, "BrokenAnimations"))
                        {
                            if (RivenMenu.CheckBox(RivenMenu.Combo, "UseWCombo") || W.IsReady())
                            {
                                if (RivenMenu.CheckBox(RivenMenu.Combo, "W/" + FocusTarget.BaseSkinName) || FocusTarget.IsValidTarget(W.Range))
                                {
                                    R.Cast();
                                    W.Cast();
                                }
                            }
                            else if (RivenMenu.CheckBox(RivenMenu.Combo, "UseECombo") || E.IsReady())
                            {
                                if (FocusTarget.IsValidTarget(E.Range))
                                {
                                    R.Cast();
                                    Player.CastSpell(SpellSlot.E, FocusTarget.Position);
                                }
                            }
                            else
                            {
                                R.Cast();
                            }
                        }
                    }
                }
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

                    if (HasTiamat())
                    {
                        Tiamat.Cast();
                    }

                    if (HasHydra())
                    {
                        Hydra.Cast();
                    }
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

                    if (HasTiamat())
                    {
                        Tiamat.Cast();
                    }

                    if (HasHydra())
                    {
                        Hydra.Cast();
                    }
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

            FocusTarget = EntityManager.Heroes.Enemies.FindAll(x => !x.IsDead || x.IsValid || x.Distance(Game.CursorPos) < 3000 || x.IsVisible || !x.IsDead).OrderBy(x => x.Distance(Game.CursorPos)).FirstOrDefault();
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (RivenMenu.CheckBox(RivenMenu.Combo, "UseQCombo"))
                {
                    if (Q.IsReady())
                    {
                        if (CountQ == 0 || !Orbwalker.IsAutoAttacking)
                        {
                            if (Youmuu.IsOwned() || Youmuu.IsReady() || RivenMenu.CheckBox(RivenMenu.Combo, "UseYoumuu"))
                            {
                                Youmuu.Cast();
                            }

                            if (Player.Instance.Distance(target) < Q.Range + Player.Instance.GetAutoAttackRange())
                            {
                                Q.Cast(target.Position);
                            }
                        }

                        if (CountQ == 1 || !Orbwalker.IsAutoAttacking)
                        {
                            if (Player.Instance.Distance(target) < Q.Range + Player.Instance.GetAutoAttackRange())
                            {
                                Q.Cast(target.Position);
                            }
                        }

                        if (CountQ == 2 || !Orbwalker.IsAutoAttacking)
                        {
                            if (Player.Instance.Distance(FocusTarget) < Q.Range + Player.Instance.GetAutoAttackRange())
                            {
                                Q.Cast(target.Position);
                            }
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (RivenMenu.CheckBox(RivenMenu.Laneclear, "UseQLane"))
                {
                    if (Q.IsReady())
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

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (RivenMenu.CheckBox(RivenMenu.Jungleclear, "UseQJG"))
                {
                    if (Q.IsReady())
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
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(RivenMenu.CheckBox(RivenMenu.Misc, "Skin"))
            {
                Player.Instance.SetSkinId(RivenMenu.Slider(RivenMenu.Misc, "SkinID"));
            }

            if(Player.Instance.CountEnemiesInRange(W.Range) >= RivenMenu.Slider(RivenMenu.Combo, "W/Auto"))
            {
                W.Cast();

                if (HasTiamat())
                {
                    Tiamat.Cast();
                }

                if (HasHydra())
                {
                    Hydra.Cast();
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (FocusTarget == null || FocusTarget.Health == 0)
                    return;


                if (RivenMenu.Keybind(RivenMenu.Burst, "BurstAllowed"))
                {
                    Burst();

                    if (CheckUlt())
                    {
                        var RPred = R2.GetPrediction(FocusTarget);

                        if (RPred.HitChance >= HitChance.High)
                        {
                            if (FocusTarget.Distance(Player.Instance.ServerPosition) >= 60)
                            {
                                R2.Cast(RPred.UnitPosition);
                            }
                        }
                    }
                }
                else
                {
                    Combo();
                }

                if (RivenMenu.ComboBox(RivenMenu.Combo, "UseR2Type") == 0)
                {
                    if (!CheckUlt())
                        return;

                    if (FocusTarget.IsValidTarget(R2.Range))
                    {
                        if (RDamage(FocusTarget, FocusTarget.Health) >= FocusTarget.Health)
                        {
                            var RPred = R2.GetPrediction(FocusTarget);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                if (FocusTarget.Distance(Player.Instance.Position) >= 60)
                                {
                                    R2.Cast(RPred.UnitPosition);
                                }
                            }
                        }
                    }
                }
                else if (RivenMenu.ComboBox(RivenMenu.Combo, "UseR2Type") == 1)
                {
                    if (!CheckUlt())
                        return;

                    if (FocusTarget.IsValidTarget(R2.Range))
                    {
                        var RPred = R2.GetPrediction(FocusTarget);

                        if (FocusTarget.Distance(Player.Instance.Position) >= 60)
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
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            var EPos = Player.Instance.ServerPosition + (Player.Instance.ServerPosition - sender.ServerPosition).Normalized() * 300;

            if(Player.Instance.Distance(sender.ServerPosition) <= args.SData.CastRange)
            {
                if(args.Slot == SpellSlot.Q)
                {
                    if(RivenMenu.CheckBox(RivenMenu.Shield, "E/" + sender.BaseSkinName + "/Q"))
                    {
                        E.Cast(EPos);
                    }
                }

                if(args.Slot == SpellSlot.W)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Shield, "E/" + sender.BaseSkinName + "/W"))
                    {
                        E.Cast(EPos);
                    }
                }

                if(args.Slot == SpellSlot.E)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Shield, "E/" + sender.BaseSkinName + "/E"))
                    {
                        E.Cast(EPos);
                    }
                }

                if(args.Slot == SpellSlot.R)
                {
                    if (RivenMenu.CheckBox(RivenMenu.Shield, "E/" + sender.BaseSkinName + "/R"))
                    {
                        E.Cast(EPos);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if (!RivenMenu.CheckBox(RivenMenu.Misc, "Gapcloser"))
                return;

            if(RivenMenu.CheckBox(RivenMenu.Misc, "GapcloserW"))
            {
                if (sender.IsValidTarget(W.Range))
                {
                    if(W.IsReady())
                    {
                        W.Cast();
                    }
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if (!RivenMenu.CheckBox(RivenMenu.Misc, "Interrupter"))
                return;

            if(RivenMenu.CheckBox(RivenMenu.Misc, "InterrupterW"))
            {
                if(sender.IsValidTarget(W.Range))
                {
                    W.Cast();
                }
            }
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (!sender.IsMe)
                return;

            var T = 0;

            switch(args.Animation)
            {
                case "Spell1a":

                    LastQ = Core.GameTickCount;
                    CountQ = 1;
                    T = 291;

                    break;

                case "Spell1b":

                    LastQ = Core.GameTickCount;
                    CountQ = 2;
                    T = 291;

                    break;

                case "Spell1c":

                    LastQ = 0;
                    CountQ = 0;
                    T = 393;

                    break;

                case "Spell2":
                    T = 170;

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

            if(T != 0 || !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None))
            {
                Orbwalker.ResetAutoAttack();
                Core.DelayAction(CancelAnimation, T - Game.Ping);
            }
        }

        private static void CancelAnimation()
        {
            Player.DoEmote(Emote.Dance);
            Orbwalker.ResetAutoAttack();
        }

        public static float DamageTotal(AIHeroClient target)
        {
            double dmg = 0;
            var passiveStacks = 0;

            dmg += Q.IsReady()
                ? QDamage(!CheckUlt()) * (3 - CountQ)
                : 0;
            passiveStacks += Q.IsReady()
                ? (3 - CountQ)
                : 0;

            dmg += W.IsReady()
                ? WDamage()
                : 0;
            passiveStacks += W.IsReady()
                ? 1
                : 0;
            passiveStacks += E.IsReady()
                ? 1
                : 0;

            dmg += PassiveDamage() * passiveStacks;
            dmg += (R.IsReady() && !CheckUlt() && !Player.Instance.HasBuff("RivenFengShuiEngine")
                ? Player.Instance.TotalAttackDamage * 1.2
                : Player.Instance.TotalAttackDamage) * passiveStacks;

            if (dmg < 10)
            {
                return 3 * Player.Instance.TotalAttackDamage;
            }

            dmg += R.IsReady() && !CheckUlt()
                ? RDamage(target, Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, (float)dmg))
                : 0;
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, (float)dmg);
        }

        public static float QDamage(bool useR = false)
        {
            return (float)(new double[] { 10, 30, 50, 70, 90 }[Q.Level - 1] +
                            ((Riven.R.IsReady() && useR && !Player.Instance.HasBuff("RivenFengShuiEngine")
                                ? Player.Instance.TotalAttackDamage * 1.2
                                : Player.Instance.TotalAttackDamage) / 100) *
                            new double[] { 40, 45, 50, 55, 60 }[Q.Level - 1]);
        }

        public static float WDamage()
        {
            return (float)(new double[] { 50, 80, 110, 140, 170 }[W.Level - 1] +
                            1 * ObjectManager.Player.FlatPhysicalDamageMod);
        }

        public static double PassiveDamage()
        {
            return ((20 + ((Math.Floor((double)ObjectManager.Player.Level / 3)) * 5)) / 100) *
                   (ObjectManager.Player.BaseAttackDamage + ObjectManager.Player.FlatPhysicalDamageMod);
        }

        public static float RDamage(Obj_AI_Base target, double healthMod = 0)
        {
            if (!R.IsLearned) return 0;
            var hpPercent = (target.Health - healthMod > 0 ? 1 : target.Health - healthMod) / target.MaxHealth;
            return (float)((new double[] { 80, 120, 160 }[Riven.R.Level - 1]
                             + 0.6 * Player.Instance.FlatPhysicalDamageMod) *
                            (hpPercent < 25 ? 3 : (((100 - hpPercent) * 2.67) / 100) + 1));
        }
    }
}