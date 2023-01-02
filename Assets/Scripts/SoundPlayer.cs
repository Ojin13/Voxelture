using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer PlayerSoundController;
    public AudioSource Source;
    public AudioClip clip__footStep;
    public AudioClip clip_punchAir;
    public AudioClip clip_jump;
    public AudioClip clip_land;
    public AudioClip clip_death;
    public AudioClip clip_takeItem;
    public AudioClip clip_drawWeapon;
    public AudioClip clip_swallow;
    public AudioClip clip_exolosion;
    public AudioClip clip_bolt;
    public AudioClip clip_thunder;
    public AudioClip clip_levelUp;
    public AudioClip[] HungrySounds;
    public AudioClip[] HurtSounds;

    public AudioClip[] Enemy_RandomSound;
    public AudioClip[] Enemy_HurtSound;

    public AudioClip[] WeaponSlash;


    private bool player_death_sound_already_played = false;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        if (gameObject.CompareTag("Player"))
        {
            PlayerSoundController = this;
        }
    }


    /*
     * Footsteps
     */
    private int stepSoundCount = 0;
    public void sound__FootStep()
    {
        if (GetComponent<SpeedCalculator>().PlayerCurrentSpeed >= 2)
        {
            if (stepSoundCount < 1 && GetComponent<GroundedChecker>().CollisionCount >= 1)
            {
                stepSoundCount++;
                Source.PlayOneShot(clip__footStep, 0.1f);
                Invoke("finishStepSound",clip__footStep.length/2);
            }
        }
    }

    public void finishStepSound()
    {
        stepSoundCount--;
    }
    
    /*
     * Punch
     */

    public void sound_punchAir()
    {
        Source.PlayOneShot(clip_punchAir, 0.5f);
    }
    
    /*
     * Jump
     */

    public void sound_jump()
    {
        Source.PlayOneShot(clip_jump,1);
    }
    
    /*
     * Landing
     */

    public void sound_land()
    {
        Source.PlayOneShot(clip_land, 0.2f);
    }
    
    /*
     * Death
     */

    public void sound_death()
    {
        Debug.Log("Player died - playing dead sound");

        if(player_death_sound_already_played == false)
        {
            player_death_sound_already_played = true;
            Source.PlayOneShot(clip_death,1);
        }
    }
    
    /*
     * Roll
     */

    public void sound_takeItem()
    {
        Source.PlayOneShot(clip_takeItem,1f);
    }
    
    /*
     *    Draw weapon
     */

    public void sound_drawWeapon()
    {
        Source.PlayOneShot(clip_drawWeapon,1);
    }
    
    /*
     *     Getting Hit
     */

    public void sound_gettingHit()
    {
        int randomSound = Random.Range(0,HurtSounds.Length);
        Source.PlayOneShot(HurtSounds[randomSound], 1);
    }
    
    /*
     *     Swallow
     */

    public void sound_swallow()
    {
        Source.PlayOneShot(clip_swallow, 1);
    }
    
    /*
     *     Hungry
     */

    public void sound_hungry()
    {
        int randomSound = Random.Range(0,HungrySounds.Length);
        Source.PlayOneShot(HungrySounds[randomSound], 1);
    }
    
    /*
     *     Explosion
     */

    public void sound_explosion(AudioSource source)
    {
        source.PlayOneShot(clip_exolosion,1);
    }
    
    /*
     *     Enemy Hurt
     */
    
    public void sound_enemyHurt()
    {
        int randomSound = Random.Range(0,Enemy_HurtSound.Length);
        Source.PlayOneShot(Enemy_HurtSound[randomSound], 1);
    }
    
    /*
    *     Enemy Random Sound
    */
    
    public void sound_enemyRandomSound()
    {
        if (GetComponent<EnemyController>().isDead)
        {
            CancelInvoke("sound_enemyRandomSound");
            return;
        }
        
        int randomSound = Random.Range(0,Enemy_RandomSound.Length);
        Source.PlayOneShot(Enemy_RandomSound[randomSound], 0.4f);
    }
    
    /*
     *     Weapon Slash
     */

    public void sound_weaponSlash()
    {
        int randomSound = Random.Range(0,WeaponSlash.Length);
        Source.PlayOneShot(WeaponSlash[randomSound], 1);
    }
    
    /*
     *     Thunder
     */
    
    public void sound_thunder()
    {
        Source.PlayOneShot(clip_thunder, 1);
    }
    
    /*
     *     Thunder
    */
    
    public void sound_bolt()
    {
        Source.PlayOneShot(clip_bolt, 1);
    }
    
    /*
     *     Level Up
     */

    public void sound_levelUp()
    {
        Source.PlayOneShot(clip_levelUp, 1);
    }
}
