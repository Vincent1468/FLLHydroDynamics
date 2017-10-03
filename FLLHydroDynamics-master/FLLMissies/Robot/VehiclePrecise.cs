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

			_leftMotor.ResetTacho ();
			Thread.Sleep (5000);
			_leftMotor.SetSpeed (10);

				Thread.Sleep (10);
			}

			_leftMotor.Off ();
			Thread.Sleep (10000);
			return;

			//Motor synchronisation position control 
			Thread.Sleep(3000);
			_leftMotor.ResetTacho();
			_rightMotor.ResetTacho();
			MotorSync sync = new MotorSync(MotorPort.OutA, MotorPort.OutD);
			LcdConsole.WriteLine("Sync motors to move 3000 steps forward");
			motorWaitHandle = sync.StepSync(40,0, 3000, true);
			//you could do something else here
			LcdConsole.WriteLine("Waiting for sync to stop");
			motorWaitHandle.WaitOne();
			LcdConsole.WriteLine("Done sync moving both motors");
			LcdConsole.WriteLine("Position A: " + _leftMotor.GetTachoCount());
			LcdConsole.WriteLine("Position D: " + _rightMotor.GetTachoCount());
			sync.Off();

			/*
			// Test tacho values
		var l = new Motor (Constants.MOTOR_LEFT_PORT);
		var r = new Motor (Constants.MOTOR_RIGHT_PORT);


		// PID controller
		l.ResetTacho();
		r.ResetTacho ();

		using (var pidLeft = new PositionPID (l, 2000, true, 50, 0f, 100f, 0f, 5000))
		using (var pidRight = new PositionPID (r, 2000, true, 50, 0f, 100f, 0f, 5000)) {
			var waitR = pidRight.Run ();
			var waitL = pidLeft.Run ();

			waitR.WaitOne ();
			waitL.WaitOne ();
		}

		var dialog = new InfoDialog ("Stop de motoren");

		dialog.Show ();

		l.Off ();
		r.Off ();

		return;*/

		// VehicleTacho test

		var vt = new VehicleTacho ();

		vt.Forward (20, 10000);
		vt.Backward (100, 1000);

		return;

		private const sbyte CORRECTION_SPEED_INCREASE = 2;

		public VehiclePrecise ()
		{
			_leftMotor = new Motor(Constants.MOTOR_LEFT_PORT);
			_rightMotor = new Motor(Constants.MOTOR_RIGHT_PORT);
			_gyroSensor = new EV3GyroSensor (Constants.GYRO_PORT, GyroMode.Angle);
		}

		public void fwTach(sbyte speed, int maxTachoCount) {
			_leftMotor.ResetTacho ();
			_rightMotor.ResetTacho ();

			_leftMotor.ResetTacho ();
			_rightMotor.ResetTacho ();

			_leftMotor.SetSpeed (speed);
			_rightMotor.SetSpeed (speed);

			while (_leftMotor.GetTachoCount () < maxTachoCount && _rightMotor.GetTachoCount () < maxTachoCount) {
				// Running
			}

			_leftMotor.Off ();
			_rightMotor.Off ();	
		}


		public void Forward(sbyte speed, int ms) {

			CurrentLogger.Logger.debug ($"fwd: s:{speed}");

			_leftMotor.SetSpeed (speed);
			_rightMotor.SetSpeed (speed);

			_leftMotor.Off ();
			_rightMotor.Off ();
		}

		public void Backward(sbyte speed, int ms) {

			speed = convertSpeedToNegative (speed);

			Forward (speed, ms); // Same as forward, only with a negative speed
		}

		public void Left(int degrees, sbyte speed = 10) {
			_gyroSensor.Reset ();

			sbyte negativeSpeed = convertSpeedToNegative (speed);

			int startDegrees = getCurrentDegrees ();

			CurrentLogger.Logger.debug ($"left: d:{degrees} sd:{startDegrees}");

			_rightMotor.SetSpeed (negativeSpeed);
			_leftMotor.SetSpeed (speed);

			bool turning = true;
			int differenceDegrees = 0;

			while (turning) {
				differenceDegrees = startDegrees - _gyroSensor.Read ();

				LcdConsole.WriteLine ($"diff: {differenceDegrees}");

				if (differenceDegrees >= 90) {
					turning = false;
				}

			_leftMotor.Off ();
			_rightMotor.Off ();
		}

		public void Right(int degrees, sbyte speed = 10) {
			_gyroSensor.Reset ();

			sbyte negativeSpeed = convertSpeedToNegative (speed);

			int startDegrees = getCurrentDegrees ();

			CurrentLogger.Logger.debug ($"right: d:{degrees} sd:{startDegrees}");

			_rightMotor.SetSpeed (speed);
			_leftMotor.SetSpeed (negativeSpeed);

			bool turning = true;
			int differenceDegrees = 0;

			while (turning) {
				differenceDegrees = startDegrees - _gyroSensor.Read ();

				LcdConsole.WriteLine ($"diff: {differenceDegrees}");

				if (differenceDegrees >= 90) {
					turning = false;
				}

				_leftMotor.Off ();
				_rightMotor.Off ();
			}
		public void Stop() {
			_leftMotor.Off ();
			_rightMotor.Off ();
		}

		private int getCurrentDegrees() {

			return _gyroSensor.Read ();
		}

		private sbyte convertSpeedToNegative(sbyte speed) {
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