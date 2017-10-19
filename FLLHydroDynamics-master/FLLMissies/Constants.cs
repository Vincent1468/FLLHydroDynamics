using System;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace FLLMissies
{
	public static class Constants
	{
		public static MotorPort MOTOR_LEFT_PORT = MotorPort.OutA;
		public static MotorPort MOTOR_RIGHT_PORT = MotorPort.OutD;

		public static MotorPort HEFVORK_MOTOR_PORT = MotorPort.OutB;
		public static SensorPort HEFVORK_STOP_SENSOR_PORT = SensorPort.In1;
		public static SensorPort GYRO_PORT = SensorPort.In2;

	}
}

