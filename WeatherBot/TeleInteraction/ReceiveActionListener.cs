namespace WeatherBot.TeleInteraction {

    using System.Collections.Generic;
    using System.Threading;
    using TelegramAdapters;

    public delegate Message MessageProcessingEvent(Message message);

    public class ReceiveActionListener {

        public event MessageProcessingEvent MessageProcessingEventHandlers = (Message message) => { return null; };

        private int _lastProcessingMessageId;

        private static ReceiveActionListener _instance;

        public static ReceiveActionListener Instance {
            get {
                if (_instance == null)
                    _instance = new ReceiveActionListener();
                return _instance;
            }
        }

        private void UpdatesReceived(Queue<Telegram.Bot.Types.Update> updatesQueue) {

            while (updatesQueue.Count > 0) {
                Message message = new Message(updatesQueue.Dequeue());
                TeleInteractor.Instance.SendResponse(
                    MessageProcessingEventHandlers(
                        message.Id != _lastProcessingMessageId ? message : null
                        ));
                _lastProcessingMessageId = message.Id;
            }
        }

        public void Process() {

            while (true) {
                TeleInteractor.Instance.RequestAsync();
                Thread.Sleep(10);
            }
        }

        private void Detach() {

            TeleInteractor.Instance.Received -= UpdatesReceived;
        }

        private ReceiveActionListener() {

            TeleInteractor.Instance.Received += new ReceivedUpdatesEventHandler(UpdatesReceived);
        }

    }
}