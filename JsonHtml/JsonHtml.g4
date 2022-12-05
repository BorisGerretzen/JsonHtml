grammar JsonHtml;

root: ':root' LBRACE element+ RBRACE;
element: IDENTIFIER options? LBRACE (element*|content?) RBRACE;
content: STRING SEMI;
options: LBRACKET (IDENTIFIER EQUALS STRING)+ RBRACKET;

LBRACE : '{' ;
RBRACE : '}' ;
LBRACKET: '[';
RBRACKET: ']';
COLON: ':';
SEMI: ';';
EQUALS: '=';
STRING: '"' ( '""' | ~["] )* '"';
IDENTIFIER: VALID_ID+;

WS : [ \r\n\t] + -> channel(HIDDEN) ;

fragment NUMBER : ('0' .. '9') + ('.' ('0' .. '9') +)? ;
fragment UNSIGNED_INTEGER : ('0' .. '9')+ ;
fragment SIGN : ('+' | '-') ;
fragment VALID_ID : ('a' .. 'z') | ('A' .. 'Z');