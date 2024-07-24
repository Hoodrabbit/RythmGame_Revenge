using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RectTransform uiImage; // UI 이미지의 RectTransform
    public Transform targetObject; // 특정 위치에 있는 오브젝트

    void Update()
    {
        // 오브젝트의 월드 좌표 계산
        Vector3 worldPosition = targetObject.position;

        // 오브젝트의 월드 좌표를 스크린 좌표로 변환
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // UI 이미지의 크기를 조정
        // 여기서는 UI 이미지의 크기를 스크린 좌표의 크기로 맞추는 예제입니다.
        uiImage.position = screenPosition;

        // UI 이미지 크기 조정 (예: 특정 스케일로 조정)
        Vector2 newSize = new Vector2(screenPosition.x, 50);
        uiImage.sizeDelta = newSize;
    }
}
