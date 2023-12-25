using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> WhereCriteria { get; set; }
        public List<Expression<Func<T, object>>> IncludeCriterias { get; set; } = new List<Expression<Func<T, object>>>();
        public BaseSpecifications()
        {
            // I Was Thinking Why We Doing This Constractor although if the developer use it then 
            // he dosn't need any specifications
            // but i know that: we created it because if we doing any special specification inherit from it
            // and don't need any whereCriteriaExpression
        }
        public BaseSpecifications(Expression<Func<T, bool>> whereCriteriaExpression)
        {
            WhereCriteria = whereCriteriaExpression;
        }
    }
}