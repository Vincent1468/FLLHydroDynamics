
using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using System.Threading;
using FLLMissies;
using FLLMissies.Missions;
using FLLMissies.Robot;
using FLLMissies.Logging;

namespace MonoBrickHelloWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			var vp = new VehiclePrecise ();

			vp.Forward (50, 2000);

			return;
			MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();

			return;

			IMission[] missions = { new Mission1 () };

			foreach (var mission in missions) {
				CurrentLogger.Logger.info ($"Starting mission {mission.Name}");
				MissionRunner.Run (mission);
			}



		}
	}
}

