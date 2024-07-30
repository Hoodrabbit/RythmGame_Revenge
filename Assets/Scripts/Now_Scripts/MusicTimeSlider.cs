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


    public float Pos = 0;
    public float MovePos = 0;
    float Value = 0;

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
                NPpos.position = new Vector3(-slider.value * GameManager.Instance.speed, 0, 0); //이상한 부분
            }

            

        }
        
    }

    public void SliderFunc()
    {
        float Beat = 60 / GameManager.Instance.musicInfo.BPM;


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Mathf.Abs((slider.value % Beat) - Beat) <= 0.001)
            {
                slider.value += Beat;
                Debug.Log("잘되는 경우 : " + (slider.value / Beat));
            }
            else
            {
                Debug.Log("안되는 경우 : " + Mathf.Abs((slider.value % Beat)));
                slider.value = (int)(slider.value / Beat) * Beat + Beat;
                
            }


        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //slider.value -= 60 / GameManager.Instance.musicInfo.BPM;
            if (Mathf.Abs((slider.value % Beat)) <= 0.001)
            {
                slider.value -= Beat;
            }
            else
            {

                Debug.Log((slider.value % Beat) - Beat);
                slider.value = (int)(slider.value / Beat) * Beat ;
                //정수형값이 0이 되는 순간 값을 제대로 못받아서 문제가 생기는 것 같음
                //해결해줘야함

            }

        }


        //MoveTime(Pos, MovePos, Value);

        SetValue();

        NowSongTime.text = MusicTime().ToString("0.000");
    }


    public void MoveTime(float Pos, float MovePos, float Value)
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            
            //Pos = slider.value;
            Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            Debug.Log(Pos);
            //Debug.Log(slider.value);
            //Value = slider.value;
            Debug.Log(slider.value);
        }

        if(Input.GetMouseButton(0))
        {
            MovePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //Debug.Log(Value);
            slider.value =Value + MovePos- Pos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Pos = 0;
            MovePos = 0;
            Value = slider.value;
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
            slider.value = (float)GameManager.Instance.MainAudio.timeSamples / GameManager.Instance.MainAudio.clip.frequency;
            Transform NPpos = EditManager.Instance.NoteParent.transform;
            NPpos.position = new Vector3(-slider.value * GameManager.Instance.speed , 0, 0);
            //NPpos.position = new Vector3(-(float)(AudioSettings.dspTime - GameManager.Instance.StartDspTime() ) * (EditManager.Instance.NoteParent.GetDistance() / ((int)(GameManager.Instance.MainAudio.clip.length) + 1)), 0, 0);
            // / GameManager.Instance.MainAudio.clip.length

        }
    }
}
