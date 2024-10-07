using UnityEngine;

public class BossNoteMaker : Event_Note_Maker
{
    public override GameObject Note { get => EventNote; set => EventNote = value; }

    public BossNoteType bossNoteType;

  

    protected override void Update()
    {
        base.Update();
    } // 클릭 이벤트를 처리하는 메서드



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
                    //위와 동일 


                    BossEventNote bossEventNote = AddEvent.GetComponent<BossEventNote>();

                    DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos,0, (int)bossEventNote.bossEventType, (double)RealXpos / GameManager.Instance.speed));

                    EventChanged?.Invoke();
                }
                i++;
            }

            if (DeleteMode)
            {
                i = 0;
                while (i < hit.Length && DeleteMode)
                {
                    //Debug.Log("작동" + hit[i].collider.name);


                    if (hit[i].collider.CompareTag("BossActionNote"))
                    {

                        Destroy(hit[i].collider.gameObject);
                        EventChanged?.Invoke();


                    }
                    i++;
                }
            }
        }
    }



    protected override bool NoteCheck(Vector2 Pos)
    {

        Vector2 rayDirection = Pos;
        //Debug.Log("Posup : " + rayDirection);
        RaycastHit2D[] hit_Detail;
        int count = 0;
        hit_Detail = Physics2D.RaycastAll(rayDirection, transform.forward, 10);

        while (count < hit_Detail.Length)
        {
            if (hit_Detail[count].collider.CompareTag("BossActionNote") || hit_Detail[count].collider.CompareTag("EventNote"))
            {
                Debug.Log("중복입니다.");
                return false;
            }
            count++;
        }

        return true;

    }


}
