using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Modules;
using Eco.Shared.Items;
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    using System;
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
    public class PrimitivePaintingTableRecipe : RecipeFamily
    {
        public PrimitivePaintingTableRecipe()
        {
            var product = new Recipe(
                "PrimitivePaintingTable",
                Localizer.DoStr("Primitive Painting Table"),
                new IngredientElement[]
                {
                    new IngredientElement("Wood", 20, typeof(LoggingSkill)),
                    new IngredientElement("WoodBoard", 10, typeof(LoggingSkill)),
                    new IngredientElement(typeof(PlantFibersItem), 40, typeof(LoggingSkill)),
                },
                new CraftingElement<PrimitivePaintingTableItem>()
            );
            this.Recipes = new List<Recipe> { product };
            this.LaborInCalories = CreateLaborInCaloriesValue(150); 
            this.CraftMinutes = CreateCraftTimeValue(5);
            this.Initialize(Localizer.DoStr("Primitive Painting Table"), typeof(PrimitivePaintingTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
