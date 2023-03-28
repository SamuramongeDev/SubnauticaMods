using System;
using System.Collections;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;



namespace MoreBatteriesBepinEx.InternalAPI
{
    public class CBattery : Equipable
    {
        public TechData BatteryRecipe = new TechData() { craftAmount = 1, Ingredients = new List<Ingredient> { new Ingredient(TechType.Titanium, 1) } };

        public Atlas.Sprite BatterySprite = SpriteManager.Get(TechType.Battery);

        public Texture2D BatteryTexture = null;

        public TechType UnlocksWith = TechType.Battery;

        public int BatteryCapacity = 100;

        public string[] FabricatorTab;

        public bool isCoffee = false;

        // Essentials

        public Battery battery;

        public static TechType techType;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;

        public override TechCategory CategoryForPDA => TechCategory.Electronics;

        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override EquipmentType EquipmentType => EquipmentType.BatteryCharger;

        public override string[] StepsToFabricatorTab => FabricatorTab;

        public CBattery(string id, string name, string description) : base(id, name, description)
        {
            this.OnFinishedPatching = (PatchEvent)Delegate.Combine(this.OnFinishedPatching, new PatchEvent(delegate ()
            {
                techType = base.TechType;
            }));
        }

        protected override TechData GetBlueprintRecipe()
        {
            return BatteryRecipe;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return BatterySprite;
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Battery, true);
            yield return task;
            GameObject OriginalPrefab = task.GetResult();
            GameObject ResultPrefab = UnityEngine.Object.Instantiate(OriginalPrefab);

            battery = ResultPrefab.GetComponentInChildren<Battery>();
            battery._capacity = BatteryCapacity;

            if (!(BatteryTexture is null))
            {
                MeshRenderer meshRenderer = ResultPrefab.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material.mainTexture = BatteryTexture;
            }
            if (isCoffee)
            {
                Eatable eatableComponent = ResultPrefab.AddComponent<Eatable>();
                eatableComponent.waterValue = 50;
                eatableComponent.foodValue = -50;
            }

            gameObject.Set(ResultPrefab);
        }
    }
}
