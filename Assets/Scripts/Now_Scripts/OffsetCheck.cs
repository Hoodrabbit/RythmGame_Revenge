using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;



//4번째 악센트에 키를 누르기 



public class OffsetCheck : MonoBehaviour
{
    public AudioSource MainSound;

    public float MusicBPM;
    public float StdBPM;


    public List<double> Offsetimes = new List<double>();


    double StartdspTIme;
    double CurdspTime;

    double bitPerSec;
    double oneBeatTime;
    double beatPerSample;
    double NextBeatTime;

    double aa;

    private void Start()
    {
        //Timecheck = 0;
        oneBeatTime = StdBPM / MusicBPM;
        bitPerSec = MusicBPM / StdBPM;
        NextBeatTime = oneBeatTime * MainSound.clip.frequency;

        Debug.Log(StdBPM / MusicBPM);
        //mysr = GetComponent<Transform>();

        StartdspTIme = 3f;
        //Debug.Log(StartdspTIme);
    }

    private void Update()
    {
        //if(AudioSettings.dspTime - StartdspTIme > 0.5)
        //{
        //    //이거 메트로놈 다시 시작될때 기준으로 설정해줘야함
        //    StartdspTIme = AudioSettings.dspTime;
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Offsetimes.Add(aa);

            ////0.5
            //CurdspTime = AudioSettings.dspTime;
            //if (StartdspTIme != 0)
            //{
            //    aa = CurdspTime - StartdspTIme;
            //    Debug.Log(aa);
            //    Offsetimes.Add(aa);
            //}

            //StartdspTIme = AudioSettings.dspTime;



        }

    }


    private async void OnApplicationQuit()
    {
        int count = 0;
        double sum =0;

        for (int i = 0; i < Offsetimes.Count; i++)
        {
            if (Offsetimes.Count - 1 == i)
            {
                sum /= i;
                await SaveNoteTimesToFile(sum);
            }

            sum += Offsetimes[i];
        }

        //while (true)
        //{
        //    if (count > Offsetimes.Count)
        //    {
        //        sum /= count--;
        //        await SaveNoteTimesToFile(sum);
        //        break;
        //    }

        //    sum += Offsetimes[count];


            
        //}

    }


    private async Task SaveNoteTimesToFile(double sum)
    {

#if UNITY_EDITOR
        string directoryPath = Application.dataPath + "\\NOTEDATA_Folder";
#else
        string directoryPath = Application. streamingAssetsPath + "\\NOTEDATA_Folder";
#endif
        string filePath = Path.Combine(directoryPath, "OffSetData.txt");

        if (!Directory.Exists(directoryPath))
        {
            Debug.Log("디렉토리가 없어요");
            Directory.CreateDirectory(directoryPath);
        }


        if (File.Exists(filePath))
        {

            StreamWriter writer = File.CreateText(filePath);

            writer.Write(0.5 - sum);
            writer.Close();
        }
        else
        {
            FileStream fileStream = File.Create(filePath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(0.5 - sum);
            
            fileWriter.Close();

        }
    }



    //IEnumerator MetroNome()
    //{
    //    //MainSound.PlayOneShot(MainSound.clip);
    //    beatPerSample = oneBeatTime * MainSound.clip.frequency;
    //    NextBeatTime += beatPerSample;

    //    yield return null;
    //}






}
