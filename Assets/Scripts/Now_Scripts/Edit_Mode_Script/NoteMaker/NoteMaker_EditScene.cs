using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    //public GameObject Normal; //���� ������ ����� ��Ʈ
    //public GameObject Long; //���� ������ ����� ��Ʈ
    //Button NoteMakerButton;

    public static NoteMaker_EditScene instance;


    [Header("��Ʈ ������ִ� ����Ŀ ������Ʈ�� ������ ��� ����Ʈ")]
    public List<GameObject> MakerObjList_Prefab;
    //�Ϲ� ��Ʈ, �ճ�Ʈ, ����



    public List<GameObject> MakerObjList;
    GameObject Note_Normal;
    GameObject Note_Long;
    GameObject ExpandCheckerLineMaker;
    GameObject Ghost_Note;
    GameObject BossAppearNoteMaker;
    GameObject BossDisappearNoteMaker;
    GameObject ObstacleMaker;

    bool Already_Using = false;

    public void Start()
    {
        instance = this;


        foreach (GameObject obj in MakerObjList_Prefab) 
        { 
            MakerObjList.Add(Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity));
        }
        
        foreach (GameObject obj in MakerObjList) 
        { 
            obj.SetActive(false);
        }
        Note_Normal = MakerObjList[0];
        Note_Long = MakerObjList[1];
       // ExpandCheckerLineMaker = MakerObjList[2];
        Ghost_Note = MakerObjList[2];
        BossAppearNoteMaker = MakerObjList[3];
        BossDisappearNoteMaker = MakerObjList[4];
        ObstacleMaker = MakerObjList[5];

        //NoteMakerButton = GetComponent<Button>();

        //NoteMakerButton.onClick.AddListener(Instantiate_NormalNote);

    }

    public void TurnOffMaker()
    {
        foreach (GameObject obj in MakerObjList)
        {
            obj.SetActive(false);
        }
    }


    public void Instantiate_NormalNote()
    {
        ////�������ִ� ��Ʈ�� ���� ���� ��Ʈ�� �������ִ� ���
        ////�ݴ�� ���� ���� ��Ʈ�� �������ִ� ���(���ִ� ���)
        
        if (Note_Normal.activeSelf == false)
        {
            if (!Already_Using)
            {
                Note_Normal.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                Note_Normal.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            Note_Normal.SetActive(false);
            Already_Using = false;
        }
        
    }

    public void Instantiate_LongNote()
    {
        if(Note_Long.activeSelf == false) 
        {
            if (!Already_Using)
            {
                Note_Long.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                Note_Long.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            Note_Long.SetActive(false);
            Already_Using = false;
        }
        
    }

    public void Instantiate_ExpandCheckerLine()
    {
        if(ExpandCheckerLineMaker.activeSelf == false)
        {
            if (!Already_Using)
            {
                ExpandCheckerLineMaker.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                ExpandCheckerLineMaker.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            ExpandCheckerLineMaker.SetActive(false);
            Already_Using = false;
        }
       
    }

    public void Instantiate_GhostNote()
    {
        ////�������ִ� ��Ʈ�� ���� ���� ��Ʈ�� �������ִ� ���
        ////�ݴ�� ���� ���� ��Ʈ�� �������ִ� ���(���ִ� ���)

        if (Ghost_Note.activeSelf == false)
        {
            if (!Already_Using)
            {
                Ghost_Note.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                Ghost_Note.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            Ghost_Note.SetActive(false);
            Already_Using = false;
        }

    }

    public void Instantiate_BossAppearNote()
    {
        if (BossAppearNoteMaker.activeSelf == false)
        {
            if (!Already_Using)
            {
                BossAppearNoteMaker.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                BossAppearNoteMaker.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            BossAppearNoteMaker.SetActive(false);
            Already_Using = false;
        }
    }

    public void Instantiate_BossDisappearNote()
    {
        if (BossDisappearNoteMaker.activeSelf == false)
        {
            if (!Already_Using)
            {
                BossDisappearNoteMaker.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                BossDisappearNoteMaker.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            BossDisappearNoteMaker.SetActive(false);
            Already_Using = false;
        }
    }

    public void Instantiate_Obstacle()
    {
        if (ObstacleMaker.activeSelf == false)
        {
            if (!Already_Using)
            {
                ObstacleMaker.SetActive(true);
            }
            else
            {
                TurnOffMaker();
                ObstacleMaker.SetActive(true);
            }
            Already_Using = true;
        }
        else
        {
            ObstacleMaker.SetActive(false);
            Already_Using = false;
        }
    }



}
