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
using System;


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

            try
            {
                var familyService = new FamilyService();
                var elementService = new ElementService();
                var coordinatesService = new CoordinatesService();
                var calculationService = new CalculationService();

                var calculator = new HolesMainCalculation(doc, familyService, elementService, calculationService, coordinatesService);

                using (Transaction tx = new Transaction(doc, "Calculate Hole Elevations"))
                {
                    tx.Start(); 
                    calculator.CalculateHoleElevations(); 
                    tx.Commit(); 
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
