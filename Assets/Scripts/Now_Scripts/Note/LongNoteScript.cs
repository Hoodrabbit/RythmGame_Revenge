using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteScript : MonoBehaviour
{
    public GameObject Body; // 롱노트 몸통
    //롱노트의 경우 끝 노트의 위치와 시작 노트의 줄이 동일해야 함 
    //일단 어떻게 될 지 궁금하니까 놔둬보고 해보는 걸로
    public GameObject Tail;

    Note n_Y;

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
                    // 현재 SpriteRenderer의 size를 가져와서 width만 변경
                    //Vector2 newSize = Body_SR.size;
                    //newSize.x = newWidth;
                    //newSize.y = ChangeHeight;
                    //Body_SR.size = newSize;
                    //Body.transform.localPosition = new Vector3(newSize.x, 0);
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

            if (Vector3.Distance(transform.position, Tail.transform.position) <= 0.2f || transform.position.x >= Tail.transform.position.x)
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
                        // 현재 SpriteRenderer의 size를 가져와서 width만 변경

                    //    Vector2 newSize = Body_SR.size;
                    //    newSize.x = newWidth;
                    //newSize.y = ChangeHeight;
                    //    Body_SR.size = newSize;
                    //Body.transform.localPosition = new Vector3(newSize.x, 0);
                }
                }
            
        }

        if (Delete==true)
        {
            
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }

    public void StopHeadPos(Vector3 judgepos)
    {
        //Vector2.Lerp(transform.position, judgepos, Time.deltaTime * 10);
        n_Y = GetComponent<Note>();
        n_Y.enabled = false;
        Tail.GetComponent<Note>().enabled = true;
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


}
