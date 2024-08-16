using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MusicSlot : MonoBehaviour, IPointerClickHandler
{

    public MusicInfo musicInfo;
    public Image image;
    public TMP_Text text;

    public Action Clicked;

    int num;


    // Start is called before the first frame update
    void Start()
    {
        //Clicked += MusicSelectPanel.SlotClicked;
        //image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        //Debug.Log("��������");
        Clicked += MusicSelectPanel.SlotClicked;
    }

    

    private void OnDestroy()
    {
       // Debug.Log("��������");
        Clicked -= MusicSelectPanel.SlotClicked;
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
        Clicked?.Invoke();


        //���� �̰ɷ� ����
        if (musicInfo != null)
        {
            MusicManager.Instance.SetMusic(musicInfo);
            AudioManager.Instance.SetValue(musicInfo);
        }
            
        else
            Debug.LogError("�����");
    }
}
