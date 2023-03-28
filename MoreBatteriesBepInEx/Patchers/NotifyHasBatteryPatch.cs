using HarmonyLib;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MoreBatteriesBepinEx.Patchers
{
    [HarmonyPatch(typeof(EnergyMixin), nameof(EnergyMixin.NotifyHasBattery))]
    public class NotifyHasBatteryPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref EnergyMixin __instance, InventoryItem item)
        {
            List<TechType> powercells = new List<TechType>() 
            {
                Batteries.LithiumCell.TechType,
                Batteries.AdvLithiumCell.TechType,
                Batteries.ThermalCell.TechType,
                Batteries.FissionCell.TechType
            };

            if (powercells.Count == 0)
                return;

            TechType? itemInSlot = item?.item?.GetTechType();

            if (!itemInSlot.HasValue || itemInSlot.Value == TechType.None)
                return;

            TechType powerCellTechType = itemInSlot.Value;
            bool isKnownModdedPowerCell = powercells.Find(techType => techType == powerCellTechType) != TechType.None;

            if (isKnownModdedPowerCell)
            {
                int modelToDisplay = 0;
                for (int b = 0; b < __instance.batteryModels.Length; b++)
                {
                    if (__instance.batteryModels[b].techType == powerCellTechType)
                    {
                        modelToDisplay = b;
                        break;
                    }
                }

                __instance.batteryModels[modelToDisplay].model.SetActive(true);
            }
        }
    }
}
