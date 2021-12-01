using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Modules;
using Eco.Shared.Items;
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.EM.Framework.Resolvers;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    public class AdvancedPaintingTableObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("AdvancedPaintingTable");

        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        public virtual Type RepresentedItemType => typeof(AdvancedPaintingTableItem);

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));
        }

        static AdvancedPaintingTableObject()
        {
            AddOccupancy<AdvancedPaintingTableObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(-1, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(1, 0, 0)),
                new BlockOccupancy(new Vector3i(-1, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(1, 1, 0)),
            });
        }
    }

    [Serialized]
    [LocDisplayName("Advanced Painting Table")]
    [Ecopedia("CavRnMods", "Colored Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    [AllowPluginModules(Tags = new[] {"AdvancedUpgrade", "ModernUpgrade"}, ItemTypes = new[] {typeof(MechanicsAdvancedUpgradeItem), typeof(IndustryUpgradeItem)})]
    public class AdvancedPaintingTableItem : WorldObjectItem<AdvancedPaintingTableObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A table used to paint steam trucks and trucks");

        static AdvancedPaintingTableItem() { }
    }

    [RequiresSkill(typeof(SmeltingSkill), 3)]
    public class AdvancedPaintingTableRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(AdvancedPaintingTableRecipe).Name,
            Assembly = typeof(AdvancedPaintingTableRecipe).AssemblyQualifiedName,
            HiddenName = "AdvancedPaintingTable",
            LocalizableName = Localizer.DoStr("Advanced Painting Table"),
            IngredientList = new()
            {
                new EMIngredient("IronBarItem", false, 30),
                new EMIngredient("Lumber", true, 10),
            },
            ProductList = new()
            {
                new EMCraftable("AdvancedPaintingTableItem"),
            },
            BaseExperienceOnCraft = 1,
            BaseLabor = 300,
            LaborIsStatic = false,
            BaseCraftTime = 15,
            CraftTimeIsStatic = false,
            CraftingStation = "AnvilItem",
            RequiredSkillType = typeof(SmeltingSkill),
            RequiredSkillLevel = 3,
            IngredientImprovementTalents = typeof(SmeltingLavishResourcesTalent),
        };

        static AdvancedPaintingTableRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public AdvancedPaintingTableRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }
}
