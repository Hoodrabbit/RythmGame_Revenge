using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteColliderAdjust : MonoBehaviour
{
    BoxCollider2D bodyCollider;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(spriteRenderer.size.x / 2, transform.position.y);


        bodyCollider.offset = new Vector2(spriteRenderer.size.x/2, 0);
        bodyCollider.size = new Vector2(spriteRenderer.size.x,transform.localScale.y);
    }
}
