using Eco.Gameplay.Skills;

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
    [LocDisplayName("Scooter Purple")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Scooter", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredScooter")]
    public partial class ScooterPurpleItem : WorldObjectItem<ScooterPurpleObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A Purple scooter for fast travel."); } }
    }
    
    public class PaintScooterPurpleRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintScooterPurpleRecipe).Name,
            Assembly = typeof(PaintScooterPurpleRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Scooter Purple",
            LocalizableName = Localizer.DoStr("Paint Scooter Purple"),
            IngredientList = new()
            {
                new EMIngredient("ScooterItem", false, 1, true),
                new EMIngredient("PurpleDyeItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("ScooterPurpleItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 100,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
        };

        static PaintScooterPurpleRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintScooterPurpleRecipe()
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
    public partial class ScooterPurpleObject : PhysicsWorldObject, IRepresentsItem
    {
        static ScooterPurpleObject()
        {
            WorldObject.AddOccupancy<ScooterPurpleObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Scooter Purple"); } }
        public Type RepresentedItemType { get { return typeof(ScooterPurpleItem); } }

        private ScooterPurpleObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(18, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.125f);           
        }
    }
}
