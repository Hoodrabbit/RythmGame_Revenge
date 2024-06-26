using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoteMaker : MonoBehaviour
{
    StreamReader NoteParsing = new StreamReader("SaveNoteData/NoteData.txt");
    int NoteCount;

    public GameObject Note;


    // Start is called before the first frame update
    void Awake()
    {

        //여기에서 리스트 내의 인덱스 갯수 체크해서 그만큼 생성시켜주도록    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
