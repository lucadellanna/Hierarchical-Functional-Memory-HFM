﻿using System;

namespace HFM
{
	public static class Constants
	{
		// TODO: remove obsolete constants (not all are needed, some are vestiges of previous implementations)

		// SENSORIAL DIMENSIONS
		public const int SDR_OVERLAP = 2;

		// PROXIMAL SYNAPSES
		public const double PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_FORM_IN_RECEPTIVE_FIELD = 0.3;
		public const double PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE = 0.7;

		//BASAL SYNAPSES
		public const double PERCENTAGE_OF_BASAL_SYNAPSES_TO_FORM_IN_RECEPTIVE_FIELD = 0.3;
		public const double PERCENTAGE_OF_BASAL_SYNAPSES_TO_ACTIVATE = 0.7;

		// APICAL SYNAPSES
		public const double PERCENTAGE_OF_APICAL_SYNAPSES_TO_FORM_IN_RECEPTIVE_FIELD = 0.3;
		public const double PERCENTAGE_OF_APICAL_SYNAPSES_TO_ACTIVATE = 0.7;

		// IN GENERAL:
		public const int NUMBER_OF_DISTAL_SYNAPSES_TO_PREDICT = 5;
		public const int COLUMNAR_INHIBITION_DISTANCE = 5;

		// NEURONS
		public const int RECEPTIVE_FIELD_RADIUS = 5;
		public const int NEURONS_PER_MINICOLUMN = 10;

		// READONLY (CALCULATED AT STARTUP)

		public static readonly int NUMBER_OF_PROXIMAL_SYNAPSES =
			(int)Math.Round(PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE *
							 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2), 0);

		public static readonly int NUMBER_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE =
			(int)Math.Round(PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE *
							 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2) *
							PERCENTAGE_OF_PROXIMAL_SYNAPSES_TO_ACTIVATE
							, 0);

		public static readonly int NUMBER_OF_BASAL_SYNAPSES =
			(int)Math.Round(PERCENTAGE_OF_BASAL_SYNAPSES_TO_ACTIVATE *
							 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2), 0);

		public static readonly int NUMBER_OF_BASAL_SYNAPSES_TO_ACTIVATE =
			(int)Math.Round(PERCENTAGE_OF_BASAL_SYNAPSES_TO_ACTIVATE *
							 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2) *
							PERCENTAGE_OF_BASAL_SYNAPSES_TO_ACTIVATE
							, 0);

		public static readonly int NUMBER_OF_APICAL_SYNAPSES =
	(int)Math.Round(PERCENTAGE_OF_APICAL_SYNAPSES_TO_ACTIVATE *
					 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2), 0);

		public static readonly int NUMBER_OF_APICAL_SYNAPSES_TO_ACTIVATE =
			(int)Math.Round(PERCENTAGE_OF_APICAL_SYNAPSES_TO_ACTIVATE *
							 3.14 * Math.Pow(RECEPTIVE_FIELD_RADIUS, 2) *
							PERCENTAGE_OF_APICAL_SYNAPSES_TO_ACTIVATE
							, 0);
	}
}
