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
    internal class FissionBattery : Equipable
    {
        public FissionBattery() : base("batteryfission","Fission Battery","Uses nuclear fission to produce energy") 
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(delegate () { FissionBattery.techType = base.TechType; }));
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Battery, true);
            yield return task;
            GameObject OgPrefab = task.GetResult();
            GameObject ResPrefab = UnityEngine.Object.Instantiate(OgPrefab);

            MeshRenderer renderer= ResPrefab.GetComponentInChildren<MeshRenderer>();
            battery = ResPrefab.GetComponentInChildren<Battery>();
            battery._capacity = 1250;
            renderer.material.mainTexture = fissionTexture;

            gameObject.Set(ResPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.ReactorRod, 3),
                    new Ingredient(TechType.CopperWire, 2),
                    new Ingredient(TechType.Nickel, 2)
                },
                craftAmount = 1
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return fissionSprite;
        }

        public override CraftTree.Type FabricatorType { get { return CraftTree.Type.Fabricator; } }

        public override EquipmentType EquipmentType { get { return EquipmentType.BatteryCharger; } }

        public override TechCategory CategoryForPDA { get { return TechCategory.Electronics; } }

        public override TechGroup GroupForPDA { get { return TechGroup.Resources; } }

        public override TechType RequiredForUnlock { get { return ThermalBattery.techType; } }

        public override QuickSlotType QuickSlotType { get { return QuickSlotType.None; } }

        public static TechType techType;

        public override string[] StepsToFabricatorTab { get { return new string[] { "Resources", "Electronics" }; } }

        public Battery battery;

        public Atlas.Sprite fissionSprite = ImageUtils.LoadSpriteFromFile(Main.assetsDirectory + "FissionBatteryRender.png");
        public Texture2D fissionTexture = ImageUtils.LoadTextureFromFile(Main.assetsDirectory + "FissionBattery.png");


    }
}
