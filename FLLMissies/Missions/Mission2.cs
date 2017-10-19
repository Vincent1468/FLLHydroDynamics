using System;
using System.Threading;
using FLLMissies.Robot;

namespace FLLMissies.Missions
{
	public class Mission2 : IMission
	{
		public string Name { get; } = "Water laten vallen";

		public void Run() {

			RobotControl.Movement.Backward (50);

			RobotControl.Movement.Right (56);

			RobotControl.Movement.Backward (325);

			RobotControl.Movement.Right (40);

			RobotControl.Movement.Forward (150);

			RobotControl.Movement.Right (20);

			RobotControl.Movement.Forward (500);

			RobotControl.Movement.Right (40);

			RobotControl.Movement.Forward (1200);

		}
	}
}

