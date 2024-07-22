using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class EditManager : Singleton<EditManager>
{
    //���� ���������� �ʿ��� ��ɵ��� ��Ƴ��� 
    public BarNote NoteParent;

    public GameObject Note;

    public float GetNPXpos()
    {
        return NoteParent.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int longNoteLength)
    {
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height,noteType,longNoteLength));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height,noteType,longNoteLength));


        }

    }

}
