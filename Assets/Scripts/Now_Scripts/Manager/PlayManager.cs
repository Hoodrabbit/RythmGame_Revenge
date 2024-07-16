using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이씬을 관리하는 싱글톤
public class PlayManager : Singleton<PlayManager>
{
    public GameObject Note;
    public List<GameObject> Notes;


    public void PlayScene_NoteMaker(float xpos, int heightnum)
    {
        //Notes.Add(Instantiate(xpos));//내일 할거임
    }
}
