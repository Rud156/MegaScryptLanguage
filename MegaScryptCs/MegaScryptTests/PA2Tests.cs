using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using MegaScrypt;
using NUnit.Framework;

namespace MegaScryptTests
{
    class PA2Tests
    {
        Machine machine;
        [SetUp]
        public void Setup()
        {
            machine = new Machine();
        }

        #region Function Tests

        private static int[] fibSeq = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void TestFibonacci(int n)
        {
            int expected = fibSeq[n];
            string script = @"
var Fib = function(var n) {
    if(n <= 1)
        return n;
    return Fib(n-1) + Fib(n-2);
};
";
            machine.Execute(script);
            object result = machine.Evaluate($"Fib({n})");
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3 * 2)]
        [TestCase(4, 4 * 3 * 2)]
        [TestCase(5, 5 * 4 * 3 * 2)]
        [TestCase(6, 6 * 5 * 4 * 3 * 2)]
        public void TestFactorial(int n, int expected)
        {
            string script = @"
var Factorial = function(var n) {
    if(n <= 1)
        return 1;
    return n * Factorial(n-1);
};
";

            machine.Execute(script);
            object result = machine.Evaluate($"Factorial({n})");
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestAssignToInvocation()
        {
            string script = @"
var obj = {
    Add: function(var a, var b) {
        return a + b;
    }
};

obj.Add(1, 2) = 4;
";
            Exception ex = null;
            try
            {
                machine.Execute(script);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex);
        }

        [Test]
        public void TestObjectPrototype()
        {
            string script = @"
var unit = {
    name: null,
    hp: 100,
    attack: 10
};
var unit1 = {
    prototype: unit,
    name: ""Berserker"",
    hp: 50
};
";
            machine.Execute(script);
            object hp = machine.Evaluate("unit1.hp");
            Assert.AreEqual(50, hp);
            object name = machine.Evaluate("unit1.name");
            Assert.AreEqual("Berserker", name);
            object attack = machine.Evaluate("unit1.attack");
            Assert.AreEqual(10, attack);
        }

        [Test]
        public void TestObjectFunctionScope()
        {
            string script = @"
var unit1 = {
    name: ""Berserker"",
    hp: 50,
    Damage: function(var amount) {
        hp -= amount;
    }
};
var origHp = unit1.hp;
unit1.Damage(10);
";
            machine.Execute(script);
            object origHp = machine.Evaluate("origHp");
            Assert.AreEqual(50, origHp);
            object finalHp = machine.Evaluate("unit1.hp");
            Assert.AreEqual(40, finalHp);
        }

        [Test]
        public void TestObjectPrototypeFunctionScope()
        {
            string script = @"
var unit = {
    name: null,
    hp: 100,
    Damage: function(var amount) {
        hp -= amount;
    }
};

var unit1 = {
    prototype: unit,
    name: ""Berserker"",
    hp: 50
};
var origHp = unit1.hp;
unit1.Damage(10);
";
            machine.Execute(script);
            object origHp = machine.Evaluate("origHp");
            Assert.AreEqual(50, origHp);
            object finalHp = machine.Evaluate("unit1.hp");
            Assert.AreEqual(40, finalHp);
        }

        #endregion

        #region Array Tests

        [Test]
        public void TestArrayAccess()
        {
            MegaScrypt.Array array = new MegaScrypt.Array(new string[] { "hello", "how", "are", "you", "?" });
            machine.Declare("a", array);
            object result = machine.Evaluate("a[3]");
            Assert.AreEqual("you", result);
        }

        [Test]
        public void TestArrayAssignment()
        {
            string script = @"
var obj = { list: [1, 2, 3] };
obj.list[1] = 4;
";
            machine.Execute(script);
            object result = machine.Evaluate("obj.list[1]");
            int expected = 4;
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("[1, 2, 3, 4]")]
        public void TestArrayInstantiation(string expression)
        {
            object result = machine.Evaluate(expression);
            Assert.IsTrue(result is MegaScrypt.Array);
        }

        [Test]
        public void TestArrayAdd()
        {
            string script = @"
var list = [1, 2, 3];
list.Add(4);
";
            machine.Execute(script);
            object result = machine.Evaluate("list[3]");
            int expected = 4;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestArrayAddRange()
        {
            string script = @"
var a = [1, 2, 3];
var b = [4, 5, 6];
a.AddRange(b);
";
            machine.Execute(script);
            object result = machine.Evaluate("a[3]");
            int expected = 4;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestArrayInsert()
        {
            string script = @"
var list = [1, 2, 3];
list.Insert(1, 4);
";
            machine.Execute(script);
            object result = machine.Evaluate("list[1]");
            int expected = 4;
            Assert.AreEqual(expected, result);

            result = machine.Evaluate("list[0]");
            expected = 1;
            Assert.AreEqual(expected, result);

            result = machine.Evaluate("list[2]");
            expected = 2;
            Assert.AreEqual(expected, result);
        }

        public void TestArrayRemoveAt()
        {
            string script = @"
var list = [1, 2, 3];
list.RemoveAt(0);
";
            machine.Execute(script);
            object result = machine.Evaluate("list[0]");
            int expected = 2;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestArrayCount()
        {
            string script = @"
var list = [1, 2, 3];
var a = list.Count;
list.AddRange([4, 5, 6, 7]);
var b = list.Count;
";
            machine.Execute(script);
            Assert.AreEqual(3, machine.Evaluate("a"));
            Assert.AreEqual(7, machine.Evaluate("b"));
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 6)]
        [TestCase(2, 1, 9)]
        [TestCase(3, 3, 15)]
        public void TestArrayNesting(int i, int j, int expected)
        {
            string script = @"
var a = [
    [0, 1, 2, 3],
    [4, 5, 6, 7],
    [8, 9, 10, 11],
    [12, 13, 14, 15]
];
";
            machine.Execute(script);
            object result = machine.Evaluate($"a[{i}][{j}]");
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Loop Tests

        [Test]
        public void TestForLoop()
        {
            string script = @"
var x = 1;
for(var i = 0; i < 5; i++) {
    x *= 2;
}
";
            machine.Execute(script);
            int expected = 32;
            object result = machine.Evaluate("x");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestForEachLoop()
        {
            string script = @"
var list = [1, 3, 5, 7, 9];
var sum = 0;
foreach(var i in list) {
    sum += i;
}
";
            machine.Execute(script);
            int expected = 25;
            object result = machine.Evaluate("sum");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestWhileLoop()
        {
            string script = @"
var x = 0;
while(x < 10)
{
    x++;
}
";
            machine.Execute(script);
            object result = machine.Evaluate("x");
            int expected = 10;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestDoWhileLoop()
        {
            string script = @"
var x = 0;
do
{
    x++;
} while(x < 10);
";
            machine.Execute(script);
            object result = machine.Evaluate("x");
            int expected = 10;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBreak()
        {
            string script = @"
var x = 0;
while(true) {
    x++;
    if(x > 5)
        break;
}
";
            machine.Execute(script);
            int expected = 6;
            object result = machine.Evaluate("x");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestContinue()
        {
            string script = @"
for(var i = 0; i < 10; i++) {
    if(i % 2 == 0)
        continue;
    Emit(i);
}
";
            List<object> emitted = new List<object>();
            machine.Declare(new NativeFunction("Emit", (List<object> parameters) =>
            {
                emitted.Add(parameters[0]);
                return null;
            }));
            List<object> expected = new List<object>() { 1, 3, 5, 7, 9 };
            machine.Execute(script);
            Assert.AreEqual(expected, emitted);
        }

        #endregion

        #region Object Tests

        // TODO: test prototype

        #endregion

        [Test]
        public void TestBigComplexScript()
        {
            string script = @"
var unit = {
    name: null,
    hp: 20,
    shield: 0,
    attack: 7,
    Damage: function(var amount) {
        if(shield > 0) {
            shield -= amount;
            Print(name + ""'s shield blocked "" + amount + "" damage"");
            return;
        }
        hp -= amount;
        Print(name + "" took "" + amount + "" damage: "" + hp);
    },
    IsAlive: function() { return hp > 0; }
};
var u1 = {
    prototype: unit,
    name: ""Unit 1"",
    hp: 25,
    shield: 20
};
var u2 = {
    prototype: unit,
    name: ""Unit 2"",
    attack: 15
};

var rounds = 0;
while(u1.IsAlive() && u2.IsAlive())
{
    rounds++;
    u2.Damage(u1.attack);
    u1.Damage(u2.attack);
}

var winner;
if(u1.IsAlive() && !u2.IsAlive())
    winner = u1.name;
else if(!u1.IsAlive() && u2.IsAlive())
    winner = u2.name;
else
    winner = ""Tie"";
";
            machine.Declare(new NativeFunction("Print", (List<object> parameters) =>
            {
                Console.WriteLine(parameters[0].ToString());
                return null;
            }));
            machine.Execute(script);
            object winner = machine.Evaluate("winner");
            object u1_hp = machine.Evaluate("u1.hp");
            object u2_hp = machine.Evaluate("u2.hp");
            object rounds = machine.Evaluate("rounds");

            Assert.AreEqual("Unit 1", winner);
            Assert.AreEqual(10, u1_hp);
            Assert.AreEqual(-1, u2_hp);
            Assert.AreEqual(3, rounds);
        }
    }
}
