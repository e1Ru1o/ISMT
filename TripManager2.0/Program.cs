using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TripManager2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    //TODO: Make the neccesary adjustments of OnModelCreating in EfCoreContext. Needs testing on delete entities, i'm worried about how a Workflow is affected when it Responsabilidad is deleted or one of its EstadoViaje.
    //TODO: Make the back-end task.
    //TODO: Make a API for assign Responsabilidades to a user.
    //TODO: Back-end of Pasaporte, Viaje, Pasaporte_Visa, Pais_Visa.
    //TODO: Check what happens in the follow cases: when a user is deleted, his passports are also deleted? I hope so; when a passport is deleted, what happens to its user; when a viaje is deleted, its user leaves it and no is being deleted too? I hope so.
    //TODO: I don't understand the relation between Visa and Pasaporte.
    //TODO: Make the update Visa, calling BuildListOfPais_Visa from the controller.
    //TODO: Check if when a Visa is created, a country related to it, have into its collection a Pais_Visa that contains this Visa.

}
