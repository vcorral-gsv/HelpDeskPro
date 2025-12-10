using System.Linq.Expressions;

namespace HelpDeskPro.Criterias
{
    /// <summary>
    /// Clase base para criterios de filtrado con construcción de expresión.
    /// </summary>
    public abstract class CriteriaBase<T> : ICriteria<T>
    {
        /// <summary>
        /// Construye la expresión de filtro específica.
        /// </summary>
        protected abstract Expression<Func<T, bool>> BuildFilterExpression();

        public CriteriaBase() { }

        /// <inheritdoc />
        public Expression<Func<T, bool>> BuildExpression()
        {
            return BuildFilterExpression();
        }
    }
}
