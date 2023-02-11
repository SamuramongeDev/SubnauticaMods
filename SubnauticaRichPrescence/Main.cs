using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using System;
using System.Diagnostics;
using Discord;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace DiscordRPCBepInEx
{
    [BepInPlugin(GUID,NAME,VERSION)]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "samuramongedev.discordrpcmodbepinex";
        private const string NAME = "Discord RPC";
        private const string VERSION = "0.1.0";

        private Harmony harmony = new Harmony(GUID);

        private static ManualLogSource logSource;

        private static bool oneTime = false;

        private void Awake()
        {
            
            Logger.LogInfo("Patching DiscordRPC...");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());

                Logger.LogInfo("Patched succesfully!");
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();

                Logger.LogError("Patching failed. CatchedExeption = "+ex);
                Logger.LogWarning($"Exception Line: {line}");

            }

        }

        private void Update()
        {
            if (!oneTime)
            {
                gameObject.AddComponent<DiscordController>();
                oneTime = true;
            }
        }
    }
}
