using System;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace FLLMissies
{
	public static class Constants
	{
		public static readonly MotorPort 	MOTOR_LEFT_PORT = MotorPort.OutA;
		public static readonly MotorPort 	MOTOR_RIGHT_PORT = MotorPort.OutD;

		public static readonly MotorPort 	HEFVORK_MOTOR_PORT = MotorPort.OutB;
		public static readonly SensorPort 	HEFVORK_STOP_SENSOR_PORT = SensorPort.In1;
		public static readonly SensorPort 	GYRO_PORT = SensorPort.In2;
	}
}