using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DesroyBlock : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private Tilemap DestructableTilemap;
    // Start is called before the first frame update
    void Start()
    {
        DestructableTilemap = GetComponent < Tilemap >();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        string name = collision.gameObject.name;
        
        if (name == "bullets_side(Clone)")//change to bullet later
        {
            Vector3 hitPosition = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                DestructableTilemap.SetTile(DestructableTilemap.WorldToCell(hitPosition), null);
            }

        }
    }
}
