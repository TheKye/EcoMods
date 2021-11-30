namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;

    [Serialized]
    [LocDisplayName("Small Wood Cart Orange")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartOrangeItem : WorldObjectItem<SmallWoodCartOrangeObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small orange cart for hauling small loads."); } }
    }

    public class PaintSmallWoodCartOrangeRecipe : RecipeFamily
    {
        public PaintSmallWoodCartOrangeRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Small Wood Cart Orange",
                    Localizer.DoStr("Paint Small  Cart Orange"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(SmallWoodCartItem), 1, true),
                        new IngredientElement(typeof(OrangePaintItem), 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    },
                    new CraftingElement<SmallWoodCartOrangeItem>()
                )
            };
            this.ExperienceOnCraft = 0.05f;
            this.LaborInCalories = CreateLaborInCaloriesValue(125, typeof(BasicEngineeringSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintSmallWoodCartOrangeRecipe), 2.5f, typeof(BasicEngineeringSkill));

            this.Initialize(Localizer.DoStr("Paint Small Wood Cart Orange"), typeof(PaintSmallWoodCartOrangeRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class SmallWoodCartOrangeObject : PhysicsWorldObject, IRepresentsItem
    {
        static SmallWoodCartOrangeObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartOrangeObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart Orange"); } }
        public Type RepresentedItemType { get { return typeof(SmallWoodCartOrangeItem); } }


        private SmallWoodCartOrangeObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(8, 1400000);           
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}
