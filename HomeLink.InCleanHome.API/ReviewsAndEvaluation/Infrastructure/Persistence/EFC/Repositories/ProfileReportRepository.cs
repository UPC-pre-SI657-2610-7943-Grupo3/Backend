using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Infrastructure.Persistence.EFC.Repositories;

public class ProfileReportRepository(AppDbContext context)
    : BaseRepository<ProfileReport>(context), IProfileReportRepository
{
}
