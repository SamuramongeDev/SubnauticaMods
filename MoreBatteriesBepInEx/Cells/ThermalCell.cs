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
    internal class ThermalCell : Equipable
    {
        public ThermalCell() : base("powercellthermal","Thermal Powercell","Uses the heat of chemical reactions to produce heat") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { ThermalCell.techType = base.TechType;}));
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
            cell._capacity = 2000;
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
                    new Ingredient(ThermalBattery.techType, 2),
                    new Ingredient(TechType.Sulphur, 2),
                    new Ingredient(TechType.Aerogel, 2)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return thermalCellSprite;
        }


        // Adjustments

        public override EquipmentType EquipmentType { get { return EquipmentType.PowerCellCharger; } }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public override TechType RequiredForUnlock { get { return ThermalBattery.techType; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources","Electronics" }; } }

        public static Battery cell;

        public Atlas.Sprite thermalCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "ThermalCellRender.png");
        //public Texture2D lithiumCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumBattery.png");
    }
}
