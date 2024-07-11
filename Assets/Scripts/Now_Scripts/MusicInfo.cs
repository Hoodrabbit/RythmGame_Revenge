using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicInfo : ScriptableObject
{
    public string Artist_Name;

    public string Music_Name;

    public AudioClip Music;

    public float BPM;

    public string NoteFileDirection;
}
