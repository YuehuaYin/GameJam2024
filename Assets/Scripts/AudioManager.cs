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
        
    }
}
