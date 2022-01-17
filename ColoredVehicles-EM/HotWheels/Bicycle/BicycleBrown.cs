namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Bicycle Brown")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleBrownItem : WorldObjectItem<BicycleBrownObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A brown bicycle for fast travel."); } }
    }
    
    public class PaintBicycleBrownRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleBrownRecipe).Name,
            Assembly = typeof(PaintBicycleBrownRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle Brown",
            LocalizableName = Localizer.DoStr("Paint Bicycle Brown"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("BrownPaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleBrownItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleBrownRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleBrownRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    public partial class BicycleBrownObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleBrownObject()
        {
            WorldObject.AddOccupancy<BicycleBrownObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle Brown"); } }
        public Type RepresentedItemType { get { return typeof(BicycleBrownItem); } }

        
        private BicycleBrownObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
