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
    [Header("�Ϲ� ��Ʈ ��������Ʈ")]
    public List<Sprite> NoteSpriteList;

    


    [Header("�ճ�Ʈ ��������Ʈ")]
    public List<Sprite> LongNoteSpriteList;

    [Header("��Ÿ ��� ")]
    public List<Sprite> ETCSpriteList;
    public List<SpriteListScriptable> asdf;

    // Start is called before the first frame update
    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }



}
