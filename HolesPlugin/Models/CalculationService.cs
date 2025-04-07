using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Models
{
    public class CalculationService : ICalculationService
    {
        public double CalculateHalfHeight(double height)
        {
            return height / 2;
        }
    }
}
