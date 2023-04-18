using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoUpgradesBepInEx
{
    public class Module : Equipable
    {
        public bool isWorkbech = false;

        public string[] FabricatorTabSteps;

        public TechData ModuleBlueprint = new TechData() { craftAmount = 1, Ingredients = new List<Ingredient> { new Ingredient(TechType.Titanium, 1) } };

        public Atlas.Sprite ModuleSprite = SpriteManager.Get(TechType.SeamothElectricalDefense);

        public ModuleType ModuleType = ModuleType.None;

        // Code

        public override CraftTree.Type FabricatorType => GetCraftTreeType();

        public override TechCategory CategoryForPDA => GetTechCategory();

        public override TechGroup GroupForPDA => GetTechGroup();

        public override EquipmentType EquipmentType => EquipmentType.VehicleModule;

        public override string[] StepsToFabricatorTab => FabricatorTabSteps;

        public TechCategory GetTechCategory()
        {
            if (isWorkbech)
            {
                return TechCategory.Workbench;
            }
            return TechCategory.VehicleUpgrades;
        }

        public TechGroup GetTechGroup()
        {
            if (isWorkbech)
            {
                return TechGroup.Workbench;
            }
            return TechGroup.VehicleUpgrades;
        }

        public CraftTree.Type GetCraftTreeType()
        {
            if (isWorkbech)
            {
                return CraftTree.Type.Workbench;
            }
            return CraftTree.Type.SeamothUpgrades;
        }

        public Module(string id, string name, string desc) : base(id, name, desc){ }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.SeamothElectricalDefense);
            yield return task;
            GameObject Result = task.GetResult();
            GameObject Prefab = UnityEngine.Object.Instantiate(Result);

            gameObject.Set(Prefab);
        }

        protected override TechData GetBlueprintRecipe()
        {
            return ModuleBlueprint;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return ModuleSprite;
        }
    }

    public class Upgrades
    {
        public static List<Module> Modules = new List<Module>();

        public static void PatchAll()
        {
            // Speed Modules
            var smodulemk1 = new Module("speedmodulemk1", "Speed Upgrade MK1", "Enhances vehicle speed by 25%")
            {
                isWorkbech = false,
                ModuleSprite = SpriteManager.Get(TechType.VehicleHullModule1),
                FabricatorTabSteps = new string[] { "CommonModules" },
                ModuleType = ModuleType.SpeedModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.Titanium, 5),
                        new Ingredient(TechType.AluminumOxide, 2),
                        new Ingredient(TechType.CrashPowder, 2),
                        new Ingredient(TechType.WiringKit, 1)
                    }
                }
            };
            smodulemk1.Patch();

            var smodulemk2 = new Module("speedmodulemk2", "Speed Upgrade MK2", "Enhances vehicle speed by 50%")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleHullModule1),
                ModuleType = ModuleType.SpeedModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(smodulemk1.TechType, 1),
                        new Ingredient(TechType.PowerCell, 2),
                        new Ingredient(TechType.AdvancedWiringKit, 1)
                    }
                }
            };
            smodulemk2.Patch();

            var smodulemk3 = new Module("speedmoduleemk3", "Speed Upgrade MK3", "Enhances vehicle speed by 75%")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleHullModule1),
                ModuleType = ModuleType.SpeedModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(smodulemk2.TechType, 1),
                        new Ingredient(TechType.PlasteelIngot, 1),
                        new Ingredient(TechType.PrecursorIonCrystal, 1),
                        new Ingredient(TechType.AdvancedWiringKit, 1)
                    }
                }
            };
            smodulemk3.Patch();

            var smodulemk4 = new Module("speedmodulemk4", "Speed Upgrade MK4", "Enhances vehicle speed by 100%")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleHullModule1),
                ModuleType = ModuleType.SpeedModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(smodulemk3.TechType, 1),
                        new Ingredient(TechType.Sulphur, 3),
                        new Ingredient(TechType.Nickel, 2),
                        new Ingredient(TechType.AdvancedWiringKit, 2)
                    }
                }
            };
            smodulemk4.Patch();

            // Hull Reinforcement Modules
            var hrmodulemk2 = new Module("hullreinforcementmodulemk2", "Hull Reinforcement Upgrade MK2", "Provides more defense against impacts. (x2)")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleArmorPlating),
                ModuleType = ModuleType.HullReinforcementModule,
                ModuleBlueprint = new TechData 
                { 
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.VehicleArmorPlating, 2),
                        new Ingredient(TechType.Diamond, 2),
                        new Ingredient(TechType.TitaniumIngot, 1)
                    }
                }
            };
            hrmodulemk2.Patch();

            var hrmodulemk3 = new Module("hullreinforcementmodulemk3", "Hull Reinforcement Upgrade MK3", "Provides more defense against impacts. (x4)")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleArmorPlating),
                ModuleType = ModuleType.HullReinforcementModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(hrmodulemk2.TechType, 1),
                        new Ingredient(TechType.Diamond, 5),
                        new Ingredient(TechType.AluminumOxide, 5),
                        new Ingredient(TechType.TitaniumIngot, 2)
                    }
                }
            };
            hrmodulemk3.Patch();

            var hrmodulemk4 = new Module("hullreinforcementmodulemk4", "Hull Reinforcement Upgrade MK4", "Provides more defense against impacts. (x6)")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehicleArmorPlating),
                ModuleType = ModuleType.HullReinforcementModule,
                ModuleBlueprint = new TechData
                {
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(hrmodulemk3.TechType, 1),
                        new Ingredient(TechType.Diamond, 4),
                        new Ingredient(TechType.TitaniumIngot, 2),
                        new Ingredient(TechType.PlasteelIngot, 2)
                    }
                }
            };
            hrmodulemk4.Patch();

            var powermodulemk2 = new Module("powerefficiencymodulemk2", "Engine Efficiency Module MK2", "Recycles heat by-product to minimize power inefficiencies. It also increases Gear efficiency and complexity to minimize even more power inneficiencies. (2.2x)")
            {
                isWorkbech = true,
                ModuleSprite = SpriteManager.Get(TechType.VehiclePowerUpgradeModule),
                ModuleType = ModuleType.PowerEfficiencyModule,
                ModuleBlueprint = new TechData 
                { 
                    craftAmount = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient(TechType.VehiclePowerUpgradeModule, 1),
                        new Ingredient(TechType.Polyaniline, 2),
                        new Ingredient(TechType.Gold, 4),
                        new Ingredient(TechType.AluminumOxide, 2)
                    }
                }
            };
            powermodulemk2.Patch();

            Modules.Add(smodulemk1);
            Modules.Add(smodulemk2);
            Modules.Add(smodulemk3);
            Modules.Add(smodulemk4);
            Modules.Add(hrmodulemk2);
            Modules.Add(hrmodulemk3);
            Modules.Add(hrmodulemk4);
            Modules.Add(powermodulemk2);
        }
    }

    public enum ModuleType
    {
        None,
        HullModule,
        HullReinforcementModule,
        SpeedModule,
        PowerEfficiencyModule,
    }
}
