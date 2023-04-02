namespace BaseLib
{
    using BaseLib.Handlers;
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Crafting;
    using SMLHelper.V2.Handlers;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class ITube : Buildable
    {
        public ITube() : base("precursoritube", "I Alien Compartment", "I Shaped alien compartment.") 
        {
            OnFinishedPatching += OnFinishPatch;
        }

        public static Base.Piece Piece;

        public static TechType techType;

        public GameObject prefab = null;

        public override TechCategory CategoryForPDA => TechCategory.BasePiece;
        public override TechGroup GroupForPDA => TechGroup.BasePieces;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 5)
                }
            };
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var prefab = GetGameObj();
            yield return null;
            AddBaseComponents(prefab);
            prefab.AddComponent<SettingComponent>();
            prefab.SetActive(true);
            gameObject.Set(prefab);
        }

        private GameObject GetGameObj()
        {
            if (prefab is null)
            {
                prefab = Main.bundle.LoadAsset<GameObject>("PrecursorITube");
                prefab.SetActive(false);
            }
            var obj = Object.Instantiate(prefab);
            return obj;
        }


        private void OnFinishPatch()
        {
            var piece = EnumHandler.AddPiece("PrecursorITube");
            Piece = piece;
            techType = TechType;
        }

        private void AddBaseComponents(GameObject prefab)
        {
            Base @base = prefab.AddComponent<Base>();
            BaseAddCorridorGhost baseGhost = prefab.AddComponent<BaseAddCorridorGhost>();
            baseGhost.allowedAboveWater = true;
            Constructable constructable = prefab.AddComponent<Constructable>();
            constructable.allowedOutside = true;
            constructable.allowedUnderwater = true;
            constructable.attachedToBase = true;
            constructable.rotationEnabled = true;
            constructable.forceUpright = false;
            constructable.alignWithSurface = false;
            constructable.allowedInBase = false;
            constructable.allowedInSub = false;
            constructable.allowedOnCeiling = false;
            constructable.allowedOnConstructables = false;
            constructable.allowedOnGround = false;
            constructable.allowedOnWall = false;
            constructable.techType = techType;
            constructable.model = prefab;
            ConstructableBase constructableBase = prefab.AddComponent<ConstructableBase>();
            constructableBase.rotatableBasePiece = true;
            constructableBase.model = prefab;
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = techType;
            PrefabIdentifier prefabIdentifier = prefab.AddComponent<PrefabIdentifier>();
            prefabIdentifier.ClassId = ClassID;
        }
    }

    public class SettingComponent : MonoBehaviour
    {
        void Awake()
        {
            Base @base = GetComponent<Base>();
            BaseAddCorridorGhost baseGhost = GetComponent<BaseAddCorridorGhost>();

            baseGhost.ghostBase = @base;
            Main.logSource.LogInfo(baseGhost.ghostBase);
        }
    }
}
