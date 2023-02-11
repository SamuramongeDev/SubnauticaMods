using Discord;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiscordRPCBepInEx
{   
    public class DiscordController : MonoBehaviour
    {
        public static void Load()
        {
            GameObject discordController = new GameObject("DiscordController").AddComponent<DiscordController>().gameObject;
            GameObject.DontDestroyOnLoad(discordController);
            SceneInit();
        }

        // Unity Normal
        private void Awake()
        {
            discord = new Discord.Discord(1073381647287853097, (System.UInt64)Discord.CreateFlags.Default);

            activityManager = discord.GetActivityManager();

            activity.Details = "In Menu";
            activity.Assets.LargeImage = "subnauticalogo";

            UpdateActivity(activity);
        }

        private void Update()
        {
            discord.RunCallbacks();
            UpdateState();
            UpdatePrescence();
        }

        private static void SceneInit()
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene.ToLower().Contains("startscreen"))
            {
                sceneState = SceneState.Menu;
            }
           
        }

        // Main
        private void UpdatePrescence()
        {
            if (sceneState == SceneState.Menu)
            {
                activity.Details = "In Menu";
                activity.State = "";
                activity.Assets.LargeImage = "subnauticalogo";
                activity.Assets.LargeText = "Subnautica";

                UpdateActivity(activity);
            }
            else
            {
                SubRoot currSub = Player.main.GetCurrentSub();
                Vehicle currVehicle = Player.main.GetVehicle();

                biome = Player.main.GetBiomeString().ToLower();
                biomeDisplay = Utils.GetBiomeDisplayName(Utils.GetBiomeStringForImage(biome));

                activity.Details = $"At The {biomeDisplay}";

                activity.Assets.LargeImage = Utils.GetBiomeStringForImage(biome);
                activity.Assets.LargeText = biomeDisplay;

                if (currSub)
                {
                    string currSubString = currSub.GetType().Equals(typeof(BaseRoot)) ? "Base" : "Cyclops";

                    activity.Assets.SmallImage = Utils.GetCurrentSubForImage(currSubString.ToLower());
                    activity.Assets.SmallText = currSubString;

                    if (currSubString == "Base")
                    {
                        activity.State = $"In Base";
                    }
                    else
                    {
                        activity.State = $"In {currSubString} ({Mathf.Abs(Mathf.Round(Player.main.GetDepth()))} meters deep)";
                    }
                }
                else
                {
                    if (currVehicle)
                    {
                        string currVehicleString = currVehicle.GetType().Equals(typeof(SeaMoth)) ? "Seamoth" : "Prawn";

                        activity.Assets.SmallImage = Utils.GetCurrentSubForImage(currVehicleString.ToLower());
                        activity.Details = currVehicleString;

                        activity.State = $"In {currVehicleString} ({Mathf.Abs(Mathf.Round(Player.main.GetDepth()))} meters deep)";
                    }
                    else
                    {
                        activity.Assets.SmallImage = string.Empty;
                        if (Player.main.IsUnderwaterForSwimming())
                        {
                            activity.State = $"Swimming ({Mathf.Abs(Mathf.Round(Player.main.GetDepth()))} meters deep)";
                        }
                        else if (!Player.main.IsSwimming())
                        {
                            activity.State = "In Land";
                        }
                    }
                }

                UpdateActivity(activity);
            }
        }

        private void UpdateState()
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene.ToLower().Contains("startscreen"))
            {
                sceneState = SceneState.Menu;
            }
            else
            {
                sceneState = SceneState.InGame;
            }

        }


        // For more easier use.
        private void UpdateActivity(Activity activity)
        {
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Discord.Result.Ok)
                {
                    
                }
                else
                {
                    
                }
            });
        }

        private void OnDisable()
        {
            discord.Dispose();
        }

        public Discord.Discord discord;

        public ActivityManager activityManager;

        public Activity activity;

        public string biome = "";

        public string biomeDisplay = "";

        private static SceneState sceneState;

        private static string currScene;
    }
}
