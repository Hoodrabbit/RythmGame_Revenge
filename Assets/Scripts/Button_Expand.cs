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
        //Navigation nav = btn.navigation;
        //nav.mode = Navigation.Mode.None;
        //btn.navigation = nav;
        btn.onClick.AddListener(DisableInteraction);
    }


    public void DisableInteraction()
    {
        ButtonController.btnController.Onclick(gameObject.name);
        btn.interactable = false;
        StartCoroutine(EnableInteraction(0.1f));

    }


    IEnumerator EnableInteraction(float delay)
    {
        yield return new WaitForSeconds(delay);
        btn.interactable = true;
    }



}
