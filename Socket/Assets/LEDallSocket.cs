﻿using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class LEDallSocket : MonoBehaviour {
	public string all = "0";
	/*public string red = "0";
	public string green = "0";
	public string blue = "0";
	public string yellow = "0";*/
	// Use this for initialization
	void Start () {
		LEDswitch ();
	}
	
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKey (KeyCode.A)) {
			LEDswitch();
			
		//}
	}
	
	void LEDswitch(){
		//IPアドレスとポート番号を指定
		//string型とint型なのが不思議
		//勿論送信先のIPアドレスとポート番号です
		string ipAddress = "172.20.11.68";
		int port = 14649;
		
		//IPアドレスとポート番号を渡してサーバ側へ接続
		TcpClient client = new TcpClient(ipAddress, port);
		
		//表示するのは「Hello! C#」
		//これを送信用にbyte型へ直します
		string str = "SET_ALL " +all+ "," +all+ "," +all+ "," +all+ ",\n";
		byte[] tmp = Encoding.UTF8.GetBytes(str);
		
		//NWのデータを扱うストリームを作成
		NetworkStream stream = client.GetStream();
		
		//送信
		//引数は（データ , データ書き込み開始位置 , 書き込むバイト数）
		//だそうです
		stream.Write(tmp, 0, tmp.Length);
		
		//サーバとの接続を終了
		client.Close();
	}
}
