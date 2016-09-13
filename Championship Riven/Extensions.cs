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

using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Version = System.Version;

namespace Championship_Riven
{
    internal class Extensions
    {
        public static Spell.Targeted Ignite, Flash;
        public static Spell.Skillshot Q, E, R2;
        public static Spell.Active W, R;

        public static bool IsActive => R.Name == "RivenIzunaBlade";

        public static int RealQ() => (Player.Instance.HasBuff("RivenFengShuiEngine") ? 330 : 265);
        public static int RealW() => (Player.Instance.HasBuff("RivenFengShuiEngine") ? 330 : 265);

        private static Item Hydra = new Item((int)ItemId.Ravenous_Hydra, 250);
        private static Item Tiamat = new Item((int)ItemId.Tiamat, 250);
        private static Item Youmu = new Item((int)ItemId.Youmuus_Ghostblade);

        private static bool HasItem() => (Hydra.IsReady() && Hydra.IsOwned() || Tiamat.IsReady() && Tiamat.IsOwned());
        public static void Reset() { if (HasItem()) { Tiamat.Cast(); Hydra.Cast(); } }
        public static void Use() { if (Youmu.IsReady() && Youmu.IsOwned()) { Youmu.Cast(); } }

        public static float QDMG(Obj_AI_Base T) => T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 10, 30, 50, 70, 90 }[Riven.Q.Level - 1] + ((Player.Instance.BaseAttackDamage + Player.Instance.FlatPhysicalDamageMod) / 100) * new float[] { 40, 45, 50, 55, 60 }[Riven.Q.Level - 1]);
        public static float WDMG(Obj_AI_Base T) => T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 50, 80, 110, 140, 170 }[Riven.W.Level - 1] + 1 * Player.Instance.FlatPhysicalDamageMod);
        public static float RDMG(Obj_AI_Base T) => T.CalculateDamageOnUnit(T, DamageType.Physical, (new float[] { 80, 120, 160 }[Riven.R.Level - 1] + 0.6f * Player.Instance.FlatPhysicalDamageMod) * ((T.MaxHealth - T.Health) / T.MaxHealth * 2.67f + 1));
        public static float PDMG(Obj_AI_Base T)
        {
            int Buff = Player.Instance.GetBuffCount("rivenpassiveaaboost");

            if (Buff > 0)
                T.CalculateDamageOnUnit(T, DamageType.Physical, (Player.Instance.Level < 3 ? 0.25f : (Player.Instance.Level < 6 ? 0.29167f : (Player.Instance.Level < 9 ? 0.3333f : (Player.Instance.Level < 12 ? 0.375f : (Player.Instance.Level < 15 ? 0.4167f : (Player.Instance.Level < 18 ? 0.4583f : 0.5f)))))) * Player.Instance.TotalAttackDamage);

            return 0;
        }
        public static void Debug(string M) => Console.WriteLine(M);
        public static AIHeroClient Target => TargetSelector.GetTarget(R2.Range, DamageType.Physical);
        public static bool Combo() { return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo); }
        public static bool Jungle() { return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear); }
        public static bool Lane() { return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear); }
        public static bool Flee() { return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee); }

        public static string[] SpecialSpells = new[] { "RengarPassiveBuffDashAADummy", "RengarPassiveBuffDash", "RengarQ", "GarenQAttack", "GarenRPreCast", "RenektonPreExecute", "TalonCutthroat", "IreliaEquilibriumStrike", "MonkeyKingQAttack", "PoppyPassiveAttack", "FioraEAttack", "illaoiwattack" };
        public static string[] SpecialBuffs = new[] { "kindredrnodeathbuff", "UndyingRage", "JudicatorIntervention" };

        public static void Update()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/Private/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Chat.Print(Assembly.GetExecutingAssembly().GetName().Name + " - Outdated", "You have an older version please update :D");
                }
                else
                {
                    Chat.Print(Assembly.GetExecutingAssembly().GetName().Name + " - Loaded", "Enjoy free elo! Keppo");
                }
            }
        }

        public static bool CheckLicense()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/Private/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/License.txt");

            if (RawVersion.Contains("Allowed"))
            {
                return true;
            }

            return false;
        }
    }
}
