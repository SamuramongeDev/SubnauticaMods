using System.Collections.Generic;
using CustomBatteries.API;
using QModManager.API.ModLoading;
using HarmonyLib;
using System.Reflection;
using QModManager.Utility;
using Loggerd = QModManager.Utility.Logger;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using System;
using System.IO;
using UnityEngine;

namespace FixMoreBatteries_SN
{
    [QModCore]
    public static class QMod
    { 
        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var name = ($"samuramongedev.{assembly.GetName().Name}");
            Harmony h = new Harmony(name);


            h.PatchAll(assembly);
            BatteriesPatch.Patch();


            Loggerd.Log(Loggerd.Level.Info, $"{name} has been patched succesfully.");
        }

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
