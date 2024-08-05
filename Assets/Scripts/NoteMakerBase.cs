using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMakerBase : MonoBehaviour
{
    protected RaycastHit2D[] hit;

    protected BarNote barNote;


    protected virtual void Awake()
    {
        barNote = FindObjectOfType<BarNote>();
    }


    protected virtual void Update()
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Pos; //���콺�� ���� ��ġ�� ��Ʈ�� ���������ִ� ������Ʈ�� ��ġ�� �� �ֵ��� ��

        if (Input.GetMouseButtonDown(0))
        {
            AreaCheck(transform.position, false);
        }

        if (Input.GetMouseButton(1))
        {
            AreaCheck(transform.position, true);
        }
    }

    protected virtual void AreaCheck(Vector2 pos, bool check) { }


}
