using UnityEngine;
using System.Collections;

public class material : MonoBehaviour {
	int i;
	int num = 10;
	// Use this for initialization
	void Start () {
		i = num;
	}
	float height = 0.0f;
	// Update is called once per frame
	void Update () {

		transform.position += new Vector3(0,Mathf.Sin(height*Time.deltaTime)*0.05f,0);
		height += 2.0f;
		print ("color : "+ this.renderer.material.color);
		/*if(i < num*2){
			transform.position += new Vector3(0,height * Time.deltaTime,0);
		}
		else if(i < num * 4){
			transform.position -= new Vector3(0,height * Time.deltaTime,0);
		}
		else {
			i = 0;
		}
		i++;*/
	}
}
