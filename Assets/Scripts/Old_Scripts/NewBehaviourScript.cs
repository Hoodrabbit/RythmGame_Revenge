using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float fixedWidth = 10f; // ������ �ʺ�

    void Start()
    {
        AdjustCamera();
    }

    void Update()
    {
        AdjustCamera(); // ������ ũ�� ���� �� �������� ������ �ʿ��ϸ� �� ���� ����
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
