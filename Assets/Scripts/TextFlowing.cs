using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TextFlowing : MonoBehaviour
{
    public RectTransform rect;
    float endxpos;

    float speed;


    private void Start()
    {
        speed = Random.Range(5, 17);
    }

    // Update is called once per frame
    void Update()
    {

        if(rect.anchoredPosition.x >= endxpos)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - speed * 100 * Time.deltaTime, rect.anchoredPosition.y);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }



    public void Init_TextPos(Vector2 InitPos)
    {
        endxpos = -InitPos.x;
        rect.anchoredPosition = InitPos;
    }


}
