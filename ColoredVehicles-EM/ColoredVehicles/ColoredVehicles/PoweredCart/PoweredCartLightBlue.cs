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
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    
    [Serialized]
    [LocDisplayName("Powered Cart LightBlue")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartLightBlueItem : WorldObjectItem<PoweredCartLightBlueObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Large light blue cart for hauling sizable loads.");
    }

    public class PaintPoweredCartLightBlueRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintPoweredCartLightBlueRecipe).Name,
            Assembly = typeof(PaintPoweredCartLightBlueRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Powered Cart Light Blue",
            LocalizableName = Localizer.DoStr("Paint Powered Cart Light Blue"),
            IngredientList = new()
            {
                new EMIngredient("PoweredCartItem", false, 1, true),
                new EMIngredient("BluePaintItem", false, 1, true),
                new EMIngredient("WhitePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartLightBlueItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 5,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintPoweredCartLightBlueRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintPoweredCartLightBlueRecipe()
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
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartLightBlueObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartLightBlueObject()
        {
            WorldObject.AddOccupancy<PoweredCartLightBlueObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart LightBlue"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartLightBlueItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartLightBlueObject() { }

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
