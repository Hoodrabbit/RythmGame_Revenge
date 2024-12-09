using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultViewScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> DifficultStateImage = new List<GameObject>();

    public void ShowDifficultState()
    {
        if(GameManager.Instance.difficultState == DifficultState.Normal)
        {
            DifficultStateImage[0].SetActive(true);
        }
        else
        {
            DifficultStateImage[1].SetActive(true);
        }
    }


}
