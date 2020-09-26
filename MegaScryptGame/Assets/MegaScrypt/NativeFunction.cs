using MegaScrypt;
using MegaScryptLib;
using System.Collections.Generic;

namespace MegaScrypt
{
    public class NativeFunction : IFunction
    {
        private string _name;
        public string Name => _name;

        private List<string> _parameterNames;
        public List<string> ParameterNames => _parameterNames;

        public delegate object Callback(List<object> parameters);

        private Callback _callback;

        public NativeFunction(Callback callback, IEnumerable<string> paramterNames)
        {
            _callback = callback;
            _parameterNames = paramterNames != null ? new List<string>(paramterNames) : null;
            _name = callback.Method.Name;
        }

        public NativeFunction(string functionName, Callback callback)
        {
            _callback = callback;
            _parameterNames = null;
            _name = functionName;
        }

        public object Invoke(List<object> paramaters, InvocationContext ctx)
        {
            object retValue = _callback.Invoke(paramaters);
            return retValue;
        }
    }
}
