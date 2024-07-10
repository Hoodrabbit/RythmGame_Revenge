using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

public class NoteMaker : MonoBehaviour
{
    static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
    static string NoteDataPath = Path.Combine(NoteDataFolder, "RainyDayNoteData.txt");


    StreamReader NoteParsing = new StreamReader(NoteDataPath);
    int NoteCount;

    public GameObject Note;
    GameObject Notes;

    // Start is called before the first frame update
    void Start()
    {
        Notes = new GameObject("Notes");
        Notes.transform.position = Vector3.zero;
        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            NoteCount = Int32.Parse(NoteParsing.ReadLine());
            for (int i = 0; i < NoteCount; i++)
            {
                Instantiate(Note,
                            new Vector2(float.Parse(NoteParsing.ReadLine()) * (10) - 0.0436521739130443f/*¿ÀÇÁ¼Â*/, 
                            0),
                            Quaternion.identity,Notes.transform);
            }
        }

    }
}
