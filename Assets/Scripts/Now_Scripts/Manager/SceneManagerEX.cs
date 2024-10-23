using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


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


    public void GoSelectSongScene()
    {
        GameManager.Instance.state = GameState.None;
        SceneManager.LoadScene("SelectSong");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene((int)scene_T+1);
    }

    public void GoPlayScene()
    {
        SceneManager.LoadScene("Play");
    }


}
