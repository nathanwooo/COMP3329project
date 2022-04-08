using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Animations;

public class CharControl : MonoBehaviour{
    public float movementSpeed = 3.0f; //set movement speed
    public Rigidbody2D rb; //set rigidbody
    private Vector2 movement;//for moving the character
    private Vector3 mousePosition;//for tracking mouse position

    public GameObject idle, run_side, run_up, run_down;
    public GameObject gun_side, gun_up, gun_down;
    public GameObject sword_up, sword_down, sword_side;
    void Start(){
        setActive("idle");
    }

    void Update(){
        
        //movement
        movementSpeed = 3.0f;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //animation

        //no key is pressed
        if (!Input.anyKey){
            setActive("idle");
        }

        //WASD key is pressed
        if(Input.GetKey(KeyCode.W)){
            running(KeyCode.W);
        }
        else if(Input.GetKey(KeyCode.A)){
            running(KeyCode.A);
        }
        else if(Input.GetKey(KeyCode.S)){
            running(KeyCode.S);
        }
        else if(Input.GetKey(KeyCode.D)){
            running(KeyCode.D);
        }
        //Mouse left right is pressed
        else if(Input.GetMouseButtonDown(0)){
            gunning();
        }
        else if(Input.GetMouseButtonDown(1)){
            swording();
        }

        
    }

    private void FixedUpdate(){//update postion 
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void running(KeyCode key){
        if(key == KeyCode.A){
            run_side.GetComponent<SpriteRenderer>().flipX = false;
            setActive("run_side");
        }

        else if(key == KeyCode.D){
            run_side.GetComponent<SpriteRenderer>().flipX = true;
            setActive("run_side");
        }

        else if(key == KeyCode.S){
            setActive("run_down");
        }

        else if(key == KeyCode.W){
            setActive("run_up");
        }
    }

    private void gunning(){

        movementSpeed = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Debug.Log(mousePosition);
        // Debug.Log(this.gameObject.transform.position);

        if (mousePosition.x - this.gameObject.transform.position.x < 0){//LHS shooting
            flipGun_SideSprite(false);
            if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y)){
                setActive("gun_side");
            }
            else{
                if(mousePosition.y - this.gameObject.transform.position.y > 0){
                    setActive("gun_up");
                }
                if(mousePosition.y - this.gameObject.transform.position.y < 0){
                    setActive("gun_down");
                }
            }
        }

        if (mousePosition.x - this.gameObject.transform.position.x > 0){//RHS shooting
            flipGun_SideSprite(true);
            if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y)){
                setActive("gun_side");
            }
            else{
                if(mousePosition.y - this.gameObject.transform.position.y > 0){
                    setActive("gun_up");
                }
                if(mousePosition.y - this.gameObject.transform.position.y < 0){
                    setActive("gun_down");
                }
            }
        }
    }

    private void swording(){
        movementSpeed = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Debug.Log(mousePosition);
        // Debug.Log(this.gameObject.transform.position);

        if (mousePosition.x - this.gameObject.transform.position.x < 0){//LHS shooting
            flipSword_SideSprite(false);
            if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y)){
                setActive("sword_side");
            }
            else{
                if(mousePosition.y - this.gameObject.transform.position.y > 0){
                    setActive("sword_up");
                }
                if(mousePosition.y - this.gameObject.transform.position.y < 0){
                    setActive("sword_down");
                }
            }
        }

        if (mousePosition.x - this.gameObject.transform.position.x > 0){//RHS shooting
            flipSword_SideSprite(true);
            if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y)){
                setActive("sword_side");
            }
            else{
                if(mousePosition.y - this.gameObject.transform.position.y > 0){
                    setActive("sword_up");
                }
                if(mousePosition.y - this.gameObject.transform.position.y < 0){
                    setActive("sword_down");
                }
            }
        }
    }

    private void flipGun_SideSprite(bool flipOrNot){ //for fliping the gun
        int actionLength = gun_side.gameObject.transform.childCount;

        for (int i = 0; i <actionLength; i++){
            
            GameObject child = gun_side.transform.GetChild(i).gameObject;

            if (flipOrNot == true){
                if (child.name == "muzzleflash_gun"){//for flipping the muzzle sprite Need Help
                    child.GetComponent<SpriteRenderer>().flipX = true;
                    // child.transform.position = new Vector3(-child.transform.position.x, child.transform.position.y, child.transform.position.z);
                }
                else{
                    child.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else{
                child.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void flipSword_SideSprite(bool flipOrNot){
        int actionLength = sword_side.gameObject.transform.childCount;
        for (int i = 0; i <actionLength; i++){
            GameObject child = sword_side.transform.GetChild(i).gameObject;
            child.GetComponent<SpriteRenderer>().flipX = flipOrNot;
        }
    }

    private void setActive(string activity){
        if (activity == "run_up"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(true);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "run_down"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(true);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "run_side"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(true);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "gun_side"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(true);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "gun_up"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(true);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "gun_down"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(true);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "idle"){
            idle.gameObject.SetActive(true);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "sword_side"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(true);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "sword_up"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(true);
            sword_down.gameObject.SetActive(false);
        }
        else if (activity == "sword_down"){
            idle.gameObject.SetActive(false);
            run_up.gameObject.SetActive(false);
            run_down.gameObject.SetActive(false);
            run_side.gameObject.SetActive(false);
            gun_side.gameObject.SetActive(false);
            gun_up.gameObject.SetActive(false);
            gun_down.gameObject.SetActive(false);
            sword_side.gameObject.SetActive(false);
            sword_up.gameObject.SetActive(false);
            sword_down.gameObject.SetActive(true);
        }
    }
    
}
