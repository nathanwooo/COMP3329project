using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regionDetector : MonoBehaviour
{
    public GameObject attacker_object;
    public float speed = 1.0f;
    public string playerBoundingName = "PlayerBoundary";

    private GameObject attacker;
    private Vector3 destination;
    private HashSet<Collider2D> enemies;
    private Collider2D target_enemy;
    private Vector3 initialPosition;


    void Start()
    {
        // initialPosition = attacker.transform.position;

        // Randomly pick a point within the spawn object    
        initialPosition = transform.position;
        enemies = new HashSet<Collider2D>();
        spawnAttacker();
    }

    void spawnAttacker()
    {
        attacker = Instantiate(attacker_object, initialPosition, Quaternion.identity);
    }

    void addEnemy(Collider2D obj)
    {
        enemies.Add(obj);
    }

    void removeEnemy(Collider2D obj)
    {
        enemies.Remove(obj);
    }

    void nextTarget()
    {
        foreach (Collider2D e in enemies)
        {
            Debug.Log(e);
            target_enemy = e;
            return;
        }
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log("Enter " + name);
        if (name == playerBoundingName)
        {
            addEnemy(obj);
            target_enemy = obj;
        }

    }

    void Update()
    {
        if (attacker != null)
        {
            if (target_enemy != null)
            {
                destination = target_enemy.transform.position;
                float distance = Vector3.Distance(attacker.transform.position, destination);
                if (distance > 0)
                {
                    attacker.transform.position = Vector3.Lerp(attacker.transform.position, destination, Time.deltaTime * speed / distance);
                }
            } else
            {
                float distance = Vector3.Distance(attacker.transform.position, initialPosition);
                if (distance > 0)
                {
                    attacker.transform.position = Vector3.Lerp(attacker.transform.position, initialPosition, Time.deltaTime * speed / distance);
                }
            }
            
        } else
        {
            spawnAttacker();
        }
        
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        Debug.Log("Ohhhhh " + name);
        // if (obj.tag == "Player")
        // {
        if (name == playerBoundingName)
        {
            removeEnemy(obj);
            if (target_enemy == obj)
            {
                target_enemy = null;
                nextTarget();

            }
        }
        
    }
}
