using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Xml;

namespace VGBClipRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private String targetFilename = "";
        private String xefFilepathOld = "";
        private String vgbclipFilepathOld = "";
        public MainWindow()
        {
            InitializeComponent();
            updateUI();

        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Drag Enter");
        }

        private void dragLeave(object sender, DragEventArgs e)
        {
           System.Diagnostics.Trace.WriteLine("Drag Leave");
        }

        private void drop(object sender, DragEventArgs e)
        {
           string filepath = (string)((DataObject)e.Data).GetFileDropList()[0];
           string filename = System.IO.Path.GetFileName(filepath);
           Trace.WriteLine("Drop:\t" +filename);
           String[] s = filename.Split('.');
           
           targetFilename = System.IO.Path.GetFileNameWithoutExtension(filepath);
           this.textBox.Text = targetFilename;
            if (s.Last().Equals("xef")){
                Trace.WriteLine("XEF file found!");
                xefFilepathOld = filepath;
                // construct a filepath to the matching vgclip file
                String vgbclipFilenameOld = System.IO.Path.GetFileNameWithoutExtension(xefFilepathOld) + ".vgbclip";
                vgbclipFilepathOld =System.IO.Path.Combine(new String[]{System.IO.Path.GetDirectoryName(filepath), vgbclipFilenameOld});
                // check if vgbclip file exists
                Trace.WriteLine(vgbclipFilepathOld);
                if (System.IO.File.Exists(vgbclipFilepathOld)) {
                    Trace.WriteLine("Found a matching VGBCLIP file");
                }
            }

            else if (s.Last().Equals("vgbclip"))
            {
                vgbclipFilepathOld = filepath;
                Trace.WriteLine("VGBCLIP file found!");

                xefFilepathOld = filepath;
                // construct a filepath to the matching xef file
                String xefFilenameOld = System.IO.Path.GetFileNameWithoutExtension(xefFilepathOld) + ".xef";
                xefFilepathOld = System.IO.Path.Combine(new String[] { System.IO.Path.GetDirectoryName(filepath), xefFilenameOld });
                // check if xef file exists
                Trace.WriteLine(xefFilepathOld);
                if (System.IO.File.Exists(xefFilepathOld)){
                    Trace.WriteLine("Found a matching XEF file");
                }
            }

            else
            {
                Trace.WriteLine("Unsupported format!");
            }


        }

        private void dragOver(object sender, DragEventArgs e)
        {

        }

        private void changeXEFPathInVGBCLIP(String pathVGBCLIP, String xefPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathVGBCLIP);
            var element = doc.GetElementsByTagName("Clip");
            element[0].InnerText = xefPath;
            doc.Save(pathVGBCLIP);
        }

        private void updateUI()
        {
            if (textBox.Text.Equals("")) this.buttonRename.IsEnabled = false;
            else this.buttonRename.IsEnabled = true;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            targetFilename = this.textBox.Text;
            updateUI();
        }

        private void renameButtonClicked(object sender, RoutedEventArgs e)
        {
            String xefFilenameNew = targetFilename+".xef";
            String xefNewFilepath = System.IO.Path.Combine(new String[]{System.IO.Path.GetDirectoryName(xefFilepathOld),xefFilenameNew});
            String vgbclipFilenameNew = targetFilename + ".vgbclip";
            String vgbclipNewFilepath = System.IO.Path.Combine(new String[] { System.IO.Path.GetDirectoryName(vgbclipFilepathOld), vgbclipFilenameNew });
            System.IO.File.Move(vgbclipFilepathOld, vgbclipNewFilepath);
            System.IO.File.Move(xefFilepathOld, xefNewFilepath);

            changeXEFPathInVGBCLIP(vgbclipNewFilepath, targetFilename + ".xef");
        }


    }
}
