%{
#include <stdio.h>
int words_count = 0;
%}

%option noyywrap

%%
if { words_count++; }
return { words_count++; }
\( { words_count++; }
\) { words_count++; }
\{ { words_count++; }
\} { words_count++; }
\= { words_count++; }
\- { words_count++; }
\+ { words_count++; }
\* { words_count++; }
\/ { words_count++; }
\< { words_count++; }
\> { words_count++; }
\; { words_count++; }
[A-Z_a-z][A-Z_a-z0-9]* { words_count++; }
0|[1-9][0-9]* { words_count++; }
[ \t\n]+     ; /* skip whitespace */
%%

int main(int argc, char *argv[])
{
    yylex();
    printf("words_count = %d", words_count);
    return 0;
}
