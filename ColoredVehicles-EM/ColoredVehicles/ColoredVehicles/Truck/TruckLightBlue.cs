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
    [LocDisplayName("Truck LightBlue")]
    [Weight(25000)]  
    [AirPollution(0.5f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]                  
    [Tag("ColoredTruck")]
    public partial class TruckLightBlueItem : WorldObjectItem<TruckLightBlueObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern light blue truck for hauling sizable loads."); } }
    }
    
      
    public class PaintTruckLightBlueRecipe : RecipeFamily
    {
        public PaintTruckLightBlueRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Truck LightBlue",
                    Localizer.DoStr("Paint Truck LightBlue"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(TruckItem), 1, true), 
                        new IngredientElement(typeof(BlueDyeItem), 36, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                        new IngredientElement(typeof(WhitePaintItem), 24, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                    },
                    new CraftingElement<TruckLightBlueItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(750, typeof(IndustrySkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintTruckLightBlueRecipe), 15, typeof(IndustrySkill));    

            this.Initialize(Localizer.DoStr("Paint Truck LightBlue"), typeof(PaintTruckLightBlueRecipe));
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
    public partial class TruckLightBlueObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckLightBlueObject()
        {
            WorldObject.AddOccupancy<TruckLightBlueObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Truck LightBlue"); } }
        public Type RepresentedItemType { get { return typeof(TruckLightBlueItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private TruckLightBlueObject() { }

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
