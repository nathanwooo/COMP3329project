using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regionDetector : MonoBehaviour
{
    public Rigidbody2D attacker;
    public float speed = 1.0f;
    private Vector3 destination;
    private Collider2D enemy;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = attacker.transform.position;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log("Enter " + name);
        enemy = obj;
        
        //Move the object in question
        /* Vector2 v = attacker.velocity;
        v.y -= 2;
        attacker.velocity = v; */
        Debug.Log(attacker.velocity);
    }

    void Update()
    {
        if (enemy != null)
        {
            destination = enemy.transform.position;
            Debug.Log(destination);
            Debug.Log(attacker.transform.position);
            float distance = Vector3.Distance(attacker.transform.position, destination);
            if (distance > 0)
            {
                attacker.transform.position = Vector3.Lerp(attacker.transform.position, destination, Time.deltaTime * speed / distance);
            }
        } else
        {
            Debug.Log(initialPosition);
            float distance = Vector3.Distance(attacker.transform.position, initialPosition);
            if (distance > 0)
            {
                attacker.transform.position = Vector3.Lerp(attacker.transform.position, initialPosition, Time.deltaTime * speed / distance);
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log("Ohhhhh " + name);
        enemy = null;
    }
}
