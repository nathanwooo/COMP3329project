using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 v;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        v= rb.velocity;
        v.x = Input.GetAxis("Horizontal") * 10;
        v.y = Input.GetAxis("Vertical") * 10;
        rb.velocity = v;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log(name);
    }
}