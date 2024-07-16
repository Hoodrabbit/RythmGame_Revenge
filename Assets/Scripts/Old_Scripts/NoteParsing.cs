using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//노트의 x좌표, y좌표, 노트의 시간, 노트의 종류, 롱노트시간 순으로 기술하게 된다.

public class NoteParsing : MonoBehaviour
{
#if UNITY_EDITOR
    StreamReader Notenote = new StreamReader(Application.dataPath + "/NoteInfo_test.txt");
#else
        StreamReader Notenote = new StreamReader(Application.streamingAssetsPath + "/NoteInfo_test.txt");
#endif

    string sheetText;
    string[] TextSplit;
    public int num = 0;
    public int[] noteLaneNum;
    public int[] notePos;
    public int[] noteTime;
    public int[] noteMode;
    public int[] longNoteTime;

    private void Awake()
    {
        NoteParse();
        //int.TryParse("a", out test);
        //Debug.Log(test);
    }



    public void NoteParse()
    {
        while(!Notenote.EndOfStream)
        {
            sheetText = Notenote.ReadLine();

            if (sheetText.Equals("[NoteInfo]"))
            {
                while (!Notenote.EndOfStream)
                {
                    sheetText = Notenote.ReadLine();
                    TextSplit = sheetText.Split(',');

                    int laneNum;
                    int.TryParse(TextSplit[0], out laneNum);
                    noteLaneNum[num] = laneNum;

                    int timenum;
                    int.TryParse(TextSplit[2], out timenum);
                    noteTime[num] = timenum;

                    int mode;
                    int.TryParse(TextSplit[3], out mode);
                    noteMode[num] = mode;

                    num++;
                    //Debug.Log(num);
                    
                }
                    



            }
            
        }
    }
}
