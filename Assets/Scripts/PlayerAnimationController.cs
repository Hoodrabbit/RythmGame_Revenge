using Spine;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator MainAnimator;
    public Animator NormalStateAnimator;

    public RuntimeAnimatorController MainweaponController;
    public RuntimeAnimatorController SubWeaponController;




    Rigidbody2D PlayerRigid;
    RigidbodyConstraints2D normalConstraints;




    public float UP_Player = 2;
    public float DOWN_Player = -3;

    Vector2 UpPos;
    Vector2 DownPos;




    public List<Judgement> Judgements = new List<Judgement>();

    [Header("캐릭터 검 공격 모션")]
    public List<GameObject> knife_motion_obj = new List<GameObject>();

    [Header("캐릭터 망치 공격 모션")]
    public List<GameObject> hammer_motion_obj = new List<GameObject>();

    void Start()
    {
        MainAnimator = GetComponent<Animator>();
        PlayerRigid = GetComponent<Rigidbody2D>();
        normalConstraints = PlayerRigid.constraints;
        UpPos = new Vector2(transform.position.x, UP_Player);
        DownPos = new Vector2(transform.position.x, DOWN_Player);

        NormalStateAnimator.keepAnimatorStateOnDisable = true;

        if (Judgements.Count > 0)
        {
            foreach (var judge in Judgements)
            {
                judge.PressEvent_NoneHit += SetRandom;
                judge.PressEvent_Hit += SetRandom_Hit;
                judge.HoldingEndEvent += HoldingEnd;
                judge.HoldingEvent += Holding;
                judge.MissNoteEvent += DamagedMotion;
            }
        }

    }

    private void OnDisable()
    {
        if (Judgements.Count > 0)
        {
            foreach (var judge in Judgements)
            {
                judge.PressEvent_NoneHit += SetRandom;
                judge.PressEvent_Hit -= SetRandom_Hit;
                judge.HoldingEndEvent -= HoldingEnd;
                judge.HoldingEvent -= Holding;
                judge.MissNoteEvent -= DamagedMotion;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, DOWN_Player, UP_Player);
        transform.position = position;


        if(Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeWeapon();
        }

    }

    void SetRandom(JudgementHeight_State height)
    {
        if (height == JudgementHeight_State.UP)
        {
            //addforce impulse
            //PlayerRigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);
            Debug.Log("점프 작동 확인");
            PlayerRigid.velocity = Vector2.zero;
            transform.position = UpPos;

            //이걸 애니메이션 스크립트에 넣어야 할 것 같음
            //Vector2.Lerp(transform.position, UpPos, 1);


        }

        if (height == JudgementHeight_State.DOWN)
        {
            transform.position = DownPos;
            //StopCoroutine(JumpRoutine());
            //StartCoroutine(FallRoutine());
        }
        PlayerRigid.constraints = normalConstraints;
        PlayerRigid.isKinematic = false;

        PlayerAttack();
    }

    void SetRandom_Hit(JudgementHeight_State height)
    {


        if (height == JudgementHeight_State.UP)
        {
            //addforce impulse
            //PlayerRigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);
            //Debug.Log("점프 작동 확인");
            //SwordJumpMotion();
            PlayerRigid.velocity = Vector2.zero;
            transform.position = UpPos;

            //이걸 애니메이션 스크립트에 넣어야 할 것 같음
            //Vector2.Lerp(transform.position, UpPos, 1);


        }

        if (height == JudgementHeight_State.DOWN)
        {
            transform.position = DownPos;
        }
        //PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        //PlayerRigid.isKinematic = true;

        PlayerAttack();
    }



    void Holding(JudgementHeight_State height)
    {
        Debug.Log("홀딩 작동");

        if (height == JudgementHeight_State.UP)
        {
            transform.position = UpPos;
            PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            transform.position = DownPos;
        }

        NormalStateAnimator.SetBool("LongNoteHold", true);

        //PlayerRigid.isKinematic = true;
    }

    void HoldingEnd(JudgementHeight_State height)
    {
        Debug.Log("홀딩 종료 확인");

        NormalStateAnimator.SetBool("LongNoteHold", false);

        //PlayerRigid.isKinematic = false;
        PlayerRigid.constraints = normalConstraints;
        PlayerRigid.velocity = Vector2.down;

        
    }


    IEnumerator JumpRoutine()
    {
        float StartTime = 0f;
        float EndTime = 0.1f;

        while (StartTime < EndTime)
        {
            // Debug.Log("작동이 되나요");

            Debug.Log(StartTime / EndTime);

            Vector2.Lerp(transform.position, UpPos, StartTime / EndTime);

            StartTime += Time.deltaTime;
            yield return null;
        }


    }

    IEnumerator FallRoutine()
    {
        float StartTime = 0f;
        float EndTime = 0.2f;
        //PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        while (StartTime <= EndTime)
        {
            Debug.Log("작동이 되나요");
            Vector2.Lerp(transform.position, DownPos, StartTime / EndTime);

            StartTime += Time.deltaTime;
            yield return null;
        }
        transform.position = DownPos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            
        }
    }


    IEnumerator ChangeNormalState()
    {
        yield return new WaitForSeconds(1.5f);
        SetNormal();
    }

    public void SetNormal()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");

        PlayerRigid.constraints = normalConstraints;
        PlayerRigid.isKinematic = false;
    }

    public void ChangeWeapon()
    {
        //해당 코드를 이용하여 애니메이터 변경
        //노말 상태의 애니메이터 bool값 변경

        if (MainAnimator.runtimeAnimatorController == MainweaponController)
        {
            MainAnimator.runtimeAnimatorController = SubWeaponController;
            NormalStateAnimator.keepAnimatorStateOnDisable = true;
            NormalStateAnimator.SetBool("KNIFE", false);
            NormalStateAnimator.SetBool("HAMMER", true);
        }
        else
        {
            MainAnimator.runtimeAnimatorController = MainweaponController;
            NormalStateAnimator.keepAnimatorStateOnDisable = true;
            NormalStateAnimator.SetBool("KNIFE", true);
            NormalStateAnimator.SetBool("HAMMER", false);
        }

    }

    public void PlayerAttack()
    {
        int randomnum = Random.Range(0, 3);

        switch (randomnum)
        {
            case 0:
                MainAnimator.SetTrigger("Attack1");
                break;

            case 1:
                MainAnimator.SetTrigger("Attack2");
                break;
            default:
                MainAnimator.SetTrigger("Attack3");
                break;
        }
    }

    public void DamagedMotion()
    {
        //Debug.Log("작동횟수");
        //if(!MainAnimator.GetCurrentAnimatorClipInfo())
        //{
            NormalStateAnimator.SetTrigger("Damaged");

        PlayerController.Instance.TakeHPMethod(20);
    }




    public void CopyAnimatorParameters(Animator sourceAnimator, Animator targetAnimator)
    {
        foreach (AnimatorControllerParameter param in sourceAnimator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Float:
                    targetAnimator.SetFloat(param.name, sourceAnimator.GetFloat(param.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    targetAnimator.SetInteger(param.name, sourceAnimator.GetInteger(param.name));
                    break;
                case AnimatorControllerParameterType.Bool:
                    targetAnimator.SetBool(param.name, sourceAnimator.GetBool(param.name));
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if (sourceAnimator.GetBool(param.name))
                    {
                        targetAnimator.SetTrigger(param.name);
                    }
                    break;
            }
        }
    }










}
