using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Linq;
using System;

public class NotePosition //���� ��Ʈ�� ���� ������ ��� �� Ŭ����
{
    public GameObject Note;
    public float xpos; // ��Ʈ�� X ��(������ �� �����̱� ������ ���߿� ���� ���� �������� �ش� �����Ϳ� �߰��� �������̳� �ӵ��� ���� ��ġ�� ������ �� ����
    public int HeightValue; //��Ʈ ���� ��ȣ�� ����޾Ƽ� ���߿� �ҷ��� ���� �� ��ȣ�� ���� Y���� Ư�� ������ �Ҵ��Ŵ

    public NotePosition(GameObject Note, float x, int h)
    {
        this.Note = Note;
        xpos = x;
        HeightValue = h;
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
        EditNotes = EditNotes.OrderBy(N => N.xpos).ToList();

        if (File.Exists(NoteDataPath))
        {
            Debug.Log("���� ����");
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
            Debug.Log("���� ����");
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
