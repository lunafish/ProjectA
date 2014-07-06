using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private GameObject _corgi;

	// Use this for initialization
	void Start () {
		_corgi = GameObject.FindGameObjectWithTag("Player");
	
	}

	void Move () { // Coin 을 회전시키는 역할
		Vector3 v = _corgi.transform.position - transform.parent.transform.position;
		if(v.magnitude < 1.5f) {
			if(v.magnitude < 1.0f)
			{
				v.Normalize();
				v.z = 0.0f;
				transform.parent.transform.position += v;
			} else {
				v.Normalize();
				v.z = 0.0f;
				transform.parent.transform.position += (v * 0.25f);
			}
		}

		this.transform.Rotate (0, 0, 4);		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
}
