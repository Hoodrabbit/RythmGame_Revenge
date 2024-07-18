using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene_Type
{
    SelectSong,
    NoteEdit,
    OffsetMode,
    Play
};

public class SceneManagerEX : Singleton<SceneManagerEX>
{
    public Scene_Type scene_T = Scene_Type.NoteEdit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoOffsetScene()
    {
        GameManager.Instance.state = GameState.Offset_Mode;
        SceneManager.LoadScene("Offset");
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene((int)scene_T);
    }


}
