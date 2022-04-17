using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class DesroyBlock : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private Tilemap DestructableTilemap;
    private PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        DestructableTilemap = GetComponent < Tilemap >();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string name = collision.gameObject.name;

            if (name == "bullets_side(Clone)")//change to bullet later
            {
                var contacts = collision.contacts;
                Vector3[] desPoints = new Vector3()[contacts.Length];
                Vector3 hitPosition = Vector3.zero;
                foreach (ContactPoint2D hit in contacts)
                {

                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

                    DestructableTilemap.SetTile(DestructableTilemap.WorldToCell(hitPosition), null);
                }
                PV.RPC("DestroyMap", RpcTarget.All, collision.contacts);
            }
        }
    }
    [PunRPC]
    void DestroyMap(ContactPoint2D[] contacts)
    {
        

            

     }
}
