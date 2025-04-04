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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return Result.Succeeded;
        }
    }
}
