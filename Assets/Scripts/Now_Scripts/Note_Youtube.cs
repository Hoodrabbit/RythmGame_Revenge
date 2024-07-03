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
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - 10 * Time.deltaTime, 0);
    }
}
