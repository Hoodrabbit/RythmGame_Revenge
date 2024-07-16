using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Linq;
using System;

public class NotePosition //찍은 노트에 대한 정보를 담아 줄 클래스
{
    public GameObject Note;
    public float xpos; // 노트의 X 값(에디터 상 정보이기 때문에 나중에 실제 게임 씬에서는 해당 데이터에 추가로 오프셋이나 속도에 따라 위치를 조절할 수 있음
    public int HeightValue; //노트 높이 번호로 저장받아서 나중에 불러올 때는 그 번호에 따라 Y값을 특정 값으로 할당시킴

    public NotePosition(GameObject Note, float x, int h)
    {
        this.Note = Note;
        xpos = x;
        HeightValue = h;
    }



}




public class DataManager : Singleton<DataManager>
{
    //파일 관리를 해줄 스크립트 게임 매니저의 스크립터블에서 파일 주소를 뽑아옴
#if UNITY_EDITOR
    static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
#else
        static string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#endif
    string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);

    public List<NotePosition> EditNotes = new List<NotePosition>();



    protected override void Awake()
    {
        base.Awake();
        if (!Directory.Exists(NoteDataFolder))
        {
            Directory.CreateDirectory(NoteDataFolder);
        }
    }


    public void ListNullCheck() //해당 리스트 중에서 null이 존재하는지 확인하고 만약 존재한다면 해당 데이터는 제거함
    {
        int i = EditNotes.Count - 1;
        while (i >= 0)
        {
            if (EditNotes[i].Note == null)
            {
                Debug.Log(i + "이거 지워요");
                EditNotes.RemoveAt(i);
            }
            i--;
        }
    }


    public void SaveNote()
    {
        EditNotes = EditNotes.OrderBy(N => N.xpos).ToList();

        if (File.Exists(NoteDataPath))
        {
            Debug.Log("파일 존재");
            StreamWriter writer = File.CreateText(NoteDataPath);


            ListNullCheck();
            writer.WriteLine(EditNotes.Count);

            foreach (NotePosition np in EditNotes)
            {

                writer.WriteLine($"{np.xpos} , {np.HeightValue}");
            }
            writer.Close();
        }
        else
        {
            Debug.Log("파일 생성");
            FileStream fileStream = File.Create(NoteDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(EditNotes.Count);

            foreach (NotePosition np in EditNotes)
            {
                fileWriter.WriteLine($"{np.xpos} , {np.HeightValue}");
            }
            fileWriter.Close();

        }

        //EditNotes.Add(songtimes.Count.ToString());


        //노트 리스트 정보에 대해서 파싱을 함

    }

    public void LoadNote()
    {
        //이미 생성되어 있으면 해당 데이터를 지우고 다시 깔아줘야 함


        //string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
        //로드 할 때는 메인 노트 관리해주는 오브젝트 위치를 받아서 그만큼 더해줘야함(0 부터 -값이기 때문에 빼줘도 더해지기만 함)

        StreamReader NoteParsing = new StreamReader(NoteDataPath);
        int NoteCount = Int32.Parse(NoteParsing.ReadLine());


        //여기에서 로드할 때 싹다 초기화시켜줘야함
        while (EditNotes.Count > 0)
        {
            Destroy(EditNotes[EditNotes.Count - 1].Note);
            EditNotes.RemoveAt(EditNotes.Count - 1);
            Debug.Log("노트 초기화 중입니다.");
        }




        for (int i = 0; i < NoteCount; i++)
        {
            float xpos; int heightnum; string LineText;

            LineText = NoteParsing.ReadLine();

            string[] split_Text = LineText.Split(',');
            xpos = float.Parse(split_Text[0]);
            heightnum = Int32.Parse(split_Text[1]);

            if(GameManager.Instance.state != GameState.Play_Mode)
                EditManager.Instance.MakeNote(xpos + EditManager.Instance.GetNPXpos(), heightnum);
            else
            {
                PlayManager.Instance.PlayScene_NoteMaker( xpos,heightnum);
            }
        
        }


        NoteParsing.Close();
    }






}
