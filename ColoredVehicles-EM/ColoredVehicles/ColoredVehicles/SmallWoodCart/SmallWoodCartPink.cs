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
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Small Wood Cart Pink")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartPinkItem : WorldObjectItem<SmallWoodCartPinkObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small pink cart for hauling small loads.");
    }

    public class PaintSmallWoodCartPinkRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSmallWoodCartPinkRecipe).Name,
            Assembly = typeof(PaintSmallWoodCartPinkRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Small Wood Cart Pink",
            LocalizableName = Localizer.DoStr("Paint Small Wood Cart Pink"),
            IngredientList = new()
            {
                new EMIngredient("SmallWoodCartItem", false, 1, true),
				new EMIngredient("PurplePaintItem", false, 1, true),
				new EMIngredient("WhitePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartPinkItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 125,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintSmallWoodCartPinkRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSmallWoodCartPinkRecipe()
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
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class SmallWoodCartPinkObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Small Wood Cart Pink");
        public Type RepresentedItemType => typeof(SmallWoodCartPinkItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(SmallWoodCartPinkObject)) { StorageSlots = 8, };

        static SmallWoodCartPinkObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartPinkObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private SmallWoodCartPinkObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 1400000);           
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}
