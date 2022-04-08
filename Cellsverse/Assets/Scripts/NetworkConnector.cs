using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkConnector : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    #region Pun Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to photon!");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Failed to connect: {cause}");
    }
    #endregion
}