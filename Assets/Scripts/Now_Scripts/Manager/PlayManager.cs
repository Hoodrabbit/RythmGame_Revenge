using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� �����ϴ� �̱���
public class PlayManager : Singleton<PlayManager>
{
    public GameObject Note;
    public List<GameObject> Notes;
    public NoteInfoPos NotePos;


    public void PlayScene_NoteMaker(float xpos, int heightnum)
    {
        //Notes.Add(Instantiate(xpos));//���� �Ұ���

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
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));


        }

    }
 
 */