using BizData.Entities;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class VisaViewModel : NameOnlyViewModel
    {
        //Be sure that this collection is filled only with the names from the list of paises returned by GetPaisesWithoutVisa
        //this is for the user to pick the desired country to have a visa with, and the instructed above makes sure that no previous
        //relationship is established with the name of the chosen visa and this countries. This is more useful when the visa with name
        //Nombre is edited. Call GetPaisesWithoutVisa from the razor page when you already get the name in case that you don't have it previosly.
        public IEnumerable<string> paisesNames { get; set; }

        public IEnumerable<string> regionesName { get; set; }
    }
}
