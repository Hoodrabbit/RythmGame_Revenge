using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DifficultButtonController : MonoBehaviour
{
    public static DifficultButtonController instance;
    
    public List<Button> Buttons = new List<Button>();



    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.difficultState == DifficultState.Normal)
        {
            Buttons[0].onClick.Invoke();
            Buttons[0].GetComponent<Animator>().SetBool("Selected", true);
            Buttons[1].GetComponent<Animator>().SetBool("Selected", false);
        }
        else
        {
            Buttons[1].onClick.Invoke();
            Buttons[1].GetComponent<Animator>().SetBool("Selected", true);
            Buttons[0].GetComponent<Animator>().SetBool("Selected", false);
        }
    }

    public void PressNormal()
    {
        Buttons[0].GetComponent<Animator>().SetBool("Selected", true);
        Buttons[1].GetComponent<Animator>().SetBool("Selected", false);
    }


    public void PressHard()
    {
        Buttons[1].GetComponent<Animator>().SetBool("Selected", true);
        Buttons[0].GetComponent<Animator>().SetBool("Selected", false);
    }



}
