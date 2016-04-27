using System;
using System.ServiceModel;

namespace WeatherBot.DatabaseWorker {

    internal class Management : IManagementContract {

        // Start weather update timer and weather update
        public void Start() {
            throw new NotImplementedException();
        }

        // Stop weather update timer and weather update
        public void Stop() {
            throw new NotImplementedException();
        }

        // Save MessageConveyor InstanceContext for callback response
        public void SubscribeClient(InstanceContext callbackContext) {
            throw new NotImplementedException();
        }
    }
}