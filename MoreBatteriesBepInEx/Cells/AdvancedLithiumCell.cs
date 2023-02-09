using MoreBatteriesBepinEx.Batteries;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreBatteriesBepinEx.Cells
{
    internal class AdvancedLithiumCell : Equipable
    {
        public AdvancedLithiumCell() : base("powercelllithiumadvancedc","Advanced Lithium Powercell","Uses lithium micro-ions to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { AdvancedLithiumCell.techType = base.TechType;}));
        }

        // Main
  
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PowerCell, true);
            yield return task;
            GameObject OgPrefab = task.GetResult();
            GameObject ResPrefab = UnityEngine.Object.Instantiate(OgPrefab);
            
            //MeshRenderer renderer = ResPrefab.GetComponentInChildren<MeshRenderer>();
            cell = ResPrefab.GetComponentInChildren<Battery>();
            cell._capacity = 1500;
            //renderer.material.mainTexture = lithiumCellTexture;

            gameObject.Set(ResPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AdvancedLithiumBattery.techType, 2),
                    new Ingredient(TechType.AluminumOxide, 2),
                    new Ingredient(TechType.Copper, 1)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return advLithiumCellSprite;
        }


        // Adjustments

        public override EquipmentType EquipmentType { get { return EquipmentType.PowerCellCharger; } }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public override TechType RequiredForUnlock { get { return AdvancedLithiumBattery.techType; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources","Electronics" }; } }

        public static Battery cell;

        public Atlas.Sprite advLithiumCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "AdvLithiumCellRender.png");
        //public Texture2D lithiumCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumBattery.png");
    }
}
