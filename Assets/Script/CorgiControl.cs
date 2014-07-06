using UnityEngine;
using System.Collections;

public class CorgiControl : MonoBehaviour {

	public GameObject _corgi_normal;
	public GameObject _corgi_buster;

	public CorgiCollision _corgi;

//	public float _JumpPower = 150.0f;
	public float _JumpPower = 5.0f;

	// Use this for initialization
	void Start () {
		// get default collision
		_corgi = _corgi_normal.GetComponent<CorgiCollision>();
	}

	public void ChangeBuster( bool isBuster ) {
		// isBuster : true : buster mode
		// isBUster : false : normal mode

		if(isBuster == true ) {
			_corgi = _corgi_buster.GetComponent<CorgiCollision>();
			_corgi_buster.SetActive(true);
			_corgi_normal.SetActive(false);
		} else {
			_corgi = _corgi_normal.GetComponent<CorgiCollision>();
			_corgi_buster.SetActive(false);
			_corgi_normal.SetActive(true);
		}

	}

	// Update is called once per frame
	void Update () {
//		Debug.Log(_corgi.STATE);


		//if(Input.GetKeyDown(KeyCode.Space) && _corgi.STATE == "RUN" ) {
		if(Input.GetKeyDown(KeyCode.Space) && (_corgi.STATE == "RUN" || _corgi.STATE =="JUMP") ) {
			//_corgi.rigidbody.AddForce(0,_JumpPower,0);
			_corgi.rigidbody.velocity = new Vector3(0, _JumpPower, 0);
			_corgi.GetComponent<Animator>().SetBool("jump", true);
			_corgi.STATE = "JUMP";

		}//if
		/*
		if(Input.GetKeyDown(KeyCode.Space)
		   && _corgi.STATE=="RUN")
		{
			_corgi.SetBool ("Run", false);
			_corgi.SetBool ("ToJump2", false);
			_corgi.SetBool ("ToJump1", true); // 점프 on	
			// 점프를 뛰도록 만들어준다. (AddForce)
			_corgi.rigidbody.AddForce(0,500,0);
			// 지금 공중에 있다는 신호를 준다. 
			_corgi.STATE = "JUMP";
		}
		else if(Input.GetKeyDown(KeyCode.Space) 
		        && _corgi.STATE=="JUMP")
		{
			// 더블점프 애니메이션으로 바꿔준다.
			_corgi.SetBool ("Run", false);
			//PLAYER_ANIMATOR.SetBool ("ToJump2", true);
			_corgi.SetTrigger ("ToJump2"); // 트리거로 바꿔줌. 애니메이션이 한번 재싱후 원래대로 돌아가도록..
			// 점프를 한번 위로 더 뛰어준다.
			_corgi.rigidbody.AddForce (0, 200, 0);
			// 더블점프 상태
			//player_collision_script.STATE="JUMP2";
		}
		*/
	}//Update
}
