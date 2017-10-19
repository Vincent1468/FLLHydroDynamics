using System;
using FLLMissies.Robot;
using MonoBrickFirmware.Display.Dialogs;

namespace FLLMissies.Missions
{
	public class Mission5 : IMission
	{
		public string Name {get;} = "Put verplaatsen";

		public void Run() {
			var dialog = new QuestionDialog ("Staat de robot op de startpositie?", "Positie", "Ja", "Nee", false);

			while (!dialog.IsPositiveSelected) {
				dialog.Show ();
			}


			RobotControl.Hefvork.Reset ();
			RobotControl.Hefvork.MoveDown (70);

			RobotControl.Movement.Backward (200);

			RobotControl.Hefvork.Reset ();

			RobotControl.Movement.Backward (3450);

			RobotControl.Movement.Right (60);

			RobotControl.Movement.Backward (300);

			RobotControl.Movement.Right (60);

			RobotControl.Movement.Backward (300);

			RobotControl.Movement.Right (60);

	
			RobotControl.Hefvork.MoveDown (60);


			// go back
			RobotControl.Movement.Forward (150);

			RobotControl.Movement.Left (80);

			RobotControl.Movement.Forward (350);

			RobotControl.Movement.Left (55);

			RobotControl.Movement.Forward (3450);




		}

	}
}

