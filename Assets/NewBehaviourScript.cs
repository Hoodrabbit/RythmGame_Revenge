using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RectTransform uiImage; // UI �̹����� RectTransform
    public Transform targetObject; // Ư�� ��ġ�� �ִ� ������Ʈ

    void Update()
    {
        // ������Ʈ�� ���� ��ǥ ���
        Vector3 worldPosition = targetObject.position;

        // ������Ʈ�� ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // UI �̹����� ũ�⸦ ����
        // ���⼭�� UI �̹����� ũ�⸦ ��ũ�� ��ǥ�� ũ��� ���ߴ� �����Դϴ�.
        uiImage.position = screenPosition;

        // UI �̹��� ũ�� ���� (��: Ư�� �����Ϸ� ����)
        Vector2 newSize = new Vector2(screenPosition.x, 50);
        uiImage.sizeDelta = newSize;
    }
}
