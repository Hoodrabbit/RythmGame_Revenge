using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNote : Note
{
    public GameObject MelodyObj;
    public List<Sprite> melodySprite;


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
