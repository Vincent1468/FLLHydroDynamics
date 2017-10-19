using System;
using System.Threading;
using FLLMissies.Robot;
using MonoBrickFirmware.Display.Dialogs;

namespace FLLMissies.Missions
{
	public class Mission3 : IMission
	{
		public string Name { get; } = "Vat omdraaien";


		public void Run() {

			var dialog = new QuestionDialog ("Staat de robot op de startpositie?", "Positie", "Ja", "Nee", false);

			while (!dialog.IsPositiveSelected) {
				dialog.Show ();
			}

			RobotControl.Hefvork.Reset ();
			RobotControl.Hefvork.MoveDown (100);

			RobotControl.Movement.Backward (750);

			RobotControl.Movement.Right (90);

			RobotControl.Movement.Backward (610);


			RobotControl.Movement.Forward (610);

			RobotControl.Movement.Left (90);

			RobotControl.Movement.Forward (750);


			RobotControl.Hefvork.Reset ();

		}

		// Shortcut method to sleep 
		private void s() {
			Thread.Sleep (100);
		}

	}
}

