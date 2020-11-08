using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumerates all available sounds
/// </summary>
public enum SoundType
{
    Explosion = 0,
    Bumper,
    CannonFire,
    CollectBox,
    LaserGunFire
}

public enum MusicType
{
    Racing,
    MainMenu,
}

[Serializable]
public class SoundEffect
{
    public SoundType SoundType;
    public AudioClip AudioClip;

}

[Serializable]
public class BackgroundMusic
{
    public MusicType MusicType;
    public AudioSource AudioSource;
}

public class SoundController : MonoBehaviour
{

    public List<SoundEffect> SoundEffects;
    public List<BackgroundMusic> BackgrounMusics;
    

    public void PlaySound(SoundType soundType, Vector3 position, float volume = 1f)
    {
        SoundEffect soundEffect  = SoundEffects.Find(x => x.SoundType.Equals(soundType));
        
        try
        {
            AudioSource.PlayClipAtPoint(soundEffect.AudioClip, position, volume);
            Debug.Log(position);
        } catch (NullReferenceException)
        {
            Debug.LogError("Sound Effect " + soundType + " not set, check SoundManager (SoundController Script)");
        }
    }

    public void PlayMusic(MusicType musicType, float volume = 1f)
    {
        BackgroundMusic music = BackgrounMusics.Find(x => x.MusicType.Equals(musicType));

        try
        {
            music.AudioSource.Play();
        } catch (NullReferenceException)
        {
            Debug.LogError("Backround music " + musicType + " not set, check SoundManager (SoundController Script)");

        }
    }
    public void PauseMusic(MusicType musicType, float volume = 1f)
    {
        BackgroundMusic music = BackgrounMusics.Find(x => x.MusicType.Equals(musicType));

        try
        {
            music.AudioSource.Pause();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Backround music " + musicType + " not set, check SoundManager (SoundController Script)");
        }
    }
    public void ResumeMusic(MusicType musicType, float volume = 1f)
    {
        BackgroundMusic music = BackgrounMusics.Find(x => x.MusicType.Equals(musicType));

        try
        {
            music.AudioSource.UnPause();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Backround music " + musicType + " not set, check SoundManager (SoundController Script)");

        }
    }
    public void StopMusic(MusicType musicType, float volume = 1f)
    {
        BackgroundMusic music = BackgrounMusics.Find(x => x.MusicType.Equals(musicType));

        try
        {
            music.AudioSource.Stop();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Backround music " + musicType + " not set, check SoundManager (SoundController Script)");

        }
    }
}
