using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using System.Collections.Generic;
using GunAPI;
using SlashAPI;


namespace Weapons
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
            gun.SetAnimationFPS(gun.shootAnimation, 12);
            // 
            gun.AddProjectileModuleFrom("blasphemy", true, false);
            // 
            gun.DefaultModule.ammoCost = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.burstShotCount = 3;
            gun.DefaultModule.cooldownTime = 0.5f;
            gun.DefaultModule.burstCooldownTime = 1.2f;
            gun.DefaultModule.numberOfShotsInClip = 3;
            gun.DefaultModule.angleVariance = 0f;
            gun.SetBaseMaxAmmo(3);
            gun.barrelOffset.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            //
            gun.quality = PickupObject.ItemQuality.C;
            //
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
            //
            projectile.baseData.damage *= 1.5f;
            projectile.baseData.speed *= 1f;
			projectile.transform.parent = gun.barrelOffset;
            projectile.pierceMinorBreakables = true;
            //
            //projectile.SetProjectileSpriteRight("notlim_projectile_001", 13, 13, null, null);


            /***********************JUST FOR SLASHING WEAPONS***********************/
            //sprites and sprite handling
            VFXPool SlashVFX = VFXLibrary.CreateMuzzleflash("notlim_slash", new List<string> { "notlim_slash_001", "notlim_slash_002", "notlim_slash_003", }, 10, new List<IntVector2> { new IntVector2(15, 35), new IntVector2(15, 35), new IntVector2(15, 35), }, new List<tk2dBaseSprite.Anchor> {
            tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft}, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor });
            //add slashing, specific to alternator
            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            //yea
            slashingBehaviour.SlashVFX = SlashVFX;
            //
            slashingBehaviour.SlashDimensions = 90;
            slashingBehaviour.SlashRange = 1.5f;
            slashingBehaviour.slashKnockback = 25;
            slashingBehaviour.delayBeforeSlash = .1f;
            /*************************************************************************/

            ETGMod.Databases.Items.Add(gun, null, "ANY");
            gun.AddToSubShop(ItemBuilder.ShopType.Goopton, 1);
            gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1);
        }

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_blasphemy_shot_01", gameObject);
        }
        private bool HasReloaded;
        //
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
				AkSoundEngine.PostEvent("Play_WPN_accent_ice_01", base.gameObject);
			}
		}

        //
    }
}
