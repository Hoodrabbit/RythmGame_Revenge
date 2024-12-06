using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VisualizeScoreAlphabet : MonoBehaviour
{
    public List<Sprite> Alphabet = new List<Sprite>();
    public Image AlphabetImg;


    public void VisualizeImage(float Accuracy)
    {
        AlphabetImg.enabled = true;

        if (Accuracy >= 0.85)
        {
            AlphabetImg.sprite = Alphabet[3];
        }
        else if(Accuracy >=0.8)
        {
            AlphabetImg.sprite = Alphabet[2];
        }
        else if(Accuracy >=0.7)
        {
            AlphabetImg.sprite = Alphabet[1];
        }
        else if(Accuracy <0.7)
        {
            AlphabetImg.sprite= Alphabet[0];
        }
    }

    public void UnvisualizeImage()
    {
        AlphabetImg.enabled = false;
    }

}
