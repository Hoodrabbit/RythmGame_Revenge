using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand_Line : MonoBehaviour
{
    bool Activate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicFunction();




    }

    //이름 미정
    void BasicFunction()
    {

        if(EditManager.Instance == null)
        {
            if (transform.position.x <= 0 && Activate == false)
            {

                Camera.main.GetComponent<MainAudioScript>().ExpandScreen();
                PlayManager.Instance.ControlHidingJudjement();
                Activate = true;

            }
            
        }
        else
        {
            if (GameManager.Instance.MainAudio.isPlaying == true && Activate == false)
            {
                if (transform.position.x <= 0)
                {
                    EditManager.Instance.ChangeScreenSize();
                    //gameObject.SetActive(false);
                    Activate = true;

                }
            }

            if (GameManager.Instance.MainAudio.isPlaying == false)
            {
                if (transform.position.x > 0)
                {
                    if (Activate == true)
                    {
                        EditManager.Instance.ChangeScreenSize();
                    }
                    Activate = false;

                }

            }
        }

        
    }



}
