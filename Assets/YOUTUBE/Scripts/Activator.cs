using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

int hello;
public class Activator : MonoBehaviour
{
    public enum Mode
    {
        None,
        PlayMode,
        DebugMode
    }
    
    
    public Mode Game_Mode;
    public KeyCode key;
    bool active = false;
    GameObject note;
    public static float PlayTime;
    public List<float> songtimes = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (Game_Mode)
        {
            case Mode.None:
                break;
            case Mode.PlayMode:
                if (Input.GetKeyDown(key))
                {
                    
                }
                break;
            case Mode.DebugMode:
                PlayTime += Time.deltaTime;
                if (Input.GetKeyDown(key))
                {
                    //Destroy(note);
                    songtimes.Add(PlayTime);
                }
                break;
            default:
                break;
        }


    }


   

    private async void OnApplicationQuit()
    {
        if(Game_Mode == Mode.DebugMode) 
        {
            Debug.Log("test");
            await SaveNoteTimesToFile();
        }
        
    }

    private async Task SaveNoteTimesToFile()
    {
        string directoryPath = "SaveNoteData";
        string filePath = Path.Combine(directoryPath, "NoteData.txt");

        if(!Directory.Exists(directoryPath)) 
        {
            Directory.CreateDirectory (directoryPath);
        }

        List<string> lines = new List<string>();

        //lines.Add(songtimes.Count.ToString());

        foreach(var line in songtimes)
        {
            lines.Add(line.ToString()) ;
        }

        await Task.Run(() =>
        {
            File.WriteAllLines(filePath, lines);            
            //파일 닫기 기능 넣기
        });

        //if(File.Exists(filePath))
        //{
        //    FileStream fileStream = File.OpenWrite(filePath);
        //}
        //else
        //{
        //    FileStream fileStream = File.Create(filePath);
        //    fileStream.Write()

        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Note"))
        {
            active = true;
            note = collision.gameObject;
            //Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        
    }

}
