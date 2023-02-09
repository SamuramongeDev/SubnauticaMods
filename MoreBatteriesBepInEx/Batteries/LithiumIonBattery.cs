using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreBatteriesBepinEx.Batteries
{
    internal class LithiumIonBattery : Equipable
    {
        public LithiumIonBattery() : base("batterylithiumcustom","Lithium Ion Battery","Uses lithium ions to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () {LithiumIonBattery.techType = base.TechType;}));
        }

        // Main
  
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Battery, true);
            yield return task;
            GameObject OgPrefab = task.GetResult();
            GameObject ResPrefab = UnityEngine.Object.Instantiate(OgPrefab);
            
            MeshRenderer renderer = ResPrefab.GetComponentInChildren<MeshRenderer>();
            battery = ResPrefab.GetComponentInChildren<Battery>();
            battery._capacity = 250;
            renderer.material.mainTexture = lithiumBatteryTexture;

            gameObject.Set(ResPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Lithium, 2),
                    new Ingredient(TechType.Titanium, 2),
                    new Ingredient(TechType.Copper, 1)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return lithiumBatterySprite;
        }


        // Adjustments

        public override EquipmentType EquipmentType { get { return EquipmentType.BatteryCharger; } }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public override TechType RequiredForUnlock { get { return TechType.Lithium; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources","Electronics" }; } }

        public static Battery battery;

        public Atlas.Sprite lithiumBatterySprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "LithiumBatteryRender.png");
        public Texture2D lithiumBatteryTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumBattery.png");
    }
}
