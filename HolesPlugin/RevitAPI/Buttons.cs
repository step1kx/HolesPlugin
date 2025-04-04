using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using Autodesk.Revit.Exceptions;

namespace HolesPlugin
{
    internal class Buttons : IExternalApplication
    {
        string assemblyLocation = Assembly.GetExecutingAssembly().Location;
        string tabName = "ETM";


        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel;
            try
            {
                panel = application.GetRibbonPanels(tabName).First();
            }
            catch (Exception)
            {
                application.CreateRibbonTab(tabName);
            }

            panel = application.CreateRibbonPanel(tabName, "Отверстия");

            panel.AddItem(new PushButtonData(nameof(MainRevitFunction), "Отметка низа\nотносительно 0.000", assemblyLocation, typeof(MainRevitFunction).FullName)
            {
                LargeImage = GetBitmapImage(Properties.Resources.ETMHolesPluginLogo, 32, 32),
                LongDescription = "Плагин для расположения отверстии относительно отметки 0.000"
            });

            return Result.Succeeded;
        }

        BitmapImage GetBitmapImage(Bitmap bitmap, int width, int height)
        {
            // Создание нового битмапа с заданными размерами
            var resizedBitmap = new Bitmap(bitmap, new Size(width, height));

            using (var memory = new MemoryStream())
            {
                resizedBitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
