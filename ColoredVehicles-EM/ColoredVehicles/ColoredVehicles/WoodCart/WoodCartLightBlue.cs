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
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Wood Cart LightBlue")]
    [Weight(15000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredWoodCart")]
    public partial class WoodCartLightBlueItem : WorldObjectItem<WoodCartLightBlueObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small light blue cart for hauling small loads.");
    }

    public class PaintWoodCartLightBlueRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintWoodCartLightBlueRecipe).Name,
            Assembly = typeof(PaintWoodCartLightBlueRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Wood Cart LightBlue",
            LocalizableName = Localizer.DoStr("Paint Wood Cart LightBlue"),
            IngredientList = new()
            {
                new EMIngredient("WoodCartItem", false, 1, true),
				new EMIngredient("BluePaintItem", false, 1, true),
				new EMIngredient("WhitePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("WoodCartLightBlueItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintWoodCartLightBlueRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintWoodCartLightBlueRecipe()
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
    [RequireComponent(typeof(ModularStockpileComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class WoodCartLightBlueObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Wood Cart LightBlue");
        public Type RepresentedItemType => typeof(WoodCartLightBlueItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(WoodCartLightBlueItem)) { StorageSlots = 12, };

        static WoodCartLightBlueObject()
        {
            WorldObject.AddOccupancy<WoodCartLightBlueObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private WoodCartLightBlueObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 2100000);           
            this.GetComponent<VehicleComponent>().Initialize(12, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));        
        }
    }
}
