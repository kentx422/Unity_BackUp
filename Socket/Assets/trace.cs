using UnityEngine;
using System.Collections;

public class trace : MonoBehaviour {
	GameObject cube;
	public float X = 0.0f; 
	public float Z = 0.0f;
	// Use this for initialization
	void Start () {
		cube = GameObject.Find ("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		cube = GameObject.Find ("Cube");
		float disX = Mathf.Abs (cube.transform.position.x - transform.position.x);
		float disZ = Mathf.Abs (cube.transform.position.z - transform.position.z);
		float dis = Mathf.Sqrt (disX * disX + disZ * disZ);

		if (Input.GetKey (KeyCode.W)) {
						X = 0.0f;
						Z = 0.1f;
				} else if (Input.GetKey (KeyCode.S)) {
						X = 0.0f;
						Z = -0.1f;
				} else if (Input.GetKey (KeyCode.D)) {
						X = 0.1f;
						Z = 0.0f;
				} else if (Input.GetKey (KeyCode.A)) {
						X = -0.1f;
						Z = 0.0f;
				} else {
						X = 0.0f;
						Z = 0.0f;
				}

			if(dis > 3){
				float VdisX = Mathf.Abs (cube.transform.position.x - transform.position.x - X);
				float VdisZ = Mathf.Abs (cube.transform.position.z - transform.position.z - Z);
				float Vdis = Mathf.Sqrt (VdisX * VdisX + VdisZ * VdisZ);
			print (dis+","+Vdis);
				if(dis > Vdis){
					this.transform.Translate(X,0,Z);
				}
			}



	}
}
