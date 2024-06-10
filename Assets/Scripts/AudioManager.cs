using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        //StopMusic();
    }
    
    public AudioSource menuMusic;
    public AudioSource battleMusic;
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
}
