using System.Reflection;
using DocuWare.Application.Features.Quote.Command;
using DocuWare.Application.Features.Quote.Queries;
using DocuWare.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DocuWare.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Register command and query handlers
        services.AddTransient<IRequestHandler<CreateQuoteCommand, Unit>, CreateQuoteCommandHandler>();
        services.AddTransient<IRequestHandler<GetQuotesQuery, IEnumerable<Quote>>, GetQuotesQueryHandler>();
        services
            .AddTransient<IRequestHandler<GetQuotesByMovieQuery, IEnumerable<Quote>>, GetQuotesByMovieQueryHandler>();
        services
            .AddTransient<IRequestHandler<GetQuotesByActorQuery, IEnumerable<Quote>>, GetQuotesByActorQueryHandler>();

        return services;
    }
}