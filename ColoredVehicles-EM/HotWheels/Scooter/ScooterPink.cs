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
    [LocDisplayName("Scooter Pink")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Scooter", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredScooter")]
    public partial class ScooterPinkItem : WorldObjectItem<ScooterPinkObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A Pink scooter for fast travel."); } }
    }
    
    public class PaintScooterPinkRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintScooterPinkRecipe).Name,
            Assembly = typeof(PaintScooterPinkRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Scooter Pink",
            LocalizableName = Localizer.DoStr("Paint Scooter Pink"),
            IngredientList = new()
            {
                new EMIngredient("ScooterItem", false, 1, true),
                new EMIngredient("PinkDyeItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("ScooterPinkItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 100,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
        };

        static PaintScooterPinkRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintScooterPinkRecipe()
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
    public partial class ScooterPinkObject : PhysicsWorldObject, IRepresentsItem
    {
        static ScooterPinkObject()
        {
            WorldObject.AddOccupancy<ScooterPinkObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Scooter Pink"); } }
        public Type RepresentedItemType { get { return typeof(ScooterPinkItem); } }

        private ScooterPinkObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(18, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.125f);           
        }
    }
}
