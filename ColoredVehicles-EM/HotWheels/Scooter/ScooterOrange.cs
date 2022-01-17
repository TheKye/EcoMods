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
    [LocDisplayName("Scooter Orange")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Scooter", true, InPageTooltip.DynamicTooltip)]
    [Tag("ColoredScooter")]
    public partial class ScooterOrangeItem : WorldObjectItem<ScooterOrangeObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A Orange scooter for fast travel."); } }
    }
    
    public class PaintScooterOrangeRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintScooterOrangeRecipe).Name,
            Assembly = typeof(PaintScooterOrangeRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Scooter Orange",
            LocalizableName = Localizer.DoStr("Paint Scooter Orange"),
            IngredientList = new()
            {
                new EMIngredient("ScooterItem", false, 1, true),
                new EMIngredient("OrangeDyeItem", false, 1, true)
            },
            ProductList = new()
            {
                new EMCraftable("ScooterOrangeItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 100,
            LaborIsStatic = false,
            BaseCraftTime = 2.5f,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
        };

        static PaintScooterOrangeRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintScooterOrangeRecipe()
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
    public partial class ScooterOrangeObject : PhysicsWorldObject, IRepresentsItem
    {
        static ScooterOrangeObject()
        {
            WorldObject.AddOccupancy<ScooterOrangeObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Scooter Orange"); } }
        public Type RepresentedItemType { get { return typeof(ScooterOrangeItem); } }

        private ScooterOrangeObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(18, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.125f);           
        }
    }
}
