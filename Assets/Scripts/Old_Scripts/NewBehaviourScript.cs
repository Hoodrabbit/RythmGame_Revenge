using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float fixedWidth = 10f; // 고정할 너비

    void Start()
    {
        AdjustCamera();
    }

    void Update()
    {
        AdjustCamera(); // 윈도우 크기 변경 시 동적으로 조정이 필요하면 이 라인 유지
    }

    void AdjustCamera()
    {
        Camera camera = GetComponent<Camera>();
        if (camera.orthographic)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float orthographicSize = fixedWidth / screenAspect / 2f;
            camera.orthographicSize = orthographicSize;
        }
        else
        {
            Debug.LogWarning("Camera is not Orthographic");
        }
    }
}
