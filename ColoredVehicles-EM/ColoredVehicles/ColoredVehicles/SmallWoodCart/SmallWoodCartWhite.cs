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
    using Eco.EM.Framework.Resolvers;

    [Serialized]
    [LocDisplayName("Small Wood Cart White")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartWhiteItem : WorldObjectItem<SmallWoodCartWhiteObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small white cart for hauling small loads.");
    }

    public class PaintSmallWoodCartWhiteRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSmallWoodCartWhiteRecipe).Name,
            Assembly = typeof(PaintSmallWoodCartWhiteRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Small Wood Cart White",
            LocalizableName = Localizer.DoStr("Paint Small Wood Cart White"),
            IngredientList = new()
            {
                new EMIngredient("SmallWoodCartItem", false, 1, true),
                new EMIngredient("WhiteDyeItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartWhiteItem"),
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

        static PaintSmallWoodCartWhiteRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSmallWoodCartWhiteRecipe()
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
    public partial class SmallWoodCartWhiteObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Small Wood Cart White");
        public Type RepresentedItemType => typeof(SmallWoodCartWhiteItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(PoweredCartWhiteObject)) { StorageSlots = 8, };

        static SmallWoodCartWhiteObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartWhiteObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private SmallWoodCartWhiteObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 1400000);
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);
        }
    }
}
