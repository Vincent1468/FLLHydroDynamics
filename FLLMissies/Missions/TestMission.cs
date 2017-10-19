using System;
using System.Threading;
using FLLMissies;
using FLLMissies.Robot;

namespace FLLMissies.Missions
{
	public class TestMission : IMission	
	{
		public string Name { get; } = "Test mission";

		public void Run() {
			RobotControl.Movement.Left ();	

			Thread.Sleep (2000);

			RobotControl.Movement.Right ();	


		}
			

	}
}

