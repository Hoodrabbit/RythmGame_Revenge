using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int Score = 0;
    public TMP_Text ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if(Score ==0)
        {
            
        }
        else
        {
            ScoreText.text = Score.ToString();
        }
        
    }

    public void IncreaseScore()
    {
        Score += 200;
    }



}
