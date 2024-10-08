using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicTimeLeftLine : MonoBehaviour
{
    Image MusicTimeLine;
    // Start is called before the first frame update
    void Start()
    {
        MusicTimeLine = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        MusicTimeLine.fillAmount = PlayManager.Instance.SetLine();

        if(MusicTimeLine.fillAmount >= 1)
        {

            //여기에 GameManager 내 메서드 실행시켜줌
            GameManager.Instance.GetScoreAndCombo(ScoreSystem.Instance.Score, ComboSystem.Instance.Combo);

            SceneManager.LoadScene("GameResult");

            Debug.Log("게임 종료");
            //gameManager 또는 저장 받는 매니저 스크립트 혹은 따로 세이브 파일 같은 걸 만들어서 해주면 좋을 것 같음
            //game result Scene 띄워줌
        }



    }
}
