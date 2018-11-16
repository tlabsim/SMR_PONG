using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLABS.Net.BasicClientServer;
using UnityEngine.UI;

public class LocalIPViewer : MonoBehaviour {
	
	void Start () {
        GetComponent<Text>().text = LocalMachine.Address.IPv4.ToString();
	}
	
}
