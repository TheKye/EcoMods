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
    [LocDisplayName("Truck Pink")]
    [Weight(25000)]  
    [AirPollution(0.5f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]                  
    [Tag("ColoredTruck")]
    public partial class TruckPinkItem : WorldObjectItem<TruckPinkObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern pink truck for hauling sizable loads."); } }
    }
    
      
    public class PaintTruckPinkRecipe : RecipeFamily
    {
        public PaintTruckPinkRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Truck Pink",
                    Localizer.DoStr("Paint Truck Pink"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(TruckItem), 1, true), 
                        new IngredientElement(typeof(PurplePaintItem), 36, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                        new IngredientElement(typeof(WhitePaintItem), 24, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                    },
                    new CraftingElement<TruckPinkItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(750, typeof(IndustrySkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintTruckPinkRecipe), 15, typeof(IndustrySkill));    

            this.Initialize(Localizer.DoStr("Paint Truck Pink"), typeof(PaintTruckPinkRecipe));
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
    public partial class TruckPinkObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckPinkObject()
        {
            WorldObject.AddOccupancy<TruckPinkObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Truck Pink"); } }
        public Type RepresentedItemType { get { return typeof(TruckPinkItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private TruckPinkObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(36, 8000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);            
            this.GetComponent<VehicleComponent>().Initialize(20, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}
