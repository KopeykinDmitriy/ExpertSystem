﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
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

namespace ExpertSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainMatrix mainMatrix;

        List<Label> labels = new List<Label>();
        public MainWindow()
        {
            InitializeComponent();
            mainMatrix = new MainMatrix();

            labels.Add(g00); labels.Add(g01); labels.Add(g02); labels.Add(g03); labels.Add(g04);
            labels.Add(g10); labels.Add(g11); labels.Add(g12); labels.Add(g13); labels.Add(g14);
            labels.Add(g20); labels.Add(g21); labels.Add(g22); labels.Add(g23); labels.Add(g24);
            labels.Add(g30); labels.Add(g31); labels.Add(g32); labels.Add(g33); labels.Add(g34);
            labels.Add(g40); labels.Add(g41); labels.Add(g42); labels.Add(g43); labels.Add(g44);
            labels.Add(g50); labels.Add(g51); labels.Add(g52); labels.Add(g53); labels.Add(g54);
            labels.Add(g60); labels.Add(g61); labels.Add(g62); labels.Add(g63); labels.Add(g64);

        }

        private void InitializeMatrix()
        {
            for (int w = 0; w < 7; w++)
                for (int h = 0; h < 5; h++)
                {
                    labels[h + w * 5].Content = Convert.ToString(mainMatrix.GetElement(w, h));
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)rejim.Content == "Режим обучения")
            {
                var inputMatrix = new List<int> { Convert.ToInt32(ComboBoxClass_Copy.Text), Convert.ToInt32(ComboBoxClass_Copy1.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy2.Text), Convert.ToInt32(ComboBoxClass_Copy3.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy4.Text), Convert.ToInt32(ComboBoxClass_Copy5.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy6.Text)};
                var inputClass = Convert.ToInt32(ComboBoxClass.SelectedIndex);

                mainMatrix.Education(inputClass, inputMatrix);
                InitializeMatrix();
            }
            else
            {
                var inputMatrix = new List<int> { Convert.ToInt32(ComboBoxClass_Copy.Text), Convert.ToInt32(ComboBoxClass_Copy1.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy2.Text), Convert.ToInt32(ComboBoxClass_Copy3.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy4.Text), Convert.ToInt32(ComboBoxClass_Copy5.Text),
                                              Convert.ToInt32(ComboBoxClass_Copy6.Text)};

                var result = mainMatrix.Expert(inputMatrix);
                ExpertResult.Content = "Ваш класс - " + (string)ComboBoxClass.Items[result];
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider.Value == 1)
            {
                rejim.Content = "Режим экспертизы";
                var brush = new SolidColorBrush(Color.FromRgb(255, 0, 255));
                rejim.Foreground = brush;
                ComboBoxClass.Visibility = Visibility.Collapsed;
                ExpertResult.Content = "";
                button.Content = "Определить";
            }
            else
            {
                rejim.Content = "Режим обучения";
                var brush = new SolidColorBrush(Color.FromRgb(50, 200, 30));
                rejim.Foreground = brush;
                ComboBoxClass.Visibility = Visibility.Visible;
                ExpertResult.Content = "";
                button.Content = "Обучить";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var formatter = new BinaryFormatter();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ExpertSystem" + DateTime.Now.ToString();
            saveFileDialog.DefaultExt = ".exp";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "ExpertSystem(*.exp)|*.exp|Все файлы(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, mainMatrix);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var formatter = new BinaryFormatter();
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ExpertSystem(*.exp)|*.exp|Все файлы(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        mainMatrix = (MainMatrix)formatter.Deserialize(fs);
                        InitializeMatrix();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
