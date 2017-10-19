using System;

using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Movement;
using System.Threading;

namespace FLLMissies.Robot
{
	public class Hefvork
	{
		private readonly Motor _hefMotor;
		private readonly EV3TouchSensor _limitSensor;

		const sbyte SPEED_UP = -100;
		const sbyte SPEED_DOWN = 100;
		const int MS_TIME_DOWN = 2700; // Time in ms untill the arm is down

		public Hefvork() {
			_hefMotor = new Motor (Constants.HEFVORK_MOTOR_PORT);
			_limitSensor = new EV3TouchSensor (Constants.HEFVORK_STOP_SENSOR_PORT);		
		}


		public void Reset() {

			_hefMotor.SetSpeed (SPEED_UP);

			while (_limitSensor.ReadAsString () == "Off") {
				Thread.Sleep (100);
			}

			_hefMotor.Brake ();

		}

		public void MoveUp(int precent) {
			_hefMotor.SetSpeed (SPEED_UP);

			for (int time = 0; time < precentToMilliSecondsOn (precent); time++) {
				Thread.Sleep (1);
				if (_limitSensor.ReadAsString () == "On")
					break;
			}

			_hefMotor.Brake ();
		}
			
		public void MoveDown(int precent) {
			_hefMotor.SetSpeed (SPEED_DOWN);
			Thread.Sleep (precentToMilliSecondsOn(precent));
			_hefMotor.Brake ();
		}

		private int precentToMilliSecondsOn(int precent) {
			if (precent > 100 || precent < 0)
				throw new ArgumentException ("percent must be between 0-100");
			
			return (MS_TIME_DOWN / 100) * precent;
		}
	}
}

