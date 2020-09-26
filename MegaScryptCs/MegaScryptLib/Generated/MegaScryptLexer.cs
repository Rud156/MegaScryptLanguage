//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\MegaScrypt.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class MegaScryptLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, Var=2, Function=3, Return=4, For=5, Do=6, While=7, In=8, Break=9, 
		Continue=10, If=11, Else=12, Null=13, Bool=14, Or=15, And=16, Equals=17, 
		NEquals=18, GTEquals=19, LTEquals=20, Excl=21, GT=22, LT=23, Add=24, Subtract=25, 
		Multiply=26, Divide=27, Modulus=28, LBrace=29, RBrace=30, Assign=31, Semicolon=32, 
		Underscore=33, Colon=34, LParen=35, RParen=36, LBracket=37, Rbracket=38, 
		PlusEq=39, MinusEq=40, MultiplyEq=41, DivideEq=42, Dot=43, Increment=44, 
		Decrement=45, Comma=46, Id=47, Number=48, Whitespace=49, Comment=50, String=51;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "Letter", "Digit", "EQUOTE", "Var", "Function", "Return", "For", 
		"Do", "While", "In", "Break", "Continue", "If", "Else", "Null", "Bool", 
		"Or", "And", "Equals", "NEquals", "GTEquals", "LTEquals", "Excl", "GT", 
		"LT", "Add", "Subtract", "Multiply", "Divide", "Modulus", "LBrace", "RBrace", 
		"Assign", "Semicolon", "Underscore", "Colon", "LParen", "RParen", "LBracket", 
		"Rbracket", "PlusEq", "MinusEq", "MultiplyEq", "DivideEq", "Dot", "Increment", 
		"Decrement", "Comma", "Id", "Number", "Whitespace", "Comment", "String"
	};


	public MegaScryptLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public MegaScryptLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'foreach'", "'var'", "'function'", "'return'", "'for'", "'do'", 
		"'while'", "'in'", "'break'", "'continue'", "'if'", "'else'", "'null'", 
		null, "'||'", "'&&'", "'=='", "'!='", "'>='", "'<='", "'!'", "'>'", "'<'", 
		"'+'", "'-'", "'*'", "'/'", "'%'", "'{'", "'}'", "'='", "';'", "'_'", 
		"':'", "'('", "')'", "'['", "']'", "'+='", "'-='", "'*='", "'/='", "'.'", 
		"'++'", "'--'", "','"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, "Var", "Function", "Return", "For", "Do", "While", "In", "Break", 
		"Continue", "If", "Else", "Null", "Bool", "Or", "And", "Equals", "NEquals", 
		"GTEquals", "LTEquals", "Excl", "GT", "LT", "Add", "Subtract", "Multiply", 
		"Divide", "Modulus", "LBrace", "RBrace", "Assign", "Semicolon", "Underscore", 
		"Colon", "LParen", "RParen", "LBracket", "Rbracket", "PlusEq", "MinusEq", 
		"MultiplyEq", "DivideEq", "Dot", "Increment", "Decrement", "Comma", "Id", 
		"Number", "Whitespace", "Comment", "String"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "MegaScrypt.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static MegaScryptLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x35', '\x159', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x4', ')', 
		'\t', ')', '\x4', '*', '\t', '*', '\x4', '+', '\t', '+', '\x4', ',', '\t', 
		',', '\x4', '-', '\t', '-', '\x4', '.', '\t', '.', '\x4', '/', '\t', '/', 
		'\x4', '\x30', '\t', '\x30', '\x4', '\x31', '\t', '\x31', '\x4', '\x32', 
		'\t', '\x32', '\x4', '\x33', '\t', '\x33', '\x4', '\x34', '\t', '\x34', 
		'\x4', '\x35', '\t', '\x35', '\x4', '\x36', '\t', '\x36', '\x4', '\x37', 
		'\t', '\x37', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', 
		'\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', 
		'\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', 
		'\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', 
		'\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', 
		'\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', 
		'\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\r', 
		'\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', 
		'\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', 
		'\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', 
		'\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', 
		'\x12', '\x5', '\x12', '\xC8', '\n', '\x12', '\x3', '\x13', '\x3', '\x13', 
		'\x3', '\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', 
		'\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x18', '\x3', '\x18', 
		'\x3', '\x18', '\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', '\x3', '\x1A', 
		'\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', 
		'\x3', '\x1D', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', 
		'\x3', ' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x3', '\"', '\x3', '\"', 
		'\x3', '#', '\x3', '#', '\x3', '$', '\x3', '$', '\x3', '%', '\x3', '%', 
		'\x3', '&', '\x3', '&', '\x3', '\'', '\x3', '\'', '\x3', '(', '\x3', '(', 
		'\x3', ')', '\x3', ')', '\x3', '*', '\x3', '*', '\x3', '+', '\x3', '+', 
		'\x3', '+', '\x3', ',', '\x3', ',', '\x3', ',', '\x3', '-', '\x3', '-', 
		'\x3', '-', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', '/', '\x3', '/', 
		'\x3', '\x30', '\x3', '\x30', '\x3', '\x30', '\x3', '\x31', '\x3', '\x31', 
		'\x3', '\x31', '\x3', '\x32', '\x3', '\x32', '\x3', '\x33', '\x3', '\x33', 
		'\x5', '\x33', '\x118', '\n', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', 
		'\x33', '\a', '\x33', '\x11D', '\n', '\x33', '\f', '\x33', '\xE', '\x33', 
		'\x120', '\v', '\x33', '\x3', '\x34', '\x6', '\x34', '\x123', '\n', '\x34', 
		'\r', '\x34', '\xE', '\x34', '\x124', '\x3', '\x34', '\x3', '\x34', '\a', 
		'\x34', '\x129', '\n', '\x34', '\f', '\x34', '\xE', '\x34', '\x12C', '\v', 
		'\x34', '\x5', '\x34', '\x12E', '\n', '\x34', '\x3', '\x35', '\a', '\x35', 
		'\x131', '\n', '\x35', '\f', '\x35', '\xE', '\x35', '\x134', '\v', '\x35', 
		'\x3', '\x35', '\x3', '\x35', '\x3', '\x36', '\x3', '\x36', '\x3', '\x36', 
		'\x3', '\x36', '\a', '\x36', '\x13C', '\n', '\x36', '\f', '\x36', '\xE', 
		'\x36', '\x13F', '\v', '\x36', '\x3', '\x36', '\x3', '\x36', '\x3', '\x36', 
		'\x3', '\x36', '\a', '\x36', '\x145', '\n', '\x36', '\f', '\x36', '\xE', 
		'\x36', '\x148', '\v', '\x36', '\x3', '\x36', '\x3', '\x36', '\x5', '\x36', 
		'\x14C', '\n', '\x36', '\x3', '\x36', '\x3', '\x36', '\x3', '\x37', '\x3', 
		'\x37', '\x3', '\x37', '\a', '\x37', '\x153', '\n', '\x37', '\f', '\x37', 
		'\xE', '\x37', '\x156', '\v', '\x37', '\x3', '\x37', '\x3', '\x37', '\x4', 
		'\x146', '\x154', '\x2', '\x38', '\x3', '\x3', '\x5', '\x2', '\a', '\x2', 
		'\t', '\x2', '\v', '\x4', '\r', '\x5', '\xF', '\x6', '\x11', '\a', '\x13', 
		'\b', '\x15', '\t', '\x17', '\n', '\x19', '\v', '\x1B', '\f', '\x1D', 
		'\r', '\x1F', '\xE', '!', '\xF', '#', '\x10', '%', '\x11', '\'', '\x12', 
		')', '\x13', '+', '\x14', '-', '\x15', '/', '\x16', '\x31', '\x17', '\x33', 
		'\x18', '\x35', '\x19', '\x37', '\x1A', '\x39', '\x1B', ';', '\x1C', '=', 
		'\x1D', '?', '\x1E', '\x41', '\x1F', '\x43', ' ', '\x45', '!', 'G', '\"', 
		'I', '#', 'K', '$', 'M', '%', 'O', '&', 'Q', '\'', 'S', '(', 'U', ')', 
		'W', '*', 'Y', '+', '[', ',', ']', '-', '_', '.', '\x61', '/', '\x63', 
		'\x30', '\x65', '\x31', 'g', '\x32', 'i', '\x33', 'k', '\x34', 'm', '\x35', 
		'\x3', '\x2', '\x6', '\x4', '\x2', '\x43', '\\', '\x63', '|', '\x3', '\x2', 
		'\x32', ';', '\x5', '\x2', '\v', '\f', '\xF', '\xF', '\"', '\"', '\x4', 
		'\x2', '\f', '\f', '\xF', '\xF', '\x2', '\x163', '\x2', '\x3', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', 
		'\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x45', '\x3', '\x2', '\x2', '\x2', '\x2', 'G', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'I', '\x3', '\x2', '\x2', '\x2', '\x2', 'K', '\x3', '\x2', '\x2', 
		'\x2', '\x2', 'M', '\x3', '\x2', '\x2', '\x2', '\x2', 'O', '\x3', '\x2', 
		'\x2', '\x2', '\x2', 'Q', '\x3', '\x2', '\x2', '\x2', '\x2', 'S', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'U', '\x3', '\x2', '\x2', '\x2', '\x2', 'W', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'Y', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'[', '\x3', '\x2', '\x2', '\x2', '\x2', ']', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '_', '\x3', '\x2', '\x2', '\x2', '\x2', '\x61', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x63', '\x3', '\x2', '\x2', '\x2', '\x2', '\x65', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'g', '\x3', '\x2', '\x2', '\x2', '\x2', 'i', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'k', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'm', '\x3', '\x2', '\x2', '\x2', '\x3', 'o', '\x3', '\x2', '\x2', '\x2', 
		'\x5', 'w', '\x3', '\x2', '\x2', '\x2', '\a', 'y', '\x3', '\x2', '\x2', 
		'\x2', '\t', '{', '\x3', '\x2', '\x2', '\x2', '\v', '~', '\x3', '\x2', 
		'\x2', '\x2', '\r', '\x82', '\x3', '\x2', '\x2', '\x2', '\xF', '\x8B', 
		'\x3', '\x2', '\x2', '\x2', '\x11', '\x92', '\x3', '\x2', '\x2', '\x2', 
		'\x13', '\x96', '\x3', '\x2', '\x2', '\x2', '\x15', '\x99', '\x3', '\x2', 
		'\x2', '\x2', '\x17', '\x9F', '\x3', '\x2', '\x2', '\x2', '\x19', '\xA2', 
		'\x3', '\x2', '\x2', '\x2', '\x1B', '\xA8', '\x3', '\x2', '\x2', '\x2', 
		'\x1D', '\xB1', '\x3', '\x2', '\x2', '\x2', '\x1F', '\xB4', '\x3', '\x2', 
		'\x2', '\x2', '!', '\xB9', '\x3', '\x2', '\x2', '\x2', '#', '\xC7', '\x3', 
		'\x2', '\x2', '\x2', '%', '\xC9', '\x3', '\x2', '\x2', '\x2', '\'', '\xCC', 
		'\x3', '\x2', '\x2', '\x2', ')', '\xCF', '\x3', '\x2', '\x2', '\x2', '+', 
		'\xD2', '\x3', '\x2', '\x2', '\x2', '-', '\xD5', '\x3', '\x2', '\x2', 
		'\x2', '/', '\xD8', '\x3', '\x2', '\x2', '\x2', '\x31', '\xDB', '\x3', 
		'\x2', '\x2', '\x2', '\x33', '\xDD', '\x3', '\x2', '\x2', '\x2', '\x35', 
		'\xDF', '\x3', '\x2', '\x2', '\x2', '\x37', '\xE1', '\x3', '\x2', '\x2', 
		'\x2', '\x39', '\xE3', '\x3', '\x2', '\x2', '\x2', ';', '\xE5', '\x3', 
		'\x2', '\x2', '\x2', '=', '\xE7', '\x3', '\x2', '\x2', '\x2', '?', '\xE9', 
		'\x3', '\x2', '\x2', '\x2', '\x41', '\xEB', '\x3', '\x2', '\x2', '\x2', 
		'\x43', '\xED', '\x3', '\x2', '\x2', '\x2', '\x45', '\xEF', '\x3', '\x2', 
		'\x2', '\x2', 'G', '\xF1', '\x3', '\x2', '\x2', '\x2', 'I', '\xF3', '\x3', 
		'\x2', '\x2', '\x2', 'K', '\xF5', '\x3', '\x2', '\x2', '\x2', 'M', '\xF7', 
		'\x3', '\x2', '\x2', '\x2', 'O', '\xF9', '\x3', '\x2', '\x2', '\x2', 'Q', 
		'\xFB', '\x3', '\x2', '\x2', '\x2', 'S', '\xFD', '\x3', '\x2', '\x2', 
		'\x2', 'U', '\xFF', '\x3', '\x2', '\x2', '\x2', 'W', '\x102', '\x3', '\x2', 
		'\x2', '\x2', 'Y', '\x105', '\x3', '\x2', '\x2', '\x2', '[', '\x108', 
		'\x3', '\x2', '\x2', '\x2', ']', '\x10B', '\x3', '\x2', '\x2', '\x2', 
		'_', '\x10D', '\x3', '\x2', '\x2', '\x2', '\x61', '\x110', '\x3', '\x2', 
		'\x2', '\x2', '\x63', '\x113', '\x3', '\x2', '\x2', '\x2', '\x65', '\x117', 
		'\x3', '\x2', '\x2', '\x2', 'g', '\x122', '\x3', '\x2', '\x2', '\x2', 
		'i', '\x132', '\x3', '\x2', '\x2', '\x2', 'k', '\x14B', '\x3', '\x2', 
		'\x2', '\x2', 'm', '\x14F', '\x3', '\x2', '\x2', '\x2', 'o', 'p', '\a', 
		'h', '\x2', '\x2', 'p', 'q', '\a', 'q', '\x2', '\x2', 'q', 'r', '\a', 
		't', '\x2', '\x2', 'r', 's', '\a', 'g', '\x2', '\x2', 's', 't', '\a', 
		'\x63', '\x2', '\x2', 't', 'u', '\a', '\x65', '\x2', '\x2', 'u', 'v', 
		'\a', 'j', '\x2', '\x2', 'v', '\x4', '\x3', '\x2', '\x2', '\x2', 'w', 
		'x', '\t', '\x2', '\x2', '\x2', 'x', '\x6', '\x3', '\x2', '\x2', '\x2', 
		'y', 'z', '\t', '\x3', '\x2', '\x2', 'z', '\b', '\x3', '\x2', '\x2', '\x2', 
		'{', '|', '\a', '^', '\x2', '\x2', '|', '}', '\a', '$', '\x2', '\x2', 
		'}', '\n', '\x3', '\x2', '\x2', '\x2', '~', '\x7F', '\a', 'x', '\x2', 
		'\x2', '\x7F', '\x80', '\a', '\x63', '\x2', '\x2', '\x80', '\x81', '\a', 
		't', '\x2', '\x2', '\x81', '\f', '\x3', '\x2', '\x2', '\x2', '\x82', '\x83', 
		'\a', 'h', '\x2', '\x2', '\x83', '\x84', '\a', 'w', '\x2', '\x2', '\x84', 
		'\x85', '\a', 'p', '\x2', '\x2', '\x85', '\x86', '\a', '\x65', '\x2', 
		'\x2', '\x86', '\x87', '\a', 'v', '\x2', '\x2', '\x87', '\x88', '\a', 
		'k', '\x2', '\x2', '\x88', '\x89', '\a', 'q', '\x2', '\x2', '\x89', '\x8A', 
		'\a', 'p', '\x2', '\x2', '\x8A', '\xE', '\x3', '\x2', '\x2', '\x2', '\x8B', 
		'\x8C', '\a', 't', '\x2', '\x2', '\x8C', '\x8D', '\a', 'g', '\x2', '\x2', 
		'\x8D', '\x8E', '\a', 'v', '\x2', '\x2', '\x8E', '\x8F', '\a', 'w', '\x2', 
		'\x2', '\x8F', '\x90', '\a', 't', '\x2', '\x2', '\x90', '\x91', '\a', 
		'p', '\x2', '\x2', '\x91', '\x10', '\x3', '\x2', '\x2', '\x2', '\x92', 
		'\x93', '\a', 'h', '\x2', '\x2', '\x93', '\x94', '\a', 'q', '\x2', '\x2', 
		'\x94', '\x95', '\a', 't', '\x2', '\x2', '\x95', '\x12', '\x3', '\x2', 
		'\x2', '\x2', '\x96', '\x97', '\a', '\x66', '\x2', '\x2', '\x97', '\x98', 
		'\a', 'q', '\x2', '\x2', '\x98', '\x14', '\x3', '\x2', '\x2', '\x2', '\x99', 
		'\x9A', '\a', 'y', '\x2', '\x2', '\x9A', '\x9B', '\a', 'j', '\x2', '\x2', 
		'\x9B', '\x9C', '\a', 'k', '\x2', '\x2', '\x9C', '\x9D', '\a', 'n', '\x2', 
		'\x2', '\x9D', '\x9E', '\a', 'g', '\x2', '\x2', '\x9E', '\x16', '\x3', 
		'\x2', '\x2', '\x2', '\x9F', '\xA0', '\a', 'k', '\x2', '\x2', '\xA0', 
		'\xA1', '\a', 'p', '\x2', '\x2', '\xA1', '\x18', '\x3', '\x2', '\x2', 
		'\x2', '\xA2', '\xA3', '\a', '\x64', '\x2', '\x2', '\xA3', '\xA4', '\a', 
		't', '\x2', '\x2', '\xA4', '\xA5', '\a', 'g', '\x2', '\x2', '\xA5', '\xA6', 
		'\a', '\x63', '\x2', '\x2', '\xA6', '\xA7', '\a', 'm', '\x2', '\x2', '\xA7', 
		'\x1A', '\x3', '\x2', '\x2', '\x2', '\xA8', '\xA9', '\a', '\x65', '\x2', 
		'\x2', '\xA9', '\xAA', '\a', 'q', '\x2', '\x2', '\xAA', '\xAB', '\a', 
		'p', '\x2', '\x2', '\xAB', '\xAC', '\a', 'v', '\x2', '\x2', '\xAC', '\xAD', 
		'\a', 'k', '\x2', '\x2', '\xAD', '\xAE', '\a', 'p', '\x2', '\x2', '\xAE', 
		'\xAF', '\a', 'w', '\x2', '\x2', '\xAF', '\xB0', '\a', 'g', '\x2', '\x2', 
		'\xB0', '\x1C', '\x3', '\x2', '\x2', '\x2', '\xB1', '\xB2', '\a', 'k', 
		'\x2', '\x2', '\xB2', '\xB3', '\a', 'h', '\x2', '\x2', '\xB3', '\x1E', 
		'\x3', '\x2', '\x2', '\x2', '\xB4', '\xB5', '\a', 'g', '\x2', '\x2', '\xB5', 
		'\xB6', '\a', 'n', '\x2', '\x2', '\xB6', '\xB7', '\a', 'u', '\x2', '\x2', 
		'\xB7', '\xB8', '\a', 'g', '\x2', '\x2', '\xB8', ' ', '\x3', '\x2', '\x2', 
		'\x2', '\xB9', '\xBA', '\a', 'p', '\x2', '\x2', '\xBA', '\xBB', '\a', 
		'w', '\x2', '\x2', '\xBB', '\xBC', '\a', 'n', '\x2', '\x2', '\xBC', '\xBD', 
		'\a', 'n', '\x2', '\x2', '\xBD', '\"', '\x3', '\x2', '\x2', '\x2', '\xBE', 
		'\xBF', '\a', 'v', '\x2', '\x2', '\xBF', '\xC0', '\a', 't', '\x2', '\x2', 
		'\xC0', '\xC1', '\a', 'w', '\x2', '\x2', '\xC1', '\xC8', '\a', 'g', '\x2', 
		'\x2', '\xC2', '\xC3', '\a', 'h', '\x2', '\x2', '\xC3', '\xC4', '\a', 
		'\x63', '\x2', '\x2', '\xC4', '\xC5', '\a', 'n', '\x2', '\x2', '\xC5', 
		'\xC6', '\a', 'u', '\x2', '\x2', '\xC6', '\xC8', '\a', 'g', '\x2', '\x2', 
		'\xC7', '\xBE', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC2', '\x3', '\x2', 
		'\x2', '\x2', '\xC8', '$', '\x3', '\x2', '\x2', '\x2', '\xC9', '\xCA', 
		'\a', '~', '\x2', '\x2', '\xCA', '\xCB', '\a', '~', '\x2', '\x2', '\xCB', 
		'&', '\x3', '\x2', '\x2', '\x2', '\xCC', '\xCD', '\a', '(', '\x2', '\x2', 
		'\xCD', '\xCE', '\a', '(', '\x2', '\x2', '\xCE', '(', '\x3', '\x2', '\x2', 
		'\x2', '\xCF', '\xD0', '\a', '?', '\x2', '\x2', '\xD0', '\xD1', '\a', 
		'?', '\x2', '\x2', '\xD1', '*', '\x3', '\x2', '\x2', '\x2', '\xD2', '\xD3', 
		'\a', '#', '\x2', '\x2', '\xD3', '\xD4', '\a', '?', '\x2', '\x2', '\xD4', 
		',', '\x3', '\x2', '\x2', '\x2', '\xD5', '\xD6', '\a', '@', '\x2', '\x2', 
		'\xD6', '\xD7', '\a', '?', '\x2', '\x2', '\xD7', '.', '\x3', '\x2', '\x2', 
		'\x2', '\xD8', '\xD9', '\a', '>', '\x2', '\x2', '\xD9', '\xDA', '\a', 
		'?', '\x2', '\x2', '\xDA', '\x30', '\x3', '\x2', '\x2', '\x2', '\xDB', 
		'\xDC', '\a', '#', '\x2', '\x2', '\xDC', '\x32', '\x3', '\x2', '\x2', 
		'\x2', '\xDD', '\xDE', '\a', '@', '\x2', '\x2', '\xDE', '\x34', '\x3', 
		'\x2', '\x2', '\x2', '\xDF', '\xE0', '\a', '>', '\x2', '\x2', '\xE0', 
		'\x36', '\x3', '\x2', '\x2', '\x2', '\xE1', '\xE2', '\a', '-', '\x2', 
		'\x2', '\xE2', '\x38', '\x3', '\x2', '\x2', '\x2', '\xE3', '\xE4', '\a', 
		'/', '\x2', '\x2', '\xE4', ':', '\x3', '\x2', '\x2', '\x2', '\xE5', '\xE6', 
		'\a', ',', '\x2', '\x2', '\xE6', '<', '\x3', '\x2', '\x2', '\x2', '\xE7', 
		'\xE8', '\a', '\x31', '\x2', '\x2', '\xE8', '>', '\x3', '\x2', '\x2', 
		'\x2', '\xE9', '\xEA', '\a', '\'', '\x2', '\x2', '\xEA', '@', '\x3', '\x2', 
		'\x2', '\x2', '\xEB', '\xEC', '\a', '}', '\x2', '\x2', '\xEC', '\x42', 
		'\x3', '\x2', '\x2', '\x2', '\xED', '\xEE', '\a', '\x7F', '\x2', '\x2', 
		'\xEE', '\x44', '\x3', '\x2', '\x2', '\x2', '\xEF', '\xF0', '\a', '?', 
		'\x2', '\x2', '\xF0', '\x46', '\x3', '\x2', '\x2', '\x2', '\xF1', '\xF2', 
		'\a', '=', '\x2', '\x2', '\xF2', 'H', '\x3', '\x2', '\x2', '\x2', '\xF3', 
		'\xF4', '\a', '\x61', '\x2', '\x2', '\xF4', 'J', '\x3', '\x2', '\x2', 
		'\x2', '\xF5', '\xF6', '\a', '<', '\x2', '\x2', '\xF6', 'L', '\x3', '\x2', 
		'\x2', '\x2', '\xF7', '\xF8', '\a', '*', '\x2', '\x2', '\xF8', 'N', '\x3', 
		'\x2', '\x2', '\x2', '\xF9', '\xFA', '\a', '+', '\x2', '\x2', '\xFA', 
		'P', '\x3', '\x2', '\x2', '\x2', '\xFB', '\xFC', '\a', ']', '\x2', '\x2', 
		'\xFC', 'R', '\x3', '\x2', '\x2', '\x2', '\xFD', '\xFE', '\a', '_', '\x2', 
		'\x2', '\xFE', 'T', '\x3', '\x2', '\x2', '\x2', '\xFF', '\x100', '\a', 
		'-', '\x2', '\x2', '\x100', '\x101', '\a', '?', '\x2', '\x2', '\x101', 
		'V', '\x3', '\x2', '\x2', '\x2', '\x102', '\x103', '\a', '/', '\x2', '\x2', 
		'\x103', '\x104', '\a', '?', '\x2', '\x2', '\x104', 'X', '\x3', '\x2', 
		'\x2', '\x2', '\x105', '\x106', '\a', ',', '\x2', '\x2', '\x106', '\x107', 
		'\a', '?', '\x2', '\x2', '\x107', 'Z', '\x3', '\x2', '\x2', '\x2', '\x108', 
		'\x109', '\a', '\x31', '\x2', '\x2', '\x109', '\x10A', '\a', '?', '\x2', 
		'\x2', '\x10A', '\\', '\x3', '\x2', '\x2', '\x2', '\x10B', '\x10C', '\a', 
		'\x30', '\x2', '\x2', '\x10C', '^', '\x3', '\x2', '\x2', '\x2', '\x10D', 
		'\x10E', '\a', '-', '\x2', '\x2', '\x10E', '\x10F', '\a', '-', '\x2', 
		'\x2', '\x10F', '`', '\x3', '\x2', '\x2', '\x2', '\x110', '\x111', '\a', 
		'/', '\x2', '\x2', '\x111', '\x112', '\a', '/', '\x2', '\x2', '\x112', 
		'\x62', '\x3', '\x2', '\x2', '\x2', '\x113', '\x114', '\a', '.', '\x2', 
		'\x2', '\x114', '\x64', '\x3', '\x2', '\x2', '\x2', '\x115', '\x118', 
		'\a', '\x61', '\x2', '\x2', '\x116', '\x118', '\x5', '\x5', '\x3', '\x2', 
		'\x117', '\x115', '\x3', '\x2', '\x2', '\x2', '\x117', '\x116', '\x3', 
		'\x2', '\x2', '\x2', '\x118', '\x11E', '\x3', '\x2', '\x2', '\x2', '\x119', 
		'\x11D', '\a', '\x61', '\x2', '\x2', '\x11A', '\x11D', '\x5', '\x5', '\x3', 
		'\x2', '\x11B', '\x11D', '\x5', '\a', '\x4', '\x2', '\x11C', '\x119', 
		'\x3', '\x2', '\x2', '\x2', '\x11C', '\x11A', '\x3', '\x2', '\x2', '\x2', 
		'\x11C', '\x11B', '\x3', '\x2', '\x2', '\x2', '\x11D', '\x120', '\x3', 
		'\x2', '\x2', '\x2', '\x11E', '\x11C', '\x3', '\x2', '\x2', '\x2', '\x11E', 
		'\x11F', '\x3', '\x2', '\x2', '\x2', '\x11F', '\x66', '\x3', '\x2', '\x2', 
		'\x2', '\x120', '\x11E', '\x3', '\x2', '\x2', '\x2', '\x121', '\x123', 
		'\x5', '\a', '\x4', '\x2', '\x122', '\x121', '\x3', '\x2', '\x2', '\x2', 
		'\x123', '\x124', '\x3', '\x2', '\x2', '\x2', '\x124', '\x122', '\x3', 
		'\x2', '\x2', '\x2', '\x124', '\x125', '\x3', '\x2', '\x2', '\x2', '\x125', 
		'\x12D', '\x3', '\x2', '\x2', '\x2', '\x126', '\x12A', '\a', '\x30', '\x2', 
		'\x2', '\x127', '\x129', '\x5', '\a', '\x4', '\x2', '\x128', '\x127', 
		'\x3', '\x2', '\x2', '\x2', '\x129', '\x12C', '\x3', '\x2', '\x2', '\x2', 
		'\x12A', '\x128', '\x3', '\x2', '\x2', '\x2', '\x12A', '\x12B', '\x3', 
		'\x2', '\x2', '\x2', '\x12B', '\x12E', '\x3', '\x2', '\x2', '\x2', '\x12C', 
		'\x12A', '\x3', '\x2', '\x2', '\x2', '\x12D', '\x126', '\x3', '\x2', '\x2', 
		'\x2', '\x12D', '\x12E', '\x3', '\x2', '\x2', '\x2', '\x12E', 'h', '\x3', 
		'\x2', '\x2', '\x2', '\x12F', '\x131', '\t', '\x4', '\x2', '\x2', '\x130', 
		'\x12F', '\x3', '\x2', '\x2', '\x2', '\x131', '\x134', '\x3', '\x2', '\x2', 
		'\x2', '\x132', '\x130', '\x3', '\x2', '\x2', '\x2', '\x132', '\x133', 
		'\x3', '\x2', '\x2', '\x2', '\x133', '\x135', '\x3', '\x2', '\x2', '\x2', 
		'\x134', '\x132', '\x3', '\x2', '\x2', '\x2', '\x135', '\x136', '\b', 
		'\x35', '\x2', '\x2', '\x136', 'j', '\x3', '\x2', '\x2', '\x2', '\x137', 
		'\x138', '\a', '\x31', '\x2', '\x2', '\x138', '\x139', '\a', '\x31', '\x2', 
		'\x2', '\x139', '\x13D', '\x3', '\x2', '\x2', '\x2', '\x13A', '\x13C', 
		'\n', '\x5', '\x2', '\x2', '\x13B', '\x13A', '\x3', '\x2', '\x2', '\x2', 
		'\x13C', '\x13F', '\x3', '\x2', '\x2', '\x2', '\x13D', '\x13B', '\x3', 
		'\x2', '\x2', '\x2', '\x13D', '\x13E', '\x3', '\x2', '\x2', '\x2', '\x13E', 
		'\x14C', '\x3', '\x2', '\x2', '\x2', '\x13F', '\x13D', '\x3', '\x2', '\x2', 
		'\x2', '\x140', '\x141', '\a', '\x31', '\x2', '\x2', '\x141', '\x142', 
		'\a', ',', '\x2', '\x2', '\x142', '\x146', '\x3', '\x2', '\x2', '\x2', 
		'\x143', '\x145', '\v', '\x2', '\x2', '\x2', '\x144', '\x143', '\x3', 
		'\x2', '\x2', '\x2', '\x145', '\x148', '\x3', '\x2', '\x2', '\x2', '\x146', 
		'\x147', '\x3', '\x2', '\x2', '\x2', '\x146', '\x144', '\x3', '\x2', '\x2', 
		'\x2', '\x147', '\x149', '\x3', '\x2', '\x2', '\x2', '\x148', '\x146', 
		'\x3', '\x2', '\x2', '\x2', '\x149', '\x14A', '\a', ',', '\x2', '\x2', 
		'\x14A', '\x14C', '\a', '\x31', '\x2', '\x2', '\x14B', '\x137', '\x3', 
		'\x2', '\x2', '\x2', '\x14B', '\x140', '\x3', '\x2', '\x2', '\x2', '\x14C', 
		'\x14D', '\x3', '\x2', '\x2', '\x2', '\x14D', '\x14E', '\b', '\x36', '\x2', 
		'\x2', '\x14E', 'l', '\x3', '\x2', '\x2', '\x2', '\x14F', '\x154', '\a', 
		'$', '\x2', '\x2', '\x150', '\x153', '\x5', '\t', '\x5', '\x2', '\x151', 
		'\x153', '\n', '\x5', '\x2', '\x2', '\x152', '\x150', '\x3', '\x2', '\x2', 
		'\x2', '\x152', '\x151', '\x3', '\x2', '\x2', '\x2', '\x153', '\x156', 
		'\x3', '\x2', '\x2', '\x2', '\x154', '\x155', '\x3', '\x2', '\x2', '\x2', 
		'\x154', '\x152', '\x3', '\x2', '\x2', '\x2', '\x155', '\x157', '\x3', 
		'\x2', '\x2', '\x2', '\x156', '\x154', '\x3', '\x2', '\x2', '\x2', '\x157', 
		'\x158', '\a', '$', '\x2', '\x2', '\x158', 'n', '\x3', '\x2', '\x2', '\x2', 
		'\x10', '\x2', '\xC7', '\x117', '\x11C', '\x11E', '\x124', '\x12A', '\x12D', 
		'\x132', '\x13D', '\x146', '\x14B', '\x152', '\x154', '\x3', '\b', '\x2', 
		'\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
