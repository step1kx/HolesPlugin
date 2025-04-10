using Autodesk.Revit.DB;
using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HolesPlugin.Models
{
    public class CoordinatesService : ICoordinatesService
    {
        public double GetBasePointZ(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .WhereElementIsNotElementType();

            BasePoint basePoint = collector.FirstOrDefault(e => (e as BasePoint).IsShared == false) as BasePoint;
            if (basePoint != null && basePoint.Position != null)
            {
                double zValue = basePoint.Position.Z;
                double roundedZ = Round(zValue, 0); // Округляем до 0 знаков, как в Dynamo
                MessageBox.Show($"Z-координата базовой точки = {roundedZ}");
                return roundedZ;
            }
            MessageBox.Show("Базовая точка не найдена.");
            return 0.0; // Возвращаем 0, если точка не найдена
        }

        public double GetSurveyPointZ(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .WhereElementIsNotElementType();

            BasePoint surveyPoint = collector.FirstOrDefault() as BasePoint;
            if (surveyPoint != null && surveyPoint.Position != null)
            {
                double zValue = surveyPoint.Position.Z;
                double roundedZ = Round(zValue, 0); // Округляем до 0 знаков
                return roundedZ;
            }
            return 0.0; // Возвращаем 0, если точка не найдена
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
