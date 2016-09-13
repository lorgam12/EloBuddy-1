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

using System.Reflection;
using System.Net;
using Version = System.Version;
using System.Text.RegularExpressions;

namespace Hextech_Annie
{
    class Annie
    {
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Skillshot R;
        public static Spell.Skillshot Flash;

        public static void Init()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 550, SkillShotType.Cone, 250, int.MaxValue, 250);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 200, int.MaxValue, 250);

            var Get = Player.Instance.GetSpellSlotFromName("summonerflash");
            if(Get != SpellSlot.Unknown)
            {
                Flash = new Spell.Skillshot(Get, 425, SkillShotType.Linear);
            }

            Check();
            Game.OnUpdate += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            GameObject.OnCreate += Tibbers.GameObject_OnCreate;
            GameObject.OnDelete += Tibbers.GameObject_OnDelete;
        }

        public static void Check()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/EloBuddy/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (!Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Chat.Print("<font color ='#042722'> This version is outdated </font> <font color='#ff0000'>" + Assembly.GetExecutingAssembly().GetName().Version + " </font>");
                }
                else
                {
                    Chat.Print("<font color ='#042722'> Thanks for using </font> <font color='#00530a'>" + Assembly.GetExecutingAssembly().GetName().Name + " </font>");
                }
            }
        }

        public static bool HasStun()
        {
            return Player.Instance.HasBuff("pyromania_particle");
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            SkinHack.Init();
            Tibbers.Init();

            if (AnnieMenu.Keybind(AnnieMenu.Combo, "Flash"))
            {
                var Target = TargetSelector.GetTarget(Flash.Range + R.Range, DamageType.Magical);

                if(Target != null)
                {
                    if(Flash.IsReady() && R.IsReady())
                    {
                        var RPred = R.GetPrediction(Target);

                        if(RPred.HitChancePercent >= AnnieMenu.Slider(AnnieMenu.Principal, "RPred"))
                        {
                            Flash.Cast(Player.Instance.Position.Extend(Target.Position, Flash.Range).To3D());

                            Core.DelayAction(() => R.Cast(RPred.UnitPosition), 150);
                        }
                    }
                }
            }

            if (AnnieMenu.CheckBox(AnnieMenu.Combo, "E"))
            {
                if (Player.Instance.IsRecalling())
                    return;

                if (!HasStun())
                {
                    if(E.IsReady())
                    {
                        E.Cast();
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Modes.Combo.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Modes.Laneclear.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Modes.Jungleclear.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                Modes.Lasthit.Init();
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValid)
            {
                if (AnnieMenu.CheckBox(AnnieMenu.Misc, "Interrupter"))
                {
                    if (HasStun())
                    {
                        if (Q.IsReady() && sender.IsValidTarget(Q.Range))
                        {
                            Q.Cast(sender);
                        }else if (W.IsReady() && sender.IsValidTarget(W.Range))
                        {
                            W.Cast(sender);
                        }else if (R.IsReady() && sender.IsValidTarget(R.Range))
                        {
                            if(e.DangerLevel == DangerLevel.High)
                            {
                                var RPred = R.GetPrediction(sender);

                                if(RPred.HitChancePercent >= AnnieMenu.Slider(AnnieMenu.Principal, "RPred"))
                                {
                                    R.Cast(RPred.UnitPosition);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValid)
            {
                if(AnnieMenu.CheckBox(AnnieMenu.Misc, "Gapcloser"))
                {
                    if(HasStun())
                    {
                        if(Q.IsReady() && sender.IsValidTarget(Q.Range))
                        {
                            Q.Cast(sender);
                        }

                        if (W.IsReady() && sender.IsValidTarget(W.Range))
                        {
                            W.Cast(sender);
                        }
                    }
                }
            }
        }
    }
}
