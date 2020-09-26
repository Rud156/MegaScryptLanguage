using System;
using System.Collections.Generic;
using System.Linq;

namespace MegaScrypt
{
    public class Object
    {
        private Object _parent = null;
        public Object Parent => _parent;

        private Dictionary<string, object> variables = new Dictionary<string, object>();
        public List<string> VariableNames => variables.Keys.ToList();

        public Object(Object parent = null)
        {
            _parent = parent;
        }

        public void Declare(string varName, object value = null)
        {
            if (varName == "prototype")
            {
                _parent = value as Object;
                return;
            }
            if (variables.ContainsKey(varName))
            {
                Console.WriteLine($"Variable {varName} already declared");
                throw new InvalidOperationException("already declared");
            }

            variables.Add(varName, value);
        }

        public object Get(string varName, bool allowParentChaining = true)
        {
            if (varName == "prototype")
            {
                return _parent;
            }

            if (_getters.ContainsKey(varName))
            {
                return _getters[varName]?.Invoke();
            }

            if (variables.ContainsKey(varName))
            {
                return variables[varName];
            }

            if (allowParentChaining && _parent != null)
            {
                return _parent.Get(varName, true);
            }

            throw new InvalidOperationException($"undeclared: {varName}");
        }

        public void Assign(string varName, object value, bool allowParentChaining = true)
        {
            if (varName == "prototype")
            {
                _parent = value as Object;
                return;
            }

            if (_getters.ContainsKey(varName))
            {
                _setters[varName]?.Invoke(value);
                return;
            }

            if (variables.ContainsKey(varName))
            {
                variables[varName] = value;
                return;
            }

            if (allowParentChaining && _parent != null)
            {
                _parent.Assign(varName, value, true);
                return;
            }

            throw new InvalidOperationException($"undeclared");
        }

        public bool Has(string varName, bool allowParentChaining = true)
        {
            if (varName == "prototype")
            {
                return _parent != null;
            }

            if (_getters.ContainsKey(varName))
            {
                return true;
            }

            bool has = variables.ContainsKey(varName);
            if (!has && allowParentChaining && _parent != null)
            {
                has = _parent.Has(varName, true);
            }

            return has;
        }

        public void Declare(NativeFunction.Callback callback, IEnumerable<string> parameterNames = null)
        {
            Declare(new NativeFunction(callback, parameterNames));
        }

        public void Declare(NativeFunction function)
        {
            Declare(function.Name, function);
        }

        public delegate object Getter();
        public delegate void Setter(object value);

        private Dictionary<string, Getter> _getters = new Dictionary<string, Getter>();
        private Dictionary<string, Setter> _setters = new Dictionary<string, Setter>();

        public void Declare(string varName, Getter getter, Setter setter = null)
        {
            if (getter != null)
            {
                _getters.Add(varName, getter);
            }

            if (_setters != null)
            {
                _setters.Add(varName, setter);
            }
        }
    }
}
