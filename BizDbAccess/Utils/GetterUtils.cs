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
                { "Viaje", "ViajeDbAccess" },
                { "Usuario", "UserDbAccess" },
                { "Institucion", "InstitucionDbAccess" },
                { "Pais", "PaisDbAccess"},
                { "Pasaporte", "PasaporteDbAccess" },
                { "Visa", "VisaDbAccess" },
                { "Responsabilidades", "ResponsabilidadesDbAccess" },
                { "EstadosViaje", "EstadosViajeDbAccess" },
                { "Workflow", "WorkflowDbAccess" }
            };

            targetAssembly = new AssemblyName("BizData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

    }
}
