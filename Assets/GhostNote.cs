using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNote : MonoBehaviour
{

    //특정 구간 지나면 점점 안보여요
    SpriteRenderer GhostNoteSprite;

    bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        GhostNoteSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("asdf"))
        {
            StartCoroutine(invisibleNote());
        }
    }



    IEnumerator invisibleNote()
    {
        float CurrentTime = 0f;
        while (GhostNoteSprite.color.a > 0)
        {
            CurrentTime += Time.deltaTime;

            float CurrentValue = Mathf.Lerp(GhostNoteSprite.color.a, 0, CurrentTime / 1f);
            
            GhostNoteSprite.color = new Color(GhostNoteSprite.color.r, GhostNoteSprite.color.g, GhostNoteSprite.color.b,CurrentValue);


            yield return null;
        }
    }


}
