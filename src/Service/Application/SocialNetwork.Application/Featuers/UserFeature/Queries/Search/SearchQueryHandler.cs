using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.Application.Featuers.UserFeature.Queries.Search;

public class SearchQueryHandler :
    IRequestHandler<SearchQuery, Result<SearchQueryResult>>
{
    private UserManager<AppUser> _userManager;
    private IMapper _mapper;

    public SearchQueryHandler(
        UserManager<AppUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<SearchQueryResult>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.Where(u => u.UserName.Contains(request.Name)).ToListAsync();

        var result = new SearchQueryResult(users.Select(u => u.UserName).ToList());

        return await Task.FromResult(Result.Success(result));
    }
}

