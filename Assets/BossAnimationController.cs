using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public Animator BossAnimator;
    CircleCollider2D BossCollider;



    public Animator BossNormalState;
    public Animator BossDashState;

    public void Start()
    {
        BossAnimator = GetComponent<Animator>();
        BossCollider = GetComponent<CircleCollider2D>();
    }



    //아래의 함수 중 특정 함수의 경우에는 


    public void ShootNote()
    {
        BossNormalState.SetTrigger("ShotNote");
        //현재 나와있는 도중 노트가 자신을 지날 경우 제일 작은 노트만 해당 됨
        //자신의 중앙에서 투명화가 해제되면서 발사되고 보스는 공격 모션을 취함(루프가 아닌 트리거 발동 형식)
    }

    public void Damaged()
    {
        BossNormalState.SetTrigger("Damaged");
        //대쉬상태에서 플레이어가 해당 보스를 히트시켰을 경우 피격 상태로 전환
    }

    //public void EnterScreen()
    //{
    //    BossNormalState.SetBool("Disable", false);
    //    //활성화 상태로 전환
    //}

    public void OperatingVisaulize()
    {
        BossNormalState.SetBool("Disable", BossAnimator.GetBool("GetOut"));
        BossAnimator.SetBool("GetOut", BossAnimator.GetBool("GetOut"));

        //비활성화 상태로 전환 보스는 뒤로 물러나는 연출을 선보임
    }

    public void Dash()
    {
        BossAnimator.SetBool("Dash", true);
        //대쉬 상태 활성화
    }

    public void TurnOffDash()
    {
        BossAnimator.SetBool("Dash", false);
        BossDashState.SetBool("Dash", false);
    }

}
