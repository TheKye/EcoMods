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
    [LocDisplayName("Bicycle White")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleWhiteItem : WorldObjectItem<BicycleWhiteObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A white bicycle for fast travel."); } }
    }
    
    public class PaintBicycleWhiteRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleWhiteRecipe).Name,
            Assembly = typeof(PaintBicycleWhiteRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle White",
            LocalizableName = Localizer.DoStr("Paint Bicycle White"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("WhitePaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleWhiteItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleWhiteRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleWhiteRecipe()
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
    public partial class BicycleWhiteObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleWhiteObject()
        {
            WorldObject.AddOccupancy<BicycleWhiteObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Bicycle White"); } }
        public Type RepresentedItemType { get { return typeof(BicycleWhiteItem); } }

        
        private BicycleWhiteObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
