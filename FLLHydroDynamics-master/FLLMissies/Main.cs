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
			var dialog = new InfoDialog ("Start");
			dialog.Show ();
			Motor motorA = new Motor (MotorPort.OutA);
			Motor motorD = new Motor (MotorPort.OutD);
			WaitHandle motorWaitHandle;
			motorA.Off();
			motorD.Off();




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

