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
    [LocDisplayName("Wood Cart Green")]
    [Weight(15000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredWoodCart")]
    public partial class WoodCartGreenItem : WorldObjectItem<WoodCartGreenObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small green cart for hauling small loads."); } }
    }

    public class PaintWoodCartGreenRecipe : RecipeFamily
    {
        public PaintWoodCartGreenRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Wood Cart Green",
                    Localizer.DoStr("Paint Wood Cart Green"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(WoodCartItem), 1, true),
                        new IngredientElement(typeof(GreenPaintItem), 20, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    },
                    new CraftingElement<WoodCartGreenItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintWoodCartGreenRecipe), 5, typeof(BasicEngineeringSkill));    

            this.Initialize(Localizer.DoStr("Paint Wood Cart Green"), typeof(PaintWoodCartGreenRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class WoodCartGreenObject : PhysicsWorldObject, IRepresentsItem
    {
        static WoodCartGreenObject()
        {
            WorldObject.AddOccupancy<WoodCartGreenObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart Green"); } }
        public Type RepresentedItemType { get { return typeof(WoodCartGreenItem); } }


        private WoodCartGreenObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(12, 2100000);           
            this.GetComponent<VehicleComponent>().Initialize(12, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));  
        }
    }
}
