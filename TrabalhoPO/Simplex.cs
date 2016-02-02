using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrabalhoPO {
    public class Simplex {
        private List<List<double>> symplexTableau;

        private List<int> basicVariables;

        private int nonBasicVariables = 0;
        private int nextRow = 1;
        private Dictionary<int, double> temp = new Dictionary<int, double>();
        public Simplex() {
            basicVariables = new List<int>();
            symplexTableau = new List<List<double>>();

        }

        public void setZ(string z) {
            z = Regex.Replace(z, @"\s", "");
            var x = Regex.Split(z, @"\+|-|<=|<|=|\s");
            foreach (var n in x) {
                if ("maxz".Equals(n) || "minz".Equals(n)) {
                } else {
                    var k = n.Split('x');
                    if (k.Length != 2) continue;
                    temp.Add(Convert.ToInt32(k[1]), k[0].Equals("") ? 1 : Convert.ToDouble(k[0]));

                }
            }
            symplexTableau.Add(new List<double>());
            foreach (var r in temp) {
                basicVariables.Add(r.Key);
            }
            basicVariables.Sort();
            foreach (var i in basicVariables) {
                symplexTableau[0].Add(temp[i]);
            }
            symplexTableau[0].Add(0);
        }

        public void AddRestriction(string restriction) {
            if (restriction.Contains("<") || restriction.Contains("<=")) {
                foreach (var list in symplexTableau) {
                    var aux = list[list.Count - 1];
                    list[list.Count - 1] = 0;
                    list.Add(aux);
                }
                nonBasicVariables++;
            }
            restriction = Regex.Replace(restriction, @"\s", "");
            var x = Regex.Split(restriction, @"\+|-|<=|<|=|\s");
            temp = new Dictionary<int, double>();
            foreach (var k in x.Select(n => n.Split('x')).Where(k => k.Length == 2)) {
                temp.Add(Convert.ToInt32(k[1]),k[0].Equals("")?1:Convert.ToDouble(k[0]));
            }
            symplexTableau.Add(new List<double>());
            foreach (var i in basicVariables) {
                if(temp.ContainsKey(i)) symplexTableau[nextRow].Add(temp[i]);
                else symplexTableau[nextRow].Add(0);
            }
            for (int i = 0; i < nonBasicVariables-1; i++) {
                symplexTableau[nextRow].Add(0);
            }
            symplexTableau[nextRow].Add(1);
            symplexTableau[nextRow].Add(Convert.ToDouble(x[x.Length-1]));
            nextRow++;
            
        }

        public void PrintTableau() {
            foreach (var list in symplexTableau) {
                foreach (var d in list) {
                    Console.Write("{0} ", d);
                }
                Console.WriteLine();
            }
        }
    }
}
