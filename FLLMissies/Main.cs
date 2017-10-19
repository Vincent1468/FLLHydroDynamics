﻿
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

			/*MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();

			return;*/

			IMission[] missions = { new Mission1 (), new Mission2(), new Mission3(), new Mission4(), new Mission5() };

			foreach (var mission in missions) {
				CurrentLogger.Logger.info (mission.Name);
				mission.Run ();
			}


		}
	}
}

