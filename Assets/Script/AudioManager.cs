using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            if(CheckScene() == 0 || (CheckScene() == 1 && !isPlaying("BattleTheme")) || CheckScene() == 3)
            {
                Destroy(instance.gameObject);
                instance = this;
            }else
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        if(CheckScene() == 0)
        {
            Play("OpeningTheme");
        }else if(CheckScene() == 3)
        {
            Play("EndingTheme");
        }else if(CheckScene() == 1)
        {
            Play("BattleTheme");
        }
    }

    public void Play(string name)
    {
        Sound s  = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s  = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Stop();
    }

    private int CheckScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if(GameManager.playerDead)
        {
            Stop("BattleTheme");
        }
    }

    public bool isPlaying(string name)
    {
        Sound s  = Array.Find(instance.gameObject.GetComponent<AudioManager>().sounds, sound => sound.name == name);
        if(s == null)
        {
            return false;
        }
        return s.source.isPlaying;
    }
}
