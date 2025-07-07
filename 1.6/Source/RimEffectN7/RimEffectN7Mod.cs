using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimEffectN7
{
    public class RimEffectN7Mod : Mod
    {
        public static RimEffectN7Mod mod;
        public static RimEffectN7Settings settings;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public RimEffectN7Mod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<RimEffectN7Settings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            Log.Message($"".Colorize(Color.cyan) + $"{CurrentVersion} ::");

            File.WriteAllText(VersionDir, CurrentVersion);

            Harmony harmony = new Harmony("Neronix17.RimEffectN7.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
