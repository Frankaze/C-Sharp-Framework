using System;
using System.Linq.Expressions;

namespace UtilityObject.Extension.Linq
{
    public static class ExpressionExtension
    {
        /// <summary>
        /// 機關函數應用True時：單個AND有效，多個AND有效；單個OR無效，多個OR無效；混應時寫在AND後的OR有效
        /// </summary>
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        /// <summary>
        /// 機關函數應用False時：單個AND無效，多個AND無效；單個OR有效，多個OR有效；混應時寫在OR後面的AND有效
        /// </summary>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 添加And條件
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            return expr1.AndAlso<T>(expr2, Expression.AndAlso);
        }

        /// <summary>
        /// 添加Or條件
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            return expr1.AndAlso<T>(expr2, Expression.OrElse);
        }

        /// <summary>
        /// 合併表達式以及參數
        /// </summary>
        private static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2,
            Func<Expression, Expression, BinaryExpression> func)
        {
            ParameterExpression _exprParameter = Expression.Parameter(typeof(T));

            ReplaceExpressionVisitor _revLeft = new ReplaceExpressionVisitor(expr1.Parameters[0], _exprParameter);
            Expression _exprLeft = _revLeft.Visit(expr1.Body);

            ReplaceExpressionVisitor _revRight = new ReplaceExpressionVisitor(expr2.Parameters[0], _exprParameter);
            Expression _exprRight = _revRight.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                func(_exprLeft, _exprRight), _exprParameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression m_exprOldValue;
            private readonly Expression m_exprNewValue;

            public ReplaceExpressionVisitor(Expression exprOldValue, Expression exprNewValue)
            {
                this.m_exprOldValue = exprOldValue;
                this.m_exprNewValue = exprNewValue;
            }

            public override Expression Visit(Expression expr)
            {
                if (expr == this.m_exprOldValue) return this.m_exprNewValue;
                return base.Visit(expr);
            }
        }
    }
}