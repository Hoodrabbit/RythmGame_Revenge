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
        transform.position = Pos; //마우스의 현재 위치에 노트를 생성시켜주는 오브젝트가 위치할 수 있도록 함

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
