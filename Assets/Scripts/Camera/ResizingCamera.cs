using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizingCamera : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();

        Rect viewportRect = camera.rect;

        float NowScreenAspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = 16f / 9f;

        if(NowScreenAspectRatio > targetAspectRatio)
        {
            viewportRect.width = targetAspectRatio / NowScreenAspectRatio;
            viewportRect.x = (1f - viewportRect.width) / 2f;
        }
        else
        {
            viewportRect.height = NowScreenAspectRatio / targetAspectRatio;
            viewportRect.y = (1f - viewportRect.height) / 2f;
        }

        camera.rect = viewportRect;

    }
}
