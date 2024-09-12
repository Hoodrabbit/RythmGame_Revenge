using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicTimeSlider : MonoBehaviour
{
    Transform m_Cam;
    Slider slider;

    TextMeshProUGUI NowSongTime;
    // Start is called before the first frame update
    void Start()
    {
        m_Cam = Camera.main.GetComponent<Transform>();
        slider = GetComponent<Slider>();
        slider.maxValue = GameManager.Instance.musicInfo.Music.length;
        slider.onValueChanged.AddListener(delegate { SetMusicTime(); });

        NowSongTime =GetComponentInChildren<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        SliderFunc();
        if(Input.anyKeyDown)
        {
            //slider.interactable = false;
        }

    }


    public void SliderFunc()
    {

        SetValue();

        NowSongTime.text = GetSliderValue().ToString("0.000");
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void SetSliderValue(float Beat)
    {
        slider.value += Beat;
    }



    void SetMusicTime() //노래 곡을 잡고 있는데
    {
        if (GameManager.Instance.MainAudio.isPlaying == false)
        {
            if (slider.value <= slider.maxValue)
            {
                GameManager.Instance.MainAudio.time = slider.value;
                Transform NPpos = EditManager.Instance.NoteParent.transform;
                NPpos.position = new Vector3(-slider.value * GameManager.Instance.speed, 0, 0); //이상한 부분
            }



        }

    }


    void SetValue()
    {
        if (GameManager.Instance.MainAudio.isPlaying == true)
        {
            //Debug.Log(GameManager.Instance.MainAudio.timeSamples/GameManager.Instance.MainAudio.clip.frequency);
            slider.value = (float)GameManager.Instance.MainAudio.timeSamples / GameManager.Instance.MainAudio.clip.frequency;
            Transform NPpos = EditManager.Instance.NoteParent.transform;
            NPpos.position = new Vector3(-slider.value * GameManager.Instance.speed , 0, 0);
            //NPpos.position = new Vector3(-(float)(AudioSettings.dspTime - GameManager.Instance.StartDspTime() ) * (EditManager.Instance.NoteParent.GetDistance() / ((int)(GameManager.Instance.MainAudio.clip.length) + 1)), 0, 0);
            // / GameManager.Instance.MainAudio.clip.length

        }
    }
}
