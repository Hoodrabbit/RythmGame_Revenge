using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButtonScript : MonoBehaviour
{
    public GameObject ToggleObj;
    Button btn;

    public void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleButton);
    }


    public void ToggleButton()
    {
        ToggleObj.SetActive(!ToggleObj.activeSelf);
    }
}
