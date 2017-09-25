using System;
using MonoBrickFirmware.Movement;
using System.Threading;
using MonoBrickFirmware.Sensors;

namespace FLLMissies.Missions
{
	public class Mission1Failed : IMission
	{
		public string Name { get; } = "Missie 1";

		private Vehicle _vehicle;
		private EV3GyroSensor _gyro;

		public Mission1Failed() {
			_vehicle = new Vehicle (Constants.MOTOR_LEFT_PORT, Constants.MOTOR_RIGHT_PORT);
			_gyro = new EV3GyroSensor (Constants.GYRO_PORT);
		}

		public void Step1() {
			_vehicle.TurnRightForward(40, 7);
			Thread.Sleep(2800);
			_vehicle.Brake();
			Thread.Sleep(200);
		}

		public void Step2() {
			_vehicle.TurnLeftForward(10, 30);
			Thread.Sleep(2500);
			_vehicle.Brake();
			Thread.Sleep(200);
		}

		public void Step3() {
			//_vehicle.SpinLeft (25, 200, true);
			//Thread.Sleep (1500);
			//_vehicle.Brake ();
			_vehicle.SpinLeft(20);

			int gyroStart = _gyro.Read ();

			while ((gyroStart - _gyro.Read ()) < 90 || (gyroStart - _gyro.Read ()) > 90)  {
				Thread.Sleep (1);
			}

			_vehicle.Brake ();
		}

		public void Step4() {
			_vehicle.Forward (10);
			Thread.Sleep (800);
			_vehicle.Brake ();
		}

	}
}

