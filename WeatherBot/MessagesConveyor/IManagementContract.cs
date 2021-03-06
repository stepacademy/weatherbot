﻿///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.ServiceModel;


namespace WeatherBot.MessagesConveyor {

    using TeleInteraction.InteractionStrategy;

    [ServiceContract]
    public interface IManagementContract {

        [OperationContract]
        void Start(string botToken, string owmToken, InteractionMode iMode);

        [OperationContract]
        void Stop();

    }
}