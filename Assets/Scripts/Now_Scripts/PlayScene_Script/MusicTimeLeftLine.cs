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

            //���⿡ GameManager �� �޼��� ���������
            GameManager.Instance.GetScoreAndCombo(ScoreSystem.Instance.Score, ComboSystem.Instance.Combo);

            SceneManager.LoadScene("GameResult");

            Debug.Log("���� ����");
            //gameManager �Ǵ� ���� �޴� �Ŵ��� ��ũ��Ʈ Ȥ�� ���� ���̺� ���� ���� �� ���� ���ָ� ���� �� ����
            //game result Scene �����
        }



    }
}
