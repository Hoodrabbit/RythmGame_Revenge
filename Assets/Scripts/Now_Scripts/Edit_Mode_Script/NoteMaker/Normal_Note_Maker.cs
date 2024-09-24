using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;




public class Normal_Note_Maker : NoteMakerBase
{
    public GameObject NormalNote;

    protected override void Awake()
    {
        base.Awake();
        NoteType = 1;
    }


    public override GameObject Note { get => NormalNote; set => NormalNote = value; }

}

  
