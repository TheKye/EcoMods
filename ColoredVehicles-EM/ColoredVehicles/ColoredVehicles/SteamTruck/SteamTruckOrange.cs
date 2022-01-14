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
    [LocDisplayName("Steam Truck Orange")]
    [Weight(25000)]  
    [AirPollution(0.2f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredSteamTruck", 1)]
    public partial class SteamTruckOrangeItem : WorldObjectItem<SteamTruckOrangeObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A orange truck that runs on steam.");
    }
    
    public class PaintSteamTruckOrangeRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSteamTruckOrangeRecipe).Name,
            Assembly = typeof(PaintSteamTruckOrangeRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Steam Truck Orange",
            LocalizableName = Localizer.DoStr("Paint Steam Truck Orange"),
            IngredientList = new()
            {
                new EMIngredient("SteamTruckItem", false, 1, true),
				new EMIngredient("OrangePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SteamTruckOrangeItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 500,
            LaborIsStatic = false,
            BaseCraftTime = 10,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(MechanicsSkill),
            RequiredSkillLevel = 0,
        };

        static PaintSteamTruckOrangeRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSteamTruckOrangeRecipe()
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
    public partial class SteamTruckOrangeObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Steam Truck Orange");
        public Type RepresentedItemType => typeof(SteamTruckOrangeItem);

        public static VehicleModel defaults = new(
            typeof(SteamTruckOrangeObject),
            displayName        : "Steam Truck Orange",
            fuelTagList        : fuelTagList,
            fuelSlots          : 2,
            fuelConsumption    : 25,
            airPollution       : 0.5f,
            maxSpeed           : 30,
            efficencyMultiplier: 2,
            storageSlots       : 24,
            maxWeight          : 5000000,
            seats              : 2
        );

        static SteamTruckOrangeObject()
        {
            WorldObject.AddOccupancy<SteamTruckOrangeObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Burnable Fuel"
        };

        private SteamTruckOrangeObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(EMVehicleResolver.Obj.ResolveStorageSlots(this), EMVehicleResolver.Obj.ResolveMaxWeight(this));           
            this.GetComponent<FuelSupplyComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelSlots(this), fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelConsumption(this));    
            this.GetComponent<AirPollutionComponent>().Initialize(EMVehicleResolver.Obj.ResolveAirPollution(this));            
            this.GetComponent<VehicleComponent>().Initialize(EMVehicleResolver.Obj.ResolveMaxSpeed(this), EMVehicleResolver.Obj.ResolveEfficiencyMultiplier(this), EMVehicleResolver.Obj.ResolveSeats(this));
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}
