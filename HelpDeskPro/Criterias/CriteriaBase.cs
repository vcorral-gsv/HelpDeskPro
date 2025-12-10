using System.Linq.Expressions;

namespace HelpDeskPro.Criterias
{
    public abstract class CriteriaBase<T> : ICriteria<T>
    {
        protected abstract Expression<Func<T, bool>> BuildFilterExpression();

        public CriteriaBase() { }

        public Expression<Func<T, bool>> BuildExpression()
        {
            return BuildFilterExpression();
        }
    }
}
