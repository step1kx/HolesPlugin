using Autodesk.Revit.DB;
using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Models
{
    internal class ElementService : IElementService
    {
        public List<Element> GetElementsOfFamilySymbols(Document doc, FamilySymbol familySymbol) // Изменили FamilyType на FamilySymbol
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .WhereElementIsNotElementType();

            List<Element> elements = new List<Element>();
            foreach (FamilyInstance fi in collector)
            {
                if (fi.Symbol.Id == familySymbol.Id) 
                    elements.Add(fi);
            }
            return elements;
        }

        public List<Element> GetElementsOfCategory(Document doc, Category category)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategoryId(category.Id);

            return collector.ToList();
        }

        public object GetParameterValue(Element element, string parameterName)
        {
            Parameter param = element.LookupParameter(parameterName);
            if (param != null)
            {
                if (param.StorageType == StorageType.Double)
                {
                    double internalValue = param.AsDouble(); 
                    return internalValue * 0.3048; 
                }
                else if (param.StorageType == StorageType.String)
                    return param.AsString();
                else if (param.StorageType == StorageType.Integer)
                    return param.AsInteger();
            }
            return null;
        }

        public void SetParameterValue(Element element, string parameterName, double valueInMeters)
        {
            Parameter param = element.LookupParameter(parameterName);
            if (param != null && !param.IsReadOnly)
            {
                if (param.StorageType == StorageType.Double)
                {
                    double valueInFeet = valueInMeters * 3.28084; 
                    param.Set(valueInFeet);
                }
                else
                {
                    throw new ArgumentException($"Параметр '{parameterName}' не поддерживает значения типа \"double\".");
                }
            }
        }

        public XYZ GetLocation(Element element) 
        {
            Location loc = element.Location;
            if (loc is LocationPoint)
            {
                return (loc as LocationPoint).Point; 
            }
            return null; 
        }
    }
}
