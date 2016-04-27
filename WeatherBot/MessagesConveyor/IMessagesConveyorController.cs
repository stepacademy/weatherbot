///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;


namespace WeatherBot.MessagesConveyor {

    [ServiceContract]
    interface IMessagesConveyorController {

        [OperationContract]
        void Start(string botTokenPath);

        [OperationContract]
        void Stop();

    }
}