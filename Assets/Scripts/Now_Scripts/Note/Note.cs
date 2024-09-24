using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class Note : MonoBehaviour
{
    [Header("노트 타입")]
    public int TypeNum;
   

    Rigidbody2D rb;
    [Space(20)]
    public float speed;

    public double SongTime;

    public double AudioTime;

    public MelodyType Type = MelodyType.Normal;
    public NoteHeight Height;
    public float xpos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GameManager.Instance.speed;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        AudioTime = AudioSettings.dspTime;
        xpos = transform.position.x;
        //if (Type == MelodyType.Obstacle)
       // {
            SpriteRenderer ChangeColor = GetComponent<SpriteRenderer>();
            Debug.Log(transform.position.y);


        if(TypeNum== 1 || TypeNum == 2)
        {
            if (transform.position.y >= 0)
                ChangeColor.color = Color.red;
            else
                ChangeColor.color = Color.blue;
        }
            
       // }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Instantiate(FindObjectOfType<OffsetUIController>().OffsetNote, new Vector3(xpos + 10 * (float)(AudioSettings.dspTime - AudioTime), 0), Quaternion.identity);
        }

  


    }


    // Update is called once per frame
    void FixedUpdate()
    {
  
        
        


        if (GameManager.Instance.state == GameState.Play_Mode)
        {
            if (transform.position.x <= -5 && GameManager.Instance.MainAudio.isPlaying == false)
            {
                //Debug.Log("�۵�����");
                //GameManager.Instance.PlayMusic();
            }

            
                transform.position = new Vector2(xpos - GameManager.Instance.speed*(float)(AudioSettings.dspTime - AudioTime), transform.position.y);
                //AudioTime += AudioSettings.dspTime - AudioTime;
            



            
        }
        else if(GameManager.Instance.state == GameState.Offset_Mode)
        {
            if ((float)(AudioSettings.dspTime - AudioTime) >= 2.4f)
            {
                transform.position = new Vector3(xpos, 0);
                AudioTime = AudioSettings.dspTime;
            }


            //transform.position = new Vector2(transform.position.x + 10 * Time.fixedDeltaTime, transform.position.y);
          //  transform.position = new Vector2(xpos + 10 * (float)(AudioSettings.dspTime - AudioTime), transform.position.y);

            

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            PlayManager.Instance.MissNote();
        }

        if(collision.CompareTag("Curve"))
        {
            Debug.Log("감지");
            Animator animator = GetComponent<Animator>();


            if (Height == NoteHeight.UP)
            {
                animator.SetTrigger("UpCurve");
            }
            else
            {
                animator.SetTrigger("DownCurve");
            }

            
            
        }

    }

    public void SetSongTime(double songTime)
    {
        SongTime = songTime;
    }

    public void SetNoteType(int num)
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
       

        switch (num)
        {
           

            case 0:
                Type = MelodyType.Normal; break;
            case 1:
                Type = MelodyType.White;
                SR.color = Color.gray;
                break;
            case 2:
                Type = MelodyType.Dark;
                SR.color = Color.black;
                break;


        }

    }

    public MelodyType GetMelodyType()
    {
        return Type;
    }


    public void HitNote()
    {
        gameObject.SetActive(false);
    }

    public void MissNote()
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();

        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.2f);

    }

    public void SetAudioTime()
    {
        xpos = transform.position.x;
        AudioTime = AudioSettings.dspTime;
    }

    public void DownNoteCurving()
    {
        StartCoroutine(DownNoteMove());
    }

    IEnumerator DownNoteMove()
    {

        float elapsed = 0.0f;

        while (elapsed < GameManager.Instance.GetBPS())
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -1), elapsed / (GameManager.Instance.GetBPS()));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 이동 완료 후 최종 위치를 정확히 설정
        transform.position = new Vector3(transform.position.x, -1);
        Debug.Log("체크");
        yield return null;
    }


    public void UpNoteCurving()
    {
        StartCoroutine(UpNoteMove());
    }

    IEnumerator UpNoteMove()
    {
        float elapsed = 0.0f;

        while (elapsed < GameManager.Instance.GetBPS())
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 3), elapsed / (GameManager.Instance.GetBPS()));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 이동 완료 후 최종 위치를 정확히 설정
        transform.position = new Vector3(transform.position.x, 3);
        Debug.Log("체크");
        yield return null;
    }
   


}
