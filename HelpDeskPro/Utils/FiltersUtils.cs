using System.Linq.Expressions;

namespace HelpDeskPro.Utils
{
    public static class FiltersUtils
    {
        public static Expression<Func<T, bool>> ExpressionAndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
            ParameterExpression parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            Expression left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            Expression right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        internal class ReplaceExpressionVisitor(Expression _oldValue, Expression _newValue) : ExpressionVisitor
        {
            public override Expression? Visit(Expression? node)
            {
                return node == null ? null : node == _oldValue ? _newValue : base.Visit(node);
            }
        }
    }
}
