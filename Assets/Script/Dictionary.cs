using UnityEngine;
using System.Collections.Generic; // Dictionary 라는 저장공간을 사용할때 필요

public class Dictionary : MonoBehaviour {

	// 어떤것을 저장할때 배열과 같은 방식으로 저장하지만,
	// 배열처럼 인덱스가 아닌 어떤 이름을 사용해서 저장할 수 있다. 
	// <string, string> 이라는 것은 저장공간의 이름도 문자열이고, 데이터도 문자열이라는 것

	//Dictionary<string, string> myContacts = new Dictionary<string, string>();
	//Dictionary<string, int> myContacts = new Dictionary<string, int>();
	Dictionary<string, float> myContacts = new Dictionary<string, float>();

	// Use this for initialization
	void Start () {
		// mayContacts 라는 저장공간에 "France" 라는 공간을 만들고, "Paris" 라는 데이터 저장
		//myContacts ["France"] = "Paris";
		//myContacts ["France"] = 12345;
		myContacts ["France"] = 12345f;
		print ( myContacts["France"]); // 잘 저장되었나 확인해보기

		//myContacts.Add("Korea","Seoul"); // 이런식으로 저장할수도 있음 
		//myContacts.Add("Korea",77777); // 이런식으로 저장할수도 있음 
		myContacts.Add("Korea",77777f);
		print(myContacts ["Korea"]);

		myContacts.Remove ("Korea"); // 삭제
		print( myContacts["Korea"]); // 삭제했기 때문에 여기서 에러발생!!
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
