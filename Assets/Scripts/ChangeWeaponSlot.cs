using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeWeaponSlot : MonoBehaviour
{

    [SerializeField] GameObject KnifeWeapon;
    [SerializeField] GameObject HammerWeapon;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(KnifeWeapon.activeSelf == true)
            {
                KnifeWeapon.SetActive(false);
                HammerWeapon.SetActive(true);
            }
            else
            {
                HammerWeapon.SetActive(false);
                KnifeWeapon.SetActive(true);
            }


        }


    }
}
