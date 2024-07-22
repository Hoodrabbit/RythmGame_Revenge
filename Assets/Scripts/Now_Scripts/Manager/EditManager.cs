using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class EditManager : Singleton<EditManager>
{
    //각종 에딧씬에서 필요한 기능들을 모아놓음 
    public BarNote NoteParent;

    public GameObject Note;

    public float GetNPXpos()
    {
        return NoteParent.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int longNoteLength)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height,noteType,longNoteLength));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height,noteType,longNoteLength));


        }

    }

}
