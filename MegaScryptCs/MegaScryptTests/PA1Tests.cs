using NUnit.Framework;
using MegaScrypt;

namespace MegaScryptTests
{
	public class PA1Tests
	{
		Machine machine;
		[SetUp]
		public void Setup()
		{
			machine = new Machine();
		}

		[Test]
		// integer operations
		[TestCase("-314", -314)]
		[TestCase("1 + 1", 1 + 1)]
		[TestCase("0 - 72", 0 - 72)]
		[TestCase("3 * 4", 3 * 4)]
		[TestCase("2 * 0", 2 * 0)]
		[TestCase("100 / 10", 100 / 10)]
		[TestCase("5 / 3", 5 / 3)]
		// float operations
		[TestCase("2.0 / 4.0", 2.0f / 4.0f)]
		[TestCase("1.5 * 2", 1.5f * 2)]
		[TestCase("14 * 1.", 14 * 1f)]
		[TestCase("-314.", -314f)]
		[TestCase("1.0 + 1", 1f + 1)]
		[TestCase("0.0 - 72", 0f - 72)]
		[TestCase("3. * 4.000", 3f * 4f)]
		[TestCase("2 * 0.", 2 * 0f)]
		[TestCase("100 / 10.0", 100 / 10f)]
		[TestCase("5 / 3.0", 5 / 3f)]
		// order of operations
		[TestCase("1 + 2 * 3", 1 + 2 * 3)]
		[TestCase("(1 + 2) * 3", (1 + 2) * 3)]
		[TestCase("3 + 6 / 2", 3 + 6 / 2)]
		[TestCase("(3 + 6) / 2", (3 + 6) / 2)]
		// boolean operations
		[TestCase("!false || false", !false || false)]
		[TestCase("false || !false", false || !false)]
		[TestCase("!true && true", !true && true)]
		[TestCase("true && !true", true && !true)]
		[TestCase("true || true && false", true || true && false)]
		[TestCase("!true", !true)]
		[TestCase("!false", !false)]
		// comparison operators
		[TestCase("5 > 3", 5 > 3)]
		[TestCase("-5 > 3", -5 > 3)]
		[TestCase("1 >= 1", 1 >= 1)]
		[TestCase("1 >= 0", 1 >= 0)]
		[TestCase("0 >= 1", 0 >= 1)]
		[TestCase("2 < -10", 2 < -10)]
		[TestCase("-2 < 10", -2 < 10)]
		[TestCase("2 <= -10", 2 <= -10)]
		[TestCase("-2 <= 10", -2 <= 10)]
		[TestCase("2 <= 2", 2 <= 2)]
		[TestCase("2. < 3 && 3 >= 0", 2f < 3 && 3 >= 0)]
		[TestCase("5 > 3 == true || 1. > -1 != false", 5 > 3 == true || 1f > -1 != false)]
		public void TestExpressions(string expression, object expected)
		{
			try
			{
				object result = machine.Evaluate(expression);
				Assert.AreEqual(expected, result);
			}
			catch (System.Exception)
			{
				Assert.Fail();
			}
		}

		[Test]
		[TestCase(@"
var x = 5;
var y = 10;
var z;
{
	var y;
	y = -4;
	z = y;
}
", 5, 10, -4)]
		[TestCase(@"
var x = 5;
var y = 10;
var z;
{
	y = -x;
}
", 5, -5, null)]
		public void TestBlockScope(string script, object expectedX, object expectedY, object expectedZ)
		{
			try
			{
				machine.Execute(script);

				object x = machine.Evaluate("x");
				object y = machine.Evaluate("y");
				object z = machine.Evaluate("z");

				Assert.AreEqual(expectedX, x);
				Assert.AreEqual(expectedY, y);
				Assert.AreEqual(expectedZ, z);
			}
			catch(System.Exception)
			{
				Assert.Fail();
			}
		}

		[Test]
		[TestCase(@"
var x = 5;
var pass = false;
if(x > 0) {
	pass = true;
}
")]
		[TestCase(@"
var x = 5;
var pass = false;
if(x > 0) {
	pass = true;
} else {
	pass = false;
}
")]
		[TestCase(@"
var x = 5;
var pass = false;
if(x < 0) {
	pass = false;
} else {
	pass = true;
}
")]
		[TestCase(@"
var pass = false;
if(-1 > 0) {
	pass = false;
} else if(1 < 0) {
	pass = false;
} else if(2 == 3) {
	pass = false;
} else {
	pass = true;
}
")]
		[TestCase(@"
var pass = false;
if(1 > 0)
	pass = true;
else if(1 < 0)
	pass = false;
else if(2 == 3)
	pass = false;
else
	pass = false;
")]
		public void TestIf(string script)
		{
			try
			{
				machine.Execute(script);

				object pass = machine.Evaluate("pass");
				Assert.AreEqual(true, pass);
			}
			catch (System.Exception)
			{
				Assert.Fail();
			}
		}

		[Test]
		// increment / decrement
		[TestCase(@"
var x = 0;
x++;
++x;
", "x", 2)]
		[TestCase(@"
var x = 0;
x--;
--x;
", "x", -2)]
		[TestCase(@"
var x = 0;
x++;
x--;
++x;
--x;
", "x", 0)]
		[TestCase(@"
var y = 0;
var x = y++;
", "x", 0)]
		[TestCase(@"
var y = 0;
var x = ++y;
", "x", 1)]
		[TestCase(@"
var y = 0;
var x = y--;
", "x", 0)]
		[TestCase(@"
var y = 0;
var x = --y;
", "x", -1)]
		// assignment operators
		[TestCase(@"
var i = -514;
i *= 10;
", "i", -514*10)]
		[TestCase(@"
var f = -514;
f /= 10.0;
", "f", -514/10f)]
		[TestCase(@"
var f = 103.;
f += 10.0;
", "f", 103f + 10f)]
		[TestCase(@"
var i = 103;
i -= 10;
", "i", 103-10)]
		// string assignment & operations
		[TestCase("var x = \"hello world\";", "x", "hello world")]
		[TestCase("var a = \"hello \"; var b = \"world\"; var x = a + b;", "x", "hello world")]
		[TestCase("var x = \"hello \"; x += \"world\";", "x", "hello world")]
		[TestCase("var x = \"world\"; x = \"hello \" + x;", "x", "hello world")]
		// nulls
		[TestCase(@"
var x;
", "x", null)]
		[TestCase(@"
var x = null;
", "x", null)]
		[TestCase(@"
var y = 5;
var x = false;
if(y != null)
	x = true;
", "x", true)]
		[TestCase(@"
var y = null;
var x = false;
if(y == null)
	x = true;
", "x", true)]
		// booleans
		[TestCase(@"var b = true && false;", "b", false)]
		[TestCase(@"var b = true || false;", "b", true)]
		[TestCase(@"var a = false; var b = !a;", "b", true)]
		public void TestVariables(string script, string expression, object expected)
		{
			try
			{
				machine.Execute(script);
				object result = machine.Evaluate(expression);
				Assert.AreEqual(expected, result);
			}
			catch (System.Exception)
			{
				Assert.Fail();
			}
		}

		[Test]
		[TestCase(@"
var obj = {
	x: 3,
	y: -4
};
", "obj.y", -4)]
		[TestCase(@"
var obj = {
	child: {
		x: 26,
		y: 200,
		z: 1
	},
	x: 3,
	y: -4
};
", "obj.child.x", 26)]
		[TestCase(@"
var obj = {
	child: {
		x: 26,
		y: 200,
		z: 1
	},
	x: 3,
	y: -4
};
obj.child.z = 5009;
", "obj.child.z", 5009)]
		[TestCase(@"
var x = 10;
var y = 12;
var z = 14;
var obj = {
	position: {
		x: x,
		y: y,
		z: z
	}
};
", "obj.position.z", 14)]
		[TestCase(@"
var summer = {
	name: ""Summer"",
	age: 17,
	brother: null
};
var morty = {
	name: ""Morty"",
	age: 14,
	sister: summer
};
summer.brother = morty;
", "morty.sister.name", "Summer")]
		[TestCase(@"
var summer = {
	name: ""Summer"",
	age: 17,
	brother: null
};
var morty = {
	name: ""Morty"",
	age: 14,
	sister: summer
};
summer.brother = morty;
", "summer.brother.name", "Morty")]
		public void TestObjects(string script, string expression, object expected)
		{
			try
			{
				machine.Execute(script);
				object result = machine.Evaluate(expression);
				Assert.AreEqual(expected, result);
			}
			catch (System.Exception)
			{
				Assert.Fail();
			}
		}


		[Test]
		[TestCase(@"
var pass = true;
// pass = false;
")]
		[TestCase(@"
var pass = true;
/*
	pass = false;
	multiline comment
*/
/* pass = false; */
")]
		public void TestComments(string script)
		{
			try
			{
				machine.Execute(script);
				object pass = machine.Evaluate("pass");
				Assert.AreEqual(true, pass);
			}
			catch(System.Exception)
			{
				Assert.Fail();
			}
		}

		// error handling
		[Test]
		[TestCase("invalid syntax", null)]
		[TestCase(@"
x = 5;
", new string[] { "undeclared", "not declared" })]
		[TestCase(@"
var x = 5;
var x = 2;
", new string[]{"already declared", "redeclared"})]
		[TestCase(@"
var y = 0;
var z = x + y;
", new string[]{"undeclared", "not declared"})]
		public void TestErrors(string script, string[] matches)
		{
			System.Exception exception = null;
			try
			{
				machine.Execute(script);
			}
			catch(System.Exception ex)
			{
				exception = ex;
			}

			Assert.NotNull(exception);

			if (matches != null)

			{
				bool match = false;
				foreach (string s in matches)
				{
					if (exception.Message.Contains(s, System.StringComparison.InvariantCultureIgnoreCase))
					{
						match = true;
						break;
					}
				}
				Assert.IsTrue(match);
			}
		}
	}
}