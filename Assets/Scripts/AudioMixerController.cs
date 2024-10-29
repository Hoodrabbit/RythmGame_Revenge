using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioMixerController : MonoBehaviour
{
    public AudioMixer GameAudio;
    Slider MyAudioSlider;
    float value_BGM;
    float value_SFX;

    public void Awake()
    {
        MyAudioSlider = gameObject.GetComponent<Slider>();
        if (gameObject.name == "BGM_Volume")
        {
            bool result = GameAudio.GetFloat("BGM", out value_BGM);
            if (result)
            {
                MyAudioSlider.value = value_BGM;
            }
        }
        if (gameObject.name == "SFX_Volume")
        {
            bool result = GameAudio.GetFloat("SFX", out value_SFX);
            if (result)
            {
                MyAudioSlider.value = value_SFX;
            }
        }


    }


    public void BGMControl()
    {
        float sound = MyAudioSlider.value;

        if (sound == -40f)
        {
            GameAudio.SetFloat("BGM", -80f);
        }
        else
        {
            GameAudio.SetFloat("BGM", sound);
        }

    }

    public void SFXControl()
    {
        float sound = MyAudioSlider.value;

        if (sound == -40f)
        {
            GameAudio.SetFloat("SFX", -80f);
        }
        else
        {
            GameAudio.SetFloat("SFX", sound);
        }
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

}
