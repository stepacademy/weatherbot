
// local WeatherBot host

namespace Test.AHost {

    class Program {

        static void Main(string[] args) {

            /// start service: 1

            // - DatabaseWorker (ex WSLweather)
            ///  include: update weather, request-response logic

            /// ---


            /// start service: 2

            // - MessageConveyor (ex TeleInteractor)
            ///  include: telegram api interaction, query preprocessor, response postprocessor

            /// ---
        }
    }
}