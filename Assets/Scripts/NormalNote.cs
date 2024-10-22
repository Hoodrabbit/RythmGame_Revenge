using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNote : Note
{
    public GameObject MelodyObj;
    public List<Sprite> melodySprite;

    public NormalNoteType NowNoteSize;

    bool event_On = false;


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameManager.Instance.state == GameState.Play_Mode && StartSong)
        {
            if(ypos<0)
            {
                transform.position = new Vector2(xpos - GameManager.Instance.speed * (float)(AudioSettings.dspTime - AudioTime), ypos);
            }
            else
            {
                transform.position = new Vector2(xpos - GameManager.Instance.speed * (float)(AudioSettings.dspTime - AudioTime), ypos );
            }
            

        }



        if (transform.position.x <= 20 && !event_On)
        {
            event_On = true;
            Determining_NoteCurve();
            
        }


    }




    protected override void ChangeSprite()
    {
        ////노트 변경할 꺼
        ////스테이지 크기에 따라 노트 변경시키기

        //if(StageSelect == SelectStage.Stage1)
        //{
        //    if(NowNoteSize == NormalNoteType.Normal)
        //    {
        //        if (transform.position.y >= 0)
        //        {
        //            spriteRenderer.sprite = SpriteLoaderScript.Instance.NoteSpriteList[ID];
        //        }
        //        else
        //        {
        //            spriteRenderer.sprite = SpriteLoaderScript.Instance.NoteSpriteList[ID + 1];
        //        }
        //    }
        //    else
        //    {
        //        if (transform.position.y >= 0)
        //        {
        //            spriteRenderer.sprite = SpriteLoaderScript.Instance.NoteSpriteList[ID+2];
        //        }
        //        else
        //        {
        //            spriteRenderer.sprite = SpriteLoaderScript.Instance.NoteSpriteList[ID + 3];
        //        }
        //    }





       //}

      
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
