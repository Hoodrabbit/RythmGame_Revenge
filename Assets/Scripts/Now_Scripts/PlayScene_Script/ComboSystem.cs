using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboSystem : MonoBehaviour
{
    public int Combo = 0;
    int FullNoteCount = 0;
    public TMP_Text ComboText;

    // Start is called before the first frame update
    void Start()
    {
        ComboText = GetComponent<TMP_Text>();
        ComboText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       // if (Combo >=5)
      //  {
            ComboText.enabled = true;
            ComboText.text = Combo.ToString() + " / " + FullNoteCount.ToString();
      //  }
      //  else
        {
      //      ComboText.enabled = false;
        }
    }

    public void HitNote()
    {
        Combo++;
    }    

    public void MissNote()
    {
        Combo = 0;
    }

    public void SetFullNoteCount(int count)
    {
        FullNoteCount = count;
    }


}
