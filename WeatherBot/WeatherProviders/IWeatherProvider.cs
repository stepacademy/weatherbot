///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

using System.Threading.Tasks;

namespace WeatherBot.WeatherProviders {

    using DatabaseWorker.QueryComponents;

    public interface IWeatherProvider {

        Task SetCurrentAsync(QueryData query);
    }
}