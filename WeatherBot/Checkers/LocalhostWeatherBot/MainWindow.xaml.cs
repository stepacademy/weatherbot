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
            string owmToken;

            try {
                using (StreamReader file = new StreamReader("botToken.txt")) {
                    botToken = file.ReadLine();                    
                    file.Close();
                }
                using (StreamReader file = new StreamReader("owmToken.txt")) {
                    owmToken = file.ReadLine();
                    file.Close();
                }

                if (botToken != null && owmToken != null) {
                    proxy.Start(botToken, owmToken, InteractionMode.GetUpdatesBased);
                    serviceStatus.Content = "Ready...";
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