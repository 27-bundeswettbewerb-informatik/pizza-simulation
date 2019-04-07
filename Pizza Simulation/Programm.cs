using System;

namespace Pizza_Simulation
{
    static class Programm
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pizza_Simulation_Allgemein game = new Pizza_Simulation_Allgemein())
            {
                game.Run();
            }
        }
    }
}

