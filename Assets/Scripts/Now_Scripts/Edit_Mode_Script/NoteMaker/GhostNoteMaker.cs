using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GhostNoteMaker : NoteMakerBase
{
    public GameObject GhostNote;

    protected override void Awake()
    {
        base.Awake();
        NoteType = 3;
    }


    public override GameObject Note { get => GhostNote; set => GhostNote = value; }
}
