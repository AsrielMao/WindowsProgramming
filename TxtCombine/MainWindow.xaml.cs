using System;
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

using System.Windows.Forms;
using System.IO;

namespace TxtCombine
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string folder_path;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_path_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folder_path = folderBrowserDialog1.SelectedPath;
                txt_path.Text = folder_path;
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(folder_path))//检查文件目录是否存在
            {
                //搜索给定字符串的文件
                string[] folder_files = Directory.GetFiles(folder_path,
                         txt_SearchKey.Text, SearchOption.AllDirectories);
                lb_SearchResult.Items.Clear();
                int selected_index = 0;
                foreach (string folder_file in folder_files)
                {
                    selected_index = lb_SearchResult.Items.Add(folder_file);
                   // lb_SearchResult.SetSelected(selected_index, true);
                }
            }

        }
    }
}
