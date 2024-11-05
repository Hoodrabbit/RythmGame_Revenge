using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultyControlDropDown : MonoBehaviour
{
    TMP_Dropdown Difficultydropdown;

    readonly List<string> difficult = new List<string> { "Easy", "Normal", "Hard" };

    private void Start()
    {
        Difficultydropdown = GetComponent<TMP_Dropdown>();
        Difficultydropdown.options.Clear();
        Difficultydropdown.AddOptions(difficult);
        Difficultydropdown.value = (int)GameManager.Instance.difficultState;
        Difficultydropdown.onValueChanged.AddListener((delegate { ChangeDifficult(Difficultydropdown.value); }));

    }

    void ChangeDifficult(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.difficultState = DifficultState.Easy;
                break;
            case 1:
                GameManager.Instance.difficultState = DifficultState.Normal;
                break;
            case 2:
                GameManager.Instance.difficultState = DifficultState.Hard;
                break;
        }
    }
}
