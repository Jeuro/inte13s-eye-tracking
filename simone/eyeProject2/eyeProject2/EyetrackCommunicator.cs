using System.Net;
using System;
using System.Net.Sockets;
using System.Text;

namespace eyeProject2 {
    public class EyetrackCommunicator {
        private int port = 4242;
        private string host = "127.0.0.1";
        private Socket socket = null;

        public EyetrackCommunicator() {
            Connect(host, port);
            StartMirametrixStream(socket);
        }

        public bool Connect(string ip, int port) {
            Console.WriteLine("Connecting to localhost socket!");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            if (!socket.Connected) {
                Console.WriteLine("Unable to connect!");
                socket = null;
                return false;
            }
            return true;
        }

        public MirametrixDatum GetData() {
            return ReadPacket(socket);
        }

        public MirametrixDatum ReadPacket(Socket socket) {
            byte[] bytes = new byte[1024];
            int bytecount = socket.Receive(bytes, SocketFlags.None);
            string data = Encoding.UTF8.GetString(bytes);

            if (bytecount == 0) {
                Console.WriteLine("GOT NO DATA");
                return null;
            }

            return new MirametrixDatum(data);
        }

        public static int StartMirametrixStream(Socket server) {
            byte[] msg_pog_fix = Encoding.UTF8.GetBytes("<SET ID=\"ENABLE_SEND_POG_FIX\" STATE=\"1\" />\r\n\"");
            byte[] msg_send_data = Encoding.UTF8.GetBytes("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" />\r\n\"");
            byte[] bytes = new byte[1024];

            try {
                // ask server to send pog fix data
                int byteCount = server.Send(msg_pog_fix, SocketFlags.None);
                byteCount = server.Receive(bytes, SocketFlags.None);

                if (byteCount > 0) {
                    bytes = new byte[1024];
                }

                // then send data
                int byteCount2 = server.Send(msg_send_data, SocketFlags.None);
                byteCount = server.Receive(bytes, SocketFlags.None);
            } catch (SocketException e) {
                Console.WriteLine("Error code: no formatting for you");
                return (e.ErrorCode);
            }
            return 0;
        }
    }
}
