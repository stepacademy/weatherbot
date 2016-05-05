///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO.Parser {

    using DatabaseWorker.QueryComponents;

    internal sealed class InputParser {

        private StringSimilarityMetric _metric;
        private List<string>      _incomingSet;

        private void ExtractCities() {

        }

        private void ExtractTimes() {

        }

        public QueryData GetQuery(string incomingText) {

            return new QueryData();
        }

        public InputParser() {
            _metric = new StringSimilarityMetric();
        }
    }
}