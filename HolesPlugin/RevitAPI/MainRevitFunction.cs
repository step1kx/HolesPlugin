/*==========================================================
                  _______ _______ __  __                
                 |  _____|__   __|  \/  |           
                 | |___     | |  | \  / | 
                 |  ___|    | |  | |\/| |
                 | |_____   | |  | |  | |
                 |_______|  |_|  |_|  |_|

[*] Developer:
[*] Stepan Yuzefovich

===========================================================*/


using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using HolesPlugin.Models;


namespace HolesPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class MainRevitFunction : IExternalCommand
    {
        public static Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument activeUIDocument = uiapp.ActiveUIDocument;
            UIDocument uidoc = activeUIDocument;
            doc = uidoc.Document;

            var familyService = new FamilyService();
            var elementService = new ElementService();
            var coordinatesService = new CoordinatesService();
            var calculationService = new CalculationService();

            var calculator = new HolesMainCalculation(doc, familyService, elementService, calculationService, coordinatesService);
            calculator.CalculateHoleElevations();

            return Result.Succeeded;
        }
    }
}
