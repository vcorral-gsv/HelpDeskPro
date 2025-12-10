using System.Linq.Expressions;

namespace HelpDeskPro.Criterias
{
    public interface ICriteria<T>
    {
        Expression<Func<T, bool>> BuildExpression();
    }
}
