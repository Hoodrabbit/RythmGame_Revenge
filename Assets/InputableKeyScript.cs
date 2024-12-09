using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputableKeyScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> A_Type_Key = new List<GameObject>();
    [SerializeField] private List<GameObject> B_Type_Key = new List<GameObject>();


    //private void Awake()
    //{
    //    foreach(GameObject obj in A_Type_Key)
    //    {
    //        obj.SetActive(false);
    //    }
    //    foreach (GameObject obj in B_Type_Key)
    //    {
    //        obj.SetActive(false);
    //    }
    //}


    public void Select_A_Type()
    {
        foreach(var Key in A_Type_Key)
        {
            Key.gameObject.SetActive(true);
        }
    }

    public void Select_B_Type()
    {
        foreach (var Key in B_Type_Key)
        {
            Key.gameObject.SetActive(true);
        }
    }

}
