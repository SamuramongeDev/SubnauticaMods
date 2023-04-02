using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib
{
    public class Tubular : Craftable
    {
        public Tubular() : base("carry", "Carrier", "aaa") { }

        public override TechCategory CategoryForPDA => TechCategory.Constructor;
        public override TechGroup GroupForPDA => TechGroup.Constructor;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Constructor;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 1)
                }
            };
        }
    }
}
