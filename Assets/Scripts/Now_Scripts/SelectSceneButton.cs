using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSceneButton : MonoBehaviour
{

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SceneChange);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            button.onClick.Invoke();
        }
    }


    void SceneChange()
    {
        SceneManagerEX.Instance.ChangeScene();
        
    }


}
