using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetNote : Note
{
    public GameObject DummyOffsetNoteImage;

    List<GameObject> OffsetNoteList = new List<GameObject>();
    int MaxCount_Start = 5;


    public int count = 0; //현재 사용중인 노트 번호가 뭔지 체크해줄 변수

    protected override void Awake()
    {
        //base.Awake();

        speed = GameManager.Instance.speed;

        Initialize_OffsetNote_Script();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (count < MaxCount_Start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("작동됨");
                OffsetNoteList[count].SetActive(true);
                OffsetNoteList[count].transform.position = transform.position;
                StartCoroutine(OnDisableOffsetNote(count));
                count++;
            }
        }
    }


    protected override void FixedUpdate()
    {
        //base.FixedUpdate();

        if ((float)(AudioSettings.dspTime - AudioTime) >= 2.4f)
        {
            transform.position = new Vector3(xpos, 0);
            AudioTime = AudioSettings.dspTime;
        }
        else
        {
            transform.position = new Vector2(xpos + 10 *(float)(AudioSettings.dspTime - AudioTime), transform.position.y);
        }


        //transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);


      
        


    }

    void Initialize_OffsetNote_Script()
    {
        for(int i=0; i< MaxCount_Start; i++)
        {
            Instantiate_OffsetNoteImage();


        }

        //5개 생성 및 리스트에 추가 그리고 오브젝트 비활성화

    }

    void Instantiate_OffsetNoteImage()
    {
        GameObject Image = Instantiate(DummyOffsetNoteImage);

        OffsetNoteList.Add(Image);

        Image.gameObject.SetActive(false);
    }

    IEnumerator OnDisableOffsetNote(int Listcount)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("여기서도 작동됨");
        OffsetNoteList[Listcount].SetActive(false);
        count--;
    }




}
