using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            z = Regex.Replace(z, @"-", "-u");
            var x = Regex.Split(z, @"\+|-|<=|<|=|\s");
            foreach (var n in x) {
                if ("maxz".Equals(n) || "minz".Equals(n)) {
                } else {
                    var k = n.Split('x');
                    if (k.Length != 2) continue;
                    k[0] = Regex.Replace(k[0], @"u", "-");
                    temp.Add(Convert.ToInt32(k[1]), k[0].Equals("") ? 1 : Convert.ToDouble(k[0]));

                }
            }
            symplexTableau.Add(new List<double>());
            foreach (var r in temp) {
                basicVariables.Add(r.Key);
            }
            basicVariables.Sort();
            foreach (var i in basicVariables) {
                symplexTableau[0].Add(temp[i]*-1);
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
            restriction = Regex.Replace(restriction, @"-", "-u");
            var x = Regex.Split(restriction, @"\+|-|<=|<|=|\s");
            temp = new Dictionary<int, double>();
            foreach (var k in x.Select(n => n.Split('x')).Where(k => k.Length == 2)) {
                k[0] = Regex.Replace(k[0], @"u", "-");
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

        public void Solve() {
            //PrintTableau();
            while (!IsOptimalSolution()) {
                PrintTableau();
                int j = FindPivotCollumn();
                int k = FindPivotRow(j);
                Console.WriteLine("Pivot Collumn: {0}", j);
                Console.WriteLine("Pivot row: {0}", k);
                symplexTableau[k].Divide(symplexTableau[k][j]);
                Console.WriteLine("Dividing the Pivot line {0} by the pivot Element {1}", k, symplexTableau[k][j]);
                
                PrintTableau();
                //Console.ReadKey();
                for (int i = 0; i < symplexTableau.Count; i++) {
                    if (i == k) {
                        Console.WriteLine("i == k");
                        continue;
                    }
                    var aux = new List<double>(symplexTableau[k]);
                    aux.Multiply(symplexTableau[i][j]);
                    symplexTableau[i].SubtractList(aux);
                    PrintTableau();
                }
                //PrintTableau();
                //Thread.Sleep(2000);
            }
            
        }

        private int FindPivotCollumn() {
            var k = double.MaxValue;
            var j = 0;
            for (var i = 0; i < symplexTableau[0].Count; i++) {
                if (symplexTableau[0][i] < k) {
                    j = i;
                    k = symplexTableau[0][i];
                }
            }
            return j;
        }

        private int FindPivotRow(int pivotCollumn) {
            var k = double.MaxValue;
            var j = 0;
            var aux = 0.0;
            var lenght = symplexTableau[0].Count - 1;
            for (var i = 1; i < symplexTableau.Count; i++) {
                if (symplexTableau[i][pivotCollumn] == 0) continue;
                aux = symplexTableau[i][lenght]/symplexTableau[i][pivotCollumn];
                if (!(aux < k)) continue;
                if(aux < 0) continue;
                k = aux ;
                j = i;
            }
            return j;
        }

        private bool IsOptimalSolution() {
            return symplexTableau[0].All(d => !(d < 0));
        }

        public void PrintTableau() {
            Console.WriteLine();
            foreach (var list in symplexTableau) {
                foreach (var d in list) {
                    Console.Write("{0,5} ", d);
                }
                Console.WriteLine();
            }
        }

         
    }
}
