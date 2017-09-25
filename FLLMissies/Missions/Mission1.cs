using System;
using MonoBrickFirmware.Movement;
using System.Threading;
using FLLMissies.Robot;

namespace FLLMissies.Missions
{
	public class Mission1 : IMission
	{
		public string Name { get; } = "Missie 1";

		private Vehicle _vehicle;
		private Hefvork _hefVork;

		public Mission1 ()
		{
			_vehicle = new Vehicle (Constants.MOTOR_LEFT_PORT, Constants.MOTOR_RIGHT_PORT);
			_hefVork = new Hefvork (Constants.HEFVORK_MOTOR_PORT, Constants.HEFVORK_STOP_SENSOR_PORT);

		}

		public void Step1() {
			_vehicle.Backward (-50);
			Thread.Sleep (1000);
			_vehicle.Brake ();
		}
	}
}

