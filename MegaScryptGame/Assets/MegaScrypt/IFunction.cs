using MegaScrypt;
using System.Collections.Generic;

namespace MegaScryptLib
{
    interface IFunction
    {
        string Name { get; }
        List<string> ParameterNames { get; }
        object Invoke(List<object> parameters, InvocationContext ctx = null);
    }
}
