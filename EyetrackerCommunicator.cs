using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

public class EyetrackCommunicator : MonoBehaviour {
	
	string data;
	
	int port = 4242;
	string host = "127.0.0.1";
	Socket socket;
	
	// Use this for initialization
	void Start () {
		Connect (host, port);
		StartMirametrixStream(socket);
	}
	
	// Update is called once per frame
	void Update () {
		data = ReadPacket(socket);
		//Debug.Log(data);
		PrintXY(data);
	}
	
	public bool Connect(string ip, int port) {
		Debug.Log("Connecting to localhost socket!");
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));	
		
		if (!socket.Connected) {
			Debug.Log ("Unable to connect!");
			socket = null;
			return false;
		}
		
		return true;
	}
	
	public string ReadPacket(Socket socket) {
		byte[] bytes = new byte[256];
		
		int bytecount = socket.Receive(bytes, SocketFlags.None);
		string data = Encoding.UTF8.GetString(bytes);
		Debug.Log(data);
		
		if (bytecount == 0) {
			Debug.Log("GOT NO DATA");
			data = "";
		}
		return data;
	}
	
	public static int StartMirametrixStream(Socket server) {
		byte[] msg_pog_fix = Encoding.UTF8.GetBytes("<SET ID=\"ENABLE_SEND_POG_FIX\" STATE=\"1\" />\r\n\"");
    	byte[] msg_send_data = Encoding.UTF8.GetBytes("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" />\r\n\"");
    	byte[] bytes = new byte[256];
    	
		try {
        	// ask server to send pog fix data
        	int byteCount = server.Send(msg_pog_fix, SocketFlags.None);

        	// print reply from the server
        	byteCount = server.Receive(bytes, SocketFlags.None);
        	if (byteCount > 0) {
            	Debug.Log(Encoding.UTF8.GetString(bytes));
				bytes = new byte[256];
			}
			
			// then send data
			int byteCount2 = server.Send(msg_send_data, SocketFlags.None);

        	// Get reply from the server.
        	byteCount = server.Receive(bytes, SocketFlags.None);
        	if (byteCount > 0) {
            	Debug.Log(Encoding.UTF8.GetString(bytes));
			}
		}
    	catch (SocketException e) {
        	Debug.Log("Error code: no formatting for you");
        	return (e.ErrorCode);
    	}
    	return 0;
	}
	
	
	// DOESN'T WORK!
	private static void PrintXY(string inputString) {
   		Match m;
		//string reString = "<REC FPOGX=\"([-0-9\\.]+)\" FPOGY=\"([-0-9\\.]+)\" FPOGS=\"[0-9\\.]+\" FPOGD=\"[0-9\\.]+\" FPOGID=\"([0-9]+)\" FPOGV=\"([01])\"/>";
   		//string dataPattern = "FPOGX=\"(\\S+.\\S+)\"\\s*FPOGY=\"(\\S+.\\S+)\"";
		string reString = "(\\bFPOG\\S+\\b)";
		
   		m = Regex.Match(inputString, reString);
   		
		Debug.Log(m.Groups.Count);
		
		//Debug.Log("FPOX: " + m.Groups[0]);
      	Debug.Log(m.Groups[0]);
	}
	// <REC FPOGX="0.000" FPOGY="0.000" FPOGS="39713.961" FPOGD="0.115" FPOGID="12443" FPOGV="0" />
}
