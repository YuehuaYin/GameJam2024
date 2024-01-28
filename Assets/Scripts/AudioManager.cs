using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source1;
    [SerializeField] private AudioSource source2;

    [SerializeField] private AudioClip basement;
    [SerializeField] private AudioClip club;
    [SerializeField] private AudioClip arena;
    [SerializeField] private AudioClip space;
    [SerializeField] private AudioClip evil;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.activeSceneChanged += SceneChange;
        }
        else
        {
            Destroy(gameObject);
        }

        source1.clip = basement;
        source1.Play();
    }

    private void SceneChange(Scene start, Scene next)
    {
        String nextScene = next.name;
        
        if (nextScene.Equals("TestSystem"))
        {
            return;
        }
        else if (nextScene.Equals("TitleScene") || nextScene.Equals("Level 1") || nextScene.Equals("Level 6"))
        {
            if (!GetPlaying().Equals(basement)) StartCoroutine(ChangeSong(basement));
        }
        else if (nextScene.Equals("Level 2"))
        {
            if (!GetPlaying().Equals(club)) StartCoroutine(ChangeSong(club));
        }
        else if (nextScene.Equals("Level 3"))
        {
            if (!GetPlaying().Equals(arena)) StartCoroutine(ChangeSong(arena));
        }
        else if (nextScene.Equals("Level 4"))
        {
            if (!GetPlaying().Equals(space)) StartCoroutine(ChangeSong(space));
        }
        else if (nextScene.Equals("Level 5"))
        {
            if (!GetPlaying().Equals(evil)) StartCoroutine(ChangeSong(evil));
        }

    }

    IEnumerator ChangeSong(AudioClip clip)
    {
        AudioSource biggie;
        AudioSource smaler;
        
        if (source1.isPlaying)
        {
            biggie = source1;
            smaler = source2;
        }
        else
        {
            biggie = source2;
            smaler = source1;
        }

        smaler.volume = 0;

        smaler.clip = clip;
        smaler.Play();
        
        while (smaler.volume < 1)
        {
            smaler.volume += (float) 0.01;
            biggie.volume -= (float) 0.01;

            yield return new WaitForFixedUpdate();
        }

        biggie.volume = 0;
        biggie.Stop();
    }

    private AudioClip GetPlaying()
    {
        if (source1.isPlaying)
        {
            return source1.clip;
        }
        else
        {
            return source2.clip;
        }
    }
}
