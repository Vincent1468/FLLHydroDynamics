using System;
using MonoBrickFirmware.Movement;
using System.Threading;
using FLLMissies.Robot;

namespace FLLMissies.Missions
{
	public class Mission1 : IMission
	{
		public string Name { get; } = "Missie 1";

		private VehiclePrecise _vehicle;

		public Mission1 ()
		{
			_vehicle = new VehiclePrecise ();

		}

		public void Step1() {
			_vehicle.Forward (50, 45);
		}
	}
}

