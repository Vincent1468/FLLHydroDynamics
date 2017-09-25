using System;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using MonoBrickFirmware.UserInput; 
using MonoBrickFirmware.Movement;
using FLLMissies.Logging;
using FLLMissies.Robot;

namespace FLLMissies
{
	// Bron voor delen van deze code:
	// http://www.monobrick.dk/forums/topic/webserver/#post-5164
	public class MissionDevelopment
	{
		private readonly HttpListener _httpListener;
		private readonly Vehicle _vehicle;
		private readonly Hefvork _hefVork;
		private bool _running = false;

		private string RootPath
		{
			get
			{
				string filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
				return Path.GetDirectoryName(filePath) + "/" ;
			}
		}


		public MissionDevelopment ()
		{
			_httpListener = new HttpListener ();
			_httpListener.Prefixes.Add("http://*:8080/");

			_vehicle = new Vehicle (Constants.MOTOR_LEFT_PORT, Constants.MOTOR_RIGHT_PORT); 
			_hefVork = new Hefvork(Constants.HEFVORK_MOTOR_PORT, Constants.HEFVORK_STOP_SENSOR_PORT);
		}

		public void Start() {
			CurrentLogger.Logger.info ("Starting web server");
			// Add a way to stop the web server
			(new ButtonEvents()).EscapePressed += () => _running = false;

			_running = true;
			_httpListener.Start ();
			while (_running) {
				var context = _httpListener.GetContext();
				Process(context);
			}
		
		}

		private void Process(HttpListenerContext context) {

			string reqPath = context.Request.Url.AbsolutePath;

			CurrentLogger.Logger.debug (reqPath);

			if (ProcessAction (reqPath, context.Request.QueryString)) {
				context.Response.StatusCode = 200;
				context.Response.Close();
				return;			
			}

			reqPath = Path.Combine(RootPath, reqPath.Substring(1));
			if (!File.Exists(reqPath))
			{
				context.Response.StatusCode = 404;
				context.Response.Close();
				return;
			}
			var contentType = GetContentType(Path.GetExtension(reqPath));
			context.Response.Headers[HttpResponseHeader.ContentType] = contentType;
			var content = File.ReadAllBytes(reqPath);
			context.Response.OutputStream.Write(content, 0, content.Length);
			context.Response.Close();

		}

		private bool ProcessAction(string requestPath, NameValueCollection parameters) {
			try {
				switch (requestPath) {
				case "/motor/forward":
					_vehicle.Forward (Convert.ToSByte (parameters.Get ("speed")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;
				case "/motor/backward":
					_vehicle.Backward (Convert.ToSByte (parameters.Get ("speed")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;
				case "/motor/left":
					_vehicle.SpinLeft (Convert.ToSByte (parameters.Get ("speed")), Convert.ToUInt32(parameters.Get ("degrees")), true);
					return true;
				case "/motor/right":
					_vehicle.SpinRight (Convert.ToSByte (parameters.Get ("speed")), Convert.ToUInt32(parameters.Get ("degrees")), true);
					return true;
				case "/motor/brake":
					_vehicle.Brake ();
					return true;
				case "/motor/leftforward":
					_vehicle.TurnLeftForward(Convert.ToSByte (parameters.Get ("speed")), Convert.ToSByte (parameters.Get ("turnPrecent")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;
				case "/motor/leftbackward":
					_vehicle.TurnLeftReverse(Convert.ToSByte (parameters.Get ("speed")), Convert.ToSByte (parameters.Get ("turnPrecent")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;
				case "/motor/rightforward":
					_vehicle.TurnRightForward(Convert.ToSByte (parameters.Get ("speed")), Convert.ToSByte (parameters.Get ("turnPrecent")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;
				case "/motor/rightbackward":
					_vehicle.TurnRightReverse(Convert.ToSByte (parameters.Get ("speed")), Convert.ToSByte (parameters.Get ("turnPrecent")));
					Thread.Sleep (Convert.ToInt32 (parameters.Get ("delay")));
					_vehicle.Brake ();
					return true;

				case "/hefvork/reset":
					_hefVork.Reset();
					return true;
				case "/hefvork/fullyDown":
					_hefVork.MoveDown(Convert.ToInt32 (parameters.Get ("precent")));
					return true;
				case "/hefvork/down":
					_hefVork.MoveDown(Convert.ToInt32 (parameters.Get ("precent")));
					return true;
				case "/hefvork/up":
					_hefVork.MoveUp(Convert.ToInt32 (parameters.Get ("precent")));
					return true;
				}
			} catch (Exception e) {
				MonoBrickFirmware.Display.LcdConsole.WriteLine (e.Message);
			}

			return false;

		}

		private string GetContentType(string ext)
		{
			switch (ext.ToLower())
			{
			case ".js":
				return "text/javascript";
			case ".htm":
			case ".html":
				return "text/html";
			case ".png":
				return "image/png";
			case ".jpg":
				return "image/jpg";
			case ".css":
				return "text/css";

			default:
				return "application/octet-stream";
			}
		}
	}
}

