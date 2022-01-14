namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Wood Cart Pride")]
    [Weight(15000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredWoodCart")]
    public partial class WoodCartPrideItem : WorldObjectItem<WoodCartPrideObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small pride cart for hauling small loads.");
    }

    public class PaintWoodCartPrideRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintWoodCartPrideRecipe).Name,
            Assembly = typeof(PaintWoodCartPrideRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Wood Cart Pride",
            LocalizableName = Localizer.DoStr("Paint Wood Cart Pride"),
            IngredientList = new()
            {
                new EMIngredient("WoodCartItem", false, 1, true),
				new EMIngredient("RedDyeItem", false, 1, true),
				new EMIngredient("OrangeDyeItem", false, 1, true),
				new EMIngredient("YellowDyeItem", false, 1, true),
				new EMIngredient("GreenDyeItem", false, 1, true),
				new EMIngredient("BlueDyeItem", false, 1, true),
				new EMIngredient("PurpleDyeItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("WoodCartPrideItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintWoodCartPrideRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintWoodCartPrideRecipe()
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
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class WoodCartPrideObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Wood Cart Pride");
        public Type RepresentedItemType => typeof(WoodCartPrideItem);

        public static VehicleModel defaults = new(
            typeof(WoodCartPrideObject),
            fuelTagList        : null,
            fuelSlots          : 0,
            fuelConsumption    : 0,
            airPollution       : 0,
            maxSpeed           : 12,
            efficencyMultiplier: 1,
            storageSlots       : 12,
            maxWeight          : 2100000
        );

        static WoodCartPrideObject()
        {
            WorldObject.AddOccupancy<WoodCartPrideObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private WoodCartPrideObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMVehicleResolver.Obj.ResolveStorageSlots(this), EMVehicleResolver.Obj.ResolveMaxWeight(this));    
            this.GetComponent<VehicleComponent>().Initialize(EMVehicleResolver.Obj.ResolveMaxSpeed(this), EMVehicleResolver.Obj.ResolveEfficiencyMultiplier(this), EMVehicleResolver.Obj.ResolveSeats(this));
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));        
        }
    }
}
