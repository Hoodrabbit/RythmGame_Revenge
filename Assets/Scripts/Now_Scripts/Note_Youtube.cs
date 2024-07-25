using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Youtube : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GameManager.Instance.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x <= -5 && GameManager.Instance.MainAudio.isPlaying == false)
        {
            Debug.Log("작동안함");
            //GameManager.Instance.PlayMusic();
        }

        transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);
    }
}
