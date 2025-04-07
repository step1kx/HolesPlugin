using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Interfaces
{
    public interface IFamilyService
    {
        Family GetFamilyByName(Document doc, string familyName);
        List<FamilySymbol> GetFamilySymbols(Document doc, Family family);
    }
}
