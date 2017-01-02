using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ExtractImagesFromXSL
    {
    public partial class SampleTool : Form
        {
        public SampleTool ()
            {
            InitializeComponent();
            }

        private void lblSelectFolderPath_Click (object sender, EventArgs e)
            {

            }

        /// <summary>
        /// Button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultsFolder_Click (object sender, EventArgs e)
            {
            Dictionary<string, HashSet<string>> fileWithImages = new Dictionary<string, HashSet<string>>();
            browseResultsFolderDialog.ShowDialog();
            string path = browseResultsFolderDialog.SelectedPath;

            if ( string.IsNullOrEmpty(path) )
                {
                MessageBox.Show("Please select proper path", "Folder not selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            else
            {
                txtDisplayFolderPath.Text = path;
                fileWithImages = GetImagesWithPath(path);
                CopyImages(fileWithImages, path);
                MessageBox.Show(string.Format("Parent folder path: {0}", path), "Completed successfully", MessageBoxButtons.OK);
                }
            }

        /// <summary>
        /// Copy images to destination folder
        /// </summary>
        /// <param name="FileWithImages"></param>
        /// <param name="path"></param>
        public void CopyImages (Dictionary<string, HashSet<string>> fileWithImages, string path)
            {
            foreach ( string fileName in fileWithImages.Keys )
                {
                string respFolderPath = Path.Combine(path, Path.GetFileNameWithoutExtension(fileName));

                if ( FolderExists(respFolderPath) )
                    PurgeFolder(respFolderPath);
                else
                    Directory.CreateDirectory(respFolderPath);
                HashSet<string> imagePaths = fileWithImages[fileName];
                foreach ( string imgPath in imagePaths )
                    {
                    FileInfo srcImg = new FileInfo(imgPath);
                    if ( File.Exists(srcImg.FullName) )
                        {
                        srcImg.CopyTo(Path.Combine(respFolderPath, srcImg.Name), true);
                        }
                    }
                }
            }

        /// <summary>
        /// Purge folder
        /// </summary>
        /// <param name="respFolderPath"></param>
        public void PurgeFolder (string respFolderPath)
            {
            if ( Directory.EnumerateFileSystemEntries(respFolderPath).Any() )
                {
                Directory.Delete(respFolderPath, true);
                Directory.CreateDirectory(respFolderPath);
                }
            }

        /// <summary>
        /// Folder exists or not
        /// </summary>
        /// <param name="respFolderPath"></param>
        /// <returns></returns>
        public bool FolderExists (string respFolderPath)
            {
            if ( Directory.Exists(respFolderPath) )
                return true;
            else
                return false;
            }

        /// <summary>
        /// Get images with full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string, HashSet<string>> GetImagesWithPath (string path)
            {
            Dictionary<string, HashSet<string>> fileWithImages = new Dictionary<string, HashSet<string>>();
            var refernceImages = new HashSet<string>();

            if ( Directory.Exists(path) )
                {
                DirectoryInfo resultsFolderPath = new DirectoryInfo(path);
                FileInfo[] xslFiles = resultsFolderPath.GetFiles("*.xsl");

                if ( xslFiles.Length == 0 )
                    {
                    MessageBox.Show("Directory does not contains xsl files", "File not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return null;
                    }

                else
                    {
                    foreach ( FileInfo xslFile in xslFiles )
                        {
                        var xsl = XDocument.Load(xslFile.FullName);
                        var nodesWithSrc = xsl.Root.XPathSelectElements("//*[@src]");
                        refernceImages = ExtractImages(nodesWithSrc, path);
                        fileWithImages.Add(xslFile.Name, refernceImages);
                        }
                    }
                return fileWithImages;
                }
            else
                {
                MessageBox.Show("Directory does not exists", "Path Not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
                }
            }

        /// <summary>
        /// Extract images from xsl folder
        /// </summary>
        /// <param name="nodesWithSrc"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public HashSet<string> ExtractImages (IEnumerable<XElement> nodesWithSrc, string rootFolder)
            {
            HashSet<string> imageList = new HashSet<string>();
            foreach ( var node in nodesWithSrc )
                {
                if ( node.Name == "img" )
                    {
                    string value = node.Attribute(XName.Get("src")).Value;
                    string filePath = Path.Combine(rootFolder, value);
                    if ( File.Exists(filePath) )
                        {
                        imageList.Add(filePath);
                        }
                    }
                }
            return imageList;
            }
        }
    }
