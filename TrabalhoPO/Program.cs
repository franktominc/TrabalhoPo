using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPO {
    class Program {
        static void Main(string[] args) {
           
            Console.Write("How many restrictions: ");
            var restrictions = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the z function: ");
            var input = Console.ReadLine();
            var simplex = new Simplex();
            simplex.setZ(input);
            for (int i = 0; i < restrictions; i++) {
                Console.Write("Restriction number {0}: ", i);
                var restriction = Console.ReadLine();
                simplex.AddRestriction(restriction);
            }

            //simplex.PrintTableau();
            simplex.Solve();
            Console.ReadKey();
           
            /*var A = new List<double>();
            A.Add(1.0);
            A.Add(2.0);
            var B = new List<double>();
            B.Add(3.0);
            B.Add(4.0);
            A.SubtractList(B).Print();
            A.Multiply(3).Print();
            Console.ReadKey();*/
        }
    }
}
