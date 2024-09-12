using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    public double SongTime;

    public double AudioTime;

    public NoteType Type = NoteType.Normal;
    public float xpos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GameManager.Instance.speed;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //rb.velocity = new Vector2(0, 0);
        //SongTime = AudioSettings.dspTime;
        AudioTime = AudioSettings.dspTime;
        xpos = transform.position.x;
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
       // Debug.Log(AudioTime + " , " + (AudioSettings.dspTime - AudioTime));


        if(transform.position.x <=0)
        {
            //Debug.Log(SongTime + "  ,  " + AudioSettings.dspTime);
        }


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
            transform.position = new Vector2(xpos + 10 * (float)(AudioSettings.dspTime - AudioTime), transform.position.y);

            

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            PlayManager.Instance.MissNote();
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
                Type = NoteType.Normal; break;
            case 1:
                Type = NoteType.White;
                SR.color = Color.gray;
                break;
            case 2:
                Type = NoteType.Dark;
                SR.color = Color.black;
                break;


        }

    }

    public NoteType GetNoteType()
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



}
