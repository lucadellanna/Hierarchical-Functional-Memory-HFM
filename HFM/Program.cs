using System;
using System.Collections.Generic;

namespace HFM
{
	class Program
	{
		static void Main()
		{
			var stimuli = GetStimuli();
			var Brain = new Brain(stimuli);
			for (int i = 0; i < stimuli.Count; i++)
			{
				Brain.ProcessNextStimulus(stimuli[i]);
			}
		}

		private static List<Stimulus> GetStimuli()
		{
			// Code here gets some sample inputs representing "cup" objects found in different rooms.
			// Future code is expected to process stimuli observed by sensors or passed via data streams.
			// Here, stimuli are sets of sensory inputs.
			// Sensory inputs are a label representing a feature and a number representing its intensity or degree of truth
			var inputs = new List<Stimulus>
			{
				new Stimulus("kitchen",
							 5,
							 new SensoryInput("isRoom", 1),
							 new SensoryInput("isEatingSpace", 2)
							),
				new Stimulus("livingRoom",
							 5,
							 new SensoryInput("isRoom", 1),
							 new SensoryInput("isEatingSpace", 1)
							),
				new Stimulus("bathroom",
							 5,
							 new SensoryInput("isRoom", 1),
							 new SensoryInput("isEatingSpace", 0)
							),
				new Stimulus("empty",
							5,
							 new SensoryInput("isEmpty",1)
							),
				new Stimulus("teacup",
							 1,
							 new SensoryInput("hardness", 5),
							 new SensoryInput("opacity", 1),
							 new SensoryInput("concave", 1),
							 new SensoryInput("kitchen"),
							 new SensoryInput("empty")
							)
			};
			return inputs;
		}
	}
}
