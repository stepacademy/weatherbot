﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.CheckTeleInteractor.TeleInteractionReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TeleInteractionReference.IMessagesConveyorService", CallbackContract=typeof(Test.CheckTeleInteractor.TeleInteractionReference.IMessagesConveyorServiceCallback))]
    public interface IMessagesConveyorService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/SendResponse")]
        void SendResponse(WeatherBot.TeleInteraction.Adapters.Message message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/SendResponse")]
        System.Threading.Tasks.Task SendResponseAsync(WeatherBot.TeleInteraction.Adapters.Message message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/Start")]
        void Start();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/Start")]
        System.Threading.Tasks.Task StartAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/Stop")]
        void Stop();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/Stop")]
        System.Threading.Tasks.Task StopAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessagesConveyorServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessagesConveyorService/Process")]
        void Process(WeatherBot.TeleInteraction.Adapters.Message message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessagesConveyorServiceChannel : Test.CheckTeleInteractor.TeleInteractionReference.IMessagesConveyorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessagesConveyorServiceClient : System.ServiceModel.DuplexClientBase<Test.CheckTeleInteractor.TeleInteractionReference.IMessagesConveyorService>, Test.CheckTeleInteractor.TeleInteractionReference.IMessagesConveyorService {
        
        public MessagesConveyorServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MessagesConveyorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MessagesConveyorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessagesConveyorServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessagesConveyorServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendResponse(WeatherBot.TeleInteraction.Adapters.Message message) {
            base.Channel.SendResponse(message);
        }
        
        public System.Threading.Tasks.Task SendResponseAsync(WeatherBot.TeleInteraction.Adapters.Message message) {
            return base.Channel.SendResponseAsync(message);
        }
        
        public void Start() {
            base.Channel.Start();
        }
        
        public System.Threading.Tasks.Task StartAsync() {
            return base.Channel.StartAsync();
        }
        
        public void Stop() {
            base.Channel.Stop();
        }
        
        public System.Threading.Tasks.Task StopAsync() {
            return base.Channel.StopAsync();
        }
    }
}
