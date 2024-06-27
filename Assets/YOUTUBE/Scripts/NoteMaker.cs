using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

public class NoteMaker : MonoBehaviour
{
    static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
    static string NoteDataPath = Path.Combine(NoteDataFolder, "NoteData.txt");


    StreamReader NoteParsing = new StreamReader(NoteDataPath);
    int NoteCount;

    public GameObject Note;


    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            NoteCount = Int32.Parse(NoteParsing.ReadLine());
            for (int i = 0; i < NoteCount; i++)
            {
                Instantiate(Note,
                            new Vector2(float.Parse(NoteParsing.ReadLine()) + 10 * (i + 1), 0),
                            Quaternion.identity);
            }
            //여기에서 리스트 내의 인덱스 갯수 체크해서 그만큼 생성시켜주도록    
        }

    }
}
