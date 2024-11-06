using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeNote : MonoBehaviour
{

    [SerializeField] SpriteRenderer fakeNoteSpriteRenderer;


    public Sprite Up;
    public Sprite Down;
    


    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.y > 3)
        {
            fakeNoteSpriteRenderer.sprite = Up;
        }
        else
        {
            fakeNoteSpriteRenderer.sprite = Down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
