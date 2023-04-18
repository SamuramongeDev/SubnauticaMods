using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoUpgradesBepInEx
{
    public class UpgradeComponent : MonoBehaviour
    {
        public Vehicle Parent;

        public DealDamageOnImpact DamageOnImpact;

        public float DefaultValue = 11.5f;

        public float SpeedMultiplier;

        public float ArmorPlatingMultiplier;

        public float EfficiencyBonus;

        public float EfficiencyPenalty;

        public void Load(Vehicle  vehicle)
        {
            Parent = vehicle;
            DamageOnImpact = GetComponent<DealDamageOnImpact>();
            Main.logSource.LogInfo("Vehicle Initialized!");

            if (vehicle is Exosuit)
            {
                Main.logSource.LogInfo("In Prawn");
            }
            else if (vehicle is SeaMoth)
            {
                Main.logSource.LogInfo("In Seamoth");
            }

            Upgrade(ModuleType.SpeedModule);
            Upgrade(ModuleType.HullReinforcementModule);
            Upgrade(ModuleType.PowerEfficiencyModule);
        }

        public void Upgrade(ModuleType moduleType)
        {
            if (moduleType == ModuleType.SpeedModule)
            {
                SpeedMultiplier = 11.5f;
                UpdateSpeed();
            }
            else if (moduleType == ModuleType.HullReinforcementModule)
            {
                ArmorPlatingMultiplier = 0f;
                UpdateArmorPlating();
            }
            else if (moduleType == ModuleType.PowerEfficiencyModule)
            {
                UpdatePowerEfficiency();
            }
        }

        private void UpdateSpeed()
        {
            SpeedMultiplier += GetModuleCountOf(ModuleType.SpeedModule, 1) * 2.875f;
            SpeedMultiplier += GetModuleCountOf(ModuleType.SpeedModule, 2) * 5.75f;
            SpeedMultiplier += GetModuleCountOf(ModuleType.SpeedModule, 3) * 8.625f;
            SpeedMultiplier += GetModuleCountOf(ModuleType.SpeedModule, 4) * 11.5f;
            EfficiencyPenalty = 1f;
            EfficiencyPenalty += GetModuleCountOf(ModuleType.SpeedModule, 1) * 1.1f;
            EfficiencyPenalty += GetModuleCountOf(ModuleType.SpeedModule, 2) * 1.16f;
            EfficiencyPenalty += GetModuleCountOf(ModuleType.SpeedModule, 3) * 1.25f;
            EfficiencyPenalty += GetModuleCountOf(ModuleType.SpeedModule, 4) * 1.46f;

            UpdatePowerEfficiency();

            Parent.forwardForce = SpeedMultiplier;
                ErrorMessage.AddMessage(string.Format("Vehicle Speed is {0}m/s", Parent.forwardForce));
        }

        private void UpdateArmorPlating()
        {
            ArmorPlatingMultiplier += GET_COUNT_INTERNAL(TechType.VehicleArmorPlating);
            ArmorPlatingMultiplier += GetModuleCountOf(ModuleType.HullReinforcementModule, 2) * 2f;
            ArmorPlatingMultiplier += GetModuleCountOf(ModuleType.HullReinforcementModule, 3) * 4f;
            ArmorPlatingMultiplier += GetModuleCountOf(ModuleType.HullReinforcementModule, 4) * 6f;
            float DefaultVal = 0.5f;
            double GeneralArmorPlating = (15f / ArmorPlatingMultiplier) / 2f * Mathf.Pow(0.5f, ArmorPlatingMultiplier);

            if (ArmorPlatingMultiplier > 0)
                DamageOnImpact.mirroredSelfDamageFraction = 0.5f * Mathf.Pow(0.5f, ArmorPlatingMultiplier);
            else
                DamageOnImpact.mirroredSelfDamageFraction = DefaultVal;
            ErrorMessage.AddMessage(string.Format("Vehicle Armor Plating is {0}", GeneralArmorPlating));
        }

        private void UpdatePowerEfficiency()
        {
            EfficiencyBonus = 1f + (GetModuleCountOf(ModuleType.PowerEfficiencyModule, 2) + GET_COUNT_INTERNAL(TechType.VehiclePowerUpgradeModule));
            float PowerRating = EfficiencyBonus / EfficiencyPenalty;
            Parent.enginePowerRating = PowerRating;
            ErrorMessage.AddMessage(string.Format("Power rating is: {0}%", Mathf.Round(PowerRating) * 100f));
        }

        private int GetModuleCountOf(ModuleType moduleType, int mk)
        {
            foreach (Module module in Upgrades.Modules)
            {
                if (module.ModuleType == moduleType && module.TechType.ToString() == $"{module.ModuleType.ToString().ToLower()}mk{mk}")
                {
                    return GET_COUNT_INTERNAL(module.TechType);
                }
            }
            return 0;
        }

        private int GET_COUNT_INTERNAL(TechType techType)
        {
            return Parent.modules.GetCount(techType);
        }
    }
}
