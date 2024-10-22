using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{

    public GameObject MainMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButtonClick(GameObject obj)
    {
        GameManager.Instance.PauseAudio();
        StartCoroutine(DelayedDeactivate(obj));
    }

    private IEnumerator DelayedDeactivate(GameObject obj)
    {
        yield return null; // 다음 프레임까지 대기
        obj.SetActive(false);
    }


}
