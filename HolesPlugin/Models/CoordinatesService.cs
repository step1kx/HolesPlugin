using Autodesk.Revit.DB;
using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Models
{
    public class CoordinatesService : ICoordinatesService
    {
        public XYZ GetBasePoint(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .WhereElementIsNotElementType();

            BasePoint basePoint = collector.FirstOrDefault(e => (e as BasePoint).IsShared == false) as BasePoint;
            return basePoint?.Position ?? XYZ.Zero;
        }

        public XYZ GetSurveyPoint(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
               .OfClass(typeof(BasePoint))
               .WhereElementIsNotElementType();

            BasePoint surveyPoint = collector.FirstOrDefault() as BasePoint;
            return surveyPoint?.Position ?? XYZ.Zero;
        }

        public double Round(double number, int digits)
        {
            return Math.Round(number, digits);
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }
    }
}
