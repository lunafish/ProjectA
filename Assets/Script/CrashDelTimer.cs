using UnityEngine;
using System.Collections;

public class CrashDelTimer : MonoBehaviour {

	public float _delay = 0.5f;
	private float _current = 0.0f;

	// Use this for initialization
	void Start () {
		_current = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if( _current > _delay )
			Destroy(transform.gameObject);
		_current += Time.deltaTime;
	}
}
