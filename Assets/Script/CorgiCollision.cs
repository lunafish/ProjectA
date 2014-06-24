using UnityEngine;
using System.Collections;

public class CorgiCollision : MonoBehaviour {
	public string STATE = "RUN";

	public StageControl _stageCtrl;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "Mon_p_tag") {
			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Coin_tag") {
			Destroy(other.gameObject);
			_stageCtrl.UpdateCoin();
		}

		GetComponent<Animator>().SetBool("jump", false);

		STATE = "RUN";

	}
}
