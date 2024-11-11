using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNote : Note
{
    public MelodyTypeScript MelodyObj;

    public NormalNoteType NowNoteSize;

    public Animator MonsterAnimator;


    bool event_On = false;

    protected override void Start()
    {
        base.Start();
        MelodyObj = GetComponentInChildren<MelodyTypeScript>();
        MonsterAnimator.enabled = false;


    }



    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameManager.Instance.state == GameState.Play_Mode && StartSong)
        {
            if(ypos<0)
            {
                transform.position = new Vector2(xpos - GameManager.Instance.speed * (float)(AudioSettings.dspTime - AudioTime), transform.position.y);
            }
            else
            {
                transform.position = new Vector2(xpos - GameManager.Instance.speed * (float)(AudioSettings.dspTime - AudioTime), transform.position.y);
            }
            

        }



        if (transform.position.x <= 20 && !event_On)
        {
            event_On = true;

            MonsterAnimator.enabled = true;


            Determining_NoteCurve();
            
        }


    }




    protected override void ChangeSprite()
    {
    }







    public void SetNoteType(int num)
    {
        //SpriteRenderer SR = GetComponent<SpriteRenderer>();
       
        if(MelodyObj == null)
        {
            Debug.Log("none");
        }
        switch (num)
        {
            case 0:
                melodyType = MelodyType.Normal;
                MelodyObj.ChangeMelody(num);
                break;
            case 1:
                melodyType = MelodyType.Yellow;
                MelodyObj.ChangeMelody(num);


                break;
            case 2:
                melodyType = MelodyType.Purple;
                MelodyObj.ChangeMelody(num);
                break;


        }

    }

    public MelodyType GetMelodyType()
    {
        return melodyType;
    }
}
