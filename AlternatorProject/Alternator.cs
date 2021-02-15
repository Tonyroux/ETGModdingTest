using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using System.Collections.Generic;


namespace Alternator
{
    
    public class Alternator : GunBehaviour
    {
  
     
        public static void Add()
        {
            //
            Gun gun = ETGMod.Databases.Items.NewGun("Alternator", "notlim");
            // 
            Game.Items.Rename("outdated_gun_mods:alternator", "pm:alternator");
            gun.gameObject.AddComponent<Alternator>();
            //
            gun.SetShortDescription("True current");
            gun.SetLongDescription("A fitting weapon for a subtle hero with a hidden story.");
            //
            gun.SetupSprite(null, "notlim_idle_001", 8);
            // 
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            // 
            gun.AddProjectileModuleFrom("38_special", true, false);
            // 
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = 350;
            gun.DefaultModule.angleVariance = 0f;
            gun.SetBaseMaxAmmo(350);
            //
            gun.quality = PickupObject.ItemQuality.B;
            //
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
            //
            projectile.baseData.damage *= 1.0f;
            projectile.baseData.speed *= 1f;
			projectile.transform.parent = gun.barrelOffset;
            //
			projectile.SetProjectileSpriteRight("notlim_projectile_001", 5, 5, null, null);
            

            /***********************JUST FOR SLASHING WEAPONS***********************
            //add slashing, specific to alternator
            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            slashingBehaviour.SlashDimensions = 65;
            slashingBehaviour.SlashRange = 2.5f;
            slashingBehaviour.delayBeforeSlash = .1f;
            //sprites and sprite handling
            VFXPool SlashVFX = VFXLibrary.CreateMuzzleflash("notlim_slash", new List<string> { "notlim_slash_001", "notlim_slash_002", "notlim_slash_003", }, 10, new List<IntVector2> { new IntVector2(72, 67), new IntVector2(72, 67), new IntVector2(72, 67), }, new List<tk2dBaseSprite.Anchor> {
            tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft}, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor });
            //yea
            slashingBehaviour.SlashVFX = SlashVFX;
            *************************************************************************/

            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_smileyrevolver_shot_01", gameObject);
        }
        private bool HasReloaded;
        //This block of code allows us to change the reload sounds.
       protected void Update()
		{
			if (gun.CurrentOwner)
			{
			
				if (!gun.PreventNormalFireAudio)
				{
					this.gun.PreventNormalFireAudio = true;
				}
				if (!gun.IsReloading && !HasReloaded)
				{
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_SAA_reload_01", base.gameObject);
			}
		}

        //All that's left now is sprite stuff. 
        //Your sprites should be organized, like how you see in the mod folder. 
        //Every gun requires that you have a .json to match the sprites or else the gun won't spawn at all
        //.Json determines the hand sprites for your character. You can make a gun two handed by having both "SecondaryHand" and "PrimaryHand" in the .json file, which can be edited through Notepad or Visual Studios
        //By default this gun is a one-handed weapon
        //If you need a basic two handed .json. Just use the jpxfrd2.json.
        //And finally, don't forget to add your Gun to your ETGModule class!
    }
}
