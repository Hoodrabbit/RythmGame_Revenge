using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand_Line_Maker : NoteMakerBase
{
    public GameObject Expand_Line;


    protected override void AreaCheck(Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);

        int i = 0;

        while (i < hit.Length && !DeleteMode)
        {

            if (hit[i].collider.CompareTag("NotePlace"))
            {

                GameObject AddNote = Instantiate(Expand_Line, new Vector3(hit[i].transform.position.x, 0), Quaternion.identity, barNote.RhythmNote.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                //위와 동일 

                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 0, 0, 0));

            }
            i++;
        }

        if (DeleteMode)
        {
            i = 0;
            while (i < hit.Length && DeleteMode)
            {
                //Debug.Log("작동" + hit[i].collider.name);


                if (hit[i].collider.CompareTag("ExpandLine"))
                {

                    Destroy(hit[i].collider.gameObject);
                    


                }
                i++;
            }
        }



    }






}
