using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButtonScript : MonoBehaviour
{
    public GameObject ToggleObj;
    List<ToggleButtonScript> toggleList = new List<ToggleButtonScript>();
    Button btn;

    public void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleButton);
        ToggleButtonScript[] toggleButtonScripts = FindObjectsOfType<ToggleButtonScript>();
        foreach(ToggleButtonScript script in toggleButtonScripts)
        {
            toggleList.Add(script);
        }
    }


    public void ToggleButton()
    {
        ToggleObj.SetActive(!ToggleObj.activeSelf);
        foreach(ToggleButtonScript script in toggleList)
        {
            if(script != this)
            {
                if(script.ToggleObj.activeSelf == true)
                {
                    script.ToggleObj.SetActive(false);
                }
                
            }
            
        }
    }
}
