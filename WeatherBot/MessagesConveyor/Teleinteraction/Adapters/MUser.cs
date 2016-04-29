﻿///
/// Jeka, please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///


namespace WeatherBot.MessagesConveyor.TeleInteraction.Adapters {

    internal sealed class MUser {

        public readonly int    Id;
        public readonly string Username;
        public readonly string FirstName;
        public readonly string LastName;

        public MUser(Telegram.Bot.Types.User user) {

            Id        = user.Id;
            Username  = user.Username;
            FirstName = user.FirstName;
            LastName  = user.LastName;

        }
    }
}