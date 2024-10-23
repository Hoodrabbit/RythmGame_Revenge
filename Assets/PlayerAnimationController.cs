using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimationController : MonoBehaviour
{
    Animator MainAnimator;



    Rigidbody2D PlayerRigid;
    RigidbodyConstraints2D normalConstraints;




    public float UP_Player = 2;
    public float DOWN_Player= -3;

    Vector2 UpPos;
    Vector2 DownPos;




    public List<Judgement> Judgements = new List<Judgement>();

    [Header("캐릭터 검 장착 모션")]
    public List<GameObject> knife_motion_obj = new List<GameObject>();
    public List<Animator> Knife_Motions = new List<Animator>();

    [Header("캐릭터 망치 장착 모션")]
    public List<GameObject> hammer_motion_obj = new List<GameObject>();
    public List<Animator> Hammer_Motions = new List<Animator>();

    // Start is called before the first frame update
    void Start()
    {
        MainAnimator = GetComponent<Animator>();
        PlayerRigid = GetComponent<Rigidbody2D>();
        normalConstraints = PlayerRigid.constraints;
        UpPos = new Vector2(transform.position.x, UP_Player);
        DownPos = new Vector2(transform.position.x, DOWN_Player);



        foreach (var obj in knife_motion_obj)
        {
            Knife_Motions.Add(obj.GetComponent<Animator>());
        }

        foreach(var obj in hammer_motion_obj)
        {
            Hammer_Motions.Add(obj.GetComponent<Animator>());
        }

        if(Judgements.Count >0)
        {
            foreach (var judge in Judgements)
            {
                judge.PressEvent += SetRandom;
                judge.HoldingEndEvent += HoldingEnd;
                judge.HoldingEvent += Holding;
            }
        }
        





    }

    private void OnDisable()
    {
        if (Judgements.Count > 0)
        {
            foreach (var judge in Judgements)
            {
                judge.PressEvent += SetRandom;
                judge.HoldingEndEvent -= HoldingEnd;
                judge.HoldingEvent -= Holding;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!MainAnimator.GetBool("IsHammer"))
            {
                MainAnimator.SetBool("IsHammer", true);
            }
            else
            {
                MainAnimator.SetBool("IsHammer", false);
            }
            
        }

    }

    void SetRandom(JudgementHeight_State height)
    {
        int randNum = Random.Range(0, 3);

        if(height == JudgementHeight_State.UP)
        {
            //addforce impulse
            //PlayerRigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);
            Debug.Log("점프 작동 확인");
            //SwordJumpMotion();
            StopCoroutine(FallRoutine());
            StartCoroutine(JumpRoutine());

            //이걸 애니메이션 스크립트에 넣어야 할 것 같음
            //Vector2.Lerp(transform.position, UpPos, 1);


        }

        if(height == JudgementHeight_State.DOWN)
        {
            StopCoroutine(JumpRoutine());
            StartCoroutine(FallRoutine());
        }


        AttackMotion(randNum);
    }

    void Holding(JudgementHeight_State height)
    {
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        PlayerRigid.isKinematic = true;
    }

    void HoldingEnd(JudgementHeight_State height)
    {
        PlayerRigid.constraints = normalConstraints;
        PlayerRigid.isKinematic = false;
    }


    IEnumerator JumpRoutine()
    {
        float StartTime = 0f;
        float EndTime = 0.1f;
        
        while (StartTime < EndTime)
        {
           // Debug.Log("작동이 되나요");

            Debug.Log(StartTime / EndTime);

            Vector2.Lerp(transform.position, UpPos, StartTime/EndTime);

            StartTime += Time.deltaTime;
            yield return null;
        }

        transform.position = UpPos;
    }

    IEnumerator FallRoutine()
    {
        float StartTime = 0f;
        float EndTime = 0.03f;
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        while (StartTime < EndTime)
        {
            Debug.Log("작동이 되나요");
            Vector2.Lerp(transform.position, DownPos, StartTime / EndTime);

            StartTime += Time.deltaTime;
            yield return null;
        }
        transform.position = DownPos;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            PlayerController.Instance.TakeHPMethod(20);
            gameObject.layer = LayerMask.NameToLayer("Damaged");
            MainAnimator.SetTrigger("Damaged");
            collision.gameObject.SetActive(false);
            
        }
    }

    public void SetNormal()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }



    public void AttackMotion(int num)
    {
        switch (num)
        {
            case 0:
                MainAnimator.SetTrigger("Attack1");
                break;
            case 1:
                MainAnimator.SetTrigger("Attack2");
                break;
            case 2:
                MainAnimator.SetTrigger("Attack3");
                break;
        
        }


        PlayerRigid.constraints = normalConstraints;
        PlayerRigid.isKinematic = false;
        //MainAnimator.SetTrigger(num);



    }



    public void SwordDamagedMotion()
    {
        Knife_Motions[0].SetTrigger("Damaged");
    }

    public void SwordJumpMotion()
    {
        Knife_Motions[0].SetTrigger("Jump");
    }

    public void HammerJumpMotion()
    {
        //MainAnimator.SetBool("IsHammer", true);
        Hammer_Motions[0].SetTrigger("Jump");
    }

    public void HammerDamagedMotion()
    {
        Hammer_Motions[0].SetTrigger("Damaged");
    }


}
