using UnityEngine;
using UnityEngine.UI;

public class NoteMaker_EditScene : MonoBehaviour
{
    public GameObject Note_Edit_V; //에딧 씬에서 사용할 노트
    Button NoteMakerButton;
    GameObject Note;

    public void Start()
    {
        NoteMakerButton = GetComponent<Button>();

        NoteMakerButton.onClick.AddListener(Instantiate_Note);

    }


    public void Instantiate_Note()
    {
        //노트가 없을 때는 노트를 생성해주는 기능
        //반대로 있을 때는 노트를 제거해주는 기능(꺼주는 기능)
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
