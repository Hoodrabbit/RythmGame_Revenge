using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizingCamera : MonoBehaviour
{
    Camera cam;
    Animator animator;


    private void Awake()
    {
        cam = GetComponent<Camera>();
        animator = GetComponent<Animator>();


        Rect viewportRect = cam.rect;

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

        cam.rect = viewportRect;

    }


    public void Camera_Zoom()
    {
        //카메라 애니메이터 실행
        
        animator.Play("CameraZoomIn");

    }

    public void Camera_ZoomOut()
    {
        animator.Play("CameraZoomOut");
    }






}
