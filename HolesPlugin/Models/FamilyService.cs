using Autodesk.Revit.DB;
using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolesPlugin.Models
{
    public class FamilyService : IFamilyService
    {
        public Family GetFamilyByName(Document doc, string familyName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(Family));
            foreach (Family family in collector)
            {
                if(family.Name == familyName) return family;
            }
            return null;
        }
        public List<FamilySymbol> GetFamilySymbols(Document doc, Family family) 
        {
            List<FamilySymbol> symbols = new List<FamilySymbol>();
            foreach (ElementId id in family.GetFamilySymbolIds())
            {
                FamilySymbol symbol = doc.GetElement(id) as FamilySymbol;
                if (symbol != null)
                {
                    symbols.Add(symbol);
                }
            }
            return symbols;
        }

        
    }
}
