using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Telegram.Bot.Types;
using Message = WeatherBot.TeleInteraction.TelegramAdapters.Message;

namespace WeatherBot.TeleInteraction
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MessagesConveyorService : IMessagesConveyorService
    {
        private int _lastProcessingMessageId;

        public MessagesConveyorService()
        {
            TeleInteractor.Instance.Received += UpdatesReceived;
        }

        private IMessageProcessor MessageProcessor
        {
            get { return OperationContext.Current.GetCallbackChannel<IMessageProcessor>(); }
        }

        public void Start()
        {
            while (true)
            {
                TeleInteractor.Instance.RequestAsync();
                Thread.Sleep(10);
            }
        }

        private void UpdatesReceived(Queue<Update> updatesQueue)
        {
            while (updatesQueue.Count > 0)
            {
                var message = new Message(updatesQueue.Dequeue());

                TeleInteractor.Instance.SendResponse(MessageProcessor.MessageProcessing(
                    message.Id != _lastProcessingMessageId ? message : null));
                _lastProcessingMessageId = message.Id;
            }
        }

        private void Detach()
        {
            TeleInteractor.Instance.Received -= UpdatesReceived;
        }
    }
}