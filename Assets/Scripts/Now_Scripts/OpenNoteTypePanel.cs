using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpenNoteTypePanel : MonoBehaviour
{
    public GameObject NoteTypePanel;
    Button button;

    bool TurnOn = false;

    public List<GameObject> testObjs = new List<GameObject>();
    List<GameObject> testObjsReal = new List<GameObject>();
    int data = 0;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenPanel);
    }

    // Update is called once per frame
    void Update()
    {

        if(TurnOn == true)
        {
            float ScrollValue = Input.GetAxis("Mouse ScrollWheel");

            if (ScrollValue != 0)
            {
                //Debug.Log((int)ScrollValue + " , " + ScrollValue);
                if (ScrollValue > 0.1)
                {
                    ScrollValue = 1;
                }
                else if (ScrollValue < 0)
                {
                    ScrollValue = -1;
                }
                Debug.Log(data);
                data += (int)ScrollValue;

                if (data > 3)
                {
                    data = 0;
                }
                else if (data < 0)
                {
                    data = 3;
                }

                InstantiatePrefab(data);

            }
        }

       
        




    }



    public void OpenPanel()
    {
        if (NoteTypePanel.activeSelf == false)
        {
            NoteTypePanel.SetActive(true);
        }
        else
        {
            NoteTypePanel.SetActive(false);
        }
    }


    public void InstantiatePrefab(int num)
    {
        if (testObjsReal.Count == 4)
        {
            int i = 0;
            foreach (var obj in testObjsReal)
            {

                if (i != num)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
                }
                i++;
            }
        }
        else
        {
            foreach (GameObject obj in testObjs)
            {
                GameObject pf = Instantiate(obj);
                testObjsReal.Add(pf);
            }
            int i = 0;
            foreach (var obj in testObjsReal)
            {
                if (i != num)
                {
                    obj.SetActive(false);
                }
                i++;
            }
        }

        if (TurnOn == false)
        {
            TurnOn = true;
        }

    }


    //public void first(int num = 0)
    //{
    //    if (testObjsReal.Count == 4)
    //    {
            
    //        foreach (var obj in testObjsReal)
    //        {
                
    //            if (num != 0)
    //            {
    //                obj.SetActive(false);
    //            }
    //            else
    //            {
    //                obj.SetActive(true);
    //            }
    //            num++;
    //        }
    //    }
    //    else
    //    {
    //        foreach (GameObject obj in testObjs)
    //        {
    //            testObjsReal.Add(Instantiate(obj));
    //        }
    //        int i = 0;
    //        foreach (var obj in testObjsReal)
    //        {
    //            if (i != 0)
    //            {
    //                obj.SetActive(false);
    //            }
    //            i++;
    //        }
    //    }

    //}
    //public void second(int num = 1)
    //{
    //    if (testObjsReal.Count == 4)
    //    {
            
    //        foreach (var obj in testObjsReal)
    //        {

    //            if (num != 1)
    //            {
    //                obj.SetActive(false);
    //            }
    //            else
    //            {
    //                obj.SetActive(true);
    //            }
    //            num++;
    //        }
    //    }
    //    else
    //    {
    //        foreach (GameObject obj in testObjs)
    //        {
    //            testObjsReal.Add(Instantiate(obj));
    //        }
    //        foreach (var obj in testObjsReal)
    //        {
    //            if (num != 1)
    //            {
    //                obj.SetActive(false);
    //            }
    //            num++;
    //        }
    //    }
    //}
    //public void third(int num = 2)
    //{

    //}
    //public void fourth()
    //{

    //}


}
