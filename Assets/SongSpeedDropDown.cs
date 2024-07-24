using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;



public class SongSpeedDropDown : MonoBehaviour
{
    TMP_Dropdown SSdropdown;

    int StartIndex = 2;

    List<string> Speed = new List<string>{ "0.25", "0.5", "1", "1.5", "2" };


    // Start is called before the first frame update
    void Start()
    {
        SSdropdown = GetComponent<TMP_Dropdown>();
        SSdropdown.options.Clear();
        SSdropdown.AddOptions(Speed);
        SSdropdown.value = StartIndex;
        SSdropdown.onValueChanged.AddListener(delegate { SongSpeed(SSdropdown.value); });
        
    }

    /// <summary>
    /// 
    /// 노래의 속도 뿐만이아니라 게임 속도를 건드림
    /// 
    /// summary//
    /// </summary>
    public void SongSpeed(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.MainAudio.pitch = 0.25f;
                //Debug.Log("작동");
                break;
            case 1:
                GameManager.Instance.MainAudio.pitch = 0.5f;
                break;
            case 2:
                GameManager.Instance.MainAudio.pitch = 1f;
                break;
            case 3:
                GameManager.Instance.MainAudio.pitch = 1.5f;
                break;
            case 4:
                GameManager.Instance.MainAudio.pitch = 2f;
                break;
        }
    }
}
