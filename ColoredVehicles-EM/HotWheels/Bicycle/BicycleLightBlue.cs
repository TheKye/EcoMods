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
    [LocDisplayName("Bicycle LightBlue")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleLightBlueItem : WorldObjectItem<BicycleLightBlueObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A light blue bicycle for fast travel."); } }
    }
    
    public class PaintBicycleLightBlueRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleLightBlueRecipe).Name,
            Assembly = typeof(PaintBicycleLightBlueRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle Light Blue",
            LocalizableName = Localizer.DoStr("Paint Bicycle Light Blue"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("BluePaintItem", false, 1, true),
                new EMIngredient("WhitePaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleLightBlueItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleLightBlueRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleLightBlueRecipe()
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
    public partial class BicycleLightBlueObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleLightBlueObject()
        {
            WorldObject.AddOccupancy<BicycleLightBlueObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle LightBlue"); } }
        public Type RepresentedItemType { get { return typeof(BicycleLightBlueItem); } }

        
        private BicycleLightBlueObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
