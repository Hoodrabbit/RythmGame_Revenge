using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Linq;
using System;


public class NoteInfoPos
{
    public float xpos; // ��Ʈ�� X ��(������ �� �����̱� ������ ���߿� ���� ���� �������� �ش� �����Ϳ� �߰��� �������̳� �ӵ��� ���� ��ġ�� ������ �� ����
    public int HeightValue; //��Ʈ ���� ��ȣ�� ����޾Ƽ� ���߿� �ҷ��� ���� �� ��ȣ�� ���� Y���� Ư�� ������ �Ҵ��Ŵ
    //�ű��߰�
    public int NoteType; //��Ʈ�� ������ ���� ������ ������� ����   (1 : �⺻ , 2 : �� ��Ʈ, 3 : Ư�� ��Ʈ�� �� ���� ���⿡�� �� �߰� �� �� ����)
    public int LongNoteStartEndCheck; //�ճ�Ʈ�� ���̸� üũ���� ����(��Ȯ���� ���۰� ��) (�ճ�Ʈ ������ �ƴ� ��� ���� 0�̰� �ճ�Ʈ�� �� 1�� ���� 2�� ���� üũ����)


    public NoteInfoPos(float x, int h, int noteType, int LongNoteStartEndCheck)
    {
        xpos = x;
        HeightValue = h;
        NoteType = noteType;
        this.LongNoteStartEndCheck = LongNoteStartEndCheck;
    }



}


public class NoteInfoAll //���� ��Ʈ�� ���� ������ ��� �� Ŭ����
{
    public GameObject Note;
    public NoteInfoPos notePos;
    

    public NoteInfoAll(GameObject Note, float x, int h, int noteType, int LongNoteStartEndCheck)
    {
        this.Note = Note;
        notePos = new NoteInfoPos(x, h, noteType, LongNoteStartEndCheck);
    }



}




public class DataManager : Singleton<DataManager>
{
    //���� ������ ���� ��ũ��Ʈ ���� �Ŵ����� ��ũ���ͺ��� ���� �ּҸ� �̾ƿ�
#if UNITY_EDITOR
    static string NoteDataFolder = Application.dataPath + "\\NOTEDATA_Folder";
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
        NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
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

                writer.WriteLine($"{np.notePos.xpos} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}");
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
                fileWriter.WriteLine($"{np.notePos.xpos} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}");
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
            float xpos; int heightnum; int NoteType; int LongNoteStartEndCheck; 
            
            string LineText; //�Ľ��� ��Ʈ ������ ������� ���ڿ�
            
            LineText = NoteParsing.ReadLine();

            string[] split_Text = LineText.Split(',');
            xpos = float.Parse(split_Text[0]);
            heightnum = Int32.Parse(split_Text[1]);
            NoteType = Int32.Parse(split_Text[2]);
            LongNoteStartEndCheck = Int32.Parse(split_Text[3]);

            if (GameManager.Instance.state != GameState.Play_Mode)
                EditManager.Instance.MakeNote(xpos + EditManager.Instance.GetNPXpos(), heightnum, NoteType, LongNoteStartEndCheck);
            else
            {
                PlayManager.Instance.PlayScene_NoteMaker( xpos,heightnum, NoteType, LongNoteStartEndCheck);
            }
        
        }


        NoteParsing.Close();
    }






}
