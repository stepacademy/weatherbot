namespace WeatherBot.TeleInteraction {

    using System.Threading;
    using System.ServiceModel;
    using System.Collections.Generic;
    using TelegramAdapters;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MessagesConveyorService : IMessagesConveyorService {        

        private int _lastProcessingMessageId;

        IMessageProcessor MessageProcessor {
            get {
                return OperationContext.Current.GetCallbackChannel<IMessageProcessor>();
            }
        }

        private void UpdatesReceived(Queue<Telegram.Bot.Types.Update> updatesQueue) {

            while (updatesQueue.Count > 0) {

                Message message = new Message(updatesQueue.Dequeue());

                TeleInteractor.Instance.SendResponse(MessageProcessor.MessageProcessing(
                        message.Id != _lastProcessingMessageId ? message : null));
                _lastProcessingMessageId = message.Id;
            }
        }

        public void Start() {

            while (true) {
                TeleInteractor.Instance.RequestAsync();
                Thread.Sleep(10);
            }
        }

        private void Detach() {

            TeleInteractor.Instance.Received -= UpdatesReceived;
        }

        public MessagesConveyorService() {

            TeleInteractor.Instance.Received += new ReceivedUpdatesEventHandler(UpdatesReceived);
        }
    }    
}