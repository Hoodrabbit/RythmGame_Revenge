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

    public Image SquareIMG;

    public TMP_Text text;
    public Animator slotAnimator;


    public Action Clicked;

    int num;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        //Debug.Log("구독시작");
        
        Clicked += MusicSelectPanel.SlotClicked;
    }

    

    private void OnDestroy()
    {
       // Debug.Log("구독해제");
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
       


        Debug.Log("뮤직슬롯 실행 전");

        //음악 이걸로 변경
        if (musicInfo != null)
        {
            MusicManager.Instance.SetMusic(musicInfo);
            AudioManager.Instance.SetValue(musicInfo);
        }
            
        else
            Debug.LogError("없어요");

        Debug.Log("뮤직슬롯 실행 후");

        Clicked?.Invoke();

    }
}
