using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System;
using System.IO;
using MoreBatteriesBepinEx.Batteries;
using MoreBatteriesBepinEx.Cells;

namespace MoreBatteriesBepinEx
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        public const string GUID = "samuramongedev.MoreBatteriesBepInEx";
        public const string NAME = "More Batteries";
        public const string VERSION = "1.0.0";
        
        private static Harmony harmony = new Harmony(GUID);

        private static ManualLogSource logSource;

        private void Awake()
        {
            Logger.LogInfo("Patching...");

            try
            {
                harmony.PatchAll();
                // Batteries
                LithiumIonBattery LithiumIonBattery = new LithiumIonBattery();
                AdvancedLithiumBattery advancedLithiumBattery = new AdvancedLithiumBattery();
                ThermalBattery thermalBattery = new ThermalBattery();
                FissionBattery fissionBattery = new FissionBattery();

                //Cells
                LithiumIonCell lithiumIonCell = new LithiumIonCell();
                AdvancedLithiumCell advancedLithiumCell = new AdvancedLithiumCell();
                ThermalCell thermalCell = new ThermalCell();
                FissionCell fissionCell = new FissionCell();

                //Patch
                LithiumIonBattery.Patch();
                advancedLithiumBattery.Patch();
                thermalBattery.Patch();
                fissionBattery.Patch();
                lithiumIonCell.Patch();
                advancedLithiumCell.Patch();
                thermalCell.Patch();
                fissionCell.Patch();

                Logger.LogInfo("Patched!");
            }
            catch (Exception ex) 
            {
                Logger.LogError("Failed to patch." + $" Expception: {ex}");
            }
            logSource = Logger;
        }

        // Assembly Directory for asset loading.
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string assetsDirectory = AssemblyDirectory + "/Assets/";
    }
}
