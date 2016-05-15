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

namespace Soul_Reaver_Draven
{
    class Draven
    {
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;

        public static void Init()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Linear, 250, 1400, 130);
            R = new Spell.Skillshot(SpellSlot.R, 1800, SkillShotType.Linear, 250, 2000, 160);

            Check();
            Game.OnTick += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            GameObject.OnCreate += AxesManager.GameObject_OnCreate;
            GameObject.OnDelete += AxesManager.GameObject_OnDelete;
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

        public static int AxesCount()
        {
            return Player.Instance.GetBuffCount("DravenSpinning");
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            SkinHack.Init();
            AxesManager.Init();

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Modes.Combo.Init();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Modes.Laneclear.Init();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Modes.Jungleclear.Init();
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Modes.Flee.Init();
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if(E.IsReady() && DravenMenu.CheckBox(DravenMenu.Misc, "Interrupter"))
                {
                    var EPred = E.GetPrediction(sender);

                    if (EPred.HitChancePercent >= DravenMenu.Slider(DravenMenu.Principal, "EPred"))
                    {
                        E.Cast(EPred.UnitPosition);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if (E.IsReady() && DravenMenu.CheckBox(DravenMenu.Misc, "Gapcloser"))
                {
                    var EPred = E.GetPrediction(sender);

                    if (EPred.HitChancePercent >= DravenMenu.Slider(DravenMenu.Principal, "EPred"))
                    {
                        E.Cast(EPred.UnitPosition);
                    }
                }
            }
        }
    }
}
