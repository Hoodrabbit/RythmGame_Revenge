using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNotetest : MonoBehaviour
{
    public GameObject Body; // 롱노트 몸통
    //롱노트의 경우 끝 노트의 위치와 시작 노트의 줄이 동일해야 함 
    //일단 어떻게 될 지 궁금하니까 놔둬보고 해보는 걸로
    public GameObject Tail;


    SpriteRenderer Body_SR;

    public float newWidth = 3f; //시작과 끝의 길이

    public Vector3 initialScale;

    public Vector3 EndPos;

    public void Start()
    {
        initialScale = Body.transform.localScale;
        Body_SR = Body.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if(transform.position.x < Tail.transform.position.x)
        {
            newWidth = Vector3.Distance(transform.position, Tail.transform.position);

            if (Body_SR.drawMode == SpriteDrawMode.Sliced || Body_SR.drawMode == SpriteDrawMode.Tiled)
            {
                // 현재 SpriteRenderer의 size를 가져와서 width만 변경
                Vector2 newSize = Body_SR.size;
                newSize.x = newWidth;
                Body_SR.size = newSize;
            }
        }
        else
        {
            //잘못된 경우
            //Destroy(gameObject);
        }

        
    }



}
