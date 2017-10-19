using System;
using MonoBrickFirmware.Display;

namespace FLLMissies.Logging
{
	public class BrickLogger : ILogger
	{
		public void info(string message) {
			LcdConsole.WriteLine ("[INFO] " + message);
		}

		public void debug(string message) {
			if (CurrentLogger.Debug)
				LcdConsole.WriteLine ("[DEBUG] " + message);
		}

		public void error(string message) {
			LcdConsole.WriteLine ("[ERROR] " + message);
		}
	}
}

