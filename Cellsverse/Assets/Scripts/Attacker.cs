using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attacker : MonoBehaviour
{
    private int life;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var collidedObject = collision.gameObject;
        // Debug.Log("Hited"+ collidedObject.name);
        if (collidedObject.name == "bullets_rifle(Clone)")
        {
            if (--life <= 0)
            {
                PhotonNetwork.Destroy(GetComponent<PhotonView>());

            }
        }
    }
}
