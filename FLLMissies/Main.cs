
using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using System.Threading;
using FLLMissies;
using FLLMissies.Missions;
using FLLMissies.Robot;
using FLLMissies.Logging;

namespace MonoBrickHelloWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Motor motorA = new Motor (MotorPort.OutA);
			Motor motorD = new Motor (MotorPort.OutD);
			WaitHandle motorWaitHandle;
			motorA.Off();
			motorD.Off();

			//Power control
			LcdConsole.WriteLine("Set power to 50");
			motorA.SetPower(50);
			Thread.Sleep(3000);
			LcdConsole.WriteLine("Break");
			motorA.Brake();

			//Speed control
			LcdConsole.WriteLine("Set speed to 50");
			motorA.SetSpeed(50);
			Thread.Sleep(1000);
			LcdConsole.WriteLine("Speed: " + motorA.GetSpeed());
			Thread.Sleep(2000);
			LcdConsole.WriteLine("Break");
			motorA.Brake();


			//Position control of single motor
			Thread.Sleep(3000);
			motorA.ResetTacho();
			LcdConsole.WriteLine("Moving motor A to 2000 ");
			motorWaitHandle =  motorA.SpeedProfile(50, 200, 1600, 200,true);
			//you could do something else here
			LcdConsole.WriteLine("Waiting for motor A to stop");
			motorWaitHandle.WaitOne();
			LcdConsole.WriteLine("Done moving motor");
			LcdConsole.WriteLine("Position A: " + motorA.GetTachoCount());

			//Individual position control of both motors
			Thread.Sleep(3000);
			motorA.ResetTacho();
			motorD.ResetTacho();
			LcdConsole.WriteLine("Moving motors A to 2000");
			LcdConsole.WriteLine("Moving motor B to 1000");
			WaitHandle[] handles = new WaitHandle[2];
			handles[0] =  motorA.SpeedProfile(50, 200, 1600, 200,true);
			handles[1] = motorD.SpeedProfile(50,200,600,200,true);
			//you could do something else here
			LcdConsole.WriteLine("Waiting for both motors to stop");
			WaitHandle.WaitAll(handles);
			LcdConsole.WriteLine("Done moving both motors");
			LcdConsole.WriteLine("Position A: " + motorA.GetTachoCount());
			LcdConsole.WriteLine("Position D: " + motorD.GetTachoCount());
			motorA.Off();
			motorD.Off();

			//Motor synchronisation position control 
			Thread.Sleep(3000);
			motorA.ResetTacho();
			motorD.ResetTacho();
			MotorSync sync = new MotorSync(MotorPort.OutA, MotorPort.OutD);
			LcdConsole.WriteLine("Sync motors to move 3000 steps forward");
			motorWaitHandle = sync.StepSync(40,0, 3000, true);
			//you could do something else here
			LcdConsole.WriteLine("Waiting for sync to stop");
			motorWaitHandle.WaitOne();
			LcdConsole.WriteLine("Done sync moving both motors");
			LcdConsole.WriteLine("Position A: " + motorA.GetTachoCount());
			LcdConsole.WriteLine("Position D: " + motorD.GetTachoCount());
			sync.Off();

			return;
			var dialog1 = new InfoDialog ("Start");

			dialog1.Show ();
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




			IMission[] missions = { new Mission1 () };

			foreach (var mission in missions) {
				CurrentLogger.Logger.info ($"Starting mission {mission.Name}");
				MissionRunner.Run (mission);
			}

			return;
			MissionDevelopment dev = new MissionDevelopment ();
			dev.Start ();


		}
	}
}

