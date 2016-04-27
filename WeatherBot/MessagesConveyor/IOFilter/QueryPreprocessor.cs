using WeatherBot.MessagesConveyor.TeleInteraction.Adapters;


namespace WeatherBot.MessagesConveyor.IOFilter {

    // TeleInteractor предварительно создаст у себя экземпляр этого класса, чтобы вызвать необходимый метод

    class QueryPreprocessor {

        // TeleInteractor вызовет этот метод при поступлении нового сообщения от пользователя, передав Message

        void MessageProcessing(Message message) { 


            // ... здесь сообщения от пользователя, преобразуется к виду запроса
            // т.е. создаётся QueryData и заполняется

            // ну, не конкретно здесь, но отсюда стартуем чего угодно в общем ;)

            // после преобразования Message.Text в QueryData отправляем базе данных
            // пока не реализовано, раскомментится, как будет готово

            // SendToDatabaseQuery(QueryData query);
        }
    }
}