using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class easyLux : MonoBehaviour {
	public string all = "0";
	public int single = 0;
	public int number = 5; 
	
	// Use this for initialization
	void Start () {
		LEDAllSwitch ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			LEDAllSwitch();
			single = 0;
			print ( "A Key pushed" );
		}
		if (Input.GetKey(KeyCode.D)) {
			LEDSingleSwitch();
			print ( "D Key pushed" );
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			single += 50;
			if(single > 200 )single = 0;
			print ( "UP Key pushed : "+ single);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			single -= 50;
			if(single < 0 )single = 200;
			print ( "Down Key pushed : "+ single);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			number +=2;
			if(number == 9)number =11;
			if(number == 19)number =20;
			if(number > 26 )number = 5;
			print ( "Right Key pushed : "+ number);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			number -=2;
			if(number == 9)number =7;
			if(number == 18)number =17;
			if(number < 5 ) number = 26;
			print ( "left Key pushed : "+ number);
		}
		LEDSingleSwitch();

	}
	void LEDAllSwitch(){
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
	void LEDSingleSwitch(){
		//IPアドレスとポート番号を指定
		//string型とint型なのが不思議
		//勿論送信先のIPアドレスとポート番号です
		string ipAddress = "172.20.11.68";
		int port = 14649;
		
		//IPアドレスとポート番号を渡してサーバ側へ接続
		TcpClient client = new TcpClient(ipAddress, port);
		
		//表示するのは「Hello! C#」
		//これを送信用にbyte型へ直します

		string str = "SET_SINGLE " +number+ " " +single+ "," +single+ "," +single+ "," +single+ ",\nUPDATE\nUPDATE\n";
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
