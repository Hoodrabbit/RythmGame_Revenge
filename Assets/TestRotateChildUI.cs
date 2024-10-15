using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestRotateChildUI : MonoBehaviour
{

    public int childCount;
    public GameObject childObj;

    float RotateAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakingMusicSlot()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float centerX = rectTransform.anchoredPosition.x;
        float centerY = rectTransform.anchoredPosition.y;

        Debug.Log("pos : " + centerX + ", " + centerY);
        RotateAngle = (360 / childCount);

        float radius = rectTransform.rect.width;
        for (int i = 0; i < childCount; i++)
        {
            float angle = i * (360 / childCount);
            float angleRadians = i * (2 * Mathf.PI / childCount);
            float x = radius * Mathf.Cos(angleRadians);
            float y = radius * Mathf.Sin(angleRadians);

            GameObject obj = Instantiate(childObj, transform.position, Quaternion.Euler(0, 0, angle), transform);
            RectTransform childrect = obj.GetComponent<RectTransform>();
            if (childrect != rectTransform)
            {
                Debug.Log("anchoredpos : " + x + ", " + y);
                childrect.anchoredPosition = new Vector3(x, y, 0);
                Debug.Log(childrect.anchoredPosition);
            }


        }
        transform.GetChild(1).transform.SetAsLastSibling();
        transform.GetChild(0).transform.SetAsLastSibling();

    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("윗방향키 누름");


            StartCoroutine(UpRotate());
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DownRotate());


        }



    }

    IEnumerator DownRotate()
    {
        Quaternion transformR = transform.rotation;
        Quaternion ChangeR = Quaternion.Euler(0, 0,transformR.z - RotateAngle);
        float differ = transform.rotation.z - ChangeR.z;

        float rotationDuration = 0.1f;
        float timeElapsed = 0f;



        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;
            
            transform.rotation = Quaternion.Lerp(transformR, ChangeR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("작동");

        transform.rotation = Quaternion.Euler(0,0, ChangeR.z);

        transform.GetChild(1).transform.SetAsLastSibling();
        transform.GetChild(0).transform.SetAsLastSibling();
    }

    IEnumerator UpRotate()
    {
        Quaternion transformR = transform.rotation;
        Quaternion ChangeR = Quaternion.Euler(0, 0, transformR.z + RotateAngle);


         float rotationDuration = 0.1f;
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;

            transform.rotation = Quaternion.Lerp(transformR, ChangeR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("작동");

        transform.rotation = Quaternion.Euler(0, 0, ChangeR.z);

        transform.GetChild(childCount - 2).transform.SetAsLastSibling();
    }



    //num
    //Vector2 SetPos(int num)
    //{
    //    float pos = 


    //    return Vector2.zero;
    //}





}
