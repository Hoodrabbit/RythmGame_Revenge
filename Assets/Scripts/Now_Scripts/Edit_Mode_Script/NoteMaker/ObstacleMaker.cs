using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker : NoteMakerBase
{
    public GameObject Obstacle;

    public override GameObject Note { get => Obstacle; set => Obstacle = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);


        int i = 0;
        bool Checkduplication = false; // ��Ʈ�� �ߺ��Ǽ� ������� ������ �˻�
        while (i < hit.Length && !DeleteMode)
        {
            //Debug.Log("�۵�" + hit[i].collider.name);
            if (hit[i].collider.CompareTag("Obstacle"))
            {
                Checkduplication = true;
                Debug.Log("�ߺ��Դϴ�.");
            }

            if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
            {
                //������ �κ�
                //���̸� ���� ��Ʈ�� �����ϴ��� ������ �ϴ� �� �Ӹ��ƴ϶� ������Ű�� ���� ��Ʈ�� �����ϴ����� Ȯ���ؾ� �� �׷��� ������ ���� �ڸ��� ��Ʈ�� �ߺ����� ������


                //1
                if (Pos.y > 0)
                {
                    Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.OBSTACLE_UP);


                    if (NoteCheck(InstantiatePos))
                    {
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�


                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, EditManager.OBSTACLE_UP, NoteType, 0, (double)RealXpos / 10));
                    }

                }

                //2
                else if (Pos.y <= 0)
                {
                    Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.OBSTACLE_DOWN);


                    if (NoteCheck(InstantiatePos))
                    {
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //���� ���� 

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, EditManager.OBSTACLE_DOWN, NoteType, 0, (double)RealXpos / 10));
                    }
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


                if (hit[i].collider.CompareTag("Obstacle"))
                {
                    Destroy(hit[i].transform.gameObject);
                }
                i++;
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
            if (hit_Detail[count].collider.CompareTag("Obstacle"))
            {
                Debug.Log("�ߺ��Դϴ�.");
                return false;
            }
            count++;
        }

        return true;

    }

}
