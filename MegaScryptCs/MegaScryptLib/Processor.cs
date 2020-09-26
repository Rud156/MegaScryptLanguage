using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MegaScryptLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MegaScrypt
{
    class Processor : MegaScryptBaseVisitor<object>
    {
        private Object _target;
        public Object Target
        {
            get => _target;
            set => _target = value;
        }

        #region Terminal Operation

        protected object GetValue(ITerminalNode node)
        {
            string varName = node.Accept(this) as string;
            object value = _target.Get(varName);

            return value;
        }

        public override object VisitTerminal(ITerminalNode node)
        {
            switch (node.Symbol.Type)
            {
                case MegaScryptParser.Id:
                    return node.GetText();

                case MegaScryptParser.Number:
                    {
                        string numberString = node.GetText();
                        if (numberString.Contains("."))
                        {
                            return float.Parse(numberString);
                        }
                        else
                        {
                            return int.Parse(numberString);
                        }
                    }

                case MegaScryptParser.String:
                    {
                        int stringLength = node.GetText().Length;
                        string newText = node.GetText().Substring(1, stringLength - 2);
                        return newText;
                    }

                case MegaScryptParser.Bool:
                    return StringToBool(node.GetText());

                case MegaScryptParser.Null:
                    return null;
            }

            return node;
        }

        public override object VisitCompoundId([NotNull] MegaScryptParser.CompoundIdContext context)
        {
            if (context.Id() != null)
            {
                return GetValue(context.Id());
            }
            else if (context.objectGetter() != null)
            {
                return context.objectGetter().Accept(this);
            }

            return null;
        }

        public override object VisitIncrement([NotNull] MegaScryptParser.IncrementContext context)
        {
            string varName = "";
            object dictionary = _target;
            MegaScryptParser.ObjectAccessContext[] objectList = null;

            if (context.compoundId().objectGetter() != null)
            {
                objectList = context.compoundId().objectGetter().objectAccess();
                varName = objectList.Last().Id().Accept(this) as string;

                dictionary = GetParentDict(objectList);
            }
            else if (context.compoundId().Id() != null)
            {
                varName = context.compoundId().Id().Accept(this) as string;
            }

            object currentValue = context.compoundId().Accept(this);
            bool hasDot = currentValue.ToString().Contains(".");
            object returnValue;
            object newValue;

            ITerminalNode leftNode = context.children[0] as ITerminalNode;
            if (leftNode != null)
            {
                if (hasDot)
                {
                    newValue = FloatOperation(currentValue, 1.0f, MegaScryptParser.Add);
                    returnValue = newValue;
                }
                else
                {
                    newValue = IntegerOperation(currentValue, 1, MegaScryptParser.Add);
                    returnValue = newValue;
                }
            }
            else
            {
                if (hasDot)
                {
                    newValue = FloatBoolOperation(currentValue, 1.0f, MegaScryptParser.Add);
                    returnValue = currentValue;
                }
                else
                {
                    newValue = IntegerOperation(currentValue, 1, MegaScryptParser.Add);
                    returnValue = currentValue;
                }
            }

            if (context.compoundId().Id() != null)
            {
                ((Object)dictionary).Assign(varName, newValue);
            }
            else
            {
                if (objectList.Last().arrayIndex() == null)
                {
                    ((Object)dictionary).Assign(varName, newValue);
                }
                else
                {
                    Array array = (Array)((Object)dictionary).Get(varName);
                    int index = Convert.ToInt32(objectList.Last().arrayIndex().expression().Accept(this));
                    array[index] = newValue;
                }
            }

            return returnValue;
        }

        public override object VisitDecrement([NotNull] MegaScryptParser.DecrementContext context)
        {
            string varName = "";
            object dictionary = _target;
            MegaScryptParser.ObjectAccessContext[] objectList = null;

            if (context.compoundId().objectGetter() != null)
            {
                objectList = context.compoundId().objectGetter().objectAccess();
                varName = objectList.Last().Id().Accept(this) as string;

                dictionary = GetParentDict(objectList);
            }
            else if (context.compoundId().Id() != null)
            {
                varName = context.compoundId().Id().Accept(this) as string;
            }

            object currentValue = context.compoundId().Accept(this);
            bool hasDot = currentValue.ToString().Contains(".");
            object returnValue;
            object newValue;

            ITerminalNode leftNode = context.children[0] as ITerminalNode;
            if (leftNode != null)
            {
                if (hasDot)
                {
                    newValue = FloatOperation(currentValue, 1.0f, MegaScryptParser.Subtract);
                    returnValue = newValue;
                }
                else
                {
                    newValue = IntegerOperation(currentValue, 1, MegaScryptParser.Subtract);
                    returnValue = newValue;
                }
            }
            else
            {
                if (hasDot)
                {
                    newValue = FloatBoolOperation(currentValue, 1.0f, MegaScryptParser.Subtract);
                    returnValue = currentValue;
                }
                else
                {
                    newValue = IntegerOperation(currentValue, 1, MegaScryptParser.Subtract);
                    returnValue = currentValue;
                }
            }

            if (context.compoundId().Id() != null)
            {
                ((Object)dictionary).Assign(varName, newValue);
            }
            else
            {
                if (objectList.Last().arrayIndex() == null)
                {
                    ((Object)dictionary).Assign(varName, newValue);
                }
                else
                {
                    Array array = (Array)((Object)dictionary).Get(varName);
                    int index = Convert.ToInt32(objectList.Last().arrayIndex().expression().Accept(this));
                    array[index] = newValue;
                }
            }

            return returnValue;
        }

        #endregion

        #region Declaration Region

        public override object VisitDecleration([NotNull] MegaScryptParser.DeclerationContext context)
        {
            string varName = context.Id().Accept(this) as string;
            object value = null;

            if (context.expression() != null)
            {
                value = context.expression().Accept(this);
            }
            else if (context.@object() != null)
            {
                value = context.@object().Accept(this);
            }
            else if (context.array() != null)
            {
                value = context.array().Accept(this);
            }

            _target.Declare(varName, value);
            return value;
        }

        #endregion

        #region Assignment Operation

        protected object NumberAssignmentOperation(object oldValue, object newValue, int tokenType)
        {
            bool hasDot = newValue.ToString().Contains(".") || newValue is float;

            switch (tokenType)
            {
                case MegaScryptParser.PlusEq:
                    {
                        if (hasDot)
                        {

                            return FloatOperation(oldValue, newValue, MegaScryptParser.Add);
                        }
                        else
                        {
                            return IntegerOperation(oldValue, newValue, MegaScryptParser.Add);
                        }
                    }

                case MegaScryptParser.MinusEq:
                    {
                        if (hasDot)
                        {
                            return FloatOperation(oldValue, newValue, MegaScryptParser.Subtract);
                        }
                        else
                        {
                            return IntegerOperation(oldValue, newValue, MegaScryptParser.Subtract);
                        }
                    }

                case MegaScryptParser.MultiplyEq:
                    {
                        if (hasDot)
                        {
                            return FloatOperation(oldValue, newValue, MegaScryptParser.Multiply);
                        }
                        else
                        {
                            return IntegerOperation(oldValue, newValue, MegaScryptParser.Multiply);
                        }
                    }

                case MegaScryptParser.DivideEq:
                    {
                        if (hasDot)
                        {
                            return FloatOperation(oldValue, newValue, MegaScryptParser.Divide);
                        }
                        else
                        {
                            return IntegerOperation(oldValue, newValue, MegaScryptParser.Divide);
                        }
                    }
            }

            throw new InvalidOperationException($"Invalid Operation: {tokenType} for Number Assignment Operation");
        }

        public override object VisitAssignment([NotNull] MegaScryptParser.AssignmentContext context)
        {
            string varName = "";
            object dictionary = _target;
            MegaScryptParser.ObjectAccessContext[] objectList = null;

            if (context.compoundId().objectGetter() != null)
            {
                objectList = context.compoundId().objectGetter().objectAccess();
                varName = objectList.Last().Id().Accept(this) as string;

                dictionary = GetParentDict(objectList);
            }
            else if (context.compoundId().Id() != null)
            {
                varName = context.compoundId().Id().Accept(this) as string;
            }

            object newValue = context.children[2].Accept(this);

            ITerminalNode operatorNode = context.children[1] as ITerminalNode;
            switch (operatorNode.Symbol.Type)
            {
                case MegaScryptParser.PlusEq:
                    {
                        if (context.expression().String() != null)
                        {
                            string value = context.compoundId().Accept(this).ToString();
                            string targetString = value + newValue.ToString();
                            newValue = targetString;
                        }
                        else
                        {
                            object oldValue = context.compoundId().Accept(this);
                            newValue = NumberAssignmentOperation(oldValue, newValue, MegaScryptParser.PlusEq);
                        }
                    }
                    break;

                case MegaScryptParser.MinusEq:
                case MegaScryptParser.MultiplyEq:
                case MegaScryptParser.DivideEq:
                    {
                        object oldValue = context.compoundId().Accept(this);
                        newValue = NumberAssignmentOperation(oldValue, newValue, operatorNode.Symbol.Type);
                    }
                    break;
            }

            if (context.compoundId().Id() != null)
            {
                ((Object)dictionary).Assign(varName, newValue);
            }
            else
            {
                if (objectList.Last().arrayIndex() == null)
                {
                    ((Object)dictionary).Assign(varName, newValue);
                }
                else
                {
                    Array array = (Array)((Object)dictionary).Get(varName);
                    int index = Convert.ToInt32(objectList.Last().arrayIndex().expression().Accept(this));
                    array[index] = newValue;
                }
            }

            return newValue;
        }

        #endregion

        #region Expression Region

        private object BinaryOperation(object oa, object ob, int operation)
        {
            if (oa is int && ob is int)
            {
                return IntegerBoolOperation(oa, ob, operation);
            }
            else if (oa is bool && ob is bool)
            {
                return BooleanBoolOperation(oa, ob, operation);
            }
            else if (oa is string && ob is string)
            {
                return StringBoolOperation(oa.ToString(), ob.ToString(), operation);
            }
            else
            {
                return FloatBoolOperation(oa, ob, operation);
            }
        }

        public override object VisitExpression([NotNull] MegaScryptParser.ExpressionContext context)
        {
            int length = context.children.Count;
            var expressions = context.expression();

            if (context.children.Count == 1)
            {
                return context.children[0].Accept(this);
            }
            else if (context.LParen() != null && context.RParen() != null && length == 3)
            {
                return context.children[1].Accept(this);
            }
            else if (context.expression().Length == 1)
            {
                object result = expressions[0].Accept(this);
                if (context.Subtract() != null)
                {
                    if (result is int)
                    {
                        return -(int)result;
                    }
                    else
                    {
                        return -(float)result;
                    }
                }
                else if (context.Excl() != null)
                {
                    if (result is bool)
                    {
                        return !(bool)result;
                    }
                }
                else
                {
                    return result;
                }
            }
            else if (context.Multiply() != null)
            {
                object resultA = expressions[0].Accept(this);
                object resultB = expressions[1].Accept(this);

                if (resultA is int && resultB is int)
                {
                    return IntegerOperation(resultA, resultB, MegaScryptParser.Multiply);
                }
                else if ((resultA is float && resultB is float) ||
                        (resultA is int && resultB is float) ||
                        (resultA is float && resultB is int))
                {
                    return FloatOperation(resultA, resultB, MegaScryptParser.Multiply);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid Operation Being Performed For {context.children[1].GetText()}");
                }
            }
            else if (context.Divide() != null)
            {
                object resultA = expressions[0].Accept(this);
                object resultB = expressions[1].Accept(this);

                if (resultA is int && resultB is int)
                {
                    return IntegerOperation(resultA, resultB, MegaScryptParser.Divide);
                }
                else if ((resultA is float && resultB is float) ||
                        (resultA is int && resultB is float) ||
                        (resultA is float && resultB is int))
                {
                    return FloatOperation(resultA, resultB, MegaScryptParser.Divide);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid Operation Being Performed For {context.children[1].GetText()}");
                }
            }
            else if (context.Modulus() != null)
            {
                object resultA = expressions[0].Accept(this);
                object resultB = expressions[1].Accept(this);

                if (resultA is int && resultB is int)
                {
                    return IntegerOperation(resultA, resultB, MegaScryptParser.Modulus);
                }
                else if ((resultA is float && resultB is float) ||
                        (resultA is int && resultB is float) ||
                        (resultA is float && resultB is int))
                {
                    return FloatOperation(resultA, resultB, MegaScryptParser.Modulus);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid Operation Being Performed For {context.children[1].GetText()}");
                }
            }
            else if (context.Add() != null)
            {
                object resultA = expressions[0].Accept(this);
                object resultB = expressions[1].Accept(this);

                if (resultA is int && resultB is int)
                {
                    return IntegerOperation(resultA, resultB, MegaScryptParser.Add);
                }
                else if ((resultA is float && resultB is float) ||
                        (resultA is int && resultB is float) ||
                        (resultA is float && resultB is int))
                {
                    return FloatOperation(resultA, resultB, MegaScryptParser.Add);
                }
                else if (resultA is string || resultB is string)
                {
                    return resultA.ToString() + resultB.ToString();
                }
                else
                {
                    throw new InvalidOperationException($"Invalid Operation Being Performed For {context.children[1].GetText()}");
                }
            }
            else if (context.Subtract() != null)
            {
                object resultA = expressions[0].Accept(this);
                object resultB = expressions[1].Accept(this);

                if (resultA is int && resultB is int)
                {
                    return IntegerOperation(resultA, resultB, MegaScryptParser.Subtract);
                }
                else if ((resultA is float && resultB is float) ||
                        (resultA is int && resultB is float) ||
                        (resultA is float && resultB is int))
                {
                    return FloatOperation(resultA, resultB, MegaScryptParser.Subtract);
                }
                else
                {
                    throw new InvalidOperationException($"Invalid Operation Being Performed For {context.children[1].GetText()}");
                }
            }
            else if (expressions.Length == 2)
            {
                object a = expressions[0].Accept(this);
                object b = expressions[1].Accept(this);
                ITerminalNode operatorNode = context.children[1] as ITerminalNode;

                return BinaryOperation(a, b, operatorNode.Symbol.Type);
            }
            else if (context.String() != null)
            {
                return context.GetText();
            }
            else if (context.Null() != null)
            {
                return null;
            }
            else if (context.Bool() != null)
            {
                string boolData = context.GetText();
                return StringToBool(boolData);
            }


            throw new NotImplementedException($"Invalid Operation for Expression");
        }

        #endregion

        #region Looping Region

        public override object VisitForStmt([NotNull] MegaScryptParser.ForStmtContext context)
        {
            Object prevTarget = _target;
            _target = new Object(prevTarget);

            ITerminalNode[] idList = context.Id();
            var expressions = context.expression();

            string varName = idList[0].Accept(this) as string;
            _target.Declare(varName, expressions[0].Accept(this));

            while (true)
            {
                bool exitWhile = false;

                string secondVarName = idList[1].Accept(this) as string;

                float secondVarValue = Convert.ToSingle(_target.Get(secondVarName));
                float expressionValue = Convert.ToSingle(expressions[1].Accept(this));

                ITerminalNode symbol = context.children[8] as ITerminalNode;
                switch (symbol.Symbol.Type)
                {
                    case MegaScryptParser.GTEquals:
                        if (secondVarValue < expressionValue)
                        {
                            exitWhile = true;
                        }
                        break;

                    case MegaScryptParser.LTEquals:
                        if (secondVarValue > expressionValue)
                        {
                            exitWhile = true;
                        }
                        break;

                    case MegaScryptParser.GT:
                        if (secondVarValue <= expressionValue)
                        {
                            exitWhile = true;
                        }
                        break;

                    case MegaScryptParser.LT:
                        if (secondVarValue >= expressionValue)
                        {
                            exitWhile = true;
                        }
                        break;
                }

                if (exitWhile || _lastReturnType == ReturnType.Break)
                {
                    _returnProcessed = true;
                    break;
                }
                else if (_lastReturnType == ReturnType.Continue)
                {
                    _returnProcessed = true;
                }

                context.block().Accept(this);

                if (context.increment() != null)
                {
                    context.increment().Accept(this);
                }
                else if (context.decrement() != null)
                {
                    context.decrement().Accept(this);
                }
            }

            _target = prevTarget;

            return null;
        }

        public override object VisitWhileStmt([NotNull] MegaScryptParser.WhileStmtContext context)
        {
            while ((bool)context.expression().Accept(this))
            {
                if (_lastReturnType == ReturnType.Break)
                {
                    _returnProcessed = true;
                    break;
                }
                else if (_lastReturnType == ReturnType.Continue)
                {
                    _returnProcessed = true;
                }

                context.block().Accept(this);
            }

            return null;
        }

        public override object VisitDoWhileStmt([NotNull] MegaScryptParser.DoWhileStmtContext context)
        {
            do
            {
                if (_lastReturnType == ReturnType.Break)
                {
                    _returnProcessed = true;
                    break;
                }
                else if (_lastReturnType == ReturnType.Continue)
                {
                    _returnProcessed = true;
                }

                context.block().Accept(this);

            } while ((bool)context.expression().Accept(this));

            return null;
        }

        public override object VisitForeachStmt([NotNull] MegaScryptParser.ForeachStmtContext context)
        {
            Array array = (Array)context.compoundId().Accept(this);

            Object prevTarget = _target;
            _target = new Object(prevTarget);

            string varName = context.Id().Accept(this) as string;
            _target.Declare(varName, array[0]);

            foreach (var data in array)
            {
                if (_lastReturnType == ReturnType.Break)
                {
                    _returnProcessed = true;
                    break;
                }
                else if (_lastReturnType == ReturnType.Continue)
                {
                    _returnProcessed = true;
                }

                _target.Assign(varName, data);
                context.block().Accept(this);
            }

            _target = prevTarget;

            return null;
        }

        #endregion

        #region Block And Scope Region

        public override object VisitBlock([NotNull] MegaScryptParser.BlockContext context)
        {
            // Push New Target, Creating Local Scope
            Object prevTarget = _target;
            _target = new Object(prevTarget);

            var statements = context.statement();
            object lastReturnValue = null;

            foreach (MegaScryptParser.StatementContext statement in statements)
            {
                if (statement.Break() != null)
                {
                    _lastReturnType = ReturnType.Break;
                    _returnProcessed = false;
                    break;
                }
                else if (statement.Continue() != null)
                {
                    _lastReturnType = ReturnType.Continue;
                    _returnProcessed = false;
                    break;
                }
                else
                {
                    if (!_returnProcessed)
                    {
                        break;
                    }

                    _lastReturnType = ReturnType.None;
                    lastReturnValue = statement.Accept(this);

                }
            }

            // Pop Target
            _target = prevTarget;
            return lastReturnValue;
        }

        public override object VisitIfStatement([NotNull] MegaScryptParser.IfStatementContext context)
        {
            // Process If
            object ifResult = context.ifBlock().Accept(this);
            if (ifResult != null)
            {
                return ifResult;
            }

            // Process Else If
            var elseIfs = context.elseIfBlock();
            foreach (var elseif in elseIfs)
            {
                object elseifResult = elseif.Accept(this);
                if (elseifResult != null)
                {
                    return elseifResult;
                }
            }

            // Process Else
            if (context.elseBlock() != null)
            {
                return context.elseBlock().Accept(this);
            }

            return null;
        }

        public override object VisitIfBlock([NotNull] MegaScryptParser.IfBlockContext context)
        {
            object result = context.expression().Accept(this);
            bool value = (bool)result;

            if (value)
            {
                object ret = context.block().Accept(this);
                return ret;
            }

            return null;
        }

        public override object VisitElseIfBlock([NotNull] MegaScryptParser.ElseIfBlockContext context)
        {
            object result = context.expression().Accept(this);
            bool value = (bool)result;

            if (value)
            {
                object ret = context.block().Accept(this);
                return ret;
            }

            return null;
        }

        public override object VisitElseBlock([NotNull] MegaScryptParser.ElseBlockContext context)
        {
            return context.block().Accept(this);
        }

        #endregion

        #region Statement Region

        public override object VisitStatement([NotNull] MegaScryptParser.StatementContext context)
        {
            if (_returned)
            {
                return _lastReturnValue;
            }

            return base.VisitStatement(context);
        }

        #endregion

        #region Object Region

        private object GetParentDict(MegaScryptParser.ObjectAccessContext[] objectList)
        {
            object currentDict = _target;

            for (int i = 0; i < objectList.Length - 1; i++)
            {
                var @object = objectList[i];
                if (@object.arrayIndex() == null)
                {
                    object value = ((Object)currentDict).Get(@object.Accept(this) as string);
                    currentDict = value;
                }
                else
                {
                    int indexValue = Convert.ToInt32(@object.arrayIndex().expression().Accept(this));
                    object value = ((Object)currentDict).Get(@object.Id().Accept(this) as string);

                    Array array = (Array)value;
                    currentDict = array[indexValue];
                }
            }

            return currentDict;
        }

        public override object VisitObjectGetter([NotNull] MegaScryptParser.ObjectGetterContext context)
        {
            var accessList = context.objectAccess();
            object currentDict = _target;

            foreach (var @object in accessList)
            {
                if (@object.arrayIndex() == null)
                {
                    object value = ((Object)currentDict).Get(@object.Accept(this) as string);
                    currentDict = value;
                }
                else
                {
                    int indexValue = Convert.ToInt32(@object.arrayIndex().expression().Accept(this));
                    object value = ((Object)currentDict).Get(@object.Id().Accept(this) as string);

                    Array array = (Array)value;
                    currentDict = array[indexValue];
                }
            }

            return currentDict;
        }

        public override object VisitObject([NotNull] MegaScryptParser.ObjectContext context)
        {
            var objectPairs = context.objectPair();
            Object objectDict = new Object(_target);

            foreach (var objectPair in objectPairs)
            {
                object objectValue = objectPair.Accept(this);
                string id = objectPair.Id().Accept(this) as string;

                objectDict.Declare(id, objectValue);
            }

            return objectDict;
        }

        public override object VisitObjectPair([NotNull] MegaScryptParser.ObjectPairContext context)
        {
            if (context.expression() != null)
            {
                return context.expression().Accept(this);
            }
            else if (context.@object() != null)
            {
                return context.@object().Accept(this);
            }

            throw new InvalidOperationException("Invalid Operation Requested for Object Pair");
        }

        #endregion

        #region Array Region

        public override object VisitArray([NotNull] MegaScryptParser.ArrayContext context)
        {
            Array array = new Array();
            if (context.paramList() != null)
            {
                array = new Array(context.paramList().Accept(this) as List<object>);
            }

            return array;
        }

        public override object VisitArrayIndex([NotNull] MegaScryptParser.ArrayIndexContext context) => context.expression().Accept(this);

        #endregion

        #region Function Region

        public override object VisitFuncDecleration([NotNull] MegaScryptParser.FuncDeclerationContext context)
        {
            ScriptFunction function = new ScriptFunction(this, Invoke, context);
            return function;
        }

        public object Invoke(ScriptFunction function, List<object> parameters, InvocationContext ctx)
        {
            // Push New Scope
            Object prevTarget = _target;
            Object parentScope = ctx != null && ctx.Container != null ? ctx.Container : prevTarget;
            _target = new Object(parentScope);

            // Declare Parameter Variables
            if (parameters != null)
            {
                if (function.ParameterNames != null && function.ParameterNames.Count != parameters.Count)
                {
                    throw new InvalidOperationException($"Function {function.Name} expected {function.ParameterNames.Count} but received {parameters.Count}");
                }

                for (int i = 0; i < function.ParameterNames.Count; i++)
                {
                    _target.Declare(function.ParameterNames[i], parameters[i]);
                }
            }

            _lastReturnValue = null;
            _returned = false;

            // Execute Body
            base.VisitFuncDecleration(function.DeclContext);

            // Pop Scope
            _target = prevTarget;

            // Return value
            object ret = _lastReturnValue;
            _lastReturnValue = null;
            _returned = false;

            return ret;
        }

        public override object VisitVarList([NotNull] MegaScryptParser.VarListContext context)
        {
            List<string> varList = new List<string>();
            ITerminalNode[] ids = context.Id();

            foreach (var id in ids)
            {
                string value = id.Accept(this) as string;
                varList.Add(value);
            }

            return varList;
        }


        private object _lastReturnValue = null;
        private bool _returned = false;
        public override object VisitReturnStmt([NotNull] MegaScryptParser.ReturnStmtContext context)
        {
            if (context.expression() != null)
            {
                _lastReturnValue = context.expression().Accept(this);
            }
            else
            {
                _lastReturnValue = null;
            }
            _returned = true;

            return _lastReturnValue;
        }

        public override object VisitInvocation([NotNull] MegaScryptParser.InvocationContext context)
        {
            IFunction function = context.compoundId().Accept(this) as IFunction;
            Object container = null;

            if (context.compoundId().Id() == null)
            {
                var objectList = context.compoundId().objectGetter().objectAccess();
                container = (Object)GetParentDict(objectList);
            }
            else if (context.compoundId().Id() != null)
            {
                container = _target;
            }

            if (function == null)
            {
                throw new InvalidOperationException($"Invalid Function Call: {context.compoundId().GetText()}");
            }

            List<object> parameters = new List<object>();
            if (context.paramList() != null)
            {
                parameters = context.paramList().Accept(this) as List<object>;
            }

            InvocationContext invCtx = new InvocationContext(container);
            return function.Invoke(parameters, invCtx);
        }

        public override object VisitParamList([NotNull] MegaScryptParser.ParamListContext context)
        {
            List<object> parameters = new List<object>();

            MegaScryptParser.ExpressionContext[] expressions = context.expression();
            foreach (var expression in expressions)
            {
                object result = expression.Accept(this);
                parameters.Add(result);
            }

            return parameters;
        }

        #endregion

        #region Utility Functions

        public int IntegerOperation(object oa, object ob, int operation)
        {
            int a = Convert.ToInt32(oa);
            int b = Convert.ToInt32(ob);

            switch (operation)
            {
                case MegaScryptParser.Add:
                    return a + b;

                case MegaScryptParser.Subtract:
                    return a - b;

                case MegaScryptParser.Multiply:
                    return a * b;

                case MegaScryptParser.Divide:
                    return a / b;

                case MegaScryptParser.Modulus:
                    return a % b;
            }

            throw new InvalidOperationException($"Invalid Operation {operation} for Integer Operation");
        }

        public object IntegerBoolOperation(object oa, object ob, int operation)
        {
            int a = Convert.ToInt32(oa);
            int b = Convert.ToInt32(ob);

            switch (operation)
            {
                case MegaScryptParser.Or:
                    return a != 0 || b != 0;

                case MegaScryptParser.And:
                    return a != 0 && b != 0;

                case MegaScryptParser.Equals:
                    return a == b;

                case MegaScryptParser.NEquals:
                    return a != b;

                case MegaScryptParser.GTEquals:
                    return a >= b;

                case MegaScryptParser.LTEquals:
                    return a <= b;

                case MegaScryptParser.GT:
                    return a > b;

                case MegaScryptParser.LT:
                    return a < b;
            }

            throw new InvalidOperationException("Invalid Operation for Integer Bool Operation");
        }

        public float FloatOperation(object oa, object ob, int operation)
        {
            float a = Convert.ToSingle(oa);
            float b = Convert.ToSingle(ob);

            switch (operation)
            {
                case MegaScryptParser.Add:
                    return a + b;

                case MegaScryptParser.Subtract:
                    return a - b;

                case MegaScryptParser.Multiply:
                    return a * b;

                case MegaScryptParser.Divide:
                    return a / b;

                case MegaScryptParser.Modulus:
                    return a % b;
            }

            throw new InvalidOperationException($"Invalid Operation {operation} for Float Operation");
        }

        public object FloatBoolOperation(object oa, object ob, int operation)
        {
            float a = Convert.ToSingle(oa);
            float b = Convert.ToSingle(ob);

            switch (operation)
            {
                case MegaScryptParser.Or:
                    return a != 0 || b != 0;

                case MegaScryptParser.And:
                    return a != 0 && b != 0;

                case MegaScryptParser.Equals:
                    return a == b;

                case MegaScryptParser.NEquals:
                    return a != b;

                case MegaScryptParser.GTEquals:
                    return a >= b;

                case MegaScryptParser.LTEquals:
                    return a <= b;

                case MegaScryptParser.GT:
                    return a > b;

                case MegaScryptParser.LT:
                    return a < b;
            }

            throw new InvalidOperationException("Invalid Operation for Float Bool Operation");
        }

        public bool StringToBool(string boolText)
        {
            if (boolText == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object StringBoolOperation(string a, string b, int operation)
        {
            switch (operation)
            {
                case MegaScryptParser.Or:
                    return !string.IsNullOrEmpty(a) || !string.IsNullOrEmpty(b);

                case MegaScryptParser.And:
                    return !string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b);

                case MegaScryptParser.Equals:
                    return a.CompareTo(b) == 0;

                case MegaScryptParser.NEquals:
                    return a.CompareTo(b) != 0;

                case MegaScryptParser.GTEquals:
                    return a.CompareTo(b) >= 0;

                case MegaScryptParser.LTEquals:
                    return a.CompareTo(b) <= 0;

                case MegaScryptParser.GT:
                    return a.CompareTo(b) > 0;

                case MegaScryptParser.LT:
                    return a.CompareTo(b) < 0;
            }

            throw new InvalidOperationException("Invalid Operation for String Bool Operation");
        }

        public object BooleanBoolOperation(object oa, object ob, int operation)
        {
            bool a = (bool)oa;
            bool b = (bool)ob;

            switch (operation)
            {
                case MegaScryptParser.Or:
                    return a || b;

                case MegaScryptParser.And:
                    return a && b;

                case MegaScryptParser.Equals:
                    return a == b;

                case MegaScryptParser.NEquals:
                    return a != b;
            }

            throw new InvalidOperationException("Invalid Operation for String Bool Operation");
        }

        #endregion

        #region Enums

        private ReturnType _lastReturnType;
        private bool _returnProcessed = true;

        private enum ReturnType
        {
            None,
            Break,
            Continue
        }

        #endregion
    }
}
