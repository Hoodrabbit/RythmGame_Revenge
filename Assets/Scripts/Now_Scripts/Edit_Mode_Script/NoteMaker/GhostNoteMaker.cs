using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GhostNoteMaker : NoteMakerBase
{
    public GameObject GhostNote;

    public override GameObject Note { get => GhostNote; set => GhostNote = value; }
}
