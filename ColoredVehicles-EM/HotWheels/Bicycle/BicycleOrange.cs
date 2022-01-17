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
    [LocDisplayName("Bicycle Orange")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleOrangeItem : WorldObjectItem<BicycleOrangeObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A orange bicycle for fast travel."); } }
    }
    
    public class PaintBicycleOrangeRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleOrangeRecipe).Name,
            Assembly = typeof(PaintBicycleOrangeRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle Orange",
            LocalizableName = Localizer.DoStr("Paint Bicycle Orange"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("OrangePaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleOrangeItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleOrangeRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleOrangeRecipe()
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
    public partial class BicycleOrangeObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleOrangeObject()
        {
            WorldObject.AddOccupancy<BicycleOrangeObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle Orange"); } }
        public Type RepresentedItemType { get { return typeof(BicycleOrangeItem); } }

        
        private BicycleOrangeObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
