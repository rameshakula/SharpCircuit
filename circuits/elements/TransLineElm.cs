using System;
using System.Collections;
using System.Collections.Generic;

namespace Circuits {

	public class TransLineElm : CircuitElement {

		double delay, imped;
		double[] voltageL, voltageR;
		int lenSteps, ptr;

		public TransLineElm(CirSim s) : base(s) {
			delay = 1000 * sim.timeStep;
			imped = 75;
			reset();
		}

		public override int getLeadCount() {
			return 4;
		}

		public override int getInternalNodeCount() {
			return 2;
		}

		public ElementLead[] posts;

		public override void reset() {
			if (sim.timeStep == 0) {
				return;
			}
			lenSteps = (int) (delay / sim.timeStep);
			//System.out.println(lenSteps + " steps");
			if (lenSteps > 100000) {
				voltageL = voltageR = null;
			} else {
				voltageL = new double[lenSteps];
				voltageR = new double[lenSteps];
			}
			ptr = 0;
			base.reset();
		}

//		public override void setPoints() {
//			base.setPoints();
//			int ds = (dy == 0) ? sign(dx) : -sign(dy);
//			Point p3 = interpPoint(point1, point2, 0, -width * ds);
//			Point p4 = interpPoint(point1, point2, 1, -width * ds);
//			int sep = sim.gridSize / 2;
//			Point p5 = interpPoint(point1, point2, 0, -(width / 2 - sep) * ds);
//			Point p6 = interpPoint(point1, point2, 1, -(width / 2 - sep) * ds);
//			Point p7 = interpPoint(point1, point2, 0, -(width / 2 + sep) * ds);
//			Point p8 = interpPoint(point1, point2, 1, -(width / 2 + sep) * ds);
//
//			// we number the posts like this because we want the lower-numbered
//			// points to be on the bottom, so that if some of them are unconnected
//			// (which is often true) then the bottom ones will get automatically
//			// attached to ground.
//			posts = new Point[] { p3, p4, point1, point2 };
//			inner = new Point[] { p7, p8, p5, p6 };
//		}

		/*void draw(Graphics g) {
			setBbox(posts[0], posts[3], 0);
			int segments = (int) (dn / 2);
			int ix0 = ptr - 1 + lenSteps;
			double segf = 1. / segments;
			int i;
			g.setColor(Color.darkGray);
			g.fillRect(inner[2].x, inner[2].y, inner[1].x - inner[2].x + 2,
					inner[1].y - inner[2].y + 2);
			for (i = 0; i != 4; i++) {
				setVoltageColor(g, volts[i]);
				drawThickLine(g, posts[i], inner[i]);
			}
			if (voltageL != null) {
				for (i = 0; i != segments; i++) {
					int ix1 = (ix0 - lenSteps * i / segments) % lenSteps;
					int ix2 = (ix0 - lenSteps * (segments - 1 - i) / segments)
							% lenSteps;
					double v = (voltageL[ix1] + voltageR[ix2]) / 2;
					setVoltageColor(g, v);
					interpPoint(inner[0], inner[1], ps1, i * segf);
					interpPoint(inner[2], inner[3], ps2, i * segf);
					g.drawLine(ps1.x, ps1.y, ps2.x, ps2.y);
					interpPoint(inner[2], inner[3], ps1, (i + 1) * segf);
					drawThickLine(g, ps1, ps2);
				}
			}
			setVoltageColor(g, volts[0]);
			drawThickLine(g, inner[0], inner[1]);
			drawPosts(g);

			curCount1 = updateDotCount(-current1, curCount1);
			curCount2 = updateDotCount(current2, curCount2);
			if (sim.dragElm != this) {
				drawDots(g, posts[0], inner[0], curCount1);
				drawDots(g, posts[2], inner[2], -curCount1);
				drawDots(g, posts[1], inner[1], -curCount2);
				drawDots(g, posts[3], inner[3], curCount2);
			}
		}*/

		public int voltSource1, voltSource2;
		public double current1, current2, curCount1, curCount2;

		public override void setVoltageSource(int n, int v) {
			if (n == 0) {
				voltSource1 = v;
			} else {
				voltSource2 = v;
			}
		}

		public override void setCurrent(int v, double c) {
			if (v == voltSource1) {
				current1 = c;
			} else {
				current2 = c;
			}
		}

		public override void stamp() {
			sim.stampVoltageSource(nodes[4], nodes[0], voltSource1);
			sim.stampVoltageSource(nodes[5], nodes[1], voltSource2);
			sim.stampResistor(nodes[2], nodes[4], imped);
			sim.stampResistor(nodes[3], nodes[5], imped);
		}

		public override void startIteration() {
			// calculate voltages, currents sent over wire
			if (voltageL == null) {
				sim.stop("Transmission line delay too large!", this);
				return;
			}
			voltageL[ptr] = volts[2] - volts[0] + volts[2] - volts[4];
			voltageR[ptr] = volts[3] - volts[1] + volts[3] - volts[5];
			// System.out.println(volts[2] + " " + volts[0] + " " +
			// (volts[2]-volts[0]) + " " + (imped*current1) + " " + voltageL[ptr]);
			/*
			 * System.out.println("sending fwd  " + currentL[ptr] + " " + current1);
			 * System.out.println("sending back " + currentR[ptr] + " " + current2);
			 */
			// System.out.println("sending back " + voltageR[ptr]);
			ptr = (ptr + 1) % lenSteps;
		}

		public override void doStep() {
			if (voltageL == null) {
				sim.stop("Transmission line delay too large!", this);
				return;
			}
			sim.updateVoltageSource(nodes[4], nodes[0], voltSource1, -voltageR[ptr]);
			sim.updateVoltageSource(nodes[5], nodes[1], voltSource2, -voltageL[ptr]);
			if (Math.Abs(volts[0]) > 1e-5 || Math.Abs(volts[1]) > 1e-5) {
				sim.stop("Need to ground transmission line!", this);
				return;
			}
		}

		public override ElementLead getLead(int n) {
			return posts[n];
		}

		// double getVoltageDiff() { return volts[0]; }
		public override int getVoltageSourceCount() {
			return 2;
		}

		public override bool hasGroundConnection(int n1) {
			return false;
		}

		public override bool getConnection(int n1, int n2) {
			return false;
			/*
			 * if (comparePair(n1, n2, 0, 1)) return true; if (comparePair(n1, n2,
			 * 2, 3)) return true; return false;
			 */
		}

		public override void getInfo(String[] arr) {
			arr[0] = "transmission line";
			arr[1] = getUnitText(imped, CirSim.ohmString);
			arr[2] = "length = " + getUnitText(2.9979e8 * delay, "m");
			arr[3] = "delay = " + getUnitText(delay, "s");
		}

		/*public override EditInfo getEditInfo(int n) {
			if (n == 0) {
				return new EditInfo("Delay (s)", delay, 0, 0);
			}
			if (n == 1) {
				return new EditInfo("Impedance (ohms)", imped, 0, 0);
			}
			return null;
		}

		public override void setEditValue(int n, EditInfo ei) {
			if (n == 0) {
				delay = ei.value;
				reset();
			}
			if (n == 1) {
				imped = ei.value;
				reset();
			}
		}*/
	}
}