using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteScript : MonoBehaviour
{
    public GameObject Body; // �ճ�Ʈ ����
    //�ճ�Ʈ�� ��� �� ��Ʈ�� ��ġ�� ���� ��Ʈ�� ���� �����ؾ� �� 
    //�ϴ� ��� �� �� �ñ��ϴϱ� ���ֺ��� �غ��� �ɷ�
    public GameObject Tail;

    Note n_Y;

    SpriteRenderer HeadSR;
    SpriteRenderer Body_SR;

    public float newWidth = 3f; //���۰� ���� ����
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
                    // ���� SpriteRenderer�� size�� �����ͼ� width�� ����
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
                        // ���� SpriteRenderer�� size�� �����ͼ� width�� ����
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
                    Debug.Log("����");
                Delete = true;
                //gameObject.SetActive(false);

            }
                else
                {
                    newWidth = Vector3.Distance(transform.position, Tail.transform.position);

                    if (Body_SR.drawMode == SpriteDrawMode.Sliced || Body_SR.drawMode == SpriteDrawMode.Tiled)
                    {
                        // ���� SpriteRenderer�� size�� �����ͼ� width�� ����

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
        //�ƿ� ���������� ������ ������ ��쵵 �ߵ��������

        //HeadCollider.enabled = false;
        HeadSR.color = new Color(159, 0, 255, 50);
    }


}
