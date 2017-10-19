using System;

namespace FLLMissies.Logging
{
	public class DummyLogger : ILogger
	{
		public void info(string message) {
		}

		public void debug(string message) {
		}

		public void error(string message) {
		}
	}
}

