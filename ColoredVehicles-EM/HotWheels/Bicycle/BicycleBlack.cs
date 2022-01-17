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
    [LocDisplayName("Bicycle Black")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredBicycle")]
    public partial class BicycleBlackItem : WorldObjectItem<BicycleBlackObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A black bicycle for fast travel.");
    }
    
    public class PaintBicycleBlackRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintBicycleBlackRecipe).Name,
            Assembly = typeof(PaintBicycleBlackRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Bicycle Black",
            LocalizableName = Localizer.DoStr("Paint Bicycle Black"),
            IngredientList = new()
            {
                new EMIngredient("BicycleItem", false, 1, true),
                new EMIngredient("BlackPaintItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("BicycleBlackItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
        };

        static PaintBicycleBlackRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintBicycleBlackRecipe()
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
    public partial class BicycleBlackObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleBlackObject()
        {
            WorldObject.AddOccupancy<BicycleBlackObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName => Localizer.DoStr("Bicycle Black");
        public Type RepresentedItemType => typeof(BicycleBlackItem);


        private BicycleBlackObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
