using System;
using System.Collections.Generic;
using System.Text;

namespace EStore;
    public class Produs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public override string ToString() => $"[{Id}] {Name} ({Category}) - {Price} RON";
}

  