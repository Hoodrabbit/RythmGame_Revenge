using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    //파일 관리를 해줄 스크립트 게임 매니저의 스크립터블에서 파일 주소를 뽑아옴
    static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
    public List<GameObject> EditNotes = new List<GameObject>();

    void ListArrange()
    {
       // EditNotes.Sort
    }


    public void SaveNote()
    {
        string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
        
        //노트 리스트 정보에 대해서 파싱을 함
        
    }

    public void LoadNote()
    {
        string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
    }

    //static string NoteTxt = GameManager.Instance.musicInfo.NoteFileDirection;
    ////이렇게 하면 되려나


    //static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
    //static string NoteDataPath = Path.Combine(NoteDataFolder, NoteTxt);


    //StreamReader NoteParsing = new StreamReader(NoteDataPath);
    //int NoteCount;

    //public GameObject Note;
    //GameObject Notes;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Notes = new GameObject("Notes");
    //    Notes.transform.position = Vector3.zero;
    //    if(GameManager.Instance.state == GameState.Play_Mode)
    //    {
    //        NoteCount = Int32.Parse(NoteParsing.ReadLine());
    //        for (int i = 0; i < NoteCount; i++)
    //        {
    //            Instantiate(Note,
    //                        new Vector2(float.Parse(NoteParsing.ReadLine()) * (10) - 0.0436521739130443f/*오프셋*/, 
    //                        0),
    //                        Quaternion.identity,Notes.transform);
    //        }
    //    }

    //}





}
