using System;
using System.Collections;
using System.Collections.Generic;

namespace Circuits {

	public class AntennaElm : RailElm {
		
		public AntennaElm( CirSim s) : base(WF_DC, s) {
			
		}

		public double fmphase;

		public override void stamp() {
			sim.stampVoltageSource(0, nodes[0], voltSource);
		}

		public override void doStep() {
			sim.updateVoltageSource(0, nodes[0], voltSource, getVoltage());
		}

		public override double getVoltage() {
			fmphase += 2 * pi * (2200 + Math.Sin(2 * pi * sim.time * 13) * 100)
					* sim.timeStep;
			double fm = 3 * Math.Sin(fmphase);
			return Math.Sin(2 * pi * sim.time * 3000)
					* (1.3 + Math.Sin(2 * pi * sim.time * 12)) * 3
					+ Math.Sin(2 * pi * sim.time * 2710)
					* (1.3 + Math.Sin(2 * pi * sim.time * 13)) * 3
					+ Math.Sin(2 * pi * sim.time * 2433)
					* (1.3 + Math.Sin(2 * pi * sim.time * 14)) * 3 + fm;
		}

	}
}