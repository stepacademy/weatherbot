﻿///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;

namespace WeatherBot.MessagesConveyor {

    using TeleInteraction.Adapters;

    [ServiceContract(CallbackContract = typeof(IMessageProcessorCallback))]
    public interface IMessagesConveyorService {

        [OperationContract(IsOneWay = true)]
        void SendResponse(Message message);

        [OperationContract(IsOneWay = true)]
        void Start(string botTokenPath);

        [OperationContract(IsOneWay = true)]
        void Stop();

    }
}