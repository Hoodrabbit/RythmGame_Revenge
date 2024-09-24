using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameResultPanel : MonoBehaviour
{
    public Text SongName;
    public Text ArtistName;

    public Text Score;
    public Text Combo;
    public Text etc; //Perfect, Great, Good, Miss �̷� �͵� �ؽ�Ʈ�� ��Ÿ���� ���� �ѹ��� ������ �ϸ� ���� �� ��


    // Start is called before the first frame update
    void Start()
    {
        SongName.text = GameManager.Instance.musicInfo.Music_Name.ToString();
        ArtistName.text = GameManager.Instance.musicInfo.Artist_Name.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
