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
        //스도코드
        //드롭다운에서 선택한 버튼에 따라서 옮길 씬을 정할 수 있음
        //바꿀 때마다 해당 메서드가 실행됨 

        Debug.Log("작동됬어요");
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
