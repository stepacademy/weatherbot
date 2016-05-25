///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO.Parser {

    using Data;
    
    internal sealed class InputParser {

        private Locations      _locations;

        public string ExtractFirstCity(string incomingText) {

            List<string> incomingSet = new List<string>(incomingText.Split(new char [] {',', ' '}));

            KeyValuePair<string, int> result = new KeyValuePair<string, int>(null, int.MaxValue);

            foreach (var item in incomingSet) {                
                KeyValuePair<string, int> localResult = _locations.GetEntry(item);

                if (localResult.Value < result.Value)
                    result = localResult;

                if (result.Value == 0)
                    return result.Key;

            }
            return result.Key;
        }

        public InputParser() {
            _locations = new Locations("WeatherBot.MessagesConveyor.IO.Parser.Data", "Locations.xml", 50.0);
        }
    }
}