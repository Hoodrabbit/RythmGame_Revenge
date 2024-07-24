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
        slider.maxValue = GameManager.Instance.musicInfo.Music.length+1;
        slider.onValueChanged.AddListener(delegate { SetMusicTime(); });

        NowSongTime =GetComponentInChildren<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        SetValue();
        NowSongTime.text = MusicTime().ToString("0.000");
    }

    void SetMusicTime() //노래 곡을 잡고 있는데
    {
        if(GameManager.Instance.MainAudio.isPlaying == false)
        {
            if(slider.value <= slider.maxValue)
            {
                //Debug.Log(NoteParent.GetDistance() / GameManager.Instance.MainAudio.clip.length);
                GameManager.Instance.MainAudio.time = slider.value;
                Transform NPpos = EditManager.Instance.NoteParent.transform;
                NPpos.position = new Vector3(-slider.value * (EditManager.Instance.NoteParent.GetDistance()/ ((int)(GameManager.Instance.MainAudio.clip.length)+1)), 0, 0); //이상한 부분
            }

            

        }
        
    }

    public float MusicTime()
    {
        return slider.value;
    }


    void SetValue()
    {
        if (GameManager.Instance.MainAudio.isPlaying == true)
        {
            //Debug.Log(GameManager.Instance.MainAudio.timeSamples/GameManager.Instance.MainAudio.clip.frequency);
            slider.value = GameManager.Instance.MainAudio.time;
            Transform NPpos = EditManager.Instance.NoteParent.transform;
            NPpos.position = new Vector3(-slider.value * (EditManager.Instance.NoteParent.GetDistance() / ((int)(GameManager.Instance.MainAudio.clip.length) + 1)), 0, 0);
            // / GameManager.Instance.MainAudio.clip.length

        }
    }
}
