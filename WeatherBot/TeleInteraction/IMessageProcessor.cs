using System.ServiceModel;
using WeatherBot.TeleInteraction.TelegramAdapters;

namespace WeatherBot.TeleInteraction
{
    public interface IMessageProcessor
    {
        [OperationContract]
        Message MessageProcessing(Message message);
    }
}