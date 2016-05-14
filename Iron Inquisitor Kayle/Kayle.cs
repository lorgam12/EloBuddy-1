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

namespace Iron_Inquisitor_Kayle
{
    class Kayle
    {
        public static Spell.Targeted Q;
        public static Spell.Targeted W;
        public static Spell.Active E;
        public static Spell.Targeted R;

        public static void Init()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Targeted(SpellSlot.W, 900);
            E = new Spell.Active(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 900);

            Check();
            Game.OnUpdate += Game_OnUpdate;
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

            Manager.Init();
            SkinHack.Init();
                
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

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Modes.Flee.Init();
            }
        }
    }
}
