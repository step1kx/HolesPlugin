using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using HolesPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HolesPlugin.Models
{
    public class HolesMainCalculation
    {
        private readonly IFamilyService _familyService;
        private readonly IElementService _elementService;
        private readonly ICalculationService _calculationService;
        private readonly ICoordinatesService _coordinatesService;
        private readonly Autodesk.Revit.DB.Document _doc;

        public HolesMainCalculation(Autodesk.Revit.DB.Document doc,
            IFamilyService familyService,
            IElementService elementService,
            ICalculationService calculationService,
            ICoordinatesService coordinatesService)
        {
            _doc = doc;
            _familyService = familyService;
            _elementService = elementService;
            _calculationService = calculationService;
            _coordinatesService = coordinatesService;

        }

        public void CalculateHoleElevations()
        {
            Autodesk.Revit.DB.Document doc = _doc;
            Family holeFamily = _familyService.GetFamilyByName(_doc, "Отверстие");
            if (holeFamily == null) throw new Exception("Семейство 'Отверстие' не найдено.");

            TaskDialog.Show("Загрузка", "Загружаем семейства");
            List<FamilySymbol> familySymbols = _familyService.GetFamilySymbols(doc, holeFamily);

            foreach (FamilySymbol familyType in familySymbols)
            {
                List<Element> holes = _elementService.GetElementsOfFamilySymbols(_doc, familyType);

                foreach (Element hole in holes)
                {
                    string heightParamName = "Отверстие.Высота";
                    string elevationParamName = "Отверстие.Отметка низа";

                    double height = (double)_elementService.GetParameterValue(hole, heightParamName);
                    double currentElevation = (double)_elementService.GetParameterValue(hole, elevationParamName);

                    double halfHeight = _calculationService.CalculateHalfHeight(height);

                    XYZ surveyPoint = _coordinatesService.GetSurveyPoint(_doc);
                    XYZ basePoint = _coordinatesService.GetBasePoint(_doc);

                    double surveyZ = _coordinatesService.Round(surveyPoint.Z, 2);
                    double baseZ = _coordinatesService.Round(basePoint.Z, 2);
                    double elevationDifference = _coordinatesService.Subtract(surveyZ, baseZ);

                    double newElevation = currentElevation + elevationDifference - halfHeight;
                    _elementService.SetParameterValue(hole, elevationParamName, _coordinatesService.Round(newElevation, 2));

                    TaskDialog.Show("Info", $"Элемент {hole.Id}: Новая отметка низа = {newElevation}");
                }
            }
        }
    }
}
