using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Powers;
using Il2CppAssets.Scripts.Models.Profile;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Upgrades;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.StoreMenu;
using Il2CppAssets.Scripts.Unity.UI_New.Upgrade;
using Il2CppAssets.Scripts.Utils;
using Harmony;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using System.Net;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;

using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Unity.UI_New.Main.MonkeySelect;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Unity.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts;
using BTD_Mod_Helper.Api.Display;

namespace GlueSprayer;

public class GlueSprayer : ModTower
{

    public override TowerSet TowerSet => TowerSet.Support;

    public override string BaseTower => "DartlingGunner";

    public override int Cost => 350;

    public override int TopPathUpgrades => 5;

    public override int MiddlePathUpgrades => 2;

    public override int BottomPathUpgrades => 3;

    public override string Description => "Someone strapped a glue tank to a railgun.";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.display = Game.instance.model.GetTowerFromId("DartlingGunner").display;
        towerModel.icon = Game.instance.model.GetTowerFromId("GlueGunner").portrait;
        towerModel.portrait = Game.instance.model.GetTowerFromId("GlueGunner").portrait;

        var attackModel = towerModel.GetAttackModel();
         attackModel.weapons[0].projectile = (Game.instance.model.GetTowerFromId("GlueGunner").GetAttackModel().weapons[0].projectile.Duplicate());
        //attackModel.weapons[0].projectile.display = (Game.instance.model.GetTowerFromId("GlueGunner-300").GetAttackModel().weapons[0].projectile.display);
    }


}
public class Speedier_Gunning : ModUpgrade<GlueSprayer>
{
    public override string Icon =>   "0ddd8752be0d3554cb0db6abe6686e8e" ;
    public override string Name => "Speedier_Gunning";
    public override string DisplayName => "Speedier Gunning";
    public override string Description => "Send the Monkey to a military Academy. Increases attack speed by 50%.";

    public override int Path => TOP;

    public override int Tier => 1;

    public override int Cost => 250;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        foreach (var weaponModel in towerModel.GetWeapons())
        {
            weaponModel.Rate *= .66666666666666666666666666666f;
        }
    }
}
public class Doublier_Gunning : ModUpgrade<GlueSprayer>
{
    public override string Icon => VanillaSprites.FasterBarrelSpinUpgradeIcon;
    public override string Name => "Doublier_Gunning";
    public override string DisplayName => "Doublier Gunning";
    public override string Description => "Add another barrel to the railgun. Adds a second glue projectile.";

    public override int Path => TOP;

    public override int Tier => 2;

    public override int Cost => 250;

    public override void ApplyUpgrade(TowerModel towerModel)
    {

        var attackModel = towerModel.GetAttackModel();
        attackModel.AddWeapon(attackModel.weapons[0]);

    }
}
public class Faster_Hands : ModUpgrade<GlueSprayer>
{
    public override string Icon => "MonkeyIcons[DartlingGunnerIcon]";
    public override string Portrait => "dd1581ea4a0692b449d6c50f45195922";
    public override string Name => "Faster_Hands";
    public override string DisplayName => "Faster Hands";
    public override string Description => "Hire a Monkey to spin the barrel (Their hands might hurt a bit). 1.5 times attack speed and rotates the gunner faster.";

    public override int Path => TOP;

    public override int Tier => 3;

    public override int Cost => 2500;

    public override void ApplyUpgrade(TowerModel towerModel)
    {

        var attackModel = towerModel.GetAttackModel();
        var weapon = attackModel.weapons[0];
        var projectileModel = weapon.projectile;

        attackModel.RemoveBehavior<RotateToPointerModel>();
        //       towerModel.AddBehavior<RotateToPointerModel>().rate=7f;
        //towerModel.RemoveBehavior
        attackModel.AddBehavior(new RotateToPointerModel("RotateToPointerModel_", 180.0f, true, 7.0f));


        foreach (var weaponModel in towerModel.GetWeapons())
        {

            weaponModel.Rate *= .66f;
        }
        //need rotation
    }
}
public class Glue_Tsunami : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Glue_Tsunami";
    public override string DisplayName => "Glue Tsunami";
    public override string Description => "A gluefull downpour! Doubles attack speed again.";

    public override int Path => TOP;

    public override int Tier => 4;

    public override int Cost => 15000;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        towerModel.display = Game.instance.model.GetTowerFromId("DartlingGunner-300").display;


        for (int i = 0;i<3;i++)
        {
            attackModel.AddWeapon(attackModel.weapons[0]);

        }
        foreach (var weaponModel in towerModel.GetWeapons())
        {
        //    weaponModel.Rate *= .5f;
        }
        //need rotation
    }
}
public class The_Mother_Load : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "The_Mother_Load";
    public override string DisplayName => "The Mother Load";
    public override string Description => "TOO MUCH GLUE!";

    public override int Path => TOP;

    public override int Tier => 5;

    public override int Cost => 37800;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        towerModel.display = Game.instance.model.GetTowerFromId("DartlingGunner-400").display;



        foreach (var weaponModel in towerModel.GetWeapons())
        {
            weaponModel.AddBehavior(new LeakDangerAttackSpeedModel("LeakDangerAttackSpeedModel_", 20f));
        }
        //need rotation
    }
}
// mother of all globs
public class Globbier_Globs : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Globbier_Globs";
    public override string DisplayName => "Globbier Globs";
    public override string Description => "Fast Glue Pump = more glue. Increaces projectile size and damage.";

    public override int Path => MIDDLE;

    public override int Tier => 1;

    public override int Cost => 250;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        foreach (var weaponModel in towerModel.GetWeapons())
        {

            weaponModel.projectile.AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetDamageModel());
         weaponModel.projectile.scale *= (float) 2;
            weaponModel.projectile.radius *= (float)2;
       //     weaponModel.projectile.AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetAttackModel().weapons[0].projectile.GetDamageModel().Duplicate());
        }
    }
}
public class Corrosive_Globs : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Corrosive_Globs";
    public override string DisplayName => "Corrosive Globs";
    public override string Description => "Replace Glue with corrosiver glue. Glue does DOT.";

    public override int Path => MIDDLE;

    public override int Tier => 2;

    public override int Cost => 1250;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        //var dot = Game.instance.model.GetTowerFromId("GlueGunner-300").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<DamageOverTimeModel>();
        //attackModel.AddBehavior(dot);
        var attackModel = towerModel.GetAttackModel();

        /*attackModel.weapons[0].projectile.RemoveBehavior<SlowModel>();
        attackModel.weapons[0].projectile.RemoveBehavior<AddBehaviorToBloonModel>();
        attackModel.weapons[0].projectile.RemoveBehavior<SlowModifierForTagModel>();
        attackModel.weapons[0].projectile.RemoveBehavior<CreateSoundOnProjectileCollisionModel>();


        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("GlueGunner-200").GetAttackModel().weapons[0].projectile.GetBehavior<SlowModel>().Duplicate());
        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("GlueGunner-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("GlueGunner-200").GetAttackModel().weapons[0].projectile.GetBehavior<SlowModifierForTagModel>().Duplicate());
        foreach (var beh in Game.instance.model.GetTowerFromId("GlueGunner-200").GetAttackModel().weapons[0].projectile.GetBehaviors<AddBehaviorToBloonModel>())
        {
            attackModel.weapons[0].projectile.AddBehavior(beh.Duplicate());
        }
        attackModel.weapons[0].projectile.RemoveBehavior<DamageModel>();
        attackModel.weapons[0].projectile.RemoveBehavior<SetSpriteFromPierceModel>();

        //attackModel.weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>().AddBehavior(new DamageOverTimeModel("DamageOverTimeModel_", 1, .65f, 0, null, -1.0f, false, ObjectId.FromString(Name), false, 0.0f, false, false, true, null));
        */
        foreach (var weaponModel in attackModel.weapons)
        {            
            weaponModel.projectile.display = (Game.instance.model.GetTowerFromId("GlueGunner-300").GetAttackModel().weapons[0].projectile.display);

            weaponModel.projectile.RemoveBehavior<SlowModel>();
            weaponModel.projectile.AddBehavior(Game.instance.model.GetTowerFromId("GlueGunner-300").GetWeapon().projectile.GetBehavior<SlowModel>().Duplicate());
            //weaponModel.projectile.GetBehavior<AddBehaviorToBloonModel>().AddBehavior(Game.instance.model.GetTowerFromId("GlueGunner-300").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<DamageOverTimeModel>());
            //weaponModel.projectile.GetBehavior<AddBehaviorToBloonModel>().AddBehavior(new DamageOverTimeModel("DamageOverTimeModel_",1,.65f,0,null,-1.0f,false, ObjectId.FromString(Game.instance.model.GetTowerFromId("GlueGunner-300").baseId), false, 0.0f,false,false,true,null));
            //weaponModel.projectile.RemoveBehaviors<AddBehaviorToBloonModel>();
            //weaponModel.projectile.GetBehavior<AddBehaviorToBloonModel>().RemoveBehavior<DamageOverTimeModel>();
            //weaponModel.projectile.AddBehavior<DamageOverTimeModel>(Game.instance.model.GetTowerFromId("GlueGunner-402").GetAttackModel().weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>())
            //foreach (var beh in Game.instance.model.GetTowerFromId("GlueGunner-300").GetAttackModel().weapons[0].projectile.GetBehaviors<AddBehaviorToBloonModel>())
            //{
            //    weaponModel.projectile.AddBehavior(beh.Duplicate());

            //}
            //weaponModel.projectile.RemoveBehavior<DamageModel>();

            

            //  dot.lifespan = 5;
            //weaponModel.projectile.RemoveBehaviors<DamageOverTimeModel>();
            //weaponModel.projectile.AddBehavior(dot);
        }

    }
    
}

public class Precise_Glob : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Precise_Glob";
    public override string DisplayName => "Precise Glob";
    public override string Description => "Glue comes out slower but applies glue to more bloons. Attack speed decreased by 50%, but pierce multiplied by 4x.";

    public override int Path => BOTTOM;

    public override int Tier => 1;

    public override int Cost => 650;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        //attackModel.AddBehavior(dot);
        foreach (var weaponModel in attackModel.weapons)
        {
            weaponModel.rate *= 2;
            weaponModel.projectile.pierce = 4;

        }

    }
}
public class Keen_Eyes : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Keen_Eyes";
    public override string DisplayName => "Keen Eyes";
    public override string Description => "Laser eye surgery allows monkey to see camo bloons.";

    public override int Path => BOTTOM;

    public override int Tier => 2;

    public override int Cost => 300;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        //attackModel.AddBehavior(dot);
        towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
        foreach (var weaponModel in attackModel.weapons)
        {

        }

    }
}
public class Quicker_Loads : ModUpgrade<GlueSprayer>
{
    public override string Icon => "CrossbowUpgradeIcon.png";
    public override string Name => "Quicker_Loads";
    public override string DisplayName => "Quicker Loads";
    public override string Description => "Add some rockets to the glue. Increases projectile speed and range.";

    public override int Path => BOTTOM;

    public override int Tier => 3;

    public override int Cost => 1500;

    public override void ApplyUpgrade(TowerModel towerModel)
    {


        towerModel.display = Game.instance.model.GetTowerFromId("DartlingGunner-300").display;

        var thingy = towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>();
        thingy.speed = 750f;
        thingy.lifespan = 1f;




        //need rotation
    }
}