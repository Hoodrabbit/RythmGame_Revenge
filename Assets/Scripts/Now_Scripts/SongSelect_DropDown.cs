using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SongSelect_DropDown : MonoBehaviour
{
    //여기 options 데이터 관리 이렇게 하지말고 다르게 해야함



    Dropdown dropdown;
    bool IsCheck = false;
    SongSelect songSelect;


    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(GetValue);
        songSelect = FindObjectOfType<SongSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCheck)
        {
            //약간 예시 노래도 나와야함

        }
    }

    void GetValue(int Value)
    {
        songSelect.SongAudio.clip = songSelect.audioClips[Value];
        MusicManager.Instance.SetMusic(Value);
        songSelect.SongAudio.PlayScheduled(0);
    }

}
