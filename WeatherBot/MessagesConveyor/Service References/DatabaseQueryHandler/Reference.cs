﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherBot.MessagesConveyor.DatabaseQueryHandler {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WindDirectionType", Namespace="http://schemas.datacontract.org/2004/07/WeatherBot.Database.Entities")]
    public enum WindDirectionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        South = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SouthEast = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        East = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NorthEast = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        North = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NorthWest = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        West = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SouthWest = 7,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DatabaseQueryHandler.IQueryHandlerContract", CallbackContract=typeof(WeatherBot.MessagesConveyor.DatabaseQueryHandler.IQueryHandlerContractCallback))]
    public interface IQueryHandlerContract {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQueryHandlerContract/QueryAsync")]
        void QueryAsync(WeatherBot.DatabaseWorker.QueryComponents.QueryData query);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQueryHandlerContract/QueryAsync")]
        System.Threading.Tasks.Task QueryAsyncAsync(WeatherBot.DatabaseWorker.QueryComponents.QueryData query);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQueryHandlerContractCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IQueryHandlerContract/Response")]
        void Response([System.ServiceModel.MessageParameterAttribute(Name="response")] WeatherBot.DatabaseWorker.QueryComponents.QueryData response1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQueryHandlerContractChannel : WeatherBot.MessagesConveyor.DatabaseQueryHandler.IQueryHandlerContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QueryHandlerContractClient : System.ServiceModel.DuplexClientBase<WeatherBot.MessagesConveyor.DatabaseQueryHandler.IQueryHandlerContract>, WeatherBot.MessagesConveyor.DatabaseQueryHandler.IQueryHandlerContract {
        
        public QueryHandlerContractClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public QueryHandlerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public QueryHandlerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public QueryHandlerContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public QueryHandlerContractClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void QueryAsync(WeatherBot.DatabaseWorker.QueryComponents.QueryData query) {
            base.Channel.QueryAsync(query);
        }
        
        public System.Threading.Tasks.Task QueryAsyncAsync(WeatherBot.DatabaseWorker.QueryComponents.QueryData query) {
            return base.Channel.QueryAsyncAsync(query);
        }
    }
}
