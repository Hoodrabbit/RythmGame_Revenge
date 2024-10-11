using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetNote : Note
{
    public GameObject DummyOffsetNoteImage;

    List<GameObject> OffsetNoteList = new List<GameObject>();
    int MaxCount_Start = 5;


    public int count = 0; //���� ������� ��Ʈ ��ȣ�� ���� üũ���� ����

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
                Debug.Log("�۵���");
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

        //5�� ���� �� ����Ʈ�� �߰� �׸��� ������Ʈ ��Ȱ��ȭ

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
        Debug.Log("���⼭�� �۵���");
        OffsetNoteList[Listcount].SetActive(false);
        count--;
    }




}
