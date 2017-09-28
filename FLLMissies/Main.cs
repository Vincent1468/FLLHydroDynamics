
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



			var dialog = new InfoDialog ("Druk op ok om te starten");

			//dialog.Show ();



			IMission[] missions = { new Mission1 () };

			foreach (var mission in missions) {
				CurrentLogger.Logger.info ($"Starting mission {mission.Name}");
				MissionRunner.Run (mission);
			}

			return;
			MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();


		}
	}
}

