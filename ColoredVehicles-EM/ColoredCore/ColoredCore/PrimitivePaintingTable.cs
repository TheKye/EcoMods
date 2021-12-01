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
    public class PrimitivePaintingTableObject: WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("PrimitivePaintingTable");
        
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        
        public virtual Type RepresentedItemType => typeof(PrimitivePaintingTableItem);

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));
        }
        
        static PrimitivePaintingTableObject()
        {
            AddOccupancy<PrimitivePaintingTableObject>(new List<BlockOccupancy>()
            {
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 0)),
            });
        }
    }

    [Serialized]
    [LocDisplayName("Primitive Painting Table")]
    [Ecopedia("CavRnMods", "Colored Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]                                                                           
    [AllowPluginModules(Tags = new[] { "BasicUpgrade", }, ItemTypes = new[] { typeof(BasicEngineeringUpgradeItem) })]
    public class PrimitivePaintingTableItem: WorldObjectItem<PrimitivePaintingTableObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A table used to paint wood carts");

        static PrimitivePaintingTableItem()
        {
        }
    }

    [RequiresSkill(typeof(LoggingSkill), 3)]
    public class PrimitivePaintingTableRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PrimitivePaintingTableRecipe).Name,
            Assembly = typeof(PrimitivePaintingTableRecipe).AssemblyQualifiedName,
            HiddenName = "PrimitivePaintingTable",
            LocalizableName = Localizer.DoStr("Primitive Painting Table"),
            IngredientList = new()
            {
                new EMIngredient("Wood", true, 20),
                new EMIngredient("WoodBoard", true, 10),
                new EMIngredient("PlantFibersItem", false, 40),
            },
            ProductList = new()
            {
                new EMCraftable("PrimitivePaintingTableItem"),
            },
            BaseExperienceOnCraft = 1,
            BaseLabor = 150,
            LaborIsStatic = false,
            BaseCraftTime = 5,
            CraftTimeIsStatic = false,
            CraftingStation = "REPLACEME",
            RequiredSkillType = typeof(LoggingSkill),
            RequiredSkillLevel = 3,
        };

        static PrimitivePaintingTableRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PrimitivePaintingTableRecipe()
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
