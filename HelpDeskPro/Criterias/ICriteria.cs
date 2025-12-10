using System.Linq.Expressions;

namespace HelpDeskPro.Criterias
{
    /// <summary>
    /// Contrato para criterios de filtrado expresados como expresiones LINQ.
    /// </summary>
    public interface ICriteria<T>
    {
        /// <summary>
        /// Construye la expresión de filtro para el tipo T.
        /// </summary>
        Expression<Func<T, bool>> BuildExpression();
    }
}
