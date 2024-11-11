using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyTypeScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator MelodyAnimator;
    public void ChangeMelody(int num)
    {
        switch(num) 
        {
            case 0:
                spriteRenderer.color = Color.clear;
                //MelodyAnimator.enabled = false;
                break;
            case 1:
                spriteRenderer.color = Color.white;
                //MelodyAnimator.enabled = true;
                MelodyAnimator.SetBool("MelodyChange", false);
                break;
            case 2:
                spriteRenderer.color = Color.white;
                //MelodyAnimator.enabled = true;
                MelodyAnimator.SetBool("MelodyChange", true);
                break;
        }
    }
}
