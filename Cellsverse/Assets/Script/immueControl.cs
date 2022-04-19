using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor.Animations;
using Photon.Pun;


public class immueControl : MonoBehaviour
{
    private Dictionary<string, GameObject> actionMap;
    private GameObject actionActive;
    private GameObject actionInactive;
    private int actionLength;
    private string[] actionInactiveList;
    private string parent;
    private bool keyInput;
    [SerializeField] private Camera cam;
    [SerializeField] private Camera cam2;
    PhotonView photonView;


    //private int actionLength = GameObject.transform.childCount;
    //private string[] actionInactiveList = new string[actionLength];


    //public string[] actionInactiveList = {"sycthe_left", "sycthe_right", "run_down", "run_left", "run_right", "sword_left", "sword_right"};
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        actionMap = new Dictionary<string, GameObject>();
        actionLength = this.gameObject.transform.childCount - 3;
        actionInactiveList = new string[actionLength];
        keyInput = true;
        //cam = Camera.main;
        for (int i = 0; i < actionLength; i++)
        {
            GameObject child = this.gameObject.transform.GetChild(i + 3).gameObject;
            actionInactiveList[i] = child.name;
            actionMap.Add(child.name, child);

            Center(actionInactiveList[i]);
            if (actionInactiveList[i].EndsWith("right"))
            {
                Flip(actionInactiveList[i]);
            }
        }

        //actionInactiveList = actionInactiveList.ToList().RemoveAt(0).ToArray();
        actionActive = actionMap["idle"];
        foreach (string action in actionInactiveList)
        {
            if (action != "idle")
            {
                Sleep(action);
                AddCollider(action);
            }
        }
        WakeFast("idle");
        AddCollider("idle");
        EnableCollider("idle");
        if (!photonView.IsMine)
        {
            Debug.Log("XDDD");
            cam.GetComponent<AudioListener>().enabled = false;
            cam.enabled = false;
            cam2.enabled = false;
            Debug.Log("========================================");
        }

    }
    void FixedUpdate()
    {
        // Only move the player object if it's the local user's player
        if (photonView.IsMine)
        {
            Move();
        }
    }
    // Update is called once per frame
    void Move()
    {
        
        if (!Input.anyKey)
        {
            Activate("idle");
        }

        if (keyInput)
        {

            if (Input.GetKey(KeyCode.A))
            {
                Activate("run_left");
            }

            if (Input.GetKey(KeyCode.D))
            {
                Activate("run_right");
            }

            if (Input.GetKey(KeyCode.W))
            {
                Activate("run_up");
            }

            if (Input.GetKey(KeyCode.S))
            {
                Activate("run_down");
            }

            if (Input.GetMouseButton(0))
            {

                Vector3 mousePosition = Input.mousePosition;

                mousePosition = cam.ScreenToWorldPoint(mousePosition);
                //Debug.Log(mousePosition);
                //Debug.Log(this.gameObject.transform.position);
                if (mousePosition.x - this.gameObject.transform.position.x > 0)
                {//RHS
                    if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y))
                    {
                        ActivateAttack("gun_right");
                    }
                    else
                    {
                        if (mousePosition.y - this.gameObject.transform.position.y > 0)
                        {
                            ActivateAttack("gun_up");
                        }
                        if (mousePosition.y - this.gameObject.transform.position.y < 0)
                        {
                            ActivateAttack("gun_down");
                        }
                    }
                }

                if (mousePosition.x - this.gameObject.transform.position.x < 0)
                {//LHS
                    if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y))
                    {
                        ActivateAttack("gun_left");
                    }
                    else
                    {
                        if (mousePosition.y - this.gameObject.transform.position.y > 0)
                        {
                            ActivateAttack("gun_up");
                        }
                        if (mousePosition.y - this.gameObject.transform.position.y < 0)
                        {
                            ActivateAttack("gun_down");
                        }
                    }
                }

            }

            if (Input.GetMouseButton(1))
            {

                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10;
                mousePosition = cam.ScreenToWorldPoint(mousePosition);

                //Debug.Log(mousePosition);
                //Debug.Log(this.gameObject.transform.position);
                if (mousePosition.x - this.gameObject.transform.position.x > 0)
                {//RHS
                    if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y))
                    {
                        ActivateAttack("sword_right");
                    }
                    else
                    {
                        if (mousePosition.y - this.gameObject.transform.position.y > 0)
                        {
                            ActivateAttack("sword_up");
                        }
                        if (mousePosition.y - this.gameObject.transform.position.y < 0)
                        {
                            ActivateAttack("sword_down");
                        }
                    }
                }

                if (mousePosition.x - this.gameObject.transform.position.x < 0)
                {//LHS
                    if (Math.Abs(mousePosition.x - this.gameObject.transform.position.x) > Math.Abs(mousePosition.y - this.gameObject.transform.position.y))
                    {
                        ActivateAttack("sword_left");
                    }
                    else
                    {
                        if (mousePosition.y - this.gameObject.transform.position.y > 0)
                        {
                            ActivateAttack("sword_up");
                        }
                        if (mousePosition.y - this.gameObject.transform.position.y < 0)
                        {
                            ActivateAttack("sword_down");
                        }
                    }
                }

            }

            // if(Input.GetMouseButton(0)){
            //     Vector3 mousePos = Input.mousePosition;
            //     Debug.Log(mousePos.x);
            //     Debug.Log(mousePos.y);
            //     Activate("_left");
            // }
        }

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 vertical = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
        if (keyInput)
        {
            transform.position = transform.position + horizontal * Time.deltaTime;
            transform.position = transform.position + vertical * Time.deltaTime;
        }
    }

    void Activate(string action)
    {
        if (actionActive != actionMap[action])
        {
            Sleep(actionActive.name);
            DisableCollider(actionActive.name);
            WakeFast(action);
            actionActive = actionMap[action];
            EnableCollider(action);
            photonView.RPC("enemyActivate", RpcTarget.Others, action);
        }
    }
    [PunRPC]
    void enemyActivate(string action)
    {
        if (!photonView.IsMine && actionActive != actionMap[action])
        {
            Sleep(actionActive.name);
            DisableCollider(actionActive.name);
            WakeFast(action);
            actionActive = actionMap[action];
            EnableCollider(action);
        }
    }

        void ActivateAttack(string action)
    {
        if (actionActive != actionMap[action])
        {
            Sleep(actionActive.name);
            DisableCollider(actionActive.name);
            StartCoroutine(WakeSlow(action));
            actionActive = actionMap[action];
            EnableCollider(action);
        }

    }

    void Sleep(string action)
    {
        var tf = actionMap[action].transform;

        if (tf.GetComponent<SpriteRenderer>())
        {
            tf.GetComponent<SpriteRenderer>().enabled = false;
        }

        for (int i = 0; i < tf.childCount; i++)
        {
            tf.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void WakeFast(string action)
    {
        var tf = actionMap[action].transform;

        if (tf.GetComponent<SpriteRenderer>())
        {
            tf.GetComponent<SpriteRenderer>().enabled = true;
        }

        for (int i = 0; i < tf.childCount; i++)
        {
            tf.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    IEnumerator WakeSlow(string action)
    {
        keyInput = false;
        var tf = actionMap[action].transform;

        if (tf.GetComponent<SpriteRenderer>())
        {
            tf.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (tf.GetComponent<Animator>())
        {
            tf.GetComponent<Animator>().Play(tf.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name, -1, 0f);
        }


        for (int i = 0; i < tf.childCount; i++)
        {
            if (tf.GetChild(i).name != "shadow")
            {
                if (tf.GetChild(i).GetComponent<Animator>())
                {
                    Animator animator = tf.GetChild(i).GetComponent<Animator>();
                    animator.Play(0, -1, 0f);
                }
                tf.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        yield return new WaitForSeconds(0.5f);
        keyInput = true;
    }

    void Flip(string action)
    {
        var tf = actionMap[action].transform;

        if (tf.GetComponent<SpriteRenderer>())
        {
            tf.GetComponent<SpriteRenderer>().flipX = true;
        }

        for (int i = 0; i < tf.childCount; i++)
        {
            tf.GetChild(i).GetComponent<SpriteRenderer>().flipX = true;
            var childPos = tf.GetChild(i).transform.localPosition;
            tf.GetChild(i).transform.localPosition = new Vector3(-childPos.x, childPos.y, childPos.z); //bug
        }
    }

    void Center(string action)
    {
        if (actionMap[action].gameObject.name != "PlayerBoundary")
        {
            var tf = actionMap[action].transform;
            tf.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    void AddCollider(string action)
    {
        if (actionMap[action].gameObject.name != "PlayerBoundary")
        {
            BoxCollider2D bc = actionMap[action].AddComponent<BoxCollider2D>();
            bc.enabled = false;
            bc.offset = new Vector2(0, 0.5f);
            bc.size = new Vector2(1, 1.5f);
            var tf = actionMap[action].transform;
        }
        
        // for(int i = 0; i < tf.childCount; i++)
        // {
        //     if (tf.GetChild(i).name != "shadow")
        //     {
        //         if(tf.GetChild(i).name.StartsWith("weapon"))
        //         {
        //             PolygonCollider2D childPc = tf.GetChild(i).gameObject.AddComponent<PolygonCollider2D>();
        //             childPc.enabled = false;
        //         }
        //         else
        //         {
        //             BoxCollider2D childBc = tf.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
        //             childBc.enabled = false;
        //             childBc.offset = new Vector2(0, 0.5f);
        //             childBc.size = new Vector2(1, 1.5f);
        //         }
        //     }
        // }
    }

    void EnableCollider(string action)
    {
        BoxCollider2D bc = actionMap[action].GetComponent<BoxCollider2D>();
        bc.enabled = true;
        var tf = actionMap[action].transform;
        // for(int i = 0; i < tf.childCount; i++)
        // {
        //     if (tf.GetChild(i).name != "shadow")
        //     {
        //         if(tf.GetChild(i).name.StartsWith("weapon"))
        //         {
        //             PolygonCollider2D childPc = tf.GetChild(i).gameObject.GetComponent<PolygonCollider2D>();
        //             childPc.enabled = true;
        //         }
        //         else
        //         {
        //             BoxCollider2D childBc = tf.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
        //             childBc.enabled = true;
        //         }
        //     }
        // }
    }

    void DisableCollider(string action)
    {
        BoxCollider2D bc = actionMap[action].GetComponent<BoxCollider2D>();
        bc.enabled = false;
        var tf = actionMap[action].transform;
        // for(int i = 0; i < tf.childCount; i++)
        // {
        //     if (tf.GetChild(i).name != "shadow")
        //     {
        //         if(tf.GetChild(i).name.StartsWith("weapon"))
        //         {
        //             PolygonCollider2D childPc = tf.GetChild(i).gameObject.GetComponent<PolygonCollider2D>();
        //             childPc.enabled = false;
        //         }
        //         else
        //         {
        //             BoxCollider2D childBc = tf.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
        //             childBc.enabled = false;
        //         }
        //     }
        // }
    }
}
