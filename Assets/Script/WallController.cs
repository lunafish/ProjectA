using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	Vector2 Offset; // 이미지의 좌표를 조정할 변수 
	public float WallScrollSpeed = 0.001f;

	// Use this for initialization
	void Start () {
		//print (this.renderer.material.mainTextureOffset);
		
	}

	// Update is called once per frame
	void Update () {
		Offset.x += WallScrollSpeed; //증가시킬수치값; 
		this.renderer.material.mainTextureOffset = Offset;//가로,세로좌표 
	}
}
