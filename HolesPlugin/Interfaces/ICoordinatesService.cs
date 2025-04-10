using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Interfaces
{
    public interface ICoordinatesService
    {
        double GetSurveyPointZ(Document doc);
        double GetBasePointZ(Document doc);
        double Round(double number, int digits);
        double Subtract(double a, double b);
    }
}
