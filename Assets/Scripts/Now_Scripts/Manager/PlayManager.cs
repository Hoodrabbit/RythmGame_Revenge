using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이씬을 관리하는 싱글톤
public class PlayManager : Singleton<PlayManager>
{
    public GameObject Note;
    public List<GameObject> Notes;
    public NoteInfoPos NotePos;


    public void PlayScene_NoteMaker(float xpos, int heightnum)
    {
        //Notes.Add(Instantiate(xpos));//내일 할거임

        NotePos = new NoteInfoPos(xpos + 1*3 * 6-5, heightnum);

        if(NotePos.HeightValue ==1)
        {
            Notes.Add(Instantiate(Note, new Vector3(NotePos.xpos, 2), Quaternion.identity));
        }
        else
        {
            Notes.Add(Instantiate(Note, new Vector3(NotePos.xpos, -2), Quaternion.identity));
        }

        

    }
}

/*
 public void MakeNote(float xpos, int height)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));


        }

    }
 
 */