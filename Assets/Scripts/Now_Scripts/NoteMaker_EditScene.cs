using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    public GameObject Normal; //���� ������ ����� ��Ʈ
    public GameObject Long; //���� ������ ����� ��Ʈ
    Button NoteMakerButton;
    GameObject Note_Normal;
    GameObject Note_Long;

    public void Start()
    {
        NoteMakerButton = GetComponent<Button>();

        //NoteMakerButton.onClick.AddListener(Instantiate_NormalNote);

    }


    public void Instantiate_NormalNote()
    {
        //�������ִ� ��Ʈ�� ���� ���� ��Ʈ�� �������ִ� ���
        //�ݴ�� ���� ���� ��Ʈ�� �������ִ� ���(���ִ� ���)
        if(Note_Normal == null)
        {
            Note_Normal = Instantiate(Normal, new Vector3(0,0,0), Quaternion.identity);
        }
        else
        {
            if(Note_Normal.activeSelf == true)
            {
                Note_Normal.SetActive(false);
            }
            else
            {
                Note_Normal.SetActive(true);
            }
        }
    }

    public void Instantiate_LongNote()
    {
        //���� �ּ��� ����

        if (Note_Long == null)
        {
            Note_Long = Instantiate(Long, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            if (Note_Long.activeSelf == true)
            {
                Note_Long.SetActive(false);
            }
            else
            {
                Note_Long.SetActive(true);
            }
        }
    }




}
