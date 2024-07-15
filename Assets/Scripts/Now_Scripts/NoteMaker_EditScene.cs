using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    public GameObject Note_Edit_V; //���� ������ ����� ��Ʈ
    Button NoteMakerButton;
    GameObject Note;

    public void Start()
    {
        NoteMakerButton = GetComponent<Button>();

        NoteMakerButton.onClick.AddListener(Instantiate_Note);

    }


    public void Instantiate_Note()
    {
        //��Ʈ�� ���� ���� ��Ʈ�� �������ִ� ���
        //�ݴ�� ���� ���� ��Ʈ�� �������ִ� ���(���ִ� ���)
        if(Note == null)
        {
            Note = Instantiate(Note_Edit_V, new Vector3(0,0,0), Quaternion.identity);
        }
        else
        {
            if(Note.activeSelf == true)
            {
                Note.SetActive(false);
            }
            else
            {
                Note.SetActive(true);
            }
        }
    }


}
