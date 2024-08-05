using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand_Line : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= 0)
        {
            EditManager.Instance.ChangeScreenSize();
            gameObject.SetActive(false);
        }
    }
}
