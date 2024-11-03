using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpriteListScriptable : ScriptableObject
{
    public int id;
    public Sprite Notesprite;
    public Animator NoteAnimator;
}







public class SpriteLoaderScript : Singleton<SpriteLoaderScript>
{
    [Header("일반 노트 스프라이트")]
    public List<Sprite> NoteSpriteList;

    


    [Header("롱노트 스프라이트")]
    public List<Sprite> LongNoteSpriteList;

    [Header("기타 등등 ")]
    public List<Sprite> ETCSpriteList;
    public List<SpriteListScriptable> asdf;

    // Start is called before the first frame update
    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }



}
