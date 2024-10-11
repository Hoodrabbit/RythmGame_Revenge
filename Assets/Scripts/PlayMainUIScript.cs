using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMainUIScript : MonoBehaviour
{
    public int StartHP;
    int NowHP;
    Image UIImage;

    // Start is called before the first frame update
    void Start()
    {
        UIImage = GetComponent<Image>();
        StartHP = PlayerController.Instance.hp;
    }

    // Update is called once per frame
    void Update()
    {
        NowHP = PlayerController.Instance.hp;

        UIImage.fillAmount = (float)NowHP / StartHP;


    }
}
