///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.UserSpeaker {

    using DatabaseWorker.QueryComponents;

    public interface IUserSpeaker {

        string FormReply(QueryData response);
    }
}