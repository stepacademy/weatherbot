using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using Test.MessageConveyorServiceReference;

namespace LocalhostWeatherBot {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private void Initialize() {

            InstanceContext instanceContext = new InstanceContext(this);
            ManagementContractClient proxy = new ManagementContractClient(instanceContext);
            proxy.Open();

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
            catch (FileNotFoundException e) {
                serviceStatus.Content = e.Message;
            }
            catch (Exception e) {
                serviceStatus.Content = e.Message;
            }
        }

        public MainWindow() {
            InitializeComponent();
            Initialize();
        }
    }
}