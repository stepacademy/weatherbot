///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.IO;
using System.Windows;
using Checkers.MessageConveyorServiceReference;

namespace LocalhostWeatherBot {

    public partial class MainWindow : Window {

        private void Initialize() {

            ManagementContractClient proxy = new ManagementContractClient();
            string botToken;

            try {
                using (StreamReader file = new StreamReader("botToken.txt")) {

                    if ((botToken = file.ReadLine()) != null) {
                        proxy.Start(botToken, InteractionMode.GetUpdatesBased);
                        serviceStatus.Content = "Ready...";
                    }
                    file.Close();
                }
            }
            catch (FileNotFoundException e) { serviceStatus.Content = e.Message; }
            catch (Exception e)             { serviceStatus.Content = e.Message; }
        }

        public MainWindow() {
            InitializeComponent();
            Initialize();
        }
    }
}