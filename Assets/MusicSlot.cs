using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MusicSlot : MonoBehaviour, IPointerClickHandler
{

    public MusicInfo musicInfo;
    public Image image;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        //image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusic(MusicInfo music)
    {
        musicInfo = music;
        image.sprite = musicInfo.MusicSprite;
        text.text = musicInfo.Music_Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //음악 이걸로 변경
        if (musicInfo != null)
        {
            MusicManager.Instance.SetMusic(musicInfo);
            AudioManager.Instance.SetValue(musicInfo);
        }
            
        else
            Debug.LogError("없어요");
    }
}
