
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
			var vehicle = new VehiclePrecise ();

			vehicle.Forward (40, 4000);

			vehicle.Left (180);

			vehicle.Forward (40, 4000);


			var dialog = new InfoDialog ("Druk op ok om te starten");
			dialog.Show ();

			IMission[] missions = { new Mission1Failed () };

			foreach (var mission in missions) {
				CurrentLogger.Logger.info ($"Starting mission {mission.Name}");
				MissionRunner.Run (mission);
			}

				
			MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();


		}
	}
}

