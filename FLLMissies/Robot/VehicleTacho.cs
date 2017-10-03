using System;
using FLLMissies.Logging;
using MonoBrickFirmware.Movement;

namespace FLLMissies
{
	public class VehicleTacho
	{
		private const sbyte TURN_SPEED = 50;

		private readonly Motor _leftMotor;
		private readonly Motor _rightMotor;

		private bool _running { get { return _leftMotor.IsRunning () || _rightMotor.IsRunning (); } set { _running = value; }
			}

		public VehicleTacho()
		{
			_leftMotor = new Motor(Constants.MOTOR_LEFT_PORT);
			_rightMotor = new Motor(Constants.MOTOR_RIGHT_PORT);		
		}


		public void Forward(sbyte speed, int turns) {


			_leftMotor.SetSpeed (speed);
			_rightMotor.SetSpeed (speed);

			_rightMotor.ResetTacho ();

			while (_running) {

				if (_rightMotor.GetTachoCount () < turns) {
					
					_leftMotor.Brake ();
					_rightMotor.Brake ();
				}
		
				CurrentLogger.Logger.debug (_rightMotor.GetTachoCount ().ToString());
			}

			Off ();

		}

		public void Backward(sbyte speed, int turns) {
			//Forward (0, turns);
		}

		public void Left(int turns) {
			ResetTachos ();

			_rightMotor.SetSpeed (TURN_SPEED);
			_leftMotor.SetSpeed (TURN_SPEED * -1);

			while (_running) {
				if (_leftMotor.GetTachoCount () < turns)
					_leftMotor.Brake ();

				if (_rightMotor.GetTachoCount () > turns)
					_rightMotor.Brake ();
			}

			Off ();
		
		}


		private void Off() {
			_leftMotor.Off ();
			_rightMotor.Off ();
		}

		private void ResetTachos() {
			_leftMotor.ResetTacho();
			_rightMotor.ResetTacho ();
		}

	}
}

