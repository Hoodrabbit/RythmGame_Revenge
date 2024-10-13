using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LongNoteColliderAdjust : MonoBehaviour
{
    BoxCollider2D bodyCollider;
    SpriteRenderer spriteRenderer;

    public GameObject Head; // 왼쪽 고정 오브젝트
    public GameObject Tail; // 이동하는 오브젝트



    // Start is called before the first frame update
    void Start()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(spriteRenderer.size.x / 2, transform.position.y);
        // A와 B 사이의 거리 계산
        float distance = Vector3.Distance(Head.transform.position, Tail.transform.position);

        spriteRenderer.size = new Vector2(distance, 2);



        // 스프라이트 위치 업데이트
        Vector3 newPosition = (Head.transform.position + Tail.transform.position) / 2f;
        transform.position = newPosition;

        //bodyCollider.offset = new Vector2(spriteRenderer.size.x/2, 0);
        bodyCollider.size = new Vector2(spriteRenderer.size.x-1,transform.localScale.y);
    }
}

