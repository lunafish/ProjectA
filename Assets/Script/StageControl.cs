﻿using UnityEngine;
using System.Collections;

public class StageControl : MonoBehaviour {

	public GameObject _back;
	public GameObject _center;
	public GameObject _front;

	public float _speed = -0.1f;
	private float _sum = 0.0f;

	public WallController _wallCtrl;

	//text
	public tk2dTextMesh _txt_score;
	private float _score;

	public tk2dTextMesh _txt_coin;
	private int _coin;

	public tk2dTextMesh _txt_flew;
	public tk2dTextMesh _txt_collected_coin;

	// tk2d Button
	public GameObject _gameover;  //text mesh : game over 
	public GameObject _playagain;
	public GameObject _start;

	//Audio
	public AudioClip _game_run_es;
	public AudioClip _game_over_es;

	// Use this for initialization
	void Start () {

		_speed = 0.0f;
		_wallCtrl.WallScrollSpeed = 0.0f;

		//Audio
		audio.clip = _game_run_es;
		audio.Play();

	}

	void Init()
	{
		_speed = -0.1f;
		_wallCtrl.WallScrollSpeed = 0.001f;

		_score = 0;
		_txt_score.text = _score + "m";
		
		_coin = 0;
		_txt_coin.text = _coin + "coin";

		_gameover.SetActive(false);
		_playagain.SetActive(false);
		_start.SetActive(false);

		_txt_flew.gameObject.SetActive(false);
		_txt_collected_coin.gameObject.SetActive(false);

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
			//Monster Create
			for(int i = 0; i < 3; i++)
				CreateMon(tmp);

			//Clear Coin
			ClearCoin(tmp);
			//Coin Create
			for(int i = 0; i < 10; i++)
				CreateCoin(tmp);
		}
		UpdateScore(_speed);
	}

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
		pos.x += Random.Range(-4.0f, 4.0f);
		pos.y += Random.Range(-1.0f, 1.0f);
		
		coin.transform.position = pos;
	}

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

	public void GameOver( )
	{
		//speed
		_speed = 0.0f;
		_wallCtrl.WallScrollSpeed = 0.0f;

		//text button
		_gameover.SetActive(true);
		_playagain.SetActive(true);

		//text
		_txt_flew.text = _txt_score.text;
		_txt_collected_coin.text = _txt_coin.text;

		_txt_flew.gameObject.SetActive(true);
		_txt_collected_coin.gameObject.SetActive(true);

		// Clear Coin
		ClearCoin(_back);
		ClearCoin(_center);
		ClearCoin(_front);
		// Clear Mon_p
		ClearMon(_back);
		ClearMon(_center);
		ClearMon(_front);

		//Audio
		audio.clip = _game_over_es;
		audio.Play();
	}

	void PlayAgain( )
	{
		Debug.Log("Play Again!!");
		/*
		_gameover.SetActive(false);
		_playagain.SetActive(false);

		_speed = -0.1f;

		_score = 0;
		_txt_score.text = _score + "M";
		_coin = 0;
		_txt_coin.text = _coin + "COIN";
		*/
		Init();
	}

	void PlayStart( )
	{
		Debug.Log("Start!!");
		Init();
	}
}
