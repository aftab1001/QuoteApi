using DocuWare.Application.Contracts;
using DocuWare.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DocuWare.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IQuoteByMovieRepository, QuoteByMovieRepository>();
        services.AddScoped<IQuoteByActorRepository, QuoteByActorRepository>();
        services.AddScoped<IMovieByQuoteContentRepository, MovieByQuoteContentRepository>();
        services.AddScoped<ICharacterByQuoteContentRepository, CharacterByQuoteContentRepository>();
        var dbContext = services.BuildServiceProvider().GetRequiredService<QuoteDbContext>();
        SeedData.Initialize(dbContext);
        return services;
    }
}