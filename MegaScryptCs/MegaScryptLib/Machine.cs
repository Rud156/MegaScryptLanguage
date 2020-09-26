using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace MegaScrypt
{
    public class Machine
    {
        private Processor _processor;
        private Object _target;

        public Object Target => _target;

        public Machine()
        {
            _processor = new Processor();
            _target = new Object();
            _processor.Target = _target;
        }

        public object Execute(string script)
        {
            AntlrInputStream input = new AntlrInputStream(script);
            MegaScryptLexer lexer = new MegaScryptLexer(input);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            MegaScryptParser parser = new MegaScryptParser(tokenStream);
            parser.AddErrorListener(new ThrowErrorListener());

            MegaScryptParser.ProgramContext root = parser.program();
            object result = root.Accept(_processor);
            return result;

        }

        public object Evaluate(string expression)
        {
            AntlrInputStream input = new AntlrInputStream(expression);
            MegaScryptLexer lexer = new MegaScryptLexer(input);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            MegaScryptParser parser = new MegaScryptParser(tokenStream);

            MegaScryptParser.ExpressionContext root = parser.expression();
            object result = root.Accept(_processor);
            return result;
        }

        public void Declare(NativeFunction function)
        {
            _target.Declare(function.Name, function);
        }

        public void Declare(NativeFunction.Callback callback, IEnumerable<string> parameterNames = null)
        {
            NativeFunction function = new NativeFunction(callback, parameterNames);
            _target.Declare(function.Name, function);
        }

        public void Declare(string varName, object value)
        {
            _target.Declare(varName, value);
        }
    }

    class ThrowErrorListener : BaseErrorListener, IAntlrErrorListener<int>
    {
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new InvalidOperationException(null);
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new InvalidOperationException(null);
        }
    }
}
