using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SongSelect_DropDown : MonoBehaviour
{
    //���� options ������ ���� �̷��� �������� �ٸ��� �ؾ���



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
            //�ణ ���� �뷡�� ���;���

        }
    }

    void GetValue(int Value)
    {
        songSelect.SongAudio.clip = songSelect.audioClips[Value];
        MusicManager.Instance.SetMusic(Value);
        songSelect.SongAudio.PlayScheduled(0);
    }

}
