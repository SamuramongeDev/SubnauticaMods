using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System;
using System.IO;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using MoreBatteriesBepinEx.InternalAPI;
using System.Collections.Generic;

namespace MoreBatteriesBepinEx
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        public const string GUID = "samuramongedev.MoreBatteriesBepInEx";
        public const string NAME = "More Batteries";
        public const string VERSION = "1.2.0";

        public static string assetsDirectory = AssemblyDirectory + "/Assets/";

        private static Harmony harmony = new Harmony(GUID);

        private static ManualLogSource logSource;

        private void Awake()
        {
            logSource = Logger;

            Logger.LogInfo("Patching...");


            Logger.LogInfo("Patching CraftTrees");
            // Create new craftrees
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "CustomBatteries", "Batteries", ImageUtils.LoadSpriteFromFile(assetsDirectory + "LithiumBatteryRender.png"), new string[]
            {
                    "Resources"
            });
            CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, "CustomPowerCells", "PowerCells", ImageUtils.LoadSpriteFromFile(assetsDirectory + "LithiumCellRender.png"), new string[]
            {
                    "Resources"
            });

            // Patch Everything

            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Logger.LogInfo("Patching Batteries");

            Batteries.PatchBatteries();
            Batteries.PatchcCells();

            Logger.LogInfo("Patching Chargers");

            PatchChargers();

            Logger.LogInfo("Patched!");
            
        }

        private void PatchChargers()
        {
            // Set batteries and powercells 
            CBattery[] batteries = new CBattery[] 
            {
                Batteries.LithiumBattery,
                Batteries.AdvLithiumBattery,
                Batteries.ThermalBattery,
                Batteries.FissionBattery,
                Batteries.CoffeBattery,
            };
            CPowerCell[] powercells = new CPowerCell[]
            {
                Batteries.LithiumCell,
                Batteries.AdvLithiumCell,
                Batteries.ThermalCell,
                Batteries.FissionCell,
            };

            // Patch Chargers
            PatchBatteryCharger(BatteryCharger.compatibleTech, batteries);
            PatchPowerCellCharger(PowerCellCharger.compatibleTech, powercells);
        }

        private void PatchBatteryCharger(HashSet<TechType> hash, CBattery[] batteries)
        {
            foreach (CBattery battery in batteries)
            {
                hash.Add(battery.TechType);
                logSource.LogInfo($"TechType Patched: {battery.TechType}");
            }
            logSource.LogInfo($"CompatibleTech: {hash.Count}");
        }

        private void PatchPowerCellCharger(HashSet<TechType> hash, CPowerCell[] powercells)
        {
            foreach (CPowerCell powercell in powercells)
            {
                hash.Add(powercell.TechType);
                logSource.LogInfo($"TechType Patched: {powercell.TechType}");
            }
            logSource.LogInfo($"CompatibleTech: {hash.Count}");
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
    }
}
