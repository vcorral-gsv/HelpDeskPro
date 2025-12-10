using System.Linq.Expressions;

namespace HelpDeskPro.Utils
{
    /// <summary>
    /// Utilidades para composición de filtros LINQ.
    /// </summary>
    public static class FiltersUtils
    {
        /// <summary>
        /// Combina dos expresiones en una AndAlso homogénea usando el mismo parámetro.
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <param name="expr1">Primera expresión</param>
        /// <param name="expr2">Segunda expresión</param>
        /// <returns>Expresión combinada</returns>
        public static Expression<Func<T, bool>> ExpressionAndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
            ParameterExpression parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            Expression? left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            Expression? right = rightVisitor.Visit(expr2.Body);

            if (left is null)
                throw new InvalidOperationException("Failed to process left expression in AndAlso operation for parameter 'expr1'. Expression body was null.");
            return right is null
                ? throw new InvalidOperationException("Failed to process right expression in AndAlso operation for parameter 'expr2'. Expression body was null.")
                : Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        internal class ReplaceExpressionVisitor(Expression _oldValue, Expression _newValue) : ExpressionVisitor
        {
            /// <summary>
            /// Reemplaza ocurrencias de una expresión por otra.
            /// </summary>
            public override Expression? Visit(Expression? node)
            {
                return node == null ? null : node == _oldValue ? _newValue : base.Visit(node);
            }
        }
    }
}
