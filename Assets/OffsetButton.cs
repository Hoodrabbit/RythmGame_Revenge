using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OffsetButton : MonoBehaviour
{
    Button button;
    public AudioClip clip;

    private void Start()
    {
        button = GetComponent<Button>();
        //button.onClick.AddListener();
    }

    public void btnClick()
    {
        //GameManager.Instance.MainAudio.clip = clip;
        SceneManagerEX.Instance.GoOffsetScene();
    }




}
