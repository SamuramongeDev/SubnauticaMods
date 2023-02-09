using HarmonyLib;
using MoreBatteriesBepinEx.Batteries;

namespace MoreBatteriesBepinEx
{
    [HarmonyPatch(typeof(BatteryCharger), "Initialize")]
    public class BatteryChargerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(BatteryCharger __instance)
        {
            __instance.allowedTech.Add(LithiumIonBattery.techType);
            __instance.allowedTech.Add(AdvancedLithiumBattery.techType);
            __instance.allowedTech.Add(ThermalBattery.techType);
            __instance.allowedTech.Add(FissionBattery.techType);
        }
    }


}
