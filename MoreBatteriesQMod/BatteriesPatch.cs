using System;
using System.Collections.Generic;
using CustomBatteries.API;
using QModManager.API.ModLoading;
using HarmonyLib;
using System.Reflection;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using System.IO;
using UnityEngine;
using QModManager.Utility;

namespace FixMoreBatteries_SN
{
    internal static class BatteriesPatch
    {
        internal static void Patch()
        {
            Atlas.Sprite lithiumSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "LithiumBatteryRender.png");
            Texture2D lithiumTexture = ImageUtils.LoadTextureFromFile(AssetsDirectory + "LithiumBattery.png");
            Atlas.Sprite lithiumCellSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "LithiumCellRender.png");

            Atlas.Sprite advancedLithSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "AdvLithiumBatteryRender.png");
            Texture2D advancedLithTexture = ImageUtils.LoadTextureFromFile(AssetsDirectory + "AdvancedLithiumBattery.png");
            Atlas.Sprite advancedLithCellSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "AdvLithiumCellRender.png");

            Atlas.Sprite thermalSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "ThermalBatteryRender.png");
            Texture2D thermalTexture = ImageUtils.LoadTextureFromFile(AssetsDirectory + "ThermalBattery.png");
            Atlas.Sprite thermalCellSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "ThermalCellRender.png");

            Atlas.Sprite fissionSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "FissionBatteryRender.png");
            Texture2D fissionTexture = ImageUtils.LoadTextureFromFile(AssetsDirectory + "FissionBattery.png");
            Atlas.Sprite fissionCellSprite = ImageUtils.LoadSpriteFromFile(AssetsDirectory + "FissionCellRender.png");


            // Lithium Ion Battery
            var LithiumBattery = new CbBattery
            {
                EnergyCapacity = 250,
                ID = "lithiumionbatterycustom",
                Name = "Lithium Ion Battery",
                FlavorText = "Slightly better than the normal lithium battery.",
                UnlocksWith = TechType.Lithium,
                CraftingMaterials = new List<TechType> { TechType.Lithium, TechType.Lithium, TechType.Titanium, TechType.Titanium, TechType.Copper },
                CustomIcon = lithiumSprite,
                CBModelData = new CBModelData { CustomTexture = lithiumTexture }
            };
            LithiumBattery.Patch();



            // Lithium Ion Powercell
            var LithiumCell = new CbPowerCell
            {
                EnergyCapacity = 500,
                ID = "lithiumcellcustom",
                Name = "Lithium Ion Powercell",
                FlavorText = "Better than two lithium ion batteries.",
                UnlocksWith = LithiumBattery.TechType,
                CraftingMaterials = new List<TechType> { LithiumBattery.TechType, LithiumBattery.TechType, TechType.WiringKit, TechType.Copper, TechType.Copper},
                CustomIcon = lithiumCellSprite
            };
            LithiumCell.Patch();



            // Advanced Lithium Ion Battery
            var AdvancedLithiumBattery = new CbBattery
            {
                EnergyCapacity = 750,
                ID = "advancedlithiumionbatterycustom",
                Name = "Advanced Lithium Ion Battery",
                FlavorText = "Uses advanced energy storage made from lithium micro-ions",
                UnlocksWith = LithiumBattery.TechType,
                CraftingMaterials = new List<TechType> { LithiumBattery.TechType, TechType.PrecursorIonBattery, TechType.AluminumOxide, TechType.AdvancedWiringKit},
                CustomIcon = advancedLithSprite,
                CBModelData = new CBModelData { CustomTexture = advancedLithTexture}
            };
            AdvancedLithiumBattery.Patch();



            // Advanced Lithium Ion Powercell
            var AdvancedLithiumCell = new CbPowerCell
            {
                EnergyCapacity = 1500,
                ID = "advancedlithiumioncellcustom",
                Name = "Advanced Lithium Ion Powercell",
                FlavorText = "Uses advanced energy storage made from lithium micro-ions",
                UnlocksWith = AdvancedLithiumBattery.TechType,
                CraftingMaterials = new List<TechType> { AdvancedLithiumBattery.TechType, AdvancedLithiumBattery.TechType, TechType.AluminumOxide, TechType.AluminumOxide, TechType.Copper },
                CustomIcon = advancedLithCellSprite
            };
            AdvancedLithiumCell.Patch();

            // Thermal Battery
            var ThermalBattery = new CbBattery
            {
                EnergyCapacity = 1000,
                ID = "thermalbatterycustom",
                Name = "Thermal Battery",
                FlavorText = "Uses the heat of chemical reactions to produce energy.",
                UnlocksWith = AdvancedLithiumBattery.TechType,
                CraftingMaterials = new List<TechType> { TechType.Sulphur, TechType.Sulphur, TechType.Kyanite, TechType.Kyanite, TechType.Nickel, TechType.Nickel, TechType.Nickel, TechType.Nickel },
                CustomIcon = thermalSprite,
                CBModelData = new CBModelData() { CustomTexture = thermalTexture}
            };
            ThermalBattery.Patch();

            // Thermal Cell
            var ThermalCell = new CbPowerCell
            {
                EnergyCapacity = 2000,
                ID = "thermalcellcustom",
                Name = "Thermal Powercell",
                FlavorText = "Uses the heat of chemical reactions to produce energy.",
                UnlocksWith = ThermalBattery.TechType,
                CraftingMaterials = new List<TechType> { ThermalBattery.TechType, ThermalBattery.TechType, TechType.Sulphur, TechType.Sulphur, TechType.Aerogel, TechType.Aerogel},
                CustomIcon = thermalCellSprite
            };
            ThermalCell.Patch();

            // Fission Battery
            var FissionBattery = new CbBattery
            {

                EnergyCapacity = 1250,
                ID = "fissionbatterucustom",
                Name = "Fission Battery",
                FlavorText = "Uses nuclear fission to produce energy.",
                UnlocksWith = ThermalBattery.TechType,
                CraftingMaterials = new List<TechType> { TechType.ReactorRod, TechType.ReactorRod, TechType.ReactorRod, TechType.CopperWire, TechType.CopperWire, TechType.Nickel, TechType.Nickel },
                CustomIcon = fissionSprite,
                CBModelData = new CBModelData { CustomTexture = fissionTexture }
            };
            FissionBattery.Patch();

            // Fission Cell
            var FissionCell = new CbPowerCell
            {
                EnergyCapacity = 2500,
                ID = "fissioncellcustom",
                Name = "Fission Powercell",
                FlavorText = "Uses nuclear fission to produce energy.",
                UnlocksWith = TechType.NuclearReactor,
                CraftingMaterials = new List<TechType> { FissionBattery.TechType, FissionBattery.TechType, TechType.ReactorRod, TechType.ReactorRod, TechType.CopperWire},
                CustomIcon = fissionCellSprite
            };
            FissionCell.Patch();
        }
        
        internal static string AssetsDirectory = QMod.AssemblyDirectory + "/Assets/";
    }
}