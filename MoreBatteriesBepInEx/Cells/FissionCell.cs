﻿using MoreBatteriesBepinEx.Batteries;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreBatteriesBepinEx.Cells
{
    internal class FissionCell : Equipable
    {
        public FissionCell() : base("powercellfission","Fission Powercell","Uses nuclear fission to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { FissionCell.techType = base.TechType;}));
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
            cell._capacity = 2500;
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
                    new Ingredient(FissionBattery.techType, 2),
                    new Ingredient(TechType.ReactorRod, 2),
                    new Ingredient(TechType.CopperWire, 1)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return fissionCellSprite;
        }


        // Adjustments

        public override EquipmentType EquipmentType { get { return EquipmentType.PowerCellCharger; } }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public override TechType RequiredForUnlock { get { return FissionBattery.techType; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources","Electronics" }; } }

        public static Battery cell;

        public Atlas.Sprite fissionCellSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "FissionCellRender.png");
        //public Texture2D lithiumCellTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "LithiumBattery.png");
    }
}
