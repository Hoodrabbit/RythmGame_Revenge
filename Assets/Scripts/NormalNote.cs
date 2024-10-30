using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNote : Note
{
    public GameObject MelodyObj;
    public List<Sprite> melodySprite;

    public NormalNoteType NowNoteSize;

    public Animator MonsterAnimator;


    bool event_On = false;

    protected override void Start()
    {
        base.Start();
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
        SpriteRenderer SR = MelodyObj.GetComponent<SpriteRenderer>();

        switch (num)
        {
            case 0:
                melodyType = MelodyType.Normal;
                SR.color = Color.clear;
                break;
            case 1:
                melodyType = MelodyType.Yellow;
                //SR.sprite = melodysprite[0];
                SR.color = Color.yellow;
                break;
            case 2:
                melodyType = MelodyType.Purple;

                SR.color = Color.cyan;
                break;


        }

    }

    public MelodyType GetMelodyType()
    {
        return melodyType;
    }
}
