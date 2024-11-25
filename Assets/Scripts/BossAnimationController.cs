using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : Singleton<BossAnimationController>
{

    CircleCollider2D BossCollider;

    public Animator BossAnimator;
    public Animator BossNormalState;
    public Animator BossDashState;

    public void Start()
    {
        BossAnimator = GetComponent<Animator>();
        BossCollider = GetComponent<CircleCollider2D>();
    }

    public void ShootNote()
    {
        BossNormalState.SetTrigger("ShotNote");
    }

    public void Damaged()
    {
        BossNormalState.SetTrigger("Damaged");
    }

    public void OperatingVisaulize()
    {
        BossNormalState.SetBool("Disable", BossAnimator.GetBool("GetOut"));
        BossAnimator.SetBool("GetOut", BossAnimator.GetBool("GetOut"));
        //비활성화 상태로 전환 보스는 뒤로 물러나는 연출을 선보임
    }

    public void Dash()
    {
        BossAnimator.SetBool("Dash", true);
    }
    public void PlayDashAniStart()
    {
        BossAnimator.ResetTrigger("Damaged");
        BossAnimator.SetBool("Dash", true);
        BossAnimator.Play("BossDashState");
       // BossDashState.SetBool("Dash", false);
        BossDashState.Play("attack_2");
    }
    
    //돌진상태 막바지 손을뻗는 애니메이션
    public void PlayDashAniEnd()
    {
        Debug.Log("3 애니메이션 작동");

        BossAnimator.ResetTrigger("Damaged");
        BossAnimator.SetBool("Dash", true);
        BossAnimator.Play("BossDashState", 0);

        if(BossDashState.gameObject.activeSelf == true)
        {
            Debug.Log("작동되는지 확인용");

            BossDashState.SetBool("Dash", true);
            BossDashState.Play("3");
        }
        else
        {
            Debug.Log("허허");
            BossDashState.gameObject.SetActive(true);
            BossDashState.Play("3");
            BossDashState.SetBool("Dash", true);
            

        }
        
    }

    public void TurnOffDash()
    {
        BossAnimator.SetBool("Dash", false);
        BossDashState.SetBool("Dash", false);
    }

}