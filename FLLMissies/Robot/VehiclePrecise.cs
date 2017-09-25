using System;
using System.Threading;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using FLLMissies.Logging;
using FLLMissies.Robot;

namespace FLLMissies.Robot
{
	public class VehiclePrecise
	{
		private readonly Motor _leftMotor;
		private readonly Motor _rightMotor;
		private readonly EV3GyroSensor _gyroSensor;

		private const sbyte CORRECTION_SPEED_INCREASE = 2;

		public VehiclePrecise ()
		{
			_leftMotor = new Motor(Constants.MOTOR_LEFT_PORT);
			_rightMotor = new Motor(Constants.MOTOR_RIGHT_PORT);
			_gyroSensor = new EV3GyroSensor (Constants.GYRO_PORT, GyroMode.Angle);
		}

		public void Forward(sbyte speed, int ms) {
			int degrees = getCurrentDegrees ();

			CurrentLogger.Logger.debug ($"fwd: s:{speed} d:{degrees}");

			_leftMotor.SetSpeed (speed);
			_rightMotor.SetSpeed (speed);

			for (int i = 0; i < ms; i++) {
				int curDegrees = getCurrentDegrees ();

				if (curDegrees != degrees) {
					// Vehicle turned!
					CurrentLogger.Logger.debug ($"fwd corr: do:{degrees} cd: {curDegrees}");
					if (curDegrees < degrees) { // Left?
						int newSpeed = _rightMotor.GetSpeed () + CORRECTION_SPEED_INCREASE;
						if (newSpeed > sbyte.MaxValue)
							newSpeed = sbyte.MaxValue;
						
						_rightMotor.SetSpeed ((sbyte)newSpeed); // Increase speed
					} else { // Right?
						int newSpeed = _leftMotor.GetSpeed () + CORRECTION_SPEED_INCREASE;
						if (newSpeed > sbyte.MaxValue)
							newSpeed = sbyte.MaxValue;

						_leftMotor.SetSpeed ((sbyte)newSpeed); // Increase speed
					}
				
				} else {
					// Vehicle is on course, check if the speeds have to be reset
					if (_rightMotor.GetSpeed () != speed)
						_rightMotor.SetSpeed (speed);

					if (_leftMotor.GetSpeed () != speed)
						_leftMotor.SetSpeed (speed);
				}

				Thread.Sleep (1);
			}


			_leftMotor.Brake ();
			_rightMotor.Brake ();
		}

		public void Backward(sbyte speed, int ms) {

			speed = convertSpeedToNegative (speed);

			Forward (speed, ms); // Same as forward, only with a negative speed
		}

		public void Left(int degrees, sbyte speed = 100) {
			sbyte negativeSpeed = convertSpeedToNegative (speed);

			int startDegrees = getCurrentDegrees ();

			CurrentLogger.Logger.debug ($"left: d:{degrees} sd:{startDegrees}");

			_leftMotor.SetSpeed (negativeSpeed);
			_rightMotor.SetSpeed (speed);

			bool turning = true;
			int differenceDegrees = 0;

			while (turning) {
				differenceDegrees = startDegrees - getCurrentDegrees ();

				CurrentLogger.Logger.debug ($"diff: {differenceDegrees}");

				if (degrees >= differenceDegrees)
					turning = false;
			}

			_leftMotor.Brake ();
			_rightMotor.Brake ();
		}

		private int getCurrentDegrees() {
			return _gyroSensor.Read ();
		}

		private sbyte convertSpeedToNegative(sbyte speed) {
		
			if (speed > 0) {
				// Bron: https://stackoverflow.com/questions/1348080/convert-a-positive-number-to-negative-in-c-sharp
				int newSpeed = speed * -1;

				if (newSpeed > sbyte.MaxValue) {
					newSpeed = sbyte.MaxValue;
				} else if (newSpeed < sbyte.MinValue) {
					newSpeed = sbyte.MinValue;
				}

				return (sbyte)newSpeed;
			}
		}
	}
}