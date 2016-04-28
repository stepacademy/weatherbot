


namespace WeatherBot.MessagesConveyor.IOFilter {

    // База данных предварительно создаст у себя экземпляр этого класса, чтобы вызвать необходимый метод

    class ResponsePostprocessor {

        // База данных вызовет этот метод по завершению обработки запроса, передав заполненную результатом QueryData

        void QueryDataProcessing(/*QueryData query*/) {

                
            // после преобразования QueryData в Message.Response.Text отправляем его обратно TeleInteractor-у
            // пока не реализовано, раскомментится, как будет готово

            // SendToUserResponse(Message message);
        }
    }
}