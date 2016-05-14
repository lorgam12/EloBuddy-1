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

namespace Koi_Nami
{
    class Nami
    {
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Spell.Skillshot R;

        public static void Init()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 875, SkillShotType.Circular, 950, int.MaxValue, 200);
            W = new Spell.Targeted(SpellSlot.W, 725);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Skillshot(SpellSlot.R, 2200, SkillShotType.Linear, 250, 850, 260);

            Check();
            Game.OnUpdate += Game_OnUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            GameObject.OnCreate += Manager.GameObject_OnCreate;
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

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            SkinHack.Init();
            Manager.Init();

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Modes.Combo.Init();
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValid)
            {
                if(NamiMenu.CheckBox(NamiMenu.Misc, "Gapcloser"))
                {
                    if(sender.IsValidTarget(Q.Range))
                    {
                        var QPred = Q.GetPrediction(sender);

                        if(QPred.HitChancePercent >= NamiMenu.Slider(NamiMenu.Principal, "QPred"))
                        {
                            Q.Cast(QPred.UnitPosition);
                        }
                    }
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if (NamiMenu.CheckBox(NamiMenu.Misc, "Interrupter"))
                {
                    if (sender.IsValidTarget(Q.Range))
                    {
                        var QPred = Q.GetPrediction(sender);

                        if (QPred.HitChancePercent >= NamiMenu.Slider(NamiMenu.Principal, "QPred"))
                        {
                            Q.Cast(QPred.UnitPosition);
                        }
                    }

                    if(e.DangerLevel == DangerLevel.High)
                    {
                        if(sender.IsValidTarget(R.Range))
                        {
                            var RPred = R.GetPrediction(sender);

                            if(RPred.HitChancePercent >= NamiMenu.Slider(NamiMenu.Principal, "RPred"))
                            {
                                R.Cast(RPred.UnitPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}
