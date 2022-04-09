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
        v.x = Input.GetAxis("Horizontal") * 30;
        v.y = Input.GetAxis("Vertical") * 30;
        rb.velocity = v;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log(name);
        if (name == "Attacker(Clone)")
        {
            Debug.Log("Collided");
            Destroy(obj.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        string name = obj.gameObject.name;
        if (name == "Attacker(Clone)")
        {
            Debug.Log("Collided");
            Destroy(obj.gameObject);
        }
    }
}