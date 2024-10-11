using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EventSlot : MonoBehaviour
{
    public TextMeshProUGUI EventNameText;
    public TextMeshProUGUI SongTimeText;

    public void SetEventName(string eventName)
    {
        EventNameText.text = eventName;
    }
    public void SetSongTime(string songTime)
    {
        SongTimeText.text = songTime;
    }






}
