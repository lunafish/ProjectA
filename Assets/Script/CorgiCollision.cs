using UnityEngine;
using System.Collections;

public class CorgiCollision : MonoBehaviour {

	public string STATE = "RUN";

	public StageControl _stageCtrl;

	public AudioClip _coin_es;
	public AudioClip _mon_p_es;

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

			audio.clip = _mon_p_es;
			audio.Play();

			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Coin_tag") {

			audio.clip = _coin_es;
			audio.Play();

			Destroy(other.gameObject);
			_stageCtrl.UpdateCoin();
		}

		GetComponent<Animator>().SetBool("jump", false);

		STATE = "RUN";

	}
}
