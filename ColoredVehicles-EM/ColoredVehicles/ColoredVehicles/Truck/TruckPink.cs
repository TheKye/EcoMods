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
    [LocDisplayName("Truck Pink")]
    [Weight(25000)]  
    [AirPollution(0.5f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]                  
    [Tag("ColoredTruck")]
    public partial class TruckPinkItem : WorldObjectItem<TruckPinkObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Modern pink truck for hauling sizable loads.");
    }
    
      
    public class PaintTruckPinkRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintTruckPinkRecipe).Name,
            Assembly = typeof(PaintTruckPinkRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Truck Pink",
            LocalizableName = Localizer.DoStr("Paint Truck Pink"),
            IngredientList = new()
            {
                new EMIngredient("TruckItem", false, 1, true),
				new EMIngredient("PurplePaintItem", false, 1, true),
				new EMIngredient("WhitePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("TruckPinkItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 750,
            LaborIsStatic = false,
            BaseCraftTime = 15,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(IndustrySkill),
            RequiredSkillLevel = 0,
        };

        static PaintTruckPinkRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintTruckPinkRecipe()
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
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class TruckPinkObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Truck Pink");
        public Type RepresentedItemType => typeof(TruckPinkItem);

        public static VehicleModel defaults = new(
            typeof(TruckPinkObject),
            fuelTagList        :fuelTagList,
            fuelSlots          :2,
            fuelConsumption    :25,
            airPollution       :0.5f,
            maxSpeed           :20,
            efficencyMultiplier:2,
            storageSlots       :36,
            maxWeight          :8000000,
            seats              : 2
        );

        static TruckPinkObject()
        {
            WorldObject.AddOccupancy<TruckPinkObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private TruckPinkObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(EMVehicleResolver.Obj.ResolveStorageSlots(this), EMVehicleResolver.Obj.ResolveMaxWeight(this));           
            this.GetComponent<FuelSupplyComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelSlots(this), EMVehicleResolver.Obj.ResolveFuelTagList(this));           
            this.GetComponent<FuelConsumptionComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelConsumption(this));    
            this.GetComponent<AirPollutionComponent>().Initialize(EMVehicleResolver.Obj.ResolveAirPollution(this));            
            this.GetComponent<VehicleComponent>().Initialize(EMVehicleResolver.Obj.ResolveMaxSpeed(this), EMVehicleResolver.Obj.ResolveEfficiencyMultiplier(this), EMVehicleResolver.Obj.ResolveSeats(this));
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}
