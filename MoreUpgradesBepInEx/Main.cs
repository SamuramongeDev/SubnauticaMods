using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using BepInEx;
using SMLHelper.V2.Handlers;

namespace MoUpgradesBepInEx
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "samuramongedev.moupgrades";
        private const string NAME = "Mo' Upgrades BepInEx";
        private const string VERSION = "1.0.0";

        private static Harmony harmony = new Harmony(GUID);

        public static Settings Settings { get; private set; }

        public static ManualLogSource logSource;

        private void Awake()
        {
            logSource = Logger;

            // Sandwich
            logSource.LogInfo("Patching");
            Upgrades.PatchAll();
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            logSource.LogInfo("Patched!");
        }
    }
}
