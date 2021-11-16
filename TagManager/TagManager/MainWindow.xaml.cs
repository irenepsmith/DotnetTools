// <copyright file="MainWindow.xaml.cs" company="Irene P. Smith">
// Copyright (c) Irene P. Smith. All rights reserved.
// </copyright>

namespace TagManager
{
    using System.Collections.Generic;
    using TagManager.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private TagCollection tagManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create tag collection
            this.tagManager = new TagCollection();
        }

        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            var tags = new List<SnippetTag>();

            if (this.txtWorkingDirectory.Text.Length > 0)
            {
                this.tagManager.GetTags(this.txtWorkingDirectory.Text, tags);
            }

            this.lstTags.ItemsSource = tags;
        }

        private void BtnPickDirectory_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog

            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
        }
    }
}
