using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LongNoteColliderAdjust : MonoBehaviour
{
    private BoxCollider2D bodyCollider;
    private SpriteRenderer spriteRenderer;

    public GameObject Head; // 왼쪽 고정 오브젝트
    public GameObject Tail; // 이동하는 오브젝트

    private Vector3 previousHeadPosition;
    private Vector3 previousTailPosition;

    void Start()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 초기 위치 저장
        previousHeadPosition = Head.transform.position;
        previousTailPosition = Tail.transform.position;

        UpdateLine();
    }

    void Update()
    {
        // 위치 변화 감지
        if (Head.transform.position != previousHeadPosition || Tail.transform.position != previousTailPosition)
        {
            UpdateLine();

            // 위치 갱신
            previousHeadPosition = Head.transform.position;
            previousTailPosition = Tail.transform.position;
        }
    }

    private void UpdateLine()
    {
        // A와 B 사이의 거리 계산
        Vector3 headPosition = Head.transform.position;
        Vector3 tailPosition = Tail.transform.position;
        float distance = (tailPosition - headPosition).magnitude;

        // 스프라이트 크기 및 위치 조정
        spriteRenderer.size = new Vector2(distance, 2);
        transform.position = (headPosition + tailPosition) / 2f;

        // 콜라이더 크기 동기화
        bodyCollider.size = new Vector2(distance - 1, spriteRenderer.size.y);
    }
}


