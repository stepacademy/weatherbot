using System.ServiceModel;


namespace WeatherBot.DatabaseWorker {

    /// <summary>
    /// Contract for management DatabaseWorker service
    /// </summary>
    [ServiceContract]
    public interface IManagementContract {

        /// <summary>
        /// Start weather update timer and weather update
        /// </summary>
        [OperationContract]
        void Start();

        /// <summary>
        /// Stop weather update timer and weather update
        /// </summary>
        [OperationContract]
        void Stop();
    }
}