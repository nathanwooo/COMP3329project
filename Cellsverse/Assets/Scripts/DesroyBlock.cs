using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Photon.Pun;
public class DesroyBlock : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private Tilemap DestructableTilemap;
    private PhotonView PV;

    private string[] targetObject = {"bullets_side(Clone)","bullets_rifle(Clone)",
                                     "weapon_sword_up", "weapon_sword_down", "weapon_sword_left", "weapon_sword_right",
                                     "weapon_hammer_up", "weapon_hammer_down", "weapon_hammer_left", "weapon_hammer_right"
                                    };
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
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         string name = collision.gameObject.name;
    //         if (targetObject.Contains(name))//change to bullet later
    //         {
    //             var contacts = collision.contacts;
    //             Vector3[] desPoints = new Vector3[contacts.Length];
    //             Vector3 hitPosition = Vector3.zero;
    //             var i = 0;
    //             foreach (ContactPoint2D hit in contacts)
    //             {

    //                 hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
    //                 hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
    //                 desPoints[i++] = hitPosition;
    //             }
    //             PV.RPC("DestroyMap", RpcTarget.All, desPoints);   
    //         }
    //     }
    // }
    public void DestroyBlock(ContactPoint2D[] contacts){
        Vector3[] desPoints = new Vector3[contacts.Length];
        Vector3 hitPosition = Vector3.zero;
        var i = 0;
        foreach (ContactPoint2D hit in contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            desPoints[i++] = hitPosition;
        }
        PV.RPC("DestroyMap", RpcTarget.All, desPoints);   
    }

    [PunRPC]
    void DestroyMap(Vector3[] desPoints)
    {
        foreach (Vector3 point in desPoints) {
            DestructableTilemap.SetTile(DestructableTilemap.WorldToCell(point), null);
        }
    }
}
