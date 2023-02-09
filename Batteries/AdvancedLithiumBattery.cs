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
    internal class AdvancedLithiumBattery : Equipable
    {
        public AdvancedLithiumBattery() : base("batterylithiumadvanced","Advanced Lithium Battery","Uses lithium micro-ions to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { AdvancedLithiumBattery.techType = base.TechType; }));
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Battery, true);
            yield return task;
            GameObject OgPrefab = task.GetResult();
            GameObject ResPrefab = UnityEngine.Object.Instantiate(OgPrefab);

            MeshRenderer renderer= ResPrefab.GetComponentInChildren<MeshRenderer>();
            battery = ResPrefab.GetComponentInChildren<Battery>();
            battery._capacity = 750;
            renderer.material.mainTexture = advancedLithiumTexture;

            gameObject.Set(ResPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(LithiumIonBattery.techType, 1),
                    new Ingredient(TechType.PrecursorIonBattery, 1),
                    new Ingredient(TechType.AluminumOxide, 1),
                    new Ingredient(TechType.AdvancedWiringKit, 1)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return advancedLithiumSprite;
        }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override EquipmentType EquipmentType { get { return EquipmentType.BatteryCharger; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechType RequiredForUnlock { get { return TechType.PrecursorIonBattery; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources", "Electronics" }; } }

        public Battery battery;

        public Atlas.Sprite advancedLithiumSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "AdvLithiumBatteryRender.png");
        public Texture2D advancedLithiumTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "AdvancedLithiumBattery.png");


    }
}
