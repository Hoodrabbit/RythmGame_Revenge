using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    public GameObject Normal; //���� ������ ����� ��Ʈ
    public GameObject Long; //���� ������ ����� ��Ʈ
    Button NoteMakerButton;
    GameObject Note_Normal;
    GameObject Note_Long;

    bool Already_Using = false;

    public void Start()
    {
        NoteMakerButton = GetComponent<Button>();

        //NoteMakerButton.onClick.AddListener(Instantiate_NormalNote);

    }


    public void Instantiate_NormalNote()
    {
        //�������ִ� ��Ʈ�� ���� ���� ��Ʈ�� �������ִ� ���
        //�ݴ�� ���� ���� ��Ʈ�� �������ִ� ���(���ִ� ���)


        if (Note_Normal == null)
        {

            if(!Already_Using)
            {
                Note_Normal = Instantiate(Normal, new Vector3(0, 0, 0), Quaternion.identity);
                Already_Using = true;
            }
            else
            {
                Note_Long.SetActive(false);
                Note_Normal = Instantiate(Normal, new Vector3(0, 0, 0), Quaternion.identity);
                Already_Using = true;
            }
            
        }
        else
        {
            if (Note_Normal.activeSelf == true)
            {
                Note_Normal.SetActive(false);
                Already_Using = false;
            }
            else
            {

                if (Already_Using)
                {
                    Note_Long.SetActive(false);
                    Note_Normal.SetActive(true);
                    Already_Using = true;
                }
                else
                {
                    Note_Normal.SetActive(true);
                    Already_Using = true;
                }




            }
        }
    }

    public void Instantiate_LongNote()
    {
        //���� �ּ��� ����

        if (Note_Long == null)
        {

            if (!Already_Using)
            {
                Note_Long = Instantiate(Long, new Vector3(0, 0, 0), Quaternion.identity);
                Already_Using = true;
            }
            else
            {
                Note_Normal.SetActive(false);
                Note_Long = Instantiate(Long, new Vector3(0, 0, 0), Quaternion.identity);
                Already_Using = true;
            }


            
        }
        else
        {
            if (Note_Long.activeSelf == true)
            {
                Note_Long.SetActive(false);
                Already_Using = false;
            }
            else
            {
                if (Already_Using)
                {
                    Note_Normal.SetActive(false);
                    Note_Long.SetActive(true);
                    Already_Using = true;
                }
                else
                {
                    Note_Long.SetActive(true);
                    Already_Using = true;
                }

            }
        }
    }




}
