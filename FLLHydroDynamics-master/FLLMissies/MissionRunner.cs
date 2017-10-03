using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using FLLMissies.Logging;

namespace FLLMissies
{
	public class MissionRunner
	{
		public static void Run(IMission instance)
		{
			CurrentLogger.Logger.info ($"Running mission {instance.Name}");

			foreach (var method in GetStepMethods(instance))
			{
				CurrentLogger.Logger.info ($"{instance.Name} {method.Name}");

				method.Invoke(instance, null);
			}
		}

		private static MethodInfo[] GetStepMethods(IMission instance)
		{
			return instance.GetType().GetMethods().Where(m => m.Name.StartsWith("Step")).OrderBy(m => m.Name).ToArray();
		}
    }
}

