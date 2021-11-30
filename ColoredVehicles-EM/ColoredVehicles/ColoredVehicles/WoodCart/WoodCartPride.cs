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
    [LocDisplayName("Wood Cart Pride")]
    [Weight(15000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredWoodCart")]
    public partial class WoodCartPrideItem : WorldObjectItem<WoodCartPrideObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small pride cart for hauling small loads."); } }
    }

    public class PaintWoodCartPrideRecipe : RecipeFamily
    {
        public PaintWoodCartPrideRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Wood Cart Pride",
                    Localizer.DoStr("Paint Wood Cart Pride"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(WoodCartItem), 1, true),
                        new IngredientElement(typeof(RedPaintItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                        new IngredientElement(typeof(OrangePaintItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                        new IngredientElement(typeof(YellowPaintItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                        new IngredientElement(typeof(GreenPaintItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                        new IngredientElement(typeof(BlueDyeItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                        new IngredientElement(typeof(PurplePaintItem), 4, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    },
                    new CraftingElement<WoodCartPrideItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintWoodCartPrideRecipe), 5, typeof(BasicEngineeringSkill));    

            this.Initialize(Localizer.DoStr("Paint Wood Cart Pride"), typeof(PaintWoodCartPrideRecipe));
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
    public partial class WoodCartPrideObject : PhysicsWorldObject, IRepresentsItem
    {
        static WoodCartPrideObject()
        {
            WorldObject.AddOccupancy<WoodCartPrideObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart Pride"); } }
        public Type RepresentedItemType { get { return typeof(WoodCartPrideItem); } }


        private WoodCartPrideObject() { }

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
