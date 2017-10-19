using System;
using FLLMissies.Robot;
using MonoBrickFirmware.Display.Dialogs;

namespace FLLMissies.Missions
{
	public class Mission4 : IMission
	{
		public string Name { get; } = "Waterleiding verwijderen";


		public void Run() {
			var dialog = new QuestionDialog ("Staat de robot op de startpositie?", "Positie", "Ja", "Nee", false);

			while (!dialog.IsPositiveSelected) {
				dialog.Show ();
			}


			RobotControl.Hefvork.Reset ();

			RobotControl.Movement.Forward (2250);

			RobotControl.Movement.Left (105);

			RobotControl.Hefvork.MoveDown (100);

			RobotControl.Movement.Backward (200);

			RobotControl.Hefvork.Reset ();

			RobotControl.Movement.Forward (200);

			RobotControl.Movement.Left (105);

			RobotControl.Movement.Forward (2250);

			RobotControl.Hefvork.MoveDown (100);

			RobotControl.Movement.Forward (100);
			RobotControl.Hefvork.Reset ();

		}

	}
}

