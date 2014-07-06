using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Toodledo.Model;
using System.Reflection;

namespace s7.cmDo.ParameterMaps
{
    public abstract class ParameterToFieldMapBase : IParameterToFieldMap
    {
        public string Identifier { get; set; }
        public PropertyInfo Property { get; set; }

        public ParameterToFieldMapBase(string identifier, Expression<Func<Task, object>> expression)
        {
            MemberExpression memberex = GetMemberExpression(expression.Body, true);
            if (memberex.Member is PropertyInfo)
                Property = memberex.Member as PropertyInfo;
            Identifier = identifier;
        }

        private static MemberExpression GetMemberExpression(Expression expression, bool enforceCheck)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
                memberExpression = expression as MemberExpression;
            if (enforceCheck && memberExpression == null)
                throw new ArgumentException("Not a member access", "expression");
            return memberExpression;
        }

        public void SetField(Task item, object value)
        {
            Property.SetValue(item, value, null);
        }

        public object GetField(Task item)
        {
            return Property.GetValue(item, null);
        }

        public virtual void Visit(Task item, string value) { SetField(item, value); }

        public virtual int CompareTo(IParameterToFieldMap other)
        {
            if (Identifier.StartsWith(other.Identifier) && Identifier.Length > other.Identifier.Length)
                return 1;
            return 0;
        }
    }
}
