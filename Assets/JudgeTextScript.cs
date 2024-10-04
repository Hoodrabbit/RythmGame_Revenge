using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JudgeTextScript : MonoBehaviour
{

    float LifeTime = 0.4f;
    float TTIme = 0f;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveUP());
    }

    private void Update()
    {
        if(TTIme >= LifeTime)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator MoveUP()
    {
        while(TTIme < LifeTime)
        {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y+0.1f);
                TTIme += Time.deltaTime;
                yield return null;
        }

        
    }


}
