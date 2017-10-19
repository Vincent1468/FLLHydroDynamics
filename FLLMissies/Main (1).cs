
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
			RobotControl.Movement.Forward ();
			RobotControl.Movement.Left ();
			RobotControl.Movement.Forward ();		

			Thread.Sleep (2000);

			RobotControl.Movement.Backward ();
			RobotControl.Movement.Right ();
			RobotControl.Movement.Backward ();	

			return;

			IMission[] missions = { new TestMission () };

			foreach (var mission in missions) {
				//CurrentLogger.Logger.info ($"Starting mission {mission.Name}");
				MissionRunner.Run (mission);
			}

			MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();


		}
	}
}

