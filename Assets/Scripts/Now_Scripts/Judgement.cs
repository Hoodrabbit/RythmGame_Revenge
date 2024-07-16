using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    public KeyCode key;

    public GameObject note;
    bool active = false;
    public static float PlayTime;
    public List<float> songtimes = new List<float>();
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("PlayTime : " + PlayTime);
    }

    // Update is called once per frame
    void Update()
    {

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
        //                Debug.Log("내가 눌러서 작동");
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
