namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    
    [Serialized]
    [LocDisplayName("Powered Cart Pink")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartPinkItem : WorldObjectItem<PoweredCartPinkObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Large pink cart for hauling sizable loads."); } }
    }

    public class PaintPoweredCartPinkRecipe : RecipeFamily
    {
        public PaintPoweredCartPinkRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Powered Cart Pink",
                    Localizer.DoStr("Paint Powered Cart Pink"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(PoweredCartItem), 1, true), 
                        new IngredientElement(typeof(PinkPaintItem), 1, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),                        
                    },
                    new CraftingElement<PoweredCartPinkItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintPoweredCartPinkRecipe), 5, typeof(BasicEngineeringSkill));    

            this.Initialize(Localizer.DoStr("Paint Powered Cart Pink"), typeof(PaintPoweredCartPinkRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
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
    public partial class PoweredCartPinkObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartPinkObject()
        {
            WorldObject.AddOccupancy<PoweredCartPinkObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart Pink"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartPinkItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartPinkObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(18, 3500000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(35);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);            
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
        }
    }
}
