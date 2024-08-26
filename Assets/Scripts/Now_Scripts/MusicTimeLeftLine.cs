using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}
