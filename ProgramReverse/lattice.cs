using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeEditor {
    public struct lattice {
        public int Number { get; set; }
        public double Value { get; set; }
        public lattice(int number, double value) {
            Number = number;
            Value = value;
        }
        public string getLattice() {
            return "#" + Number;
        }
        public string getString() {
            return "#" + Number + " = " + Math.Round(Value, 2);
        }
    }

    public struct tempLattice {
        public int Number { get; set; }
        public string Value { get; set; }
        public tempLattice(int number, string value) {
            Number = number;
            Value = value;
        }
    }
}
