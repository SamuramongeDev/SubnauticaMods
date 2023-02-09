using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MoreBatteriesBepinEx.Batteries
{
    internal class ThermalBattery : Equipable
    {
        public ThermalBattery() : base("batterythermal","Thermal Battery","Uses lithium micro-ions to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { ThermalBattery.techType = base.TechType; }));
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Battery, true);
            yield return task;
            GameObject OgPrefab = task.GetResult();
            GameObject ResPrefab = UnityEngine.Object.Instantiate(OgPrefab);

            MeshRenderer renderer= ResPrefab.GetComponentInChildren<MeshRenderer>();
            battery = ResPrefab.GetComponentInChildren<Battery>();
            battery._capacity = 1000;
            renderer.material.mainTexture = thermalTexture;

            gameObject.Set(ResPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Sulphur, 2),
                    new Ingredient(TechType.Kyanite, 2),
                    new Ingredient(TechType.Nickel, 4)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return thermalSprite;
        }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override EquipmentType EquipmentType { get { return EquipmentType.BatteryCharger; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechType RequiredForUnlock { get { return AdvancedLithiumBattery.techType; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources", "Electronics" }; } }

        public Battery battery;

        public Atlas.Sprite thermalSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "ThermalBatteryRender.png");
        public Texture2D thermalTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "ThermalBattery.png");


    }
}
