using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPunCallbacks
{
    public GameObject nutrients;
    public int NUMBER = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnJoinedRoom()
    {
        for(int i = 0; i < NUMBER; i++)
        {
            SpawnNut();
        }
    }

    // Update is called once per frame
    void SpawnNut()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;
        float s2 = rd.bounds.size.y / 2;
        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;
        float y1 = transform.position.y - s2;
        float y2 = transform.position.y + s2;
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
        PhotonNetwork.Instantiate(nutrients.name, spawnPoint, Quaternion.identity);
    }   
}
