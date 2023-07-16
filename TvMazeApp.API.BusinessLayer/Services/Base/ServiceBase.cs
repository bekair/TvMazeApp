using AutoMapper;
using TvMazeApp.DataAccess.UnitOfWorks.Base;

namespace TvMazeApp.API.BusinessLayer.Services.Base;

public class ServiceBase
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}