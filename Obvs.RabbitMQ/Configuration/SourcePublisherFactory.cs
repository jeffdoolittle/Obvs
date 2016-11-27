﻿using System;
using System.Reflection;
using Obvs.Serialization;
using RabbitMQ.Client;

namespace Obvs.RabbitMQ.Configuration
{
    internal static class SourcePublisherFactory
    {
        public static MessagePublisher<TMessage> CreatePublisher<TMessage>(string brokerUri, string routingKeyPrefix, string serviceName, IMessageSerializer messageSerializer, Lazy<IModel> channel)
            where TMessage : class
        {
            return new MessagePublisher<TMessage>(
                channel,
                messageSerializer,
                serviceName,
                routingKeyPrefix);
        }

        public static MessageSource<TMessage> CreateSource<TMessage, TServiceMessage>(string brokerUri, string routingKeyPrefix, string serviceName, IMessageDeserializerFactory deserializerFactory, Lazy<IModel> channel, Func<Assembly, bool> assemblyFilter = null, Func<Type, bool> typeFilter = null, string selector = null)
            where TServiceMessage : class
            where TMessage : class
        {
            return new MessageSource<TMessage>(
                channel,
                deserializerFactory.Create<TMessage, TServiceMessage>(assemblyFilter, typeFilter), 
                serviceName,
                routingKeyPrefix);
        }
    }
}