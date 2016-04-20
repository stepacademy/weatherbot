namespace WeatherBot.TeleInteraction {

    using System.ServiceModel;
    using WeatherBot.TeleInteraction.TelegramAdapters;

    public interface IMessageProcessor {

        [OperationContract(IsOneWay = false)]
        Message MessageProcessing(Message message);

    }
}