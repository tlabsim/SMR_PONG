using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

    public GameObject PlayerPrefab;
    public GameObject Arena;
    public GameObject Ball;
    public GameObject AIPlayer;

    int ClientConnected = 0;
    
	public void Start()
    {
        autoCreatePlayer = false;
        ClientConnected = 0;
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        GameSettings.IsHost = true;

        Debug.Log("Host started");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Hello");

        var player = (GameObject)GameObject.Instantiate(PlayerPrefab);

        if (GameSettings.IsHost)
        {            
            player.tag = "Player1";

            //player.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            player.tag = "Player2";

            //player.GetComponent<MeshRenderer>().material.color = Color.magenta;

            //Remove the AI Player
            Destroy(AIPlayer);            
        }
        //player.transform.parent = Arena.transform;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        var GameController = GameObject.FindGameObjectWithTag("Game").GetComponent<GameController>();
        
        GameController.Player1 = player;

        Debug.Log("Player added");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        

        //base.OnClientConnect(conn);
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {   
        base.OnServerConnect(conn);

        ClientConnected++;

        if (ClientConnected == 2)
        {
            Ball.SendMessage("Roll");
        }

        Debug.Log("Client connected");
    }

}
