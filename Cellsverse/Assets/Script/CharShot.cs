using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShot : MonoBehaviour{

    public Transform firePoint_south, firePoint_north, firePoint_west, firePoint_east;
    public GameObject bullet_side, bullet_vertical, character;
    public float bulletForce = 10f;
    public Camera cam;
    private Vector3 mousePosition;
    void Update(){
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)){
            if (mousePosition.x - character.transform.position.x < 0){//LHS shooting
                if (Math.Abs(mousePosition.x - character.transform.position.x) > Math.Abs(mousePosition.y - character.transform.position.y)){
                    shoot("west");
                }
                else{
                    if(mousePosition.y - character.transform.position.y > 0){
                        shoot("north");
                    }
                    if(mousePosition.y - character.transform.position.y < 0){
                        shoot("south");
                    }
                }
            }
        
            if (mousePosition.x - character.transform.position.x > 0){//RHS shooting
                if (Math.Abs(mousePosition.x - character.transform.position.x) > Math.Abs(mousePosition.y - character.transform.position.y)){
                    shoot("east");
                }
                else{
                    if(mousePosition.y - character.transform.position.y > 0){
                        shoot("north");
                    }
                    if(mousePosition.y - character.transform.position.y < 0){
                        shoot("south");
                    }
                }
            }
        }
    }
    private void shoot(string direction){
        if (direction == "south"){
            GameObject bullet = Instantiate(bullet_vertical, firePoint_south.position, firePoint_south.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint_south.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (direction == "north"){
            GameObject bullet = Instantiate(bullet_vertical, firePoint_north.position, firePoint_north.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint_north.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (direction == "east"){
            GameObject bullet = Instantiate(bullet_side, firePoint_east.position, firePoint_east.rotation);
            Debug.Log(bullet.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint_east.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (direction == "west"){
            bullet_side.GetComponent<SpriteRenderer>().flipX = true;
            GameObject bullet = Instantiate(bullet_side, firePoint_west.position, firePoint_west.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint_west.up * bulletForce, ForceMode2D.Impulse);
        }
    }

}