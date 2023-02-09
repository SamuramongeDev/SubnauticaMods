using HarmonyLib;
using MoreBatteriesBepinEx.Batteries;
using MoreBatteriesBepinEx.Cells;

namespace MoreBatteriesBepinEx
{
    [HarmonyPatch(typeof(EnergyMixin), "Start")]
    public class ChargerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(EnergyMixin __instance) 
        {
            if (__instance.compatibleBatteries.Contains(TechType.Battery))
            {
                __instance.compatibleBatteries.Add(LithiumIonBattery.techType);
                __instance.compatibleBatteries.Add(AdvancedLithiumBattery.techType);
                __instance.compatibleBatteries.Add(ThermalBattery.techType);
                __instance.compatibleBatteries.Add(FissionBattery.techType);
            }
            if (__instance.compatibleBatteries.Contains(TechType.PowerCell)) 
            {
                __instance.compatibleBatteries.Add(LithiumIonCell.techType);
                __instance.compatibleBatteries.Add(AdvancedLithiumCell.techType);
                __instance.compatibleBatteries.Add(ThermalCell.techType);
                __instance.compatibleBatteries.Add(FissionCell.techType);
            }
        }
    }
}
