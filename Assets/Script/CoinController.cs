using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Move () { // Coin 을 회전시키는 역할
		this.transform.Rotate (0, 0, 4);		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
}
