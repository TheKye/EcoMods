namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;

    [Serialized]
    [LocDisplayName("Powered Cart Orange")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartOrangeItem : WorldObjectItem<PoweredCartOrangeObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Large orange cart for hauling sizable loads.");
    }

    public class PaintPoweredCartOrangeRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintPoweredCartOrangeRecipe).Name,
            Assembly = typeof(PaintPoweredCartOrangeRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Powered Cart Orange",
            LocalizableName = Localizer.DoStr("Paint Powered Cart Orange"),
            IngredientList = new()
            {
                new EMIngredient("PoweredCartItem", false, 1, true),
                new EMIngredient("OrangePaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartOrangeItem"),
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

        static PaintPoweredCartOrangeRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintPoweredCartOrangeRecipe()
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
    public partial class PoweredCartOrangeObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartOrangeObject()
        {
            WorldObject.AddOccupancy<PoweredCartOrangeObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart Orange"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartOrangeItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartOrangeObject() { }

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
