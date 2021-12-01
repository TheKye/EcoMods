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
    [LocDisplayName("Small Wood Cart Jaguar")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartJaguarItem : WorldObjectItem<SmallWoodCartJaguarObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small jaguar cart for hauling small loads.");
    }

    public class PaintSmallWoodCartJaguarRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSmallWoodCartJaguarRecipe).Name,
            Assembly = typeof(PaintSmallWoodCartJaguarRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Small Wood Cart Jaguar",
            LocalizableName = Localizer.DoStr("Paint Small Wood Cart Jaguar"),
            IngredientList = new()
            {
                new EMIngredient("SmallWoodCartItem", false, 1, true),
                new EMIngredient("BlackDyeItem", false, 1, true),
                new EMIngredient("YellowDyeItem", false, 1, true),
                new EMIngredient("OrangeDyeItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartJaguarItem"),
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

        static PaintSmallWoodCartJaguarRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSmallWoodCartJaguarRecipe()
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
    public partial class SmallWoodCartJaguarObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Small Wood Cart Jaguar");
        public Type RepresentedItemType => typeof(SmallWoodCartJaguarItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(PoweredCartJaguarObject)) { StorageSlots = 8, };

        static SmallWoodCartJaguarObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartJaguarObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private SmallWoodCartJaguarObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 1400000);
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);
        }
    }
}
