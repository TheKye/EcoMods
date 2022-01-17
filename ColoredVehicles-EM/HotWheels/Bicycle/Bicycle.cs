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
    [LocDisplayName("Bicycle")]
    [Weight(5000)]
    [Ecopedia("CavRnMods", "Bicycle", true, InPageTooltip.DynamicTooltip)]
    public partial class BicycleItem : WorldObjectItem<BicycleObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A bicycle for fast travel.");
    }
    
    [RequiresSkill(typeof(MechanicsSkill), 1)]
    public class BicycleRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(BicycleRecipe).Name,
            Assembly = typeof(BicycleRecipe).AssemblyQualifiedName,
            HiddenName = "Bicycle",
            LocalizableName = Localizer.DoStr("Bicycle"),
            IngredientList = new()
            {
                new EMIngredient("ClothItem", false, 4),
                new EMIngredient("IronBarItem", false, 8),
                new EMIngredient("IronWheelItem", false, 2, true),
            },
            ProductList = new()
            {
                new EMCraftable("BicycleItem"),
            },
            BaseExperienceOnCraft = 10f,
            BaseLabor = 500,
            LaborIsStatic = false,
            BaseCraftTime = 5,
            CraftTimeIsStatic = false,
            CraftingStation = "MachinistTableItem",
            RequiredSkillType = typeof(MechanicsSkill),
            RequiredSkillLevel = 1,
            IngredientImprovementTalents = typeof(MechanicsLavishResourcesTalent),
        };

        static BicycleRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public BicycleRecipe()
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
    public partial class BicycleObject : PhysicsWorldObject, IRepresentsItem
    {
        static BicycleObject()
        {
            WorldObject.AddOccupancy<BicycleObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName => Localizer.DoStr("Bicycle");
        public Type RepresentedItemType => typeof(BicycleItem);

        private BicycleObject() { }

        /*private int i = 0;
         
        public override void Tick()
        {
            Vector3 vec2 = CavRnHelper.FromQ2(this.Rotation);

            i++;
            
            Log.WriteLine(new LocString("--TICK " + i + "--"));
            Log.WriteLine(new LocString("x: " + vec2.x));
            Log.WriteLine(new LocString("y: " + vec2.y));
            Log.WriteLine(new LocString("z: " + vec2.z));
            Log.WriteLine(new LocString("--------"));

            if ((vec2.x > 75 && vec2.x < 285) || (vec2.z > 120 && vec2.z < 240)) {
                VehicleComponent v = this.GetComponent<VehicleComponent>();

                if (v.Driver != null)
                {
                    Log.WriteLine(new LocString("--------"));
                    Log.WriteLine(new LocString("x: " + vec2.x));
                    Log.WriteLine(new LocString("y: " + vec2.y));
                    Log.WriteLine(new LocString("z: " + vec2.z));
                    Log.WriteLine(new LocString("--------"));
                    
                    v.Driver.InfoBoxLocStr("You have been ejected.");
                    
                    v.Dismount(v.Driver.ID);
                }
            }
        }*/

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<VehicleComponent>().Initialize(22, 1.6f, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.25f);           
        }
    }
}
