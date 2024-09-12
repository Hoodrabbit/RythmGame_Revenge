using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossNoteDetailScript : MonoBehaviour
{
    BossNote bossNoteSet;

    public TMP_InputField NoteTypeCheckText;
    public TMP_InputField TimeCheck;
    public TMP_InputField TapCountCheck;
    public Button InvisbleCheck;


    public void SetNoteData(BossNote bossNote)
    {
        bossNoteSet = bossNote;
        SetUI();
    }

    public void SetUI()
    {
        NoteTypeCheckText.text = bossNoteSet.NoteType_Boss.ToString();
        TimeCheck.text = bossNoteSet.SongTime.ToString();
        TapCountCheck.text = bossNoteSet.TapCount.ToString();
        //InvisbleCheck
    }






}
