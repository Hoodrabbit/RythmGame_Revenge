using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;

public class metronomemake : MonoBehaviour
{
    public float MusicBPM;
    public float stdBPM;

    public AudioSource Song;
    public float Timecheck;

    public GameObject OffsetNote;
    public List<GameObject> OffsetNotes;

    public List<double> songtimes = new List<double>();

    double StartdspTIme;
    double CurdspTime;

    double bitPerSec;
    double oneBeatTime;
    double beatPerSample;
    double NextBeatTime;

    // Start is called before the first frame update
    void Start()
    {

        Timecheck = 0;
        oneBeatTime = stdBPM / MusicBPM;
        bitPerSec = MusicBPM / stdBPM;
        //NextBeatTime = oneBeatTime * Song.clip.frequency;
        //NextBeatTime = 1.526;
        Debug.Log(stdBPM / MusicBPM);

        StartdspTIme = AudioSettings.dspTime + 3f;
        int i = 0;
        while(i<4)
        {
            int j=0;
            if (i >1)
            {
                j = 1;
            }
            

            OffsetNotes.Add(Instantiate(OffsetNote,new Vector2(-4 - i * 0.8f, 0 ), quaternion.identity, transform));
            Debug.Log((float)oneBeatTime);
            i++;
        }

       

    }

    private void Update()
    {
        while (GameManager.Instance.ClipLength() >= NextBeatTime)
        {
            songtimes.Add(NextBeatTime);

            NextBeatTime += oneBeatTime;
        }


        //if (Song.timeSamples+3f >= NextBeatTime)
        //{
        //    CurdspTime = AudioSettings.dspTime;
        //    Debug.Log(CurdspTime - StartdspTIme);
        //    songtimes.Add(CurdspTime - StartdspTIme);
        //    StartCoroutine(PlayMetronome());
        //    //StartdspTIme = AudioSettings.dspTime;
        //}



    }

    private async void OnApplicationQuit()
    {
        //Debug.Log(Application.dataPath);
        if (GameManager.Instance.state == GameState.Debug_Mode)
        {
            Debug.Log("test");
            await SaveNoteTimesToFile();
        }

    }

    private async Task SaveNoteTimesToFile()
    {
#if UNITY_EDITOR
        string directoryPath = Application.dataPath + "\\NOTEDATA_Folder";
#else
        string directoryPath = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#endif
        string filePath = Path.Combine(directoryPath, "NoteData.txt");

        if (!Directory.Exists(directoryPath))
        {
            Debug.Log("디렉토리가 없어요");
            Directory.CreateDirectory(directoryPath);
        }

        List<string> lines = new List<string>();

        lines.Add(songtimes.Count.ToString());

        foreach (var line in songtimes)
        {
            //Debug.Log("실행됨");
            lines.Add(line.ToString());
        }


        if (File.Exists(filePath))
        {

            StreamWriter writer = File.CreateText(filePath);

            foreach (string s in lines)
            {

                writer.WriteLine(s);
            }
            writer.Close();
        }
        else
        {
            FileStream fileStream = File.Create(filePath);
            StreamWriter fileWriter = new StreamWriter(fileStream);
            foreach (string s in lines)
            {
                fileWriter.WriteLine(s);
            }
            fileWriter.Close();

        }
    }



    //// Update is called once per frame
    //void Update()
    //{
    //    Timecheck += Time.deltaTime;

    //    if (Timecheck >= 3 + stdBPM / MusicBPM) 
    //    {
    //        metro.PlayOneShot(metro.clip);
    //        transform.Rotate(Vector3.back * 45f);
    //        Timecheck -= stdBPM / MusicBPM;

    //    }
    //}




}
