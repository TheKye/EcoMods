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
    [LocDisplayName("Bicycle Purple")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicyclePurpleItem : WorldObjectItem<BicyclePurpleObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A purple bicycle for fast travel."); } }
    }
    
    public class PaintBicyclePurpleRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicyclePurpleRecipe).Name,
            Assembly = typeof(PaintBicyclePurpleRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle Purple",
            LocalizableName = Localizer.DoStr("Paint Bicycle Purple"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("PurplePaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicyclePurpleItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicyclePurpleRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicyclePurpleRecipe()
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
    public partial class BicyclePurpleObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicyclePurpleObject()
        {
            WorldObject.AddOccupancy<BicyclePurpleObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle Purple"); } }
        public Type RepresentedItemType { get { return typeof(BicyclePurpleItem); } }

        
        private BicyclePurpleObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
