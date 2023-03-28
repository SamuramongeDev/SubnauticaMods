using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using MoreBatteriesBepinEx.InternalAPI;

namespace MoreBatteriesBepinEx
{
    public class Batteries
    {
        // Batteries

        public static CBattery LithiumBattery;

        public static CBattery AdvLithiumBattery;

        public static CBattery ThermalBattery;

        public static CBattery FissionBattery;

        public static CBattery CoffeBattery;

        // PowerCells

        public static CPowerCell LithiumCell;

        public static CPowerCell AdvLithiumCell;

        public static CPowerCell ThermalCell;

        public static CPowerCell FissionCell;

        // Patch Methods

        public static void PatchBatteries()
        {
            LithiumBattery = new CBattery("batterylithiumcustom", "Lithium Ion Battery", "Uses lithium ions to produce energy")
            {
                BatteryCapacity = 250,
                FabricatorTab = new string[] {"Resources", "CustomBatteries"},
                UnlocksWith = TechType.Lithium,
                BatterySprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "LithiumBatteryRender.png"),
                BatteryTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumBattery.png"),
                BatteryRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.Lithium, 2),
                        new Ingredient(TechType.Titanium, 2),
                        new Ingredient(TechType.Copper, 1)
                    }
                }
            };
            LithiumBattery.Patch();

            AdvLithiumBattery = new CBattery("batterylithiumadvanced", "Advanced Lithium Battery", "Uses lithium micro-ions to produce energy")
            {
                BatteryCapacity = 750,
                FabricatorTab = new string[] { "Resources", "CustomBatteries" },
                UnlocksWith = LithiumBattery.TechType,
                BatterySprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "AdvLithiumBatteryRender.png"),
                BatteryTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "AdvLithiumBattery.png"),
                BatteryRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(LithiumBattery.TechType, 1),
                        new Ingredient(TechType.PrecursorIonCrystal, 1),
                        new Ingredient(TechType.AluminumOxide, 1),
                        new Ingredient(TechType.AdvancedWiringKit, 1)
                    }
                }
            };
            AdvLithiumBattery.Patch();

            ThermalBattery = new CBattery("batterythermal", "Thermal Battery", "Uses the heat of chemical reactions to produce energy.")
            {
                BatteryCapacity = 1000,
                FabricatorTab = new string[] { "Resources", "CustomBatteries" },
                UnlocksWith = AdvLithiumBattery.TechType,
                BatterySprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "ThermalBatteryRender.png"),
                BatteryTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "ThermalBattery.png"),
                BatteryRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.Sulphur, 2),
                        new Ingredient(TechType.Kyanite, 2),
                        new Ingredient(TechType.Nickel, 4)
                    }
                }
            };
            ThermalBattery.Patch();

            FissionBattery = new CBattery("batteryfission", "Fission Battery", "Uses nuclear fission to produce energy")
            {
                BatteryCapacity = 1250,
                FabricatorTab = new string[] { "Resources", "CustomBatteries" },
                UnlocksWith = ThermalBattery.TechType,
                BatterySprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "FissionBatteryRender.png"),
                BatteryTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "FissionBattery.png"),
                BatteryRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.ReactorRod, 3),
                        new Ingredient(TechType.CopperWire, 2),
                        new Ingredient(TechType.Nickel, 2)
                    }
                }
            };
            FissionBattery.Patch();
        }

        public static void PatchcCells()
        {
            LithiumCell = new CPowerCell("powercelllithiumcustom", "Lithium Ion Powercell", "Uses lithium ions to produce energy")
            {
                PowerCellCapacity = 500,
                FabricatorTab = new string[] { "Resources", "CustomPowerCells" },
                UnlocksWith = LithiumBattery.TechType,
                PowerCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "LithiumCellRender.png"),
                PowerCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumPowerCell.png"),
                PowerCellRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(LithiumBattery.TechType, 2),
                        new Ingredient(TechType.WiringKit, 1),
                        new Ingredient(TechType.Copper, 2)
                    }
                }
             };
            LithiumCell.Patch();

            AdvLithiumCell = new CPowerCell("powercelllithiumadvancedc", "Advanced Lithium Powercell", "Uses lithium micro-ions to produce energy")
            {
                PowerCellCapacity = 1000,
                FabricatorTab = new string[] { "Resources", "CustomPowerCells" },
                UnlocksWith = AdvLithiumBattery.TechType,
                PowerCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "AdvLithiumCellRender.png"),
                PowerCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "AdvLithiumPowerCell.png"),
                PowerCellRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(AdvLithiumBattery.TechType, 2),
                        new Ingredient(TechType.AdvancedWiringKit, 1),
                        new Ingredient(TechType.PrecursorIonCrystal, 1)
                    }
                }
            };
            AdvLithiumCell.Patch();

            ThermalCell = new CPowerCell("powercellthermal", "Thermal Powercell", "Uses the heat of chemical reactions to produce heat")
            {
                PowerCellCapacity = 2000,
                FabricatorTab = new string[] { "Resources", "CustomPowerCells" },
                UnlocksWith = ThermalBattery.TechType,
                PowerCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "ThermalCellRender.png"),
                PowerCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "ThermalPowerCell.png"),
                PowerCellRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(ThermalBattery.TechType, 2),
                        new Ingredient(TechType.Sulphur, 2),
                        new Ingredient(TechType.Aerogel, 2)
                    }
                }
            };
            ThermalCell.Patch();

            FissionCell = new CPowerCell("powercellfission", "Fission Powercell", "Uses nuclear fission to produce energy")
            {
                PowerCellCapacity = 2500,
                FabricatorTab = new string[] { "Resources", "CustomPowerCells" },
                UnlocksWith = FissionBattery.TechType,
                PowerCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "FissionCellRender.png"),
                PowerCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "FissionPowerCell.png"),
                PowerCellRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(FissionBattery.TechType, 2),
                        new Ingredient(TechType.ReactorRod, 2),
                        new Ingredient(TechType.CopperWire, 2)
                    }
                }
            };
            FissionCell.Patch();


            CoffeBattery = new CBattery("coffebattery", "Coffee Battery", "Thanks for 1.2k downloads!")
            {
                BatteryCapacity = 1,
                FabricatorTab = new string[] { "Resources", "CustomBatteries" },
                UnlocksWith = FissionCell.TechType,
                isCoffee = true,
                BatteryRecipe = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.Coffee, 2)
                    }
                }
            };
            CoffeBattery.Patch();

        }
    }
}
