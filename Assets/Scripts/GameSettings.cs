using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    public static bool IsHost = false;

    public static string LocalPlayerTag
    {
        get
        {
            return IsHost ? "Player1" : "Player2";
        }
    }
}
