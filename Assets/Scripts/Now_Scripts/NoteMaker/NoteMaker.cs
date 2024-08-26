using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMaker : MonoBehaviour
{
    public GameObject LongNoteEdit;
    Button btn;


    public void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(NoteNote);
    }


    public void NoteNote()
    {
        Instantiate(LongNoteEdit);
    }
}
