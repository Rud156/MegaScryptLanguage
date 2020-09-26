grammar MegaScrypt;

// Parser Rules
compoundId: objectGetter | Id;
assignment: compoundId '=' (expression | object | array) ';' |
            compoundId '+=' expression ';' |
            compoundId '-=' expression ';' |
            compoundId '*=' expression ';' |
            compoundId '/=' expression ';';

decleration: 'var' Id ('=' (expression | object | array))? ';';
increment: ('++' compoundId) | (compoundId '++');
decrement: ('--' compoundId) | (compoundId '--');

expression: Number |
            compoundId |
            increment |
            decrement |
            array |
            invocation |
            funcDecleration |
            '(' expression ')' |
            '!' expression |
            '-' expression |
            expression ( '*' | '/' | '%' ) expression |
            expression ( '+' | '-' ) expression |
            expression ('<' | '>' | '<=' | '>=') expression |
            expression ( '==' | '!=' ) expression |
            expression '&&' expression |
            expression '||' expression |
            String |
            Null |
            Bool;

statement:  ifStatement | 
            assignment | 
            decleration |
            forStmt |
            whileStmt |
            foreachStmt |
            doWhileStmt |
            ((invocation | increment | decrement | returnStmt | Break | Continue) ';');
block: '{' statement* '}' | statement; 

object: '{' (objectPair ','?)* '}';
objectPair: Id ':' (expression | object | array);
objectGetter: (objectAccess) ('.' objectAccess)*;
objectAccess: Id arrayIndex?;

program: (statement | block)*;

ifStatement: ifBlock elseIfBlock* (elseBlock)?;
ifBlock: If '(' expression ')' block;
elseIfBlock: Else If '(' expression ')' block ;
elseBlock: Else block ;

funcDecleration: 'function' '(' varList? ')' '{' statement* '}';
varList: 'var' Id (',' 'var' Id)*;
returnStmt: 'return' expression?;
invocation: compoundId '(' paramList? ')';
paramList: expression (',' expression)*;

array: '[' paramList? ']';
arrayIndex: '[' expression ']';

forStmt: 'for' '(' 'var' Id '=' expression ';' Id ('<' | '>' | '<=' | '>=') expression ';' (increment | decrement) ')' block;
whileStmt: 'while' '(' expression ')' block;
foreachStmt: 'foreach' '(' 'var' Id 'in' compoundId ')' block;
doWhileStmt: 'do' block 'while' '(' expression ')';


// Lexer Rules
fragment Letter      : [a-zA-Z];
fragment Digit       : [0-9];
fragment EQUOTE      : '\\"';
Var         : 'var';
Function    : 'function';
Return      : 'return';
For         : 'for';
Do          : 'do';
While       : 'while';
foreach     : 'foreach';
In          : 'in';
Break       : 'break';
Continue    : 'continue';
If          : 'if';
Else        : 'else';
Null        : 'null';
Bool        : 'true' | 'false';
Or          : '||';
And         : '&&';
Equals      : '==';
NEquals     : '!=';
GTEquals    : '>=';
LTEquals    : '<=';
Excl        : '!';
GT          : '>';
LT          : '<';
Add         : '+';
Subtract    : '-';
Multiply    : '*';
Divide      : '/';
Modulus     : '%';
LBrace      : '{';
RBrace      : '}';
Assign      : '=';
Semicolon   : ';';
Underscore  : '_';
Colon       : ':';
LParen      : '(';
RParen      : ')';
LBracket    : '[';
Rbracket    : ']';
PlusEq      : '+=';
MinusEq     : '-=';
MultiplyEq  : '*=';
DivideEq    : '/=';
Dot         : '.';
Increment   : '++';
Decrement   : '--';
Comma       : ',';
Id          : ('_' | Letter)('_' | Letter | Digit)*;
Number      : Digit+ ('.' Digit*)?;
Whitespace  : [ \t\r\n]* -> skip;
Comment     : ( '//' ~[\r\n]* | '/*' .*? '*/' ) -> skip;
String      : '"' (EQUOTE | ~('\r' | '\n'))*? '"';