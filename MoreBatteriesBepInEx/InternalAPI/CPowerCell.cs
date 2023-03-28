using System;
using System.Collections;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;



namespace MoreBatteriesBepinEx.InternalAPI
{
    public class CPowerCell : Equipable
    {
        public TechData PowerCellRecipe = new TechData() { craftAmount = 1, Ingredients = new List<Ingredient> { new Ingredient(TechType.Titanium, 1) } };

        public Atlas.Sprite PowerCellSprite = SpriteManager.Get(TechType.PowerCell);

        public Texture2D PowerCellTexture = null;

        public TechType UnlocksWith = TechType.Battery;

        public int PowerCellCapacity = 100;

        public string[] FabricatorTab;

        // Essentials

        public Battery cell;

        public static TechType techType;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;

        public override TechCategory CategoryForPDA => TechCategory.Electronics;

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override EquipmentType EquipmentType => EquipmentType.PowerCellCharger;

        public override string[] StepsToFabricatorTab => FabricatorTab;

        public CPowerCell(string id, string name, string description) : base(id, name, description)
        {
            this.OnFinishedPatching = (PatchEvent)Delegate.Combine(this.OnFinishedPatching, new PatchEvent(delegate ()
            {
                techType = base.TechType;
            }));
        }

        protected override TechData GetBlueprintRecipe()
        {
            return PowerCellRecipe;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return PowerCellSprite;
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PowerCell, true);
            yield return task;
            GameObject OriginalPrefab = task.GetResult();
            GameObject ResultPrefab = UnityEngine.Object.Instantiate(OriginalPrefab);

            cell = ResultPrefab.GetComponentInChildren<Battery>();
            cell._capacity = PowerCellCapacity;

            if (!(PowerCellTexture is null))
            {
                MeshRenderer meshRenderer = ResultPrefab.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material.mainTexture = PowerCellTexture;
            }

            gameObject.Set(ResultPrefab);
        }
    }
}
