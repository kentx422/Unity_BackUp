using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class test1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
				print ("a");
				LEDSingleSwitch(12);
		}
		if (Input.GetKey (KeyCode.D)) {
			print ("D");
			LEDSingleSwitch(17);
		}
		if (Input.GetKey (KeyCode.S)) {
			print ("S");
			LEDAllSwitch();
		}
	}
	void LEDAllSwitch(){
		string ipAddress = "172.20.11.68";
		int port = 14649;
		TcpClient client = new TcpClient(ipAddress, port);
		string str = "SET_ALL 100,100,100,100,\n";
		byte[] tmp = Encoding.UTF8.GetBytes(str);
		NetworkStream stream = client.GetStream();
		stream.Write(tmp, 0, tmp.Length);
		client.Close();
	}
	void LEDSingleSwitch(int num){
		string ipAddress = "172.20.11.68";
		int port = 14649;
		TcpClient client = new TcpClient(ipAddress, port);
		string str = "SET_SINGLE "+num+" 100,0,0,0,\nUPDATE\nUPDATE\nUPDATE\n";
		byte[] tmp = Encoding.UTF8.GetBytes(str);
		NetworkStream stream = client.GetStream();
		stream.Write(tmp, 0, tmp.Length);
		client.Close();
	}
}
