﻿using CastZone.Tools.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace RabbitMQ.Client
{
    public static class RabbitExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnqueueMessage<T>(this IEnumerable<T> @this, string queue, string exchange = "",
            bool durable = false, bool exclusive = false, bool autoDelete = false,
            IDictionary<string, object> arguments = null) =>
            new ConnectionFactory()
                .OpenChannel(ch =>
                {
                    ch.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
                    @this.ToList().ForEach(x => ch.BasicPublish(exchange, queue, null, x.ToByteJson()));
                });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnqueueMessage<T>(this T @this, string queue, string exchange = "",
            bool durable = false, bool exclusive = false, bool autoDelete = false,
            IDictionary<string, object> arguments = null) =>
            new ConnectionFactory()
                .OpenChannel(ch =>
                {
                    ch.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
                    ch.BasicPublish(exchange, queue, null, @this.ToByteJson());
                });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OpenChannel(this ConnectionFactory @this, Action<IModel> fn) =>
            Disposable.Using(
                @this.CreateConnection,
                (connection) => Disposable.Using(connection.CreateModel, fn)
            );
    }
}