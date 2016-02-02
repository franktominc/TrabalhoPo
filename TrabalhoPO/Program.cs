using System;
using System.Collections.Generic;
using System.Linq;
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
            simplex.PrintTableau();
            //var s = "40x2";
            //var k = s.Split('x');
            //Console.WriteLine(k.Length);
            Console.ReadKey();
        }
    }
}
