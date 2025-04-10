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

            BasePoint basePoint = collector
                .Cast<BasePoint>()
                .FirstOrDefault(bp => bp.IsShared == false); // Ищем внутреннюю базовую точку

            if (basePoint != null && basePoint.Position != null)
            {
                // Проверяем параметр "Генплан - Базовая точка проекта"
                Parameter param = basePoint.LookupParameter("Отм");
                if (param != null)
                {
                    // Если параметр существует, можем использовать его для проверки (например, вывести значение)
                    string paramValue = param.AsString() ?? param.AsValueString() ?? "Не определено";
                    MessageBox.Show($"Параметр 'Отм': {paramValue}");
                }
                else
                {
                    MessageBox.Show("Параметр 'Отм' не найден.");
                }

                double zValue = basePoint.Position.Z; 
                double roundedZ = Round(zValue, 0); 
                MessageBox.Show($"Z-координата базовой точки = {roundedZ} (футы), конвертировано = {roundedZ * 0.3048} м");
                return roundedZ; 
            }

            MessageBox.Show("Базовая точка не найдена.");
            return 0.0; // Значение по умолчанию, если точка не найдена
        }

        public double GetSurveyPointZ(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(BasePoint))
                .WhereElementIsNotElementType();

            BasePoint surveyPoint = collector.FirstOrDefault() as BasePoint;
            if (surveyPoint != null && surveyPoint.Position != null)
            {
                double sValue = surveyPoint.Position.Z;
                double roundedS = Round(sValue, 0);
                MessageBox.Show($"Survey-координата = {roundedS}");
                return roundedS;
            }
            return 0.0; 
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
