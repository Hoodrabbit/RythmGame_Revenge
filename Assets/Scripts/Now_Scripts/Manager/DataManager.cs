using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;





//0�� Ÿ�� : ���� Ÿ���ε� ������ ������ �ִٺ��� � ������ �ϸ� ���� �� 
//�ƴϸ� ������ ������ 100���� Ÿ������ �ϴ� �͵� ����ϵ���

//��������





public class NoteInfoPos
{
    public float xpos; // ��Ʈ�� X ��(������ �� �����̱� ������ ���߿� ���� ���� �������� �ش� �����Ϳ� �߰��� �������̳� �ӵ��� ���� ��ġ�� ������ �� ����
    public int HeightValue; //��Ʈ ���� ��ȣ�� ����޾Ƽ� ���߿� �ҷ��� ���� �� ��ȣ�� ���� Y���� Ư�� ������ �Ҵ��Ŵ
    //�ű��߰�
    public int NoteType; //��Ʈ�� ������ ���� ������ ������� ����   (0 : ȭ�� ��ȯ ��Ʈ(ĥ �� ���� �ƴϸ� ĥ �� �ְ� �������?)1 : �⺻ , 2 : �� ��Ʈ, 3 : ���ɳ�Ʈ Ư�� ��Ʈ�� �� ���� ���⿡�� �� �߰� �� �� ����)
    public int LongNoteStartEndCheck; //�ճ�Ʈ�� ���̸� üũ���� ����(��Ȯ���� ���۰� ��) (�ճ�Ʈ ������ �ƴ� ��� ���� 0�̰� �ճ�Ʈ�� �� 1�� ���� 2�� ���� üũ����)

    public double SongTime; //���� ��Ʈ�� �ش��ϴ� �뷡�� �ð�

    public int EnemyType = 0; // ���� ���� ����(Ư�� ���̽�)�� ���ؼ� ������ ���� ���·δ� ������ �Ұ����� ���õǱ� ������ ���⸦ �����Ͽ� ��Ʈ�� �ľ� ��



    public NoteInfoPos(float x, int h, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        xpos = x;
        HeightValue = h;
        NoteType = noteType;
        this.LongNoteStartEndCheck = LongNoteStartEndCheck;
        SongTime = songtime;
        EnemyType = enemyType;
    
    }



}



/// <summary>
/// 1 : Note ���� 2 : ��ġ  3 : ����   4 : �ճ�Ʈ ���� ����(���� 1, �� 2)   5 : �뷡 �ð�   6 : ���� ���� ����(0�̸� �⺻ 1, 2���� ����)
/// </summary>
public class NoteInfoAll //���� ��Ʈ�� ���� ������ ��� �� Ŭ����
{
    public GameObject Note;
    public NoteInfoPos notePos;
    

    public NoteInfoAll(GameObject Note, float x, int h, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        this.Note = Note;
        notePos = new NoteInfoPos(x, h, noteType, LongNoteStartEndCheck, songtime, enemyType);
    }



}




public class DataManager : Singleton<DataManager>
{
    //���� ������ ���� ��ũ��Ʈ ���� �Ŵ����� ��ũ���ͺ��� ���� �ּҸ� �̾ƿ�
#if UNITY_EDITOR
    static string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#else
        static string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#endif
    string NoteDataPath;

    public List<NoteInfoAll> EditNotes = new List<NoteInfoAll>();



    protected override void Awake()
    {
        base.Awake();
        if (!Directory.Exists(NoteDataFolder))
        {
            Directory.CreateDirectory(NoteDataFolder);
        }
        NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.Music_Name + "_NoteData.txt");
    }


    public void ListNullCheck() //�ش� ����Ʈ �߿��� null�� �����ϴ��� Ȯ���ϰ� ���� �����Ѵٸ� �ش� �����ʹ� ������
    {
        int i = EditNotes.Count - 1;
        while (i >= 0)
        {
            if (EditNotes[i].Note == null)
            {
                Debug.Log(i + "�̰� ������");
                EditNotes.RemoveAt(i);
            }
            i--;
        }
    }


    public void SaveNote()
    {
        EditNotes = EditNotes.OrderBy(N => N.notePos.xpos).ToList();

        if (File.Exists(NoteDataPath))
        {
            Debug.Log("���� ����");
            StreamWriter writer = File.CreateText(NoteDataPath);


            ListNullCheck();
            writer.WriteLine(EditNotes.Count);

            foreach (NoteInfoAll np in EditNotes)
            {

                writer.WriteLine($"{np.notePos.xpos / GameManager.Instance.speed} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}, {np.notePos.SongTime}, {np.notePos.EnemyType}");
            }
            writer.Close();
        }
        else
        {
            Debug.Log("���� ����");
            FileStream fileStream = File.Create(NoteDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(EditNotes.Count);

            foreach (NoteInfoAll np in EditNotes)
            {
                fileWriter.WriteLine($"{np.notePos.xpos / GameManager.Instance.speed} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}, {np.notePos.SongTime}, {np.notePos.EnemyType}");
            }
            fileWriter.Close();

        }

        //EditNotes.Add(songtimes.Count.ToString());


        //��Ʈ ����Ʈ ������ ���ؼ� �Ľ��� ��

    }

    public void LoadNote()
    {
        //�̹� �����Ǿ� ������ �ش� �����͸� ����� �ٽ� ������ ��


        //string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
        //�ε� �� ���� ���� ��Ʈ �������ִ� ������Ʈ ��ġ�� �޾Ƽ� �׸�ŭ ���������(0 ���� -���̱� ������ ���൵ �������⸸ ��)

        StreamReader NoteParsing = new StreamReader(NoteDataPath);
        int NoteCount = Int32.Parse(NoteParsing.ReadLine());


        //���⿡�� �ε��� �� �ϴ� �ʱ�ȭ���������
        while (EditNotes.Count > 0)
        {
            Destroy(EditNotes[EditNotes.Count - 1].Note);
            EditNotes.RemoveAt(EditNotes.Count - 1);
            Debug.Log("��Ʈ �ʱ�ȭ ���Դϴ�.");
        }




        for (int i = 0; i < NoteCount; i++)
        {
            float xpos; int height; int NoteType; int LongNoteStartEndCheck; double SongTime; int enemyType;
            
            string LineText; //�Ľ��� ��Ʈ ������ ������� ���ڿ�
            
            LineText = NoteParsing.ReadLine();

            string[] split_Text = LineText.Split(',');
            xpos = float.Parse(split_Text[0]);
            height = Int32.Parse(split_Text[1]);
            NoteType = Int32.Parse(split_Text[2]);
            LongNoteStartEndCheck = Int32.Parse(split_Text[3]);

            if (split_Text[4] != null)
            {
                SongTime = double.Parse(split_Text[4]);
            }
            else
            SongTime = 0;

            if (split_Text[5] != null) 
            {
                enemyType = Int32.Parse(split_Text[5]);
            }
            else
            enemyType = 0;


            if (GameManager.Instance.state != GameState.Play_Mode)
                EditManager.Instance.MakeNote(xpos * GameManager.Instance.speed + EditManager.Instance.GetNPXpos() , height, NoteType, LongNoteStartEndCheck, SongTime, enemyType);
            else
            {
                PlayManager.Instance.PlayScene_NoteMaker( xpos * GameManager.Instance.speed, height, NoteType, LongNoteStartEndCheck, SongTime, enemyType);
                
                if(LongNoteStartEndCheck != 2 || NoteType < 100)
                {
                    PlayManager.Instance.AddNoteCount();
                }
                PlayManager.Instance.SetComboCount();
            }
        
        }

       
        NoteParsing.Close();
    }

    void SaveGameResult()
    {
        //Debug.Log(PlayManager.Instance);
    }







}
