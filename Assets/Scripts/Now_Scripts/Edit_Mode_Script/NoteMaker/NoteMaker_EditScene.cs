using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    //public GameObject Normal; //에딧 씬에서 사용할 노트
    //public GameObject Long; //에딧 씬에서 사용할 노트
    //Button NoteMakerButton;

    public static NoteMaker_EditScene instance;


    [Header("노트 만들어주는 메이커 오브젝트의 정보가 담긴 리스트")]
    public List<GameObject> MakerObjList_Prefab;
    //일반 노트, 롱노트, 등등등



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
        ////생성해주는 노트가 없을 때는 노트를 생성해주는 기능
        ////반대로 있을 때는 노트를 제거해주는 기능(꺼주는 기능)
        
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
        ////생성해주는 노트가 없을 때는 노트를 생성해주는 기능
        ////반대로 있을 때는 노트를 제거해주는 기능(꺼주는 기능)

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
