using System;
using UnityEngine;

public class Event_Note_Maker : NoteMakerBase
{
    public GameObject EventNote;

    //�� ��Ʈ�� ������ Ÿ���� Ȯ���ؼ� ��ȣ�� ���� �޼��嵵 �ʿ���

    public Action EventChanged;
    public override GameObject Note { get => EventNote; set => EventNote = value; }


    protected override void Awake()
    {
        base.Awake();
        EventChanged += DataManager.Instance.EventCheck;
        //NoteType = EventNote.GetComponent<Note>().TypeNum;
    }

    protected override void Update()
    {
        base.Update();
    } // Ŭ�� �̺�Ʈ�� ó���ϴ� �޼���



    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);
        int i = 0;

        while (i < hit.Length && !DeleteMode)
        {

            if (hit[i].collider.CompareTag("NotePlace"))
            {

                Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, EditManager.MIDDLE);

                if (NoteCheck(InstantiatePos))
                {

                    GameObject AddEvent = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.EventNote.transform);

                    float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();
                    //���� ���� 

                    NoteEventScript NES = AddEvent.GetComponent<NoteEventScript>();
                    AddEvent.GetComponent<Note>().SongTime = (double)RealXpos / GameManager.Instance.speed;
                    NES.SetSongTime(RealXpos / GameManager.Instance.speed);
                    DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, 0, (int)NES.eventType, (double)RealXpos / GameManager.Instance.speed));
                    EventChanged?.Invoke();
                }

            }
            i++;
        }

        if (DeleteMode)
        {
            i = 0;
            while (i < hit.Length && DeleteMode)
            {
                //Debug.Log("�۵�" + hit[i].collider.name);


                if (hit[i].collider.CompareTag("EventNote") || hit[i].collider.CompareTag("BossActionNote"))
                {


                    DataManager.Instance.ListNullCheck(hit[i].collider.gameObject);
                    Destroy(hit[i].collider.gameObject);
                    EventChanged?.Invoke();


                }
                i++;
            }
        }



    }



    protected override bool NoteCheck(Vector2 Pos)
    {

        Vector2 rayDirection = Pos;
       // Debug.Log("Posup : " + rayDirection);
        RaycastHit2D[] hit_Detail;
        int count = 0;
        hit_Detail = Physics2D.RaycastAll(rayDirection, transform.forward, 10);

        while (count < hit_Detail.Length)
        {
            if (hit_Detail[count].collider.CompareTag("BossActionNote") || hit_Detail[count].collider.CompareTag("EventNote"))
            {
                Debug.Log("�ߺ��Դϴ�.");
                return false;
            }
            count++;
        }

        return true;

    }
}
