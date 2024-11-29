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
        if(!FeverSystem.Instance.IsFever)
        {
            StartCoroutine(MoveUP());
        }
        else
        {

            //스케일크기 늘리기
            Vector3 currentScale = rect.localScale;
            rect.localScale = currentScale * 1.5f;
            StartCoroutine(MoveUP());
        }
      
    }


    IEnumerator MoveUP()
    {
       
            while (TTIme < LifeTime)
            {
                if (Time.timeScale == 1)
                {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 0.5f);
                TTIme += Time.deltaTime;
                
                }
            yield return null;
            }
            Destroy(gameObject);
    }


}
