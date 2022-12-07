grammar JsonHtml;

root: ':root' LBRACE element+ RBRACE EOF;
element
    : 
        declaration LBRACE content? RBRACE #elementContent
    |   declaration COLON LBRACE content? RBRACE #elementContentNoEscape
    |   declaration LBRACE element* RBRACE #elementElements
    ;
declaration: IDENTIFIER id? class* options?;
options: LBRACKET option+ RBRACKET;
option: IDENTIFIER EQUALS STRING;
id: HASH IDENTIFIER;
class: DOT IDENTIFIER;
content: STRING;

LBRACE : '{' ;
RBRACE : '}' ;
LBRACKET: '[';
RBRACKET: ']';
COLON: ':';
SEMI: ';';
EQUALS: '=';
DOT:'.';
HASH:'#';
STRING: '"' ( '""' | ~["] )* '"';
IDENTIFIER: VALID_ID+;

WS : [ \r\n\t] + -> channel(HIDDEN) ;

fragment NUMBER : ('0' .. '9') + ('.' ('0' .. '9') +)? ;
fragment UNSIGNED_INTEGER : ('0' .. '9')+ ;
fragment SIGN : ('+' | '-') ;
fragment VALID_ID : ('a' .. 'z') | ('A' .. 'Z') | ('0'..'9') | '-' | '_';