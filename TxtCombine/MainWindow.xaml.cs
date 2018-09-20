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
using System.Diagnostics;

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
                string searchKey = txt_SearchKey.Text;
                if (txt_SearchKey.Text.Trim().Length == 0)
                {
                    searchKey = ".";
                }
                string[] folder_files = Directory.GetFiles(folder_path,
                         searchKey, SearchOption.AllDirectories);
                lb_SearchResult.Items.Clear();

                foreach (string folder_file in folder_files)
                {
                    lb_SearchResult.Items.Add(folder_file);
                }
            }

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = lb_SearchResult.SelectedItems.Count - 1; i >= 0; i--)
            for (int i = 0; i < lb_SearchResult.SelectedItems.Count; i++) 
            {
                lb_Selected.Items.Add(lb_SearchResult.SelectedItems[i]);
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            lb_Selected.Items.Remove(lb_Selected.SelectedItem);
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            lb_Selected.Items.Clear();
        }

        private void btn_Name_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = " 选择要合并后的文件";
            //saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = " txt files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.InitialDirectory = System.Environment.SpecialFolder.DesktopDirectory.ToString();
            saveFileDialog1.OverwritePrompt = false;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label_Name.Text = saveFileDialog1.FileName;
            }
        }

        private void btn_Combine_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(label_Name.Text))
            {
                File.Delete(label_Name.Text);
            }
            if (label_Name.Text.Trim().Length == 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = " 选择要合并后的文件";
                //saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = " txt files(*.txt)|*.txt|All files(*.*)|*.*";
                saveFileDialog1.InitialDirectory = System.Environment.SpecialFolder.DesktopDirectory.ToString();
                saveFileDialog1.OverwritePrompt = false;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    label_Name.Text = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }
            FileStream fs_dest = new FileStream(label_Name.Text, FileMode.CreateNew, FileAccess.Write);
            byte[] DataBuﬀer = new byte[100000];
            byte[] ﬁle_name_buf;
            //int ﬁle_name_len=0;
            FileStream fs_source=null;
            int read_len; FileInfo ﬁ_a=null;
            for (int i = 0; i < lb_Selected.Items.Count; i++)
            {
                ﬁ_a = new FileInfo(lb_Selected.Items[i].ToString());
                ﬁle_name_buf = Encoding.Default.GetBytes(ﬁ_a.Name);
                //写入文件名 
                fs_dest.Write(ﬁle_name_buf, 0, ﬁle_name_buf.Length);
                //换行 
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_source = new FileStream(ﬁ_a.FullName, FileMode.Open, FileAccess.Read);
                read_len = fs_source.Read(DataBuﬀer, 0, 100000);
                while (read_len > 0)
                {
                    fs_dest.Write(DataBuﬀer, 0, read_len);
                    read_len = fs_source.Read(DataBuﬀer, 0, 100000);
                }
                //换行
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_source.Close();
            }
            fs_source.Dispose();
            fs_dest.Flush();
            fs_dest.Close();
            fs_dest.Dispose();
            Process.Start(label_Name.Text);
        }

        private void btn_up_Click(object sender, RoutedEventArgs e)
        {
            int count = lb_Selected.SelectedIndex;
            if (count < 0)
            {
                return;
            }
            string str = lb_Selected.SelectedItem.ToString();
            if (count > 0)
            {
                lb_Selected.Items[count] = lb_Selected.Items[count - 1];
                lb_Selected.Items[count - 1] = str;
                lb_Selected.SelectedIndex = count - 1;
            }
        }

        private void btn_down_Click(object sender, RoutedEventArgs e)
        {
            int count = lb_Selected.SelectedIndex;
            if (count < 0)
            {
                return;
            }
            string str = lb_Selected.SelectedItem.ToString();
            if (count < lb_Selected.Items.Count-1)
            {
                lb_Selected.Items[count] = lb_Selected.Items[count + 1];
                lb_Selected.Items[count + 1] = str;
                lb_Selected.SelectedIndex = count + 1;
            }
        }
    }
}
