using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxbox : MonoBehaviour
{
    public KeyCode inputKey;
    public NoteParsing NoteCheck;
    public List<GameObject>Note = new List<GameObject>();
    [SerializeField] Transform Center = null;
    [SerializeField] SpriteRenderer[] timing = null;
    Vector2[] timingBoxs = null;
    void Start()
    {
        timingBoxs = new Vector2[timing.Length];

        for (int i = 0; i < timingBoxs.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timing[i].bounds.size.x, Center.localPosition.x + timing[i].bounds.size.x);
            //Debug.Log(Center.localPosition.x-timing[i].bounds.size.x/2);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(inputKey))
        {
            //if()
            CheckTiming();
        }
        




    }

    void CheckTiming() //���� ��Ʈ ��ġ üũ�ؼ� �󸶳� ����ִ��� Ȯ���ϱ� �Ÿ��� �ʹ� �ְ�쿡�� �׳� �������� 
    {
        if(Note.Count>0)
        {
            for (int i = 0; i < Note.Count; i++)
            {
                float num = Note[i].transform.position.x;

                for (int j = 0; j < 3; j++)
                {
                    {
                        if (timingBoxs[j].x <= num && num <= timingBoxs[j].y)
                        {
                            Debug.Log("Hit : " + j);
                            
                        }
                    }
                }

            } 
        }

        
    }



}
//if (Input.GetKey(KeyCode.J))
//{
//    Debug.Log("�Ʒ�");
//    gameobjects[0].SetActive(true);
//}
//else if (Input.GetKey(KeyCode.F))
//{
//    Debug.Log("��");
//    gameobjects[1].SetActive(true);
//}

//if (Input.GetKeyUp(KeyCode.J))
//{
//    Debug.Log("�Ʒ�");
//    gameobjects[0].SetActive(false);
//}
//else if (Input.GetKeyUp(KeyCode.F))
//{
//    Debug.Log("��");
//    gameobjects[1].SetActive(false);
//}