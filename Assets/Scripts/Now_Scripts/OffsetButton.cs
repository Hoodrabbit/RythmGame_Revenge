using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class OffsetButton : MonoBehaviour
{
    //Button button;
    //public AudioClip clip;
    float Offset_Value;





    private void Start()
    {
        //button = GetComponent<Button>();
        //button.onClick.AddListener();
    }





    //public void btnClick()
    //{
    //    //GameManager.Instance.MainAudio.clip = clip;
    //    SceneManagerEX.Instance.GoOffsetScene();
    //}

    public void IncreaseOffset()
    {
        OffsetUIController.OffsetValue += 1;
    }

    public void DecreaseOffset()
    {
        OffsetUIController.OffsetValue -=1;
    }











}
