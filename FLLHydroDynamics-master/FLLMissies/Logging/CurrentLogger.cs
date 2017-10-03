using System;

namespace FLLMissies.Logging
{
	public static class CurrentLogger
	{
		public static bool Debug = true;
		public static ILogger Logger = new BrickLogger();
	}
}

