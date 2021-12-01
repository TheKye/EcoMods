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
    [LocDisplayName("Small Wood Cart Black")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartBlackItem : WorldObjectItem<SmallWoodCartBlackObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small black cart for hauling small loads.");
    }

    public class PaintSmallWoodCartBlackRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSmallWoodCartBlackRecipe).Name,
            Assembly = typeof(PaintSmallWoodCartBlackRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Small Wood Cart Black",
            LocalizableName = Localizer.DoStr("Paint Small Wood Cart Black"),
            IngredientList = new()
            {
                new EMIngredient("SmallWoodCartItem", false, 1, true),
                new EMIngredient("BlackDyeItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartBlackItem"),
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

        static PaintSmallWoodCartBlackRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSmallWoodCartBlackRecipe()
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
    public partial class SmallWoodCartBlackObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Small Wood Cart Black");
        public Type RepresentedItemType => typeof(SmallWoodCartBlackItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(PoweredCartBlackObject)) { StorageSlots = 8, };

        static SmallWoodCartBlackObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartBlackObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private SmallWoodCartBlackObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 1400000);           
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}
