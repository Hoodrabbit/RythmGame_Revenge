using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    Button btn;


    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(BackScene);
    }

    void BackScene() //일단 지금 당장은 뒤로 가기 씬 눌렀을때 곡 선택창으로 가도록
    {
        SceneManagerEX.Instance.GoSelectSongScene();
    }


}
