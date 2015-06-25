using UnityEngine;
using System.Collections;

public class angleTest : MonoBehaviour {
	float y = 1.0f;
	float x = 1.0f;
	float deg = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){

			deg = 180.0f * Mathf.Atan2(y,x) / Mathf.PI;
			transform.rotation = Quaternion.Euler(0, deg, 0);
		}
	}
}