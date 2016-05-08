///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System;
using System.IO;

namespace WeatherBot.MessagesConveyor.IO.Parser.Data {

    internal sealed class DataLoader {

        private static readonly Lazy<DataLoader> _instance = new Lazy<DataLoader>(() => new DataLoader());
        public static DataLoader Instance { get { return _instance.Value; } }

        public string GetEmbeddedTextResource(string internalPath, string filename) {

            string result = string.Empty;

            using (Stream stream = GetType().Assembly.GetManifestResourceStream(internalPath + '.' + filename)) {
                using (StreamReader sr = new StreamReader(stream)) {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        public void /*string*/ GetFromDatabaseResource(/*...*/) {
            throw new NotImplementedException();
        }
        private DataLoader() { }
    }
}