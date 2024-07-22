using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNotetest : MonoBehaviour
{
    public GameObject Body; // �ճ�Ʈ ����
    //�ճ�Ʈ�� ��� �� ��Ʈ�� ��ġ�� ���� ��Ʈ�� ���� �����ؾ� �� 
    //�ϴ� ��� �� �� �ñ��ϴϱ� ���ֺ��� �غ��� �ɷ�
    public GameObject Tail;


    SpriteRenderer Body_SR;

    public float newWidth = 3f; //���۰� ���� ����

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
                // ���� SpriteRenderer�� size�� �����ͼ� width�� ����
                Vector2 newSize = Body_SR.size;
                newSize.x = newWidth;
                Body_SR.size = newSize;
            }
        }
        else
        {
            //�߸��� ���
            //Destroy(gameObject);
        }

        
    }



}
