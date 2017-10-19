using System;
using FLLMissies;
using FLLMissies.Robot;

namespace FLLMissies.Missions
{
	public class TestMission : IMission	
	{
		public string Name { get; } = "Test mission";

		private Hefvork _hefVork;

		public TestMission() {
			_hefVork = new Hefvork  (MonoBrickFirmware.Movement.MotorPort.OutB, MonoBrickFirmware.Sensors.SensorPort.In1);
		}

		public void Step1() {
			_hefVork.Reset ();
			_hefVork.MoveDown (100);
		}

		public void Step2() {
			_hefVork.MoveUp (50);
			_hefVork.MoveDown (25);

			_hefVork.Reset ();
		}

	}
}

