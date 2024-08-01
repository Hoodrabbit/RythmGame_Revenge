using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public KeyCode key;

    public GameObject note;
    bool active = false;
    
    public bool longnotePress = false;
    float pressTime = 0;


    public static float PlayTime;
    public List<float> songtimes = new List<float>();
    AudioSource audioSource;
    LongNoteScript aa;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("PlayTime : " + PlayTime);
        Debug.Log(GameManager.Instance.GetBPS());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (active == true)
            {
                aa = note.GetComponent<LongNoteScript>();

                if (aa == null && longnotePress == false)
                {
                   
                    //Debug.Log("���� ������ �۵�");
                    note.SetActive(false);
                    note = null;
                    audioSource.Play();
                    songtimes.Add(PlayTime);
                    PlayManager.Instance.HitNote();
                }
                else
                {
                    Debug.Log("�۵��ϳ���");
                    aa.StopHeadPos(transform.position);
                    longnotePress = true;

                }
                
            }

           


            //�̽��� �ٸ� �������
        }

        if(Input.GetKeyUp(key))
        {
            if(longnotePress == true)
            {
                longnotePress = false;
                aa.CancelStopHeadPos();

                //���� ���� ������ ã�� ���ϵ��� �ؾ� �� �� ����

                //active = false;
            }


        }
        if (longnotePress == true)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= GameManager.Instance.GetBPS()/2)
            {
                PlayManager.Instance.HitNote();
                pressTime -= GameManager.Instance.GetBPS()/2;
            }
            //�뷡�� 16��Ʈ���� �޺� ������Ű��

            //if()

        }

        //switch (GameManager.Instance.state)
        //{
        //    case GameState.None:
        //        break;
        //    case GameState.Play_Mode:
        //        PlayTime += Time.deltaTime;
        //        if (Input.GetKeyDown(key))
        //        {
        //            if (note != null)
        //            {
        //                Debug.Log("���� ������ �۵�");
        //                note.SetActive(false);
        //                note = null;
        //                audioSource.Play();
        //                songtimes.Add(PlayTime);

        //            }

        //        }
        //        break;
        //    case GameState.Debug_Mode:
        //        PlayTime += Time.deltaTime;
        //        if (Input.GetKeyDown(key))
        //        {
        //            //Destroy(note);
        //            songtimes.Add(PlayTime);
        //            audioSource.Play();
        //        }
        //        break;
        //    default:
        //        break;
        //}


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note"))
        {
            active = true;

            note = collision.gameObject;



        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

}
