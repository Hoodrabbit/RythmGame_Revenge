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

        //���⿡�� ����Ʈ ���� �ε��� ���� üũ�ؼ� �׸�ŭ ���������ֵ���    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
