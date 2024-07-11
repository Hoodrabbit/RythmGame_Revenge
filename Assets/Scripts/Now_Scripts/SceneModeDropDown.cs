using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneModeDropDown : MonoBehaviour
{
    Dropdown dropdown;
    //Scene_Type scene_t = Scene_Type.NoteEdit;


    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(GetValue);
    }

    void GetValue(int Value)
    {
        //�����ڵ�
        //��Ӵٿ�� ������ ��ư�� ���� �ű� ���� ���� �� ����
        //�ٲ� ������ �ش� �޼��尡 ����� 

        Debug.Log("�۵�����");
        if(Value == 0)
        {
            GameManager.Instance.state = GameState.Debug_Mode;
            SceneManagerEX.Instance.scene_T = Scene_Type.NoteEdit;
        }
        else if(Value == 1)
        {
            GameManager.Instance.state = GameState.Play_Mode;
            SceneManagerEX.Instance.scene_T = Scene_Type.Play;
        }

    }




}
