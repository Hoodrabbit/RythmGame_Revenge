using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteScript : MonoBehaviour
{
    public GameObject Body; // 롱노트 몸통
    //롱노트의 경우 끝 노트의 위치와 시작 노트의 줄이 동일해야 함 
    //일단 어떻게 될 지 궁금하니까 놔둬보고 해보는 걸로
    public GameObject Tail;

    public Note n_Y;

    SpriteRenderer HeadSR;
    SpriteRenderer Body_SR;

    public float newWidth = 3f; //시작과 끝의 길이
    float ChangeHeight = 2f;

    public Vector3 initialScale;

    public Vector3 EndPos;

    public bool IsConnect = false;

    BoxCollider2D HeadCollider;

    public bool Delete = false;


    public void Start()
    {
        initialScale = Body.transform.localScale;
        HeadSR = GetComponent<SpriteRenderer>();
        Body_SR = Body.GetComponent<SpriteRenderer>();
        HeadCollider = GetComponent<BoxCollider2D>();
        n_Y = GetComponent<Note>();
        ChangeSprite();
    }

    public void Update()
    {
        if (IsConnect == false)
        {
            if (transform.position.x < Tail.transform.position.x)
            {
                newWidth = Vector3.Distance(transform.position, Tail.transform.position);

                if (Body_SR.drawMode == SpriteDrawMode.Sliced || Body_SR.drawMode == SpriteDrawMode.Tiled)
                {
                    IsConnect = true;

                }
            }
            else
            {
                if (transform.position.x == Tail.transform.position.x)
                {
                    newWidth = Vector3.Distance(transform.position, Tail.transform.position);

                    if (Body_SR.drawMode == SpriteDrawMode.Sliced || Body_SR.drawMode == SpriteDrawMode.Tiled)
                    {
                        // 현재 SpriteRenderer의 size를 가져와서 width만 변경
                        //Vector2 newSize = Body_SR.size;

                        //newSize.x = newWidth;

                     

                        //newSize.y = ChangeHeight;
                        //Body_SR.size = newSize;
                        //Body.transform.localPosition = new Vector3(newSize.x, 0);
                    }
                }
            }
        }
        else
        {

            if (Vector3.Distance(transform.position, Tail.transform.position) <= 1.3f || transform.position.x >= Tail.transform.position.x)
                {
                    Debug.Log("꺼짐");
                Delete = true;
                //gameObject.SetActive(false);

            }
                else
                {
                    newWidth = Vector3.Distance(transform.position, Tail.transform.position);

                    if (Body_SR.drawMode == SpriteDrawMode.Sliced || Body_SR.drawMode == SpriteDrawMode.Tiled)
                    {

                }
                }
            
        }

        if (Delete==true)
        {
            
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }






    //실제로 멈추는게 아니라 현재 위치에서 x값이 0이 되도록 서서히 움직이게 함
    public void StopHeadPos(double songTime)
    {
        //Vector2.Lerp(transform.position, judgepos, Time.deltaTime * 10);
        n_Y = GetComponent<Note>();
        n_Y.enabled = false;
        Tail.GetComponent<Note>().enabled = true;


        float value = Mathf.Abs((float)songTime - GameManager.Instance.MainAudio.time);

        StartCoroutine(MovetoJudge(value));
        //x가 0의 위치로 서서히 이동학도록 만들어주는 코루틴을 추가해줘야 함

    }

    public void CancelStopHeadPos()
    {
        if (n_Y != null)
        {
            n_Y.enabled = true;
            Tail.GetComponent<Note>().enabled = false;
            UnenabledLongNote();
        }
    }

    public void UnenabledLongNote()
    {
        //아예 판정선에서 누르지 못했을 경우도 발동해줘야함

        //HeadCollider.enabled = false;
        HeadSR.color = new Color(159, 0, 255, 50);
    }

    public void ChangeSprite()
    {
        Tail.GetComponent<SpriteRenderer>().sprite = HeadSR.sprite;
        if(transform.position.y >0)
        {
            Body_SR.sprite = SpriteLoaderScript.Instance.LongNoteSpriteList[n_Y.ID - 100 + 2];
        }
        else
        {
            Body_SR.sprite = SpriteLoaderScript.Instance.LongNoteSpriteList[n_Y.ID - 100 + 3];
        }
        
    }



    //매개변수로 현재 시간과 노래의 시간을 받음 해당 노래의 차이 값
    IEnumerator MovetoJudge(float DifferTime)
    {
        float startTime = 0;


        while(startTime < DifferTime) 
        {
            Debug.Log("작동되는 중");


            transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y), startTime/DifferTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(0, transform.position.y);
    }




}
