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
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    
    [Serialized]
    [LocDisplayName("Powered Cart White")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartWhiteItem : WorldObjectItem<PoweredCartWhiteObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Large white cart for hauling sizable loads.");
    }

    public class PaintPoweredCartWhiteRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintPoweredCartWhiteRecipe).Name,
            Assembly = typeof(PaintPoweredCartWhiteRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Powered Cart White",
            LocalizableName = Localizer.DoStr("Paint Powered Cart White"),
            IngredientList = new()
            {
                new EMIngredient("PoweredCartItem", false, 1, true),
				new EMIngredient("WhitePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartWhiteItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };
        
        static PaintPoweredCartWhiteRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintPoweredCartWhiteRecipe()
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
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartWhiteObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Powered Cart White");
        public Type RepresentedItemType => typeof(PoweredCartWhiteItem);

        public static VehicleModel defaults = new(
            typeof(PoweredCartWhiteObject),
            fuelTagList: fuelTagList,
            fuelSlots          : 2,
            fuelConsumption    : 35,
            airPollution       : 0.1f,
            maxSpeed           : 12,
            efficencyMultiplier: 1.5f,
            storageSlots       : 18,
            maxWeight          : 3500000,
            seats              : 1
        );

        static PoweredCartWhiteObject()
        {
            WorldObject.AddOccupancy<PoweredCartWhiteObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartWhiteObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(EMVehicleResolver.Obj.ResolveStorageSlots(this), EMVehicleResolver.Obj.ResolveMaxWeight(this));           
            this.GetComponent<FuelSupplyComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelSlots(this), EMVehicleResolver.Obj.ResolveFuelTagList(this));           
            this.GetComponent<FuelConsumptionComponent>().Initialize(EMVehicleResolver.Obj.ResolveFuelConsumption(this));    
            this.GetComponent<AirPollutionComponent>().Initialize(EMVehicleResolver.Obj.ResolveAirPollution(this));            
            this.GetComponent<VehicleComponent>().Initialize(EMVehicleResolver.Obj.ResolveMaxSpeed(this), EMVehicleResolver.Obj.ResolveEfficiencyMultiplier(this), EMVehicleResolver.Obj.ResolveSeats(this));
        }
    }
}
