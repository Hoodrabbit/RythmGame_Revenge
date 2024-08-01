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

    void BackScene() //�ϴ� ���� ������ �ڷ� ���� �� �������� �� ����â���� ������
    {
        SceneManagerEX.Instance.GoSelectSongScene();
    }


}
