using HarmonyLib;

namespace MoreBatteriesBepinEx
{
    [HarmonyPatch(typeof(EnergyMixin), nameof(EnergyMixin.Start))]
    public class EnergyPatch
    {
        [HarmonyPrefix]
        public static void Prefix(EnergyMixin __instance) 
        {
            if (__instance.compatibleBatteries.Contains(TechType.Battery))
            {
                __instance.compatibleBatteries.Add(Batteries.LithiumBattery.TechType);
                __instance.compatibleBatteries.Add(Batteries.AdvLithiumBattery.TechType);
                __instance.compatibleBatteries.Add(Batteries.ThermalBattery.TechType);
                __instance.compatibleBatteries.Add(Batteries.FissionBattery.TechType);
                __instance.compatibleBatteries.Add(Batteries.CoffeBattery.TechType);
            }
            if (__instance.compatibleBatteries.Contains(TechType.PowerCell)) 
            {
                __instance.compatibleBatteries.Add(Batteries.LithiumCell.TechType);
                __instance.compatibleBatteries.Add(Batteries.AdvLithiumCell.TechType);
                __instance.compatibleBatteries.Add(Batteries.ThermalCell.TechType);
                __instance.compatibleBatteries.Add(Batteries.FissionCell.TechType);
            }
        }
    }
}
