using Eco.Gameplay.Skills;

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Scooter")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Scooter", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredScooter")]
    public partial class ScooterItem : WorldObjectItem<ScooterObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A scooter for fast travel."); } }
    }
    
    [RequiresSkill(typeof(BasicEngineeringSkill), 2)]
    public class ScooterRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(ScooterRecipe).Name,
            Assembly = typeof(ScooterRecipe).AssemblyQualifiedName,
            HiddenName = "Scooter",
            LocalizableName = Localizer.DoStr("Scooter"),
            IngredientList = new()
            {
                new EMIngredient("ClothItem", false, 2),
                new EMIngredient("IronBarItem", false, 4),
                new EMIngredient("IronWheelItem", false, 2, true),
            },
            ProductList = new()
            {
                new EMCraftable("ScooterItem"),
            },
            BaseExperienceOnCraft = 5f,
            BaseLabor = 300,
            LaborIsStatic = false,
            BaseCraftTime = 3,
            CraftTimeIsStatic = false,
            CraftingStation = "WainwrightTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 2,
            IngredientImprovementTalents = typeof(BasicEngineeringLavishResourcesTalent),
        };

        static ScooterRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public ScooterRecipe()
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
    [RequireComponent(typeof(VehicleComponent))]
    public partial class ScooterObject : PhysicsWorldObject, IRepresentsItem
    {
        static ScooterObject()
        {
            WorldObject.AddOccupancy<ScooterObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Scooter"); } }
        public Type RepresentedItemType { get { return typeof(ScooterItem); } }

        private ScooterObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(18, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.125f);           
        }
    }
}
