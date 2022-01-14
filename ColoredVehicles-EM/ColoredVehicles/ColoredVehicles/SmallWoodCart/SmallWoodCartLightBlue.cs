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
    [LocDisplayName("Small Wood Cart LightBlue")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]    
    [Tag("ColoredSmallWoodCart")]
    public partial class SmallWoodCartLightBlueItem : WorldObjectItem<SmallWoodCartLightBlueObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Small light blue cart for hauling small loads.");
    }

    public class PaintSmallWoodCartLightBlueRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintSmallWoodCartLightBlueRecipe).Name,
            Assembly = typeof(PaintSmallWoodCartLightBlueRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Small Wood Cart LightBlue",
            LocalizableName = Localizer.DoStr("Paint Small Wood Cart LightBlue"),
            IngredientList = new()
            {
                new EMIngredient("SmallWoodCartItem", false, 1, true),
				new EMIngredient("BlueDyeItem", false, 1, true),
				new EMIngredient("WhiteDyeItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartLightBlueItem"),
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

        static PaintSmallWoodCartLightBlueRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintSmallWoodCartLightBlueRecipe()
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
    public partial class SmallWoodCartLightBlueObject : PhysicsWorldObject, IRepresentsItem, IConfigurableVehicle
    {
        public override LocString DisplayName => Localizer.DoStr("Small Wood Cart LightBlue");
        public Type RepresentedItemType => typeof(SmallWoodCartLightBlueItem);

        public static VehicleModel defaults = new(
            typeof(SmallWoodCartLightBlueObject),
            fuelTagList        : null,
            fuelSlots          : 0,
            fuelConsumption    : 0,
            airPollution       : 0,
            maxSpeed           : 10,
            efficencyMultiplier: 1,
            storageSlots       : 8,
            maxWeight          : 1400000
        ); 

        static SmallWoodCartLightBlueObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartLightBlueObject>(new List<BlockOccupancy>(0));
            EMVehicleResolver.AddDefaults(defaults);
        }

        private SmallWoodCartLightBlueObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMVehicleResolver.Obj.ResolveStorageSlots(this), EMVehicleResolver.Obj.ResolveMaxWeight(this));    
            this.GetComponent<VehicleComponent>().Initialize(EMVehicleResolver.Obj.ResolveMaxSpeed(this), EMVehicleResolver.Obj.ResolveEfficiencyMultiplier(this), EMVehicleResolver.Obj.ResolveSeats(this));
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}
