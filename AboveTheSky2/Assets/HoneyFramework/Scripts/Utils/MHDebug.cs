using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace HoneyFramework
{
    /*
     * Helper class containing debug functions.
     */
    class MHDebug
    {

        /// <summary>
        /// Allows to get variable name for debug purposes
        /// Usage:
        /// var varName = "content";
        /// GetVariableName(() => varName);
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;
            return body.Member.Name;
        }
    }

}