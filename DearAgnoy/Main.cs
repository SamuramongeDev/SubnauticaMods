namespace BaseLib
{
    using HarmonyLib;
    using BepInEx;
    using BepInEx.Logging;
    using System.Reflection;
    using System.IO;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using BaseLib.Handlers;
    using SMLHelper.V2.Handlers;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        const string GUID = "samuramongedev.baselib";
        const string NAME = "Base Lib";
        const string VERSION = "0.0.1";

        public static ManualLogSource logSource;
        static Harmony harmony = new Harmony(GUID);

        public static readonly AssetBundleList bundles = new AssetBundleList();
        public static AssetBundle bundle = AssetBundle.LoadFromFile($"{Paths.AssetBundleDirectory}/alienbasepieces");
        public static List<TechType> techs = new List<TechType>();

        void Awake()
        {
            logSource = Logger;
            logSource.LogInfo("Checking Directories...");
            Paths.CheckDirectories();
            logSource.LogInfo("Patching Harmony...");
            new ITube().Patch();
            new Tubular().Patch();
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            logSource.LogInfo("Patched!");
        }
    }

    public class Paths
    {
        public static readonly string AssetsDirectory = $"{AssemblyDirectory}/Assets";
        public static readonly string AssetBundleDirectory = $"{AssetsDirectory}/AssetBundles";
        public static readonly string CacheDirectory = $"{AssemblyDirectory}/Cache";

        public static Dictionary<string ,string> files = new Dictionary<string ,string>();

        public static void CheckDirectories()
        {
            if (!Directory.Exists(AssetsDirectory))
            {
                Main.logSource.LogInfo("Assets Directory not found! Creating it.");
                Directory.CreateDirectory(AssetsDirectory);
            }
            if (!Directory.Exists(AssetBundleDirectory))
            {
                Main.logSource.LogInfo("AssetBundle Directory not found! Creating it.");
                Directory.CreateDirectory(AssetBundleDirectory);
            }
            if (!Directory.Exists(CacheDirectory))
            {
                Main.logSource.LogInfo("Cache Directory not found! Creating it.");
                Directory.CreateDirectory(CacheDirectory);
            }
            Main.logSource.LogInfo("All directories set!");
        }

        static string AssemblyDirectory
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
