using System;
using System.Collections.Generic;
using HarmonyLib;
using MoreBatteriesBepinEx.Cells;

namespace MoreBatteriesBepinEx
{
    [HarmonyPatch(typeof(PowerCellCharger), "Initialize")]
    internal class PowerCellChargerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(PowerCellCharger __instance) 
        {
            __instance.allowedTech.Add(LithiumIonCell.techType);
            __instance.allowedTech.Add(AdvancedLithiumCell.techType);
            __instance.allowedTech.Add(ThermalCell.techType);
            __instance.allowedTech.Add(FissionCell.techType);
        }
    }
}
