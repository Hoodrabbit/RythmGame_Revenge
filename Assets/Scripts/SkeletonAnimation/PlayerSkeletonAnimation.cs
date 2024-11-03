using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkeletonAnimation : MonoBehaviour
{
    public Animator playerAnimator;
    bool ChangeWeapon = false;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!ChangeWeapon)
            {
                ChangeWeapon = true;
            }
            else
            {
                ChangeWeapon = false;
            }

            playerAnimator.SetBool("Change", ChangeWeapon);




        }
    }
}
