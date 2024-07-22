using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    public GameObject Normal; //에딧 씬에서 사용할 노트
    public GameObject Long; //에딧 씬에서 사용할 노트
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
        //생성해주는 노트가 없을 때는 노트를 생성해주는 기능
        //반대로 있을 때는 노트를 제거해주는 기능(꺼주는 기능)
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
        //위에 주석과 동일

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
