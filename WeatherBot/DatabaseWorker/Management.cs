///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.ServiceModel;

namespace WeatherBot.DatabaseWorker {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class Management : IManagementContract {

        // Start weather update timer and weather update
        public void Start() {
            throw new NotImplementedException();
        }

        // Stop weather update timer and weather update
        public void Stop() {
            throw new NotImplementedException();
        }
    }
}