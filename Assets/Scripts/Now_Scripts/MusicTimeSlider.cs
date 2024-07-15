using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicTimeSlider : MonoBehaviour
{
    Transform m_Cam;
    Slider slider;
    public BarNote NoteParent;
    // Start is called before the first frame update
    void Start()
    {
        m_Cam = Camera.main.GetComponent<Transform>();
        slider = GetComponent<Slider>();
        slider.maxValue = (int)GameManager.Instance.musicInfo.Music.length+1;
        slider.onValueChanged.AddListener(delegate { SetMusicTime(); });
    }

    // Update is called once per frame
    void Update()
    {
        SetValue();
        
    }

    void SetMusicTime()
    {
        if(GameManager.Instance.MainAudio.isPlaying == false)
        {
            if(slider.value <= slider.maxValue)
            {
                //Debug.Log(NoteParent.GetDistance() / GameManager.Instance.MainAudio.clip.length);
                GameManager.Instance.MainAudio.time = slider.value;
                NoteParent.transform.position = new Vector3(-slider.value * (NoteParent.GetDistance()/ GameManager.Instance.MainAudio.clip.length), 0, 0);
            }
            
        }
        
    }

    void SetValue()
    {
        if (GameManager.Instance.MainAudio.isPlaying == true)
        {
            slider.value = GameManager.Instance.MainAudio.time;
            NoteParent.transform.position = new Vector3(-slider.value * (NoteParent.GetDistance() / GameManager.Instance.MainAudio.clip.length), 0, 0);
        }
    }


}
