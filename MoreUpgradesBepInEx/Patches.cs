using System;
using HarmonyLib;

namespace MoUpgradesBepInEx
{
    [HarmonyPatch(typeof(SeaMoth), nameof(SeaMoth.Awake))]
    public class SeamothAwakePatch
    {
        [HarmonyPrefix]
        public static void Prefix(SeaMoth __instance) 
        {
            var component = __instance.gameObject.AddComponent<UpgradeComponent>();
            component.Load(__instance);
        }
    }

    [HarmonyPatch(typeof(SeaMoth), nameof(SeaMoth.OnUpgradeModuleChange))]
    public class SeamothOnUpgradeModuleChange
    {
        [HarmonyPrefix]
        public static void Prefix(SeaMoth __instance, TechType techType)
        {
            if (techType != TechType.VehicleStorageModule)
            {
                UpgradeComponent component = __instance.GetComponent<UpgradeComponent>();
                ModuleType moduleType = GetModuleTypeOfTechType(techType);

                component.Upgrade(moduleType);
            }
        }

        private static ModuleType GetModuleTypeOfTechType(TechType techType)
        {
            foreach (Module module in Upgrades.Modules)
            {
                if (module.TechType == techType)
                {
                    return module.ModuleType;
                }
            }
            return ModuleType.None;
        }
    }
}
