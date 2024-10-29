using UnityEngine;
using TMPro;
using System.Collections;




public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    private void Start()
    {
        // StartCoroutine(VisualizingFPS());
    }

    //IEnumerator VisualizingFPS()
    //{
    //    while(true)
    //    {



    //        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    //        float fps = 1.0f / deltaTime;
            
    //        yield return new WaitForSeconds(0.5f);
    //        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    //    }

        
    //}


    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}