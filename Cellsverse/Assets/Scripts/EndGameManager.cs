using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public Text username;
    public Text loserusername;
    public GameObject winPanel;
    public GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        if(lungLogic.ownGameScore == 2)
        {
            winPanel.SetActive(true);
            username.text = "You win!!";
        }
        else
        {
            losePanel.SetActive(true);
            loserusername.text = "You are second place!!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
