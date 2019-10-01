using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
    private string roomName;

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // TODO-2.a: 
    // Copy and paste the Start() and OnJoinedLobby() methods from MobileNetwork_Cube.cs
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = "testing";
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom(roomName);
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
	{
		GetComponent<MobileShooter>().Activate();
		base.OnJoinedRoom ();
    }


}
