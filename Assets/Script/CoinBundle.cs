using UnityEngine;
using System.Collections;

public class CoinBundle : MonoBehaviour {

	public float _speed_coin_bundle = -0.1f;

	// Use this for initialization
	void Start () {
		_speed_coin_bundle = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(_speed_coin_bundle, 0.0f, 0.0f);

		/*
		// Reset Coin Bundle Position
		Vector3 cb01 = this.transform.position;
		if( cb01.x < -12.0f )
		{
			cb01.x += 36.0f + 7.0f;
			this.transform.position = cb01;
		}
		Debug.Log ("cb01:  " + cb01.x);
		*/
	}//Update
}
