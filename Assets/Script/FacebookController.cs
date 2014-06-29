using UnityEngine;
using System.Collections;

public class FacebookController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Login() {
		Debug.Log("Start Facebook Login");
		Application.LoadLevel("Scene_Run");
	}
}
