using System;

namespace FLLMissies
{
	public interface IMission
	{
		string Name { get; }
			
		void Step1();
	}
}