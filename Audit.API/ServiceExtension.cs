﻿using MassTransit;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.API
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(o =>
            {
                o.SetKebabCaseEndpointNameFormatter();

                // o.AddSagaStateMachine<CustomerCreationStateMachine, CustomerCreationState>()
                //  .InMemorySagaRepository();                


                o.AddConsumers(typeof(CustomerCreatedAuditConsumer).Assembly);

                o.UsingRabbitMq((context, cfg) =>

                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });


            return services;
        }
    }
}