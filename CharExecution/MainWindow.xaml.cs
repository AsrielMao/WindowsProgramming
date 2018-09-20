using DotNetSpeech;
using Microsoft.International.Converters.PinYinConverter;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CharExecution
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //获取拼音
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                return;
            }

            //清空文本框中内容
            textBox2.Text = "";

            char[] chars = textBox1.Text.Trim().ToCharArray();
            foreach (char one_char in chars)
            {
                int ch_int = (int)one_char;

                if (ch_int > 127)
                {
                    ChineseChar chineseChar = new ChineseChar(one_char);
                    ReadOnlyCollection<string> pinyin = chineseChar.Pinyins;
                    string pin_str = textBox2.Text + one_char + "，编号："+ch_int+"\n拼音：";
                    foreach (string pin in pinyin)
                    {
                        pin_str += pin + " ";
                    }
                    pin_str += "\n";
                    textBox2.Text = pin_str;
                }
            }
            
        }

        //繁体转简体
        private void txtCht_TextChanged(object sender, TextChangedEventArgs e)
        {
            chtchsConvert();
        }

        private void btn_switch_Click(object sender, RoutedEventArgs e)
        {
            string temp = label_ch1.Content.ToString();
            label_ch1.Content = label_ch2.Content;
            label_ch2.Content = temp;

            txtCh1.Text = txtCh2.Text;
            chtchsConvert();
        }

        private void chtchsConvert()
        {
            if (label_ch1.Content.ToString() == "繁体字段")
                txtCh2.Text = ChineseConverter.Convert(txtCh1.Text,
                    ChineseConversionDirection.TraditionalToSimplified);
            else
                txtCh2.Text = ChineseConverter.Convert(txtCh1.Text,
                    ChineseConversionDirection.SimplifiedToTraditional);
        }

        //语音播放文本
        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            voice.Speak(txt_article.Text.Trim(), spFlags);
        }

        private void btn_play_Click_1(object sender, RoutedEventArgs e)
        {
            SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            voice.Speak(txt_article.Text.Trim(), spFlags);
        }
    }
}
