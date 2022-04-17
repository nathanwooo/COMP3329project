using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class lungLogic : MonoBehaviour
{
    public float time = 5;
    private bool willTp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0 && willTp)
        {

            SceneManager.LoadScene("heart");
            willTp = false;
        }
    }
}
