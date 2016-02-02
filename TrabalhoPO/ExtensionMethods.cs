using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPO {
    public static class ExtensionMethods {



        public static void SubtractList(this List<double> l, List<double> k) {
            if (l.Count != k.Count) {
                throw new Exception("The size of the lists is not the same, your asshole");
            }
            /*Console.WriteLine("Subtracting ");
            l.Print();
            Console.WriteLine("and");
            k.Print();*/
            for (var i = 0; i < l.Count; i++) {
                l[i] -= k[i];
            }
            /*Console.WriteLine("The Result is");
            l.Print();*/
            return;
        }

        public static List<double> AddList(this List<double> l, List<Double> k) {
            if (l.Count != k.Count) {
                throw new Exception("The size of the lists is not the same, your asshole");
            }
            for (var i = 0; i < l.Count; i++) {
                l[i] += k[i];
            }
            return l;
        } 

        public static List<double> Multiply(this List<double> l, double value) {
            /*Console.WriteLine("Multiplying ");
            l.Print();
            Console.WriteLine(value);*/
            for (int i = 0; i < l.Count; i++) {
                l[i] *= value;
            }/*
            Console.WriteLine("The result is");
            l.Print();*/
            return l;
        }

        public static void Divide(this List<double> l, double value) {
            /*Console.WriteLine("Dividing ");
            l.Print();
            Console.WriteLine(value);*/
            for (int i = 0; i < l.Count; i++) {
                l[i] /= value;
            }
            /*Console.WriteLine("The result is");
            l.Print();*/
            
        }

        public static void Print(this List<double> l) {
            foreach (var d in l) {
                Console.Write("{0} ", d);
            }
            Console.WriteLine();
        }
    }
}
