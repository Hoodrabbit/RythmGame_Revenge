using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeWeaponSlot : MonoBehaviour
{

    public GameObject Knife;
    public GameObject Hammer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Knife.activeSelf == true)
            {
                Knife.SetActive(false);
                Hammer.SetActive(true);
            }
            else
            {
                Hammer.SetActive(false);
                Knife.SetActive(true);
            }
        }


    }
}
