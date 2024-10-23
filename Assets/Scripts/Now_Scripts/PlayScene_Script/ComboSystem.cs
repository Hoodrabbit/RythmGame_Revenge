using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboSystem : Singleton<ComboSystem>
{
    public int Combo = 0;
    public int MaxCombo = 0;
    int FullNoteCount = 0;
    public TMP_Text ComboText;
    Animator Combo_Animator;


    // Start is called before the first frame update
    void Start()
    {
        ComboText = GetComponent<TMP_Text>();
        Combo_Animator =GetComponent<Animator>();
        ComboText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       // if (Combo >=5)
      //  {
            ComboText.enabled = true;
        ComboText.text = Combo.ToString();
      //  }
      //  else
        {
      //      ComboText.enabled = false;
        }
    }

    public void HitNote()
    {
        Combo_Animator.Play("IncreaseCombo");
        Combo++;
        MaxCombo++;


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
