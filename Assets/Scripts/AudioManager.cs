using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;

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
    
    public AudioSource menuMusic;
    public AudioSource battleMusic;

    public AudioSource victoryMusic;
    public AudioSource defeatMusic;
    public AudioSource tieMusic;

    public AudioSource[] backgroundTracks; 

    void Start()
    {
        
    }

    void Update()
    {
        
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

    public void playMenuMusic()
    {
        if(menuMusic.isPlaying == false)
        {
            StopMusic();
            menuMusic.Play();
        }
    }

    public void playBattleMusic() 
    {
        if(battleMusic.isPlaying == false)
        {
            StopMusic();
            battleMusic.Play();
        }
    }

    public void playVictoryMusic() 
    {
        if(victoryMusic.isPlaying == false)
        {
            StopMusic();
            victoryMusic.Play();
        }
    }
    public void playDefeatMusic() 
    {
        if(defeatMusic.isPlaying == false)
        {
            StopMusic();
            defeatMusic.Play();
        }
    }

     public void playTieMusic() 
    {
        if(tieMusic.isPlaying == false)
        {
            tieMusic.PlayDelayed(1.25f);
        }
    }
}
