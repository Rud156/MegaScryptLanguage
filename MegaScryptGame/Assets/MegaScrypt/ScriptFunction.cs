using Antlr4.Runtime.Tree;
using MegaScryptLib;
using System.Collections.Generic;

namespace MegaScrypt
{
    class ScriptFunction : IFunction
    {
        private string _name;
        public string Name => _name;

        private List<string> _parameterNames;
        public List<string> ParameterNames => _parameterNames;

        private MegaScryptParser.FuncDeclerationContext _declContext;
        public MegaScryptParser.FuncDeclerationContext DeclContext => _declContext;

        public delegate object Invocation(ScriptFunction function, List<object> parameters, InvocationContext ctx);
        private Invocation _invocation;

        public ScriptFunction(Processor processor, Invocation invocation, MegaScryptParser.FuncDeclerationContext declContext)
        {
            _declContext = declContext;
            _name = TryFindName(processor);

            if (declContext.varList() != null)
            {
                _parameterNames = declContext.varList().Accept(processor) as List<string>;
            }
            else
            {
                _parameterNames = new List<string>();
            }

            _invocation = invocation;
        }

        private string TryFindName(Processor processor)
        {
            IRuleNode node = _declContext.Parent;
            while (node != null)
            {
                MegaScryptParser.DeclerationContext varContext = node as MegaScryptParser.DeclerationContext;
                if (varContext != null)
                {
                    return varContext.Id().Accept(processor) as string;
                }

                node = node.Parent;
            }

            return null;
        }

        public object Invoke(List<object> parameters, InvocationContext ctx)
        {
            return _invocation.Invoke(this, parameters, ctx);
        }
    }
}
