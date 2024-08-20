using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Expand : MonoBehaviour
{
    Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(DisableInteraction);
    }


    public void DisableInteraction()
    {
       
        btn.interactable = false;
        StartCoroutine(EnableInteraction(0.1f));

    }


    IEnumerator EnableInteraction(float delay)
    {
        yield return new WaitForSeconds(delay);
        btn.interactable = true;
    }



}
