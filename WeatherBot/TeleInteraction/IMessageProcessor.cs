namespace WeatherBot.TeleInteraction {

    using System.ServiceModel;
    using WeatherBot.TeleInteraction.TelegramAdapters;

    public interface IMessageProcessor {

        [OperationContract]
        Message MessageProcessing(Message message);

    }
}