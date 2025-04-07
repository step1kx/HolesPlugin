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
                return param.AsDouble(); 
            }
            return null;
        }

        public void SetParameterValue(Element element, string parameterName, object value)
        {
            Parameter param = element.LookupParameter(parameterName);
            if (param != null)
            {
                if (value is double)
                    param.Set((double)value);
                else if (value is string)
                    param.Set((string)value);
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
