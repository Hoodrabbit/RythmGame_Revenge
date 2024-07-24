using UnityEngine;
using UnityEngine.UI;

public class AudioWaveform : MonoBehaviour
{
    public AudioSource audioSource;
    //public RectTransform WaveFormRect;
    
    //public Image image;

    AudioClip audioClip;

    SpriteRenderer spriteRenderer;
    int samplesize;
    public int width = 1024;
    public int height = 64;

    float[] waveform = null;
    float[] samples= null;
    void Start()
    {

        
        audioClip = audioSource.clip;
        //image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //WaveFormRect = GetComponent<RectTransform>();

        

        
        //width = (int)EditManager.Instance.NoteParent.GetDistance() *6;
        //Debug.Log(width);

        //WaveFormRect.sizeDelta = new Vector2(width, height);

        transform.localScale = new Vector2(100, 1);
        transform.position = new Vector2(50, 0);
        //spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D texWav = GetWaveform();


        spriteRenderer.sprite = Sprite.Create(texWav, new Rect(0, 0, width, height), new Vector2(0f,0f));
        
        //float[] samples = new float[audioClip.samples * audioClip.channels];
        //audioClip.GetData(samples, 0);

        //// 필요한 포인트 수
        //int pointCount = 1000;
        //Vector3[] points = new Vector3[pointCount];

        //// 샘플 데이터를 사용하여 포인트 계산
        //for (int i = 0; i < pointCount; i++)
        //{
        //    float sampleValue = samples[i * audioClip.samples / pointCount];
        //    points[i] = new Vector3(i / (float)pointCount * 1000, sampleValue, 0);
        //}

        //// LineRenderer 설정
        //lineRenderer.positionCount = pointCount;
        //lineRenderer.SetPositions(points);
    }

    Texture2D GetWaveform()
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        waveform = new float[width];

        samplesize = audioClip.samples * audioClip.channels;

        samples = new float[samplesize];
        audioClip.GetData(samples, 0);

        int packsize = (samplesize / width);
        for(int w= 0; w<width; w++)
        {
            waveform[w] = Mathf.Abs(samples[w * packsize]);
        }

        for(int x= 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, Color.black);
            }
        }

        for(int x= 0; x < width; x++)
        {
            for (int y = 0; y < waveform[x] * height *0.75f; y++)
            {
                tex.SetPixel(x, height / 2 + y, Color.white);
                tex.SetPixel(x, height/2 - y, Color.white);
            }
        }

        tex.Apply();

        return tex;
    }


}