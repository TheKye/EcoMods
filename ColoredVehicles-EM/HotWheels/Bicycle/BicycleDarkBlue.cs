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
    [LocDisplayName("Bicycle DarkBlue")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleDarkBlueItem : WorldObjectItem<BicycleDarkBlueObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A dark blue bicycle for fast travel."); } }
    }
    
    public class PaintBicycleDarkBlueRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleDarkBlueRecipe).Name,
            Assembly = typeof(PaintBicycleDarkBlueRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle DarkBlue",
            LocalizableName = Localizer.DoStr("Paint Bicycle Dark Blue"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("BluePaintItem", false, 1, true),
                new EMIngredient("BlackPaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleDarkBlueItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleDarkBlueRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleDarkBlueRecipe()
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
    public partial class BicycleDarkBlueObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleDarkBlueObject()
        {
            WorldObject.AddOccupancy<BicycleDarkBlueObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle DarkBlue"); } }
        public Type RepresentedItemType { get { return typeof(BicycleDarkBlueItem); } }

        
        private BicycleDarkBlueObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
