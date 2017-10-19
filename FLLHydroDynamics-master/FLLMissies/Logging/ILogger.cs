using System;

namespace FLLMissies.Logging
{
	public interface ILogger
	{
		void info(string message);
		void debug(string message);
		void error(string message);
	}
}

