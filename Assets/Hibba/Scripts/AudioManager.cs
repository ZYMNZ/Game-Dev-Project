using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource SFXSource;

    public AudioClip menu;
    public AudioClip level1;
    public AudioClip click;
    public AudioClip victory;
    public AudioClip collectKey;

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayMusicInLoop(AudioClip clip, bool loop)
    {
        musicSource.clip = clip;
        musicSource.loop = loop; 
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFx(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.PlayOneShot(clip);
    }
}
