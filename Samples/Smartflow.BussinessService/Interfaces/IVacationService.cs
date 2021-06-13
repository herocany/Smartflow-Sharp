using Smartflow.BussinessService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.BussinessService.Interfaces
{
    public interface IVacationService
    {
        void Persistent(Vacation model);

        Vacation GetVacationByID(string id);
    }
}
