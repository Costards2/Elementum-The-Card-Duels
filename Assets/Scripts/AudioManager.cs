using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;

    public AudioSource menuMusic;
    public AudioSource battleMusic;

    public AudioSource[] victoryMusic;
    public AudioSource[] defeatMusic;
    public AudioSource tieMusic;

    public AudioSource[] backgroundTracks; 

    public float fadeDuration = 0.75f;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        StopMusic();
    }

    void Update()
    {
        Debug.Log(Time.timeScale);  
    }

    public void StopMusic()
    {
        menuMusic.Stop();
        battleMusic.Stop();
        foreach(AudioSource track in backgroundTracks)
        {
            track.Stop();    
        }
    }

    public void StartFadeOutAllMusic()
    {
        StartCoroutine(FadeOut(menuMusic));
        StartCoroutine(FadeOut(battleMusic));
        foreach(AudioSource track in backgroundTracks)
        {
            StartCoroutine(FadeOut(track));
        }
    }

    public void playMenuMusic()
    {   
        if(menuMusic.isPlaying == false)
        {
            Debug.Log("MenuMusicPlayed");
            StopMusic();
            StartCoroutine(FadeOut(battleMusic));
            StartCoroutine(FadeIn(menuMusic));
            menuMusic.Play();
        }
    }

    public void playBattleMusic() 
    {   
        if(battleMusic.isPlaying == false)
        {
            Debug.Log("BattleMusicPlayed");
            StopMusic();
            StartCoroutine(FadeOut(menuMusic));
            StartCoroutine(FadeIn(battleMusic));
            battleMusic.Play();
        }
    }

    public void playVictoryMusic() 
    {
        if(victoryMusic[0].isPlaying == false && victoryMusic[1].isPlaying == false && victoryMusic[2].isPlaying == false )
        {
            //StopMusic();
            StartFadeOutAllMusic();
            int randomIndex = UnityEngine.Random.Range(0, victoryMusic.Length);
            victoryMusic[randomIndex].Play();
        }
    }
    public void playDefeatMusic() 
    {
        if(defeatMusic[0].isPlaying == false && defeatMusic[1].isPlaying == false && defeatMusic[2].isPlaying == false)
        {
            //StopMusic();
            StartFadeOutAllMusic();
            int randomIndex = UnityEngine.Random.Range(0, defeatMusic.Length);
            defeatMusic[randomIndex].Play();
        }
    }

    public void playTieMusic() 
    {
        if(tieMusic.isPlaying == false)
        {
            tieMusic.PlayDelayed(1.25f);
        }
    }

    public void MethodFadeOut(AudioSource source)
    {
        StartCoroutine(FadeOut(source));
    }

    private IEnumerator FadeOut(AudioSource source)
    {       
        float startVolume = source.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        source.volume = 0;

        source.Stop();
    }

    private IEnumerator FadeIn(AudioSource source)
    {
        source.Play();

        float startVolume = 0;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0.4f, t / fadeDuration);
            yield return null;
        }

        source.volume = 0.4f;
    }
}
