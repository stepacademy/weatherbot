///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Xml.Linq;
using System.Collections.Generic;

namespace WeatherBot.MessagesConveyor.IO.Parser.Data {

    internal sealed class Locations {

        private readonly XDocument  _locations;
        private readonly StringSimilarityMetric    _ssm;

        private readonly double _errorQuantity;

        public KeyValuePair<string, int> GetEntry(string location) {

            int currentErrorsCount = location.Length;
            string result = null;

            foreach (XElement country in _locations.Descendants("country")) {

                foreach (XElement city in country.Descendants()) {

                    string cityName = city.Value;

                    int localErrorsLimit = (int)(cityName.Length * _errorQuantity);
                    int localErrorsCount = _ssm.DamerauLevenshtein(location, cityName.ToLower());

                    if (localErrorsCount == 0)
                        return new KeyValuePair<string, int>(cityName, 0);

                    else if (
                        localErrorsCount < currentErrorsCount &&
                        localErrorsCount <= localErrorsLimit &&
                        localErrorsCount < cityName.Length
                        ) {
                        currentErrorsCount = localErrorsCount;
                        result = cityName;
                    }
                }
            }

            return new KeyValuePair<string, int>(result, currentErrorsCount);
        }

        public Locations(string internalPath, string filename, double errorQuantity) {
            _locations = XDocument.Parse(DataLoader.Instance.GetEmbeddedTextResource(internalPath, filename));
            _ssm = new StringSimilarityMetric();
            _errorQuantity = errorQuantity / 100.0;
        }
    }
}