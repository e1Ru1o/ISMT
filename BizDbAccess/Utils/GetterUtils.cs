using BizDbAccess.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BizDbAccess.Utils
{
    public class GetterUtils : IGetterUtils
    {
        public Dictionary<string, string> ReposNames { get; }
        public AssemblyName targetAssembly { get; }

        public GetterUtils()
        {
            ReposNames = new Dictionary<string, string>
            {
                { "Ciudad", "CiudadDbAccess" },
                { "EstadosViaje", "EstadosViajeDbAccess" },
                { "Institucion", "InstitucionDbAccess" },
                { "Pais_Visa", "Pais_VisaDbAccess" },
                { "Pais", "PaisDbAccess"},
                { "Pasaporte_Visa", "Pasaporte_VisaDbAccess" },
                { "Pasaporte", "PasaporteDbAccess" },
                { "Responsabilidades", "ResponsabilidadesDbAccess" },
                { "Viaje", "ViajeDbAccess" },
                { "Visa", "VisaDbAccess" },
                { "Workflow", "WorkflowDbAccess" },
                { "Usuario", "UserDbAccess" }
            };

            targetAssembly = new AssemblyName("BizData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

    }
}
