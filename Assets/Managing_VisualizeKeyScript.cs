using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managing_VisualizeKeyScript : MonoBehaviour
{
    [SerializeField] private List<InputableKeyScript> ShowKeyPanelList;

    private void Start()
    {
        Init_Script();
    }


    private void Init_Script()
    {
        foreach (var InputableKeyPanel in ShowKeyPanelList) 
        {
            if(GameManager.Instance.keyPressType == KeyPressType.A_Type)
            {
                InputableKeyPanel.Select_A_Type();
            }
            else
            {
                InputableKeyPanel.Select_B_Type();
            }
        }
    }


}
