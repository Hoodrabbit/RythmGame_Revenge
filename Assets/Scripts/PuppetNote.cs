using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetNote : Note
{
    //가짜 노트
    //public GameObject fakeNote;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //if (GameManager.Instance.state == GameState.Play_Mode)
        //{
        //    if(transform.position.y <0)
        //    {
        //        //Instantiate(fakeNote, new Vector3(transform.position.x, PlayManager.UP), Quaternion.identity, transform);
        //    }
        //    else
        //    {
        //        Instantiate(fakeNote, new Vector3(transform.position.x, 0), Quaternion.identity, transform);
        //    }

            
        //}
        //else
        //{
        //    if (transform.position.y < 0)
        //    {
        //        Instantiate(fakeNote, new Vector3(transform.position.x, EditManager.UP), Quaternion.identity, transform);
        //    }
        //    else
        //    {
        //        Instantiate(fakeNote, new Vector3(transform.position.x, EditManager.DOWN), Quaternion.identity, transform);
        //    }
        //}


    }
}
