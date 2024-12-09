using System.Collections;
using TMPro;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    ResizingCamera resizeCam;

    bool isShaking = false;
    bool isNanata = false;
    bool isSlay = false;


    public Vector3 NormalPos;
    public Vector3 targetPos;

    public int StartCamSize;
    public int TargetCamSize;



    float currentMagnitude = 0f;

    public void Start()
    {
        resizeCam = GetComponent<ResizingCamera>();
        NormalPos = transform.position;
    }


    public void Normal_Hit()
    {
        StartCoroutine(ShakeCamera_Normal(0.1f, 0.01f));
    }

    IEnumerator ShakeCamera_Normal(float duration, float magnitude)
    {
        if (isShaking) yield break; // 이미 쉐이킹 중이면 무시
        isShaking = true;

        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.position.x + x, transform.position.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }

    public void Nanta_Hit()
    {
        //Debug.Log("동작확인");

        if(!isSlay)
        {
            if (!isNanata)
            {
                Debug.Log("동작확인1");

                StartCoroutine(CameraTransition(targetPos, TargetCamSize, 0.1f, 0.2f, 0.05f));
                
            }
            else
            {
                Debug.Log("동작확인2");
                transform.position = targetPos;
                StartCoroutine(ShakeCamera(0.2f, 0.05f)); // 코루틴으로 실행
            }
        }

        

    }
    
    public void Nanta_Finish()
    {
        Debug.Log("slaying");

        StopAllCoroutines();
        isSlay = true;
        isNanata = false;

        //resizeCam.Camera_ZoomOut();
        transform.position = NormalPos;


        
        isSlay = false;
    }

    IEnumerator MoveCamera(Vector3 targetPosition, float duration)
    {
      


        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            //Debug.Log(elapsedTime / duration);


            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("아니 왜 안됨222");

        isNanata = true;
        transform.position = targetPosition;
    }
    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < duration)
        {
            //Debug.Log(elapsedTime+ " , " +duration);


            float xShake = Random.Range(-1f, 1f) * magnitude;
            float yShake = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(xShake, yShake, 0);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        Debug.Log("아니 왜 안됨111");
        transform.position = originalPosition;
    }

    IEnumerator ZoomCamera(float targetSize, float duration)
    {
        float startSize = Camera.main.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = targetSize;
    }


    IEnumerator CameraTransition(Vector3 targetPosition, float targetSize, float moveDuration, float shakeDuration, float shakeMagnitude)
    {
        
        // 1. 카메라 확대 시작
        StartCoroutine(ZoomCamera(targetSize, moveDuration));

        // 2. 카메라 이동 시작
        yield return StartCoroutine(MoveCamera(targetPosition, moveDuration));

        // 3. 카메라 쉐이킹 시작
        


       // yield return null;
        


    }









}
