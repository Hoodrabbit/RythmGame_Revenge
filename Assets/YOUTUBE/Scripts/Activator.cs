using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Runtime.Serialization;

public class Activator : MonoBehaviour
{
    //GameManager gameState;
    public KeyCode key;
    bool active = false;
    GameObject note;
    public static float PlayTime;
    public List<float> songtimes = new List<float>();
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("PlayTime : " + PlayTime);
    }

    // Update is called once per frame
    void Update()
    {

        switch (GameManager.Instance.state)
        {
            case GameState.None:
                break;
            case GameState.Play_Mode:
                PlayTime += Time.deltaTime;
                if (Input.GetKeyDown(key))
                {
                    if(note != null)
                    {
                        Debug.Log("내가 눌러서 작동");
                        note.SetActive(false);
                        audioSource.Play();
                        songtimes.Add(PlayTime);
                        
                    }
                    
                }
                break;
            case GameState.Debug_Mode:
                PlayTime += Time.deltaTime;
                if (Input.GetKeyDown(key))
                {
                    //Destroy(note);
                    songtimes.Add(PlayTime);
                    audioSource.Play();
                }
                break;
            default:
                break;
        }


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

        string directoryPath = Application.dataPath + "\\NOTEDATA_Folder";
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

            foreach(string s in lines)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note"))
        {
            active = true;
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}