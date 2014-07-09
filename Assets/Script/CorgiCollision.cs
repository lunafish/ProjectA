using UnityEngine;
using System.Collections;

public class CorgiCollision : MonoBehaviour {

	public string STATE = "RUN";

	public StageControl _stageCtrl;

	public CorgiControl _corgiCtrl;

	public AudioClip _coin_es;
	public AudioClip _mon_p_es;

	public GameObject _rocket;
	public GameObject _buster;

	// Particle Rocket
	public GameObject _rocket_fire_prefab; // 불꽃이 될 프래팹을 연결해 줄곳
	Vector3 position; // 위치변수 
	Quaternion rotation; // 각도변수 
	GameObject fire_controller; // 생성된 불꽃과 연결할 곳

	// Particle Crash //Mon_p, Hurdle
	public GameObject _crash_fire_prefab;
	Vector3 position_crash; // 위치변수 
	Quaternion rotation_crash; // 각도변수 
	GameObject fire_controller_crash; // 생성된 불꽃과 연결할 곳

	// Particle Coin
	public GameObject _coin_fire_prefab;
	Vector3 position_coin; // 위치변수 
	Quaternion rotation_coin; // 각도변수 
	GameObject fire_controller_coin; // 생성된 불꽃과 연결할 곳

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Particle Rocket
		if(Input.GetKeyDown(KeyCode.Space)){
			position = _rocket.transform.position + new Vector3(-0.5f, 0.0f, 0.0f); // 위치셋팅
			rotation = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(0,0,90); // 각도셋팅
			//rotation = Quaternion.Euler(0,180,0);
			fire_controller = Instantiate( _rocket_fire_prefab, position, rotation ) as GameObject;
//			fire_controller.transform.parent = _rocket.transform;
		}
	
	}
	/*
	void OnTriggerEnter(Collider other){ // 공이 뚫고 지나가도록 할때 Inspector 에서 Is Trigger 체크후 함수명과 매개변수 변경하여 작성
		// 왼쪽공이 부딪혔을때 불을 붙여준다.( Instantiate )
		//this.transform.Translate (0.02f, 0, 0);
		if(other.gameObject.tag == "Mon_p_tag")
		{
			// Particle
			position = other.transform.position; // 위치셋팅
			rotation = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller = Instantiate( _crash_fire_prefab, position, rotation ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );
			
			//Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}
	}
	*/
	void OnCollisionEnter(Collision other)
	{
		//Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "Mon_p_tag") {

			// Particle Mon_p
			position_crash = other.transform.position + new Vector3(0.0f, 0.1f, -0.2f); // 위치셋팅 position = _rocket.transform.position + new Vector3(-0.5f, 0.0f, 0.0f);
			rotation_crash = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_crash = Instantiate( _crash_fire_prefab, position_crash, rotation_crash ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );
//			fire_controller_mon_p.transform.parent = transform;

			//Audio
			audio.clip = _mon_p_es;
			audio.Play();

			//Life Minus
			_stageCtrl.LifeMinus();			// *

			// buster false
			_corgiCtrl.ChangeBuster( false );

			//Destroy Mon
//			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Hurdle_tag") {

			// Particle Hurdle
			position_crash = other.transform.position + new Vector3(0.0f, 0.0f, -0.2f); // 위치셋팅
			rotation_crash = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_crash = Instantiate( _crash_fire_prefab, position_crash, rotation_crash ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );
//			fire_controller_crash.transform.parent = transform;

			//Audio
			audio.clip = _mon_p_es;
			audio.Play();

			//Life Minus
			_stageCtrl.LifeMinus();			// *

			// buster false
			_corgiCtrl.ChangeBuster( false );

			//Destroy Mon
//			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Coin_tag") {

			// Particle Coin
			position_coin= other.transform.position; // 위치셋팅
			rotation_coin = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_coin = Instantiate( _coin_fire_prefab, position_coin, rotation_coin ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );

			// OverlapSphere Coin
//			float ItemRadius = 5.0f;
//			Collider3D[] items = Physics2D.OverlapCircleAll(transform.position, ItemRadius, 1 << LayerMask.NameToLayer("Coin_tag"));

			//Audio
			audio.clip = _coin_es;
			audio.Play();

			//Destroy Coin
			Destroy(other.gameObject);
			_stageCtrl.UpdateCoin();
		}

		if(other.gameObject.tag == "Power_tag") {
			
			// Particle Coin
			position_coin= other.transform.position; // 위치셋팅
			rotation_coin = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_coin = Instantiate( _coin_fire_prefab, position_coin, rotation_coin ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );

			//Audio
			audio.clip = _coin_es;
			audio.Play();
			
			//Destroy Coin
			Destroy(other.gameObject);

			//Life Plus
			_stageCtrl.LifePlus();

			// buster
			_corgiCtrl.ChangeBuster( true );
		}

		GetComponent<Animator>().SetBool("jump", false);

		if(STATE != "CRASH" && STATE != "STAY")
			STATE = "RUN";


	}//OnCollisionEnter
}
