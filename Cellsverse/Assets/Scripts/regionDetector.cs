using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class regionDetector : MonoBehaviour
{
    public GameObject attacker_object;
    public float speed = 1.0f;
    public string playerBoundingName = "PlayerBoundary";
    public GameObject[] powerUp;
    public float startTime;
    public float interval;
    public Transform powerUpTransform;
    public float attackerInterval = 1;

    private float attackerCounter;
    private float remainingTime;
    private GameObject attacker;
    private Vector3 destination;
    private HashSet<Collider2D> enemies;
    private Collider2D target_enemy;
    private Vector3 initialPosition;
    private Vector3 powerUpPos;
    private GameObject booster;

    void Start()
    {
        // initialPosition = attacker.transform.position;

        // Randomly pick a point within the spawn object
        remainingTime = startTime;
        initialPosition = transform.position;
        enemies = new HashSet<Collider2D>();
        spawnAttacker();
        powerUpPos = powerUpTransform.position;
    }

    void spawnAttacker()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            attacker = PhotonNetwork.Instantiate(attacker_object.name, initialPosition, Quaternion.identity);
        }

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
        
        var nextIndex = Random.Range(0, enemies.Count);
        Debug.Log(enemies.Count);
        Debug.Log(nextIndex);
        var i = 0;
        foreach (Collider2D e in enemies)
        {
            if (i++ == nextIndex)
            {
                target_enemy = e;
                return;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string name = obj.gameObject.name;
        // if collided with bullet
        // Debug.Log("Enter " + name);
        if (name == playerBoundingName)
        {
            addEnemy(obj);
            target_enemy = obj;
        }

    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            var nextIndex = Random.Range(0, powerUp.Length);

            if (PhotonNetwork.IsMasterClient)
            {
                if (booster != null)
                {
                    PhotonNetwork.Destroy(booster);
                }
                booster = PhotonNetwork.Instantiate(powerUp[nextIndex].name, powerUpPos, Quaternion.identity);
            }
            remainingTime = interval;
        }
        if (attacker != null)
        {
            var sourcePosition = attacker.transform.position;
            if (target_enemy != null)
            {
                destination = target_enemy.transform.position;
                sourcePosition.z = destination.z;
                var distance = Vector3.Distance(sourcePosition, destination);
                if (distance > 0)
                {
                    attacker.transform.position = Vector3.Lerp(sourcePosition, destination, (Mathf.Min(0.04f + Time.deltaTime * speed, 1)) / distance);
                }
            }
            else
            {
                sourcePosition.z = initialPosition.z;
                var distance = Vector3.Distance(sourcePosition, initialPosition);
                if (distance > 0)
                {
                    attacker.transform.position = Vector3.Lerp(sourcePosition, initialPosition, (Mathf.Min(0.06f + Time.deltaTime * speed, 1)) / distance);
                }
            }
        }
        else
        {
            attackerCounter -= Time.deltaTime;
            if (attackerCounter <= 0)
            {
                spawnAttacker();
                attackerCounter = attackerInterval;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        // if collided with bullet
        // Debug.Log("Ohhhhh " + name);
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
