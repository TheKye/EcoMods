namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;

    [Serialized]
    [LocDisplayName("Steam Truck DarkBlue")]
    [Weight(25000)]  
    [AirPollution(0.2f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredSteamTruck", 1)]
    public partial class SteamTruckDarkBlueItem : WorldObjectItem<SteamTruckDarkBlueObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A dark blue truck that runs on steam."); } }
    }
    
      
    public class PaintSteamTruckDarkBlueRecipe : RecipeFamily
    {
        public PaintSteamTruckDarkBlueRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Steam Truck DarkBlue",
                    Localizer.DoStr("Paint Steam Truck DarkBlue"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(SteamTruckItem), 1, true), 
                        new IngredientElement(typeof(BlueDyeItem), 24, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                        new IngredientElement(typeof(BlackDyeItem), 16, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    },
                    new CraftingElement<SteamTruckDarkBlueItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;
            this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintSteamTruckDarkBlueRecipe), 10, typeof(MechanicsSkill));    

            this.Initialize(Localizer.DoStr("Paint Steam Truck DarkBlue"), typeof(PaintSteamTruckDarkBlueRecipe));
            CraftingComponent.AddRecipe(typeof(AdvancedPaintingTableObject), this);
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
    public partial class SteamTruckDarkBlueObject : PhysicsWorldObject, IRepresentsItem
    {
        static SteamTruckDarkBlueObject()
        {
            WorldObject.AddOccupancy<SteamTruckDarkBlueObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Steam Truck DarkBlue"); } }
        public Type RepresentedItemType { get { return typeof(SteamTruckDarkBlueItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel"
        };

        private SteamTruckDarkBlueObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(24, 5000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.2f);            
            this.GetComponent<VehicleComponent>().Initialize(18, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}
