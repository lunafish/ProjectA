using UnityEngine;
using System.Collections;

public class StageControl : MonoBehaviour {

	public GameObject _back;
	public GameObject _center;
	public GameObject _front;

	public float _speed = -0.1f;
	private float _sum = 0.0f;

	public WallController _wallCtrl;
	// JumpPower, Jump Animation
	public CorgiControl _corgiCtrl;

	// Coin Bundle Control
	public GameObject[] _coinBundles;
	public int _currentCoinBundle = 0;

	// Power Array
	public GameObject[] _powerAry;		// *
	public int _currentPowerNum = 0;

	// Plus Life
	private int _lifePlus = 0;			// *

	//text
	public tk2dTextMesh _txt_score;
	private float _score;

	public tk2dTextMesh _txt_coin;
	private int _coin;

	public tk2dTextMesh _txt_flew;
	public tk2dTextMesh _txt_collected_coin;

	// tk2d Button
	public GameObject _gameoverBtn;  //text mesh : game over 
	public GameObject _playagainBtn;
	public GameObject _startBtn;

	// Black Out
	public GameObject _blackout;

	//Audio
	public AudioClip _game_run_es;
	public AudioClip _game_over_es;

	// Use this for initialization
	void Start () {

		_speed = 0.0f;
		_wallCtrl.WallScrollSpeed = 0.0f;

		// JumpPower
		_corgiCtrl._JumpPower = 0;
		// Jump Animation
//		_corgiCtrl._corgi.GetComponent<Animator>().SetBool("jump", false);
//		_corgiCtrl._corgi.STATE = "STAY";

		//Audio
		audio.clip = _game_run_es;
		audio.Play();

		//Animation
		_corgiCtrl._corgi.STATE = "STAY";

		//
		//c_b = (Instantiate(Resources.Load<GameObject>("CoinBundle01")) as GameObject);
		/*
		cbPos01 = coin_bundle_prefab.transform.position;
		cbPos01.x = 36.0f + 7.0f;
		cbPos01.y = 1.9f;

		coin_bundle_prefab = Instantiate( _coinBundleCtrl_01,
		                                 cbPos01
		                             	//this.transform.position,
		                             	//Quaternion.LookRotation(this.transform.forward),
		                             	) as GameObject; */
		//coin_bundle_prefab = Instantiate( _coinBundleCtrl_01, new Vector3( 43.0f, 1.9f, -1.0f) , Quaternion.identity) as GameObject;

	}

	void Init()
	{
		// Speed Scroll
		_speed = -0.1f;
		_wallCtrl.WallScrollSpeed = 0.001f;

		//Init Coin Bundle Index 
		_currentCoinBundle = 0;

		//Init Power Item Index
		_currentPowerNum = 0;		// *

		// JumpPower
//		_corgiCtrl._JumpPower = 150.0f;
		_corgiCtrl._JumpPower = 5.0f;

		// Animation
		//if(_corgiCtrl._corgi.STATE == "CRASH" || _corgiCtrl._corgi.STATE == "STAY")
		{
			_corgiCtrl._corgi.STATE = "RUN";
			//_corgiCtrl._corgi.GetComponent<Animator>().SetBool("crash", false);
			_corgiCtrl._corgi.GetComponent<Animator>().SetBool("run", true);
		}

		_score = 0;
		_txt_score.text = _score + "m";
		
		_coin = 0;
		_txt_coin.text = _coin + "coin";

		_gameoverBtn.SetActive(false);
		_playagainBtn.SetActive(false);
		_startBtn.SetActive(false);

		_txt_flew.gameObject.SetActive(false);
		_txt_collected_coin.gameObject.SetActive(false);

		//Black Out
		_blackout.SetActive(false);

		//Audio
		audio.clip = _game_run_es;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {

		_back.transform.Translate(_speed, 0.0f, 0.0f);
		_center.transform.Translate(_speed, 0.0f, 0.0f);
		_front.transform.Translate(_speed, 0.0f, 0.0f);

		_sum += _speed;
		if(_sum < -12.0f) {

			GameObject tmp = _back;
			//tmp = _back;

			_back = _center;
			_center = _front;
			_front = tmp;

			Vector3 v = tmp.transform.position;
			//Debug.Log(v);
			v.x += 36.0f;
			tmp.transform.position = v;
			//Debug.Log(v);

			_sum = 0.0f;

			//Clear Mon
			ClearMon(tmp);
			//Create Monster
			for(int i = 0; i < Random.Range(0, 2); i++)
				CreateMon(tmp);

			//Clear Coin
			ClearCoin(tmp);
			CreateCoinBundle(tmp);
			/*
			//Create Coin
			for(int i = 0; i < 10; i++)
				CreateCoin(tmp);
			*/

			//Power
			ClearPower(tmp);
			CreatePowerAry(tmp);

			//Clear Hurdle
			ClearHurdle(tmp);
			//Create Hurdle
			for(int i = 0; i < 1; i++)
				CreateHurdle(tmp);
		}//if

		//
		UpdateScore(_speed);
	}
	
	//Power Item
	void ClearPower(GameObject stage){
		Transform[] allChildren = stage.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			// do whatever with child transform here
			if(child.gameObject.tag == "Power_tag")
			{
				Destroy( child.gameObject );
			}
		}
	}
	
	void CreatePower(GameObject stage){
		GameObject power = (Instantiate(Resources.Load<GameObject>("PowerPrefab")) as GameObject);
		power.transform.parent = stage.transform;
		
		Vector3 pos = stage.transform.position;
		//pos.z = -1;
		pos.x += Random.Range(-6.0f, 6.0f);
		pos.y += Random.Range(1.0f, 3.0f);
		
		power.transform.position = pos;
	}

	void CreatePowerAry(GameObject stage, bool menual = false, float x = 0.0f, float y = 0.0f){
		
		Debug.Log("_currentPowerNum : " + _currentPowerNum);
		
		if(_currentPowerNum >= _powerAry.Length)
			_currentPowerNum = 0;
		
		GameObject powerAry = _powerAry[_currentPowerNum];
		if(powerAry == null)
		{

			// random
			Debug.Log("_currentPowerNum : " + _currentPowerNum);

			/*
			//Create Power
			for(int i = 0; i < 15; i++)
				CreatePower(stage);
			*/
		} else {
			GameObject power = Instantiate(powerAry) as GameObject;
			
			power.transform.parent = stage.transform;
			
			Vector3 pos = stage.transform.position;
			pos.z = -1.0f;
			if(menual == false) {
				pos.x += Random.Range(-3.5f, 3.5f);
				pos.y += Random.Range(0.8f, 1.8f);
			} else {
				pos.x = x;
				pos.y = y;
			}
			
			power.transform.position = pos;
		}
		
		_currentPowerNum++;
	}

	//Coin
	void ClearCoin(GameObject stage){
		Transform[] allChildren = stage.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			// do whatever with child transform here
			if(child.gameObject.tag == "Coin_tag")
			{
				Destroy( child.gameObject );
			}
		}
	}
	
	void CreateCoin(GameObject stage){
		GameObject coin = (Instantiate(Resources.Load<GameObject>("CoinPrefab")) as GameObject);
		coin.transform.parent = stage.transform;
		
		Vector3 pos = stage.transform.position;
		//pos.z = -1;
		pos.x += Random.Range(-6.0f, 6.0f);
		pos.y += Random.Range(1.0f, 3.0f);
		
		coin.transform.position = pos;
	}

	//Coin Bundle
	void CreateCoinBundle(GameObject stage, bool menual = false, float x = 0.0f, float y = 0.0f){

		Debug.Log("_currentCoinBundle : " + _currentCoinBundle);

		if(_currentCoinBundle >= _coinBundles.Length)
			_currentCoinBundle = 0;

		GameObject bundle = _coinBundles[_currentCoinBundle];
		if(bundle == null)
		{
			// random
			Debug.Log("_currentCoinBundle : " + _currentCoinBundle);
			//Create Coin
			for(int i = 0; i < 15; i++)
				CreateCoin(stage);

		} else {
			GameObject coin = Instantiate(bundle) as GameObject;
			
			coin.transform.parent = stage.transform;
			
			Vector3 pos = stage.transform.position;
			pos.z = -0.5f;
			if(menual == false) {
				pos.x += Random.Range(-3.5f, 3.5f);
				pos.y += Random.Range(1.0f, 1.8f);
			} else {
				pos.x = x;
				pos.y = y;
			}
			
			coin.transform.position = pos;
		}

		_currentCoinBundle++;
	}

	//Mon_p
	void ClearMon(GameObject stage){
		Transform[] allChildren = stage.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			// do whatever with child transform here
			if(child.gameObject.tag == "Mon_p_tag")
			{
				Destroy( child.gameObject );
			}
		}
	}

	void CreateMon(GameObject stage){
		GameObject mon = (Instantiate(Resources.Load<GameObject>("Mon_p")) as GameObject);
		mon.transform.parent = stage.transform;

		Vector3 pos = stage.transform.position;
		pos.z = -1;
		pos.x += Random.Range(-4.0f, 4.0f);

		mon.transform.position = pos;
	}

	//Hurdle
	void ClearHurdle(GameObject stage){
		Transform[] allChildren = stage.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			// do whatever with child transform here
			if(child.gameObject.tag == "Hurdle_tag")
			{
				Destroy( child.gameObject );
			}
		}
	}
	
	void CreateHurdle(GameObject stage){
		GameObject hurdle = (Instantiate(Resources.Load<GameObject>("hurdle")) as GameObject);
		hurdle.transform.parent = stage.transform;
		
		Vector3 pos = stage.transform.position;
		pos.z = -1;
		pos.x += Random.Range(-4.0f, 4.0f);
		pos.y += Random.Range(2.2f, 3.1f);
		
		hurdle.transform.position = pos;

	}
	
	//Score
	void UpdateScore( float speed )
	{
		_score+=speed;
		_txt_score.text = (int)Mathf.Abs(_score) + "m";
	}

	public void UpdateCoin( )
	{
		_coin++;
		_txt_coin.text = _coin + "coin";
	}

	//Life Plus
	public void LifePlus()
	{
		_lifePlus++;
		Debug.Log ("_lifePlus: " + _lifePlus);
//		_corgiCtrl._corgi.STATE = "BUSTER";
//		_corgiCtrl._corgi.GetComponent<Animator>().SetBool("buster", true);
	}

	//Life Minus
	public void LifeMinus()
	{
		_lifePlus--;
		Debug.Log ("_lifeMinus: " + _lifePlus);
		/*
		if(_lifePlus < 0)
		{
			_corgiCtrl._corgi.STATE = "CRASH";
			_corgiCtrl._corgi.GetComponent<Animator>().SetBool("buster", false);
		}else
		{
			if(_corgiCtrl._corgi.STATE == "BUSTER")
				_corgiCtrl._corgi.STATE = "RUN";
			_corgiCtrl._corgi.GetComponent<Animator>().SetBool("buster", false);
		}
		*/
	}

	public void GameOver( )	
	{
		if( _lifePlus < 0 )
		{
			//speed
			_speed = 0.0f;
			_wallCtrl.WallScrollSpeed = 0.0f;

			// JumpPower
	//		_corgiCtrl._JumpPower = 0;
			_corgiCtrl._JumpPower = 5.0f;
			// Jump Animation
	//		_corgiCtrl._corgi.GetComponent<Animator>().SetBool("jump", false);
	//		_corgiCtrl._corgi.STATE = "STAY";

			//
			_corgiCtrl.ChangeBuster(false);

			// Animation
			if(_corgiCtrl._corgi.STATE == "RUN" || _corgiCtrl._corgi.STATE =="JUMP")
			{
				_corgiCtrl._corgi.GetComponent<Animator>().SetBool("crash", true);
				_corgiCtrl._corgi.STATE = "CRASH";
//				Debug.Log(_corgiCtrl._corgi.STATE);
			}

			StartCoroutine("AfterDelay");

			_lifePlus = 0;
		}
	}

	IEnumerator AfterDelay()
	{
		yield return new WaitForSeconds(2.5f);

		//text button
		_gameoverBtn.SetActive(true);
		_playagainBtn.SetActive(true);
		
		//text
		_txt_flew.text = "you flew: " + _txt_score.text;
		_txt_collected_coin.text = "and collected: " + _txt_coin.text;
		
		_txt_flew.gameObject.SetActive(true);
		_txt_collected_coin.gameObject.SetActive(true);
		
		//Black Out
		_blackout.SetActive(true);
		
		// Clear Coin
		ClearCoin(_back);
		ClearCoin(_center);
		ClearCoin(_front);
		// Clear Mon_p
		ClearMon(_back);
		ClearMon(_center);
		ClearMon(_front);
		// Clear Hurdle
		ClearHurdle(_back);
		ClearHurdle(_center);
		ClearHurdle(_front);
		
		//Audio
		audio.clip = _game_over_es;
		audio.Play();

		// Animation
		//if(_corgiCtrl._corgi.STATE == "CRASH")
		{
			_corgiCtrl._corgi.GetComponent<Animator>().SetBool("crash", false);
			_corgiCtrl._corgi.GetComponent<Animator>().SetBool("run", false);
			//_corgiCtrl._corgi.GetComponent<Animator>().SetBool("stay", true);
			//_corgiCtrl._corgi.STATE = "STAY";
		}
	}

	void PlayAgain( )
	{
		Debug.Log("Play Again!!");
		Init();

		// Coin Bundle Start Position
		CreateCoinBundle(_front, true, 8.0f, 1.5f);
	}

	void PlayStart( )
	{
		Debug.Log("Start!!");
		Init();

		// Coin Bundle Start Position
		CreateCoinBundle(_front, true, 8.0f, 1.5f);
	}
}
