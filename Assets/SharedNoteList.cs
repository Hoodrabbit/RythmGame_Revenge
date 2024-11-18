using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedNoteList : MonoBehaviour
{
    public List<Note> notes_Up;
    public List<Note> notes_Down;



    //현재 큐가 작동되고 있는지 확인해주는 변수
    private bool isProcessing = false;

    //보스 노트 중복 입력 방지를 위한 감지용 큐
    public Queue<Action> NoteRemove_ActionQueue = new Queue<Action>();

    #region 위쪽 노트 리스트
    public void Add_Note_Up(Note note)
    {
        notes_Up.Add(note);
    }

    public void Delete_Note_Up()
    {
        notes_Up.RemoveAt(0);
    }

    #endregion

    #region 아래쪽 노트 리스트
    public void Add_Note_Down(Note note)
    {
        notes_Up.Add(note);
    }

    public void Delete_Note_Down()
    {
        notes_Down.RemoveAt(0);
    }


    #endregion

    #region 보스노트

    public void DeleteBossNote()
    {

        Debug.Log("작동");

        if(NoteRemove_ActionQueue.Count == 0)
        {
            NoteRemove_ActionQueue.Enqueue(() => DeleteBossNoteDetail());
        }
        if(!isProcessing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isProcessing = true;
        while(NoteRemove_ActionQueue.Count>0)
        {
            NoteRemove_ActionQueue.Dequeue().Invoke();
            yield return null;
        }
        isProcessing = false;
    }


    private void DeleteBossNoteDetail()
    {
        if(NoteRemove_ActionQueue.Count == 0)
        {
            for (int i = 0; i < notes_Up.Count; i++)
            {
                if (notes_Up[i].CompareTag("Boss"))
                {
                    notes_Up.RemoveAt(i);
                }
            }
            for (int i = 0; i < notes_Down.Count; i++)
            {
                if (notes_Down[i].CompareTag("Boss"))
                {
                    notes_Down.RemoveAt(i);
                }
            }
            
        }

        
    }


    #endregion

    public List<Note>GetNoteList(float ypos)
    {
        return ypos >= 0 ? notes_Up : notes_Down;
    }


}
