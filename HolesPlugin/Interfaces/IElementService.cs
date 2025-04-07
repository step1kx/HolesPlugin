using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Interfaces
{
    public interface IElementService
    {
        List<Element> GetElementsOfFamilyType(Document doc, FamilySymbol familySymbol);
        List<Element> GetElementsOfCategory(Document doc, Category category);
        object GetParameterValue(Element element, string parameterName);
        void SetParameterValue(Element element, string parameterName, object value);
        XYZ GetLocation(Element element);
    }
}
