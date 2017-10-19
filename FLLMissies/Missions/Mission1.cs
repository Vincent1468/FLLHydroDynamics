using System;
using FLLMissies.Robot;
using System.Threading;
using MonoBrickFirmware.Display.Dialogs;

namespace FLLMissies.Missions
{
	public class Mission1 : IMission
	{
		public string Name { get; } = "Waterpijp rechtzetten";

		public Mission1 ()
		{
		}

		public void Run() {

			var dialog = new QuestionDialog ("Staat de robot op de startpositie?", "Positie", "Ja", "Nee", false);

			while (!dialog.IsPositiveSelected) {
				dialog.Show ();
			}

			RobotControl.Movement.Forward (1000);

			RobotControl.Movement.Left (36);

			RobotControl.Movement.Forward (550);

			RobotControl.Movement.Right (45);

			RobotControl.Movement.Forward (250);

			RobotControl.Movement.Backward (20);

			RobotControl.Movement.Right (45);
	
		}


	}
}

