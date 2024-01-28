using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider slider;
    
    public void SetVolume()
    {
        AudioListener.volume = slider.value;
    }

    private void Awake()
    {
        slider.value = AudioListener.volume;
    }
}
