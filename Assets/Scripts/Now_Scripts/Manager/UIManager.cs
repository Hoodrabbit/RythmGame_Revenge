using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    public BossNoteDetailScript BossNoteDetailPanel;


    /// <summary>
    /// OperatingWayButton[0] : KeyBoard , OperatingWayButton[1] : Mouse
    /// </summary>
    public Button[] OperatingWayButton;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.Instance.MainAudio.isPlaying)
            {
                StopMusic();
            }
            else
            {
                PlayMusic();
            }
        }

        ButtonInteractable();


    }


    public void PlayMusic()
    {
        GameManager.Instance.PlayMusicOnly();
    }

    public void StopMusic()
    {
        GameManager.Instance.MainAudio.Pause();
    }



    void ButtonInteractable()
    {
        if(EditManager.Instance.OperateEditState == NoteEditOperatingState.KeyBoard)
        {
            OperatingWayButton[0].interactable = false;
            OperatingWayButton[1].interactable = true;
        }
        else
        {
            OperatingWayButton[0].interactable = true;
            OperatingWayButton[1].interactable = false;
        }

    }



    public void KeyBoardOperating()
    {
        OperatingWayButton[0].interactable = false;
        OperatingWayButton[1].interactable = true;
        EditManager.Instance.OperateEditState = NoteEditOperatingState.KeyBoard;






    }

    public void MouseOperating()
    {
        OperatingWayButton[0].interactable = true;
        OperatingWayButton[1].interactable = false;
        EditManager.Instance.OperateEditState = NoteEditOperatingState.Mouse;





    }



}
