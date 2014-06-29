using UnityEngine;
using System.Collections;

public class CorgiCollision : MonoBehaviour {

	public string STATE = "RUN";

	public StageControl _stageCtrl;

	public AudioClip _coin_es;
	public AudioClip _mon_p_es;

	public GameObject _rocket;

	// particle Rocket
	public GameObject _rocket_fire_prefab; // 불꽃이 될 프래팹을 연결해 줄곳
	Vector3 position; // 위치변수 
	Quaternion rotation; // 각도변수 
	GameObject fire_controller; // 생성된 불꽃과 연결할 곳

	// particle Mon_p
	public GameObject _mon_p_fire_prefab;
	Vector3 position_mon_p; // 위치변수 
	Quaternion rotation_mon_p; // 각도변수 
	GameObject fire_controller_mon_p; // 생성된 불꽃과 연결할 곳

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// particle Rocket
		if(Input.GetKeyDown(KeyCode.Space)){
			position = _rocket.transform.position + new Vector3(-0.5f, 0.0f, 0.0f); // 위치셋팅
			rotation = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(0,0,90); // 각도셋팅
			//rotation = Quaternion.Euler(0,180,0);
			fire_controller = Instantiate( _rocket_fire_prefab, position, rotation ) as GameObject;
			fire_controller.transform.parent = _rocket.transform;
		}
	
	}
	/*
	void OnTriggerEnter(Collider other){ // 공이 뚫고 지나가도록 할때 Inspector 에서 Is Trigger 체크후 함수명과 매개변수 변경하여 작성
		// 왼쪽공이 부딪혔을때 불을 붙여준다.( Instantiate )
		//this.transform.Translate (0.02f, 0, 0);
		if(other.gameObject.tag == "Mon_p_tag")
		{
			// particle
			position = other.transform.position; // 위치셋팅
			rotation = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller = Instantiate( _mon_p_fire_prefab, position, rotation ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );
			
			//Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}
	}
	*/
	void OnCollisionEnter(Collision other)
	{
		//Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "Mon_p_tag") {

			// particle Mon_p
			position_mon_p = other.transform.position; // 위치셋팅
			rotation_mon_p = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_mon_p = Instantiate( _mon_p_fire_prefab, position_mon_p, rotation_mon_p ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );

			//Audio
			audio.clip = _mon_p_es;
			audio.Play();

			//Destroy Mon
			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Hurdle_tag") {
			/*
			// particle Hurdle
			position_mon_p = other.transform.position; // 위치셋팅
			rotation_mon_p = Quaternion.identity; // 각도셋팅
			//rotation = Quaternion.Euler(-90,0,0); // 각도셋팅
			fire_controller_mon_p = Instantiate( _mon_p_fire_prefab, position_mon_p, rotation_mon_p ) as GameObject; //Instantiate( 프리팹, 위치, 각도 );
			*/
			//Audio
			audio.clip = _mon_p_es;
			audio.Play();
			
			//Destroy Mon
			Destroy(other.gameObject);
			_stageCtrl.GameOver();
		}

		if(other.gameObject.tag == "Coin_tag") {
			//Audio
			audio.clip = _coin_es;
			audio.Play();

			//Destroy Coin
			Destroy(other.gameObject);
			_stageCtrl.UpdateCoin();
		}

		GetComponent<Animator>().SetBool("jump", false);

		STATE = "RUN";

	}
}
