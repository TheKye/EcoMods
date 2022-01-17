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
    [LocDisplayName("Scooter Green")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Scooter", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredScooter")]
    public partial class ScooterGreenItem : WorldObjectItem<ScooterGreenObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A Green scooter for fast travel."); } }
    }
    
    public class PaintScooterGreenRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintScooterGreenRecipe).Name,
            Assembly = typeof(PaintScooterGreenRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Scooter Green",
            LocalizableName = Localizer.DoStr("Paint Scooter Green"),
            IngredientList = new()
            {
                new EMIngredient("ScooterItem", false, 1, true),
                new EMIngredient("GreenDyeItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("ScooterGreenItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 100,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
        };

        static PaintScooterGreenRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintScooterGreenRecipe()
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
    public partial class ScooterGreenObject : PhysicsWorldObject, IRepresentsItem
    {
        static ScooterGreenObject()
        {
            WorldObject.AddOccupancy<ScooterGreenObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Scooter Green"); } }
        public Type RepresentedItemType { get { return typeof(ScooterGreenItem); } }

        private ScooterGreenObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(18, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.125f);           
        }
    }
}
