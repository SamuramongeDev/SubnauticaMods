using System;
using System.Collections.Generic;
using HarmonyLib;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace MoreBatteriesBepinEx.Patchers
{
    [HarmonyPatch(typeof(Charger), "OnEquip")]

    public class ChargerPatch
    {

        [HarmonyPostfix] 

        public static void Postfix(Charger __instance, string slot, InventoryItem item, Dictionary<string, Charger.SlotDefinition> ___slots) 
        {

            Texture2D LithiumCell2D = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumPowerCell.png", TextureFormat.BC7);


            Debug.Log("Posfix!");

            Charger.SlotDefinition slotDefinition;
            if (___slots.TryGetValue(slot, out slotDefinition))
            {
                GameObject battery = slotDefinition.battery;
                Pickupable item2 = item.item;

                if (battery != null && item2 != null)
                {
                    if (!(__instance is BatteryCharger))
                    {
                        if (__instance is PowerCellCharger)
                        {
                            GameObject gameObject = item2.gameObject.FindChild("engine_power_cell_01");

                            MeshFilter GOMeshFilter;
                            Renderer GORenderer;
                            MeshFilter BatteryMeshFilter;
                            Renderer BatteryRenderer;

                            bool flag = gameObject.TryGetComponent<MeshFilter>(out GOMeshFilter);
                            bool flag2 = gameObject.TryGetComponent<Renderer>(out GORenderer);
                            bool flag3 = battery.TryGetComponent<MeshFilter>(out BatteryMeshFilter);
                            bool flag4 = battery.TryGetComponent<Renderer>(out BatteryRenderer);

                            if (flag && flag2 && flag3 && flag4)
                            {
                                BatteryMeshFilter.mesh = GOMeshFilter.mesh;
                                BatteryRenderer.material.CopyPropertiesFromMaterial(GORenderer.material);
                                if (item.item.name == "powercelllithiumcustom(Clone)")
                                {
                                    BatteryRenderer.material.mainTexture = LithiumCell2D;
                                }
                            }
                        }
                    }
                    else
                    {
                        Transform transform = item2.gameObject.transform.Find("model/battery_01");
                        GameObject gameObject2;

                        if ((gameObject2 = ((transform != null) ? transform.gameObject : null)) == null)
                        {
                            Transform transform2 = item2.gameObject.transform.Find("model/battery_ion");
                            gameObject2 = ((transform2 != null) ? transform2.gameObject : null);
                        }

                        GameObject gameObject = gameObject2;
                        Renderer renderer;
                        Renderer renderer2;

                        if (gameObject != null && gameObject.TryGetComponent<Renderer>(out renderer) && battery.TryGetComponent<Renderer>(out renderer2))
                        {
                            renderer2.material.CopyPropertiesFromMaterial(renderer.material);
                            return;
                        }
                    }
                }
            }
        }
    }
}
