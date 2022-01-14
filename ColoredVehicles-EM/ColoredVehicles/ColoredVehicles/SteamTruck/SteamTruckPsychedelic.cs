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
    [LocDisplayName("Steam Truck Psychedelic")]
    [Weight(25000)]  
    [AirPollution(0.2f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredSteamTruck", 1)]
    public partial class SteamTruckPsychedelicItem : WorldObjectItem<SteamTruckPsychedelicObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A psychedelic truck that runs on steam.");
    }
    
    public class PaintSteamTruckPsychedelicRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSteamTruckPsychedelicRecipe).Name,
            Assembly = typeof(PaintSteamTruckPsychedelicRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Steam Truck Psychedelic",
            LocalizableName = Localizer.DoStr("Paint Steam Truck Psychedelic"),
            IngredientList = new()
            {
                new EMIngredient("SteamTruckItem", false, 1, true),
				new EMIngredient("YellowPaintItem", false, 1, true),
				new EMIngredient("GreenPaintItem", false, 1, true),
				new EMIngredient("OrangePaintItem", false, 1, true),
				new EMIngredient("PurplePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SteamTruckPsychedelicItem"),
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

        static PaintSteamTruckPsychedelicRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSteamTruckPsychedelicRecipe()
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
    public partial class SteamTruckPsychedelicObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Steam Truck Psychedelic");
        public Type RepresentedItemType => typeof(SteamTruckPsychedelicItem);

        public static VehicleModel defaults = new(
            typeof(SteamTruckPsychedelicObject),
            fuelTagList: fuelTagList,
            fuelSlots          :2,
            fuelConsumption    :25,
            airPollution       :0.5f,
            maxSpeed           :30,
            efficencyMultiplier:2,
            storageSlots       :24,
            maxWeight          :5000000,
            seats              : 2
        );

        static SteamTruckPsychedelicObject()
        {
            WorldObject.AddOccupancy<SteamTruckPsychedelicObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Burnable Fuel"
        };

        private SteamTruckPsychedelicObject() { }

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
