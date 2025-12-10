using HelpDeskPro.Entities;
using HelpDeskPro.Utils;
using System.Linq.Expressions;

namespace HelpDeskPro.Criterias.Users
{
    public class GetUsersCriteria(GetUsersFiltersInputDto _filters) : CriteriaBase<User>
    {
        protected override Expression<Func<User, bool>> BuildFilterExpression()
        {
            Expression<Func<User, bool>> filter = u => true;
            if (_filters == null)
            {
                return filter;
            }
            var exactMatch = _filters.ExactMatch;

            if (!string.IsNullOrEmpty(_filters.Email))
            {
                filter = filter.ExpressionAndAlso(u => exactMatch
                    ? u.Email == _filters.Email
                    : u.Email.Contains(_filters.Email));
            }

            if (!string.IsNullOrEmpty(_filters.FirstName))
            {
                filter = filter.ExpressionAndAlso(u => exactMatch
                    ? u.FirstName == _filters.FirstName
                    : u.FirstName.Contains(_filters.FirstName));
            }

            if (!string.IsNullOrEmpty(_filters.LastName))
            {
                filter = filter.ExpressionAndAlso(u => exactMatch
                    ? u.LastName == _filters.LastName
                    : u.LastName.Contains(_filters.LastName));
            }

            if (_filters.Roles != null && _filters.Roles.Count != 0)
            {
                filter = filter.ExpressionAndAlso(u => _filters.Roles.Contains(u.Role.ToString()));
            }

            if (_filters.IsActive.HasValue)
            {
                filter = filter.ExpressionAndAlso(u => u.IsActive == _filters.IsActive.Value);
            }

            if (_filters.CreatedAfterDate.HasValue)
            {
                filter = filter.ExpressionAndAlso(u => u.CreatedAt > _filters.CreatedAfterDate.Value);
            }

            if (_filters.CreatedBeforeDate.HasValue)
            {
                filter = filter.ExpressionAndAlso(u => u.CreatedAt < _filters.CreatedBeforeDate.Value);
            }

            if (_filters.LastLoginAfterDate.HasValue)
            {
                filter = filter.ExpressionAndAlso(u => u.LastLoginAt > _filters.LastLoginAfterDate.Value);
            }

            if (_filters.LastLoginBeforeDate.HasValue)
            {
                filter = filter.ExpressionAndAlso(u => u.LastLoginAt < _filters.LastLoginBeforeDate.Value);
            }

            if (_filters.TeamIds is { Count: > 0 })
            {
                var teamIds = _filters.TeamIds;
                filter = filter.ExpressionAndAlso(u => u.Teams.Any(t => teamIds.Contains(t.Id)));
            }

            if (_filters.TeamNames is { Count: > 0 })
            {
                var teamNames = _filters.TeamNames;
                filter = filter.ExpressionAndAlso(u => u.Teams.Any(t => teamNames.Contains(t.Name)));
            }

            return filter;
        }
    }
}
