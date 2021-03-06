﻿using System;
using MQTTnet.Core.Adapter;
using MQTTnet.Core.Channel;
using MQTTnet.Core.Client;
using MQTTnet.Core.Serializer;
using MQTTnet.Implementations;

namespace MQTTnet
{
    public class MqttClientFactory : IMqttClientFactory
    {
        public IMqttClient CreateMqttClient(MqttClientOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            return new MqttClient(options, new MqttChannelCommunicationAdapter(GetMqttCommunicationChannel(options), new MqttPacketSerializer()));
        }

        private static IMqttCommunicationChannel GetMqttCommunicationChannel(MqttClientOptions options)
        {
            switch (options.ConnectionType)
            {
                case MqttConnectionType.Tcp:
                    return new MqttTcpChannel();
                case MqttConnectionType.Ws:
                    return new MqttWebSocketChannel();

                default:
                    throw new NotSupportedException();
            }
        }
    }
}