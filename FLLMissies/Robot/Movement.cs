using System;
using System.Threading;
using MonoBrickFirmware.Movement;
using FLLMissies;

namespace FLLMissies.Robot
{
	public class Movement
	{
		// The amount of tacho turns to go one degree
		private const float ONE_DEGREE_TURN = 2.35f;
		private const sbyte MOVE_SPEED = 40;
		private const sbyte TURN_SPEED = 25;
		
		private Motor _leftMotor;
		private Motor _rightMotor;

		public Movement ()
		{
			_leftMotor = new Motor (Constants.MOTOR_LEFT_PORT);
			_rightMotor = new Motor (Constants.MOTOR_RIGHT_PORT);
		}

		public void Forward(uint steps) {		
			Move (MOVE_SPEED, steps);
		}

		public void Backward(uint steps) {
			Move (convertSpeedToNegative(MOVE_SPEED), steps);				
		}

		public void Right(int degrees = 90) {
			sbyte negativeSpeed = convertSpeedToNegative (TURN_SPEED);

			_rightMotor.SetSpeed (negativeSpeed);
			_leftMotor.SetSpeed (TURN_SPEED);

			_rightMotor.ResetTacho ();
			_leftMotor.ResetTacho ();

			bool turning = true;
			int turnCount = Convert.ToInt32(ONE_DEGREE_TURN * degrees);

			while (turning) {
				bool right = _rightMotor.GetTachoCount () <= (turnCount * -1);
				bool left = _leftMotor.GetTachoCount () >= turnCount;

				if (right || left) {
					_leftMotor.Brake ();
					_rightMotor.Brake ();
					turning = false;
				}
			}

			Thread.Sleep (100);


		}

		public void Left(int degrees = 90) {
			sbyte negativeSpeed = convertSpeedToNegative (TURN_SPEED);

			_rightMotor.SetSpeed (TURN_SPEED);
			_leftMotor.SetSpeed (negativeSpeed);

			_rightMotor.ResetTacho ();
			_leftMotor.ResetTacho ();

			bool turning = true;
			int turnCount = Convert.ToInt32(ONE_DEGREE_TURN * degrees);

			while (turning) {
				bool right = _rightMotor.GetTachoCount () >= turnCount;
				bool left = _leftMotor.GetTachoCount () <= (turnCount*-1);

				if (right || left) {
					_leftMotor.Brake ();
					_rightMotor.Brake ();
					turning = false;
				}

			}

			Thread.Sleep (100);

		}

		public void Brake() {
			_leftMotor.Brake ();
			_rightMotor.Brake ();
		}


		private void Move(sbyte speed, uint steps) {
			var motorSync = new MotorSync (Constants.MOTOR_LEFT_PORT, Constants.MOTOR_RIGHT_PORT);

			WaitHandle waitHandle = motorSync.StepSync(speed, 0, steps, true);
			waitHandle.WaitOne ();

			motorSync.Off ();	

			Thread.Sleep (100);
			
		}

		private sbyte convertSpeedToNegative(sbyte speed) {
			return (sbyte)(speed * -1);
		}
	}
}

