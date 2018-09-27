grammar SIC_STD;

options {							
    language= CSharp2;
}


/*
 * Parser Rules
 */

programa: inicio (proposicion)+  fin;
inicio: ID SEP 'START' SEP NUM endl;
fin: SEP 'END' (SEP ID)? endl?;
proposicion: (instruccion | directiva | rsub) endl;
instruccion: ID? SEP INSTRUCCION SEP opinstruccion ;
directiva: ID? SEP (BYTE | (TIPODIRECTIVA SEP NUM));
opinstruccion: ID (',' ' '* 'X')?;
endl : CR NL;
rsub 	: SEP 'RSUB';


/*
 * Lexer Rules
 */


INSTRUCCION :('ADD'|'AND'|'COMP'|'DIV'|'J'|'JEQ'|'JSUB'|'LDA'|'LDCH'|'LDL'|'LDX'|'MUL'|'OR'|'RD'|'STA'|'STCH'|'STL'|'STSW'|'STX'|'SUB'|'TD'|'TIX'|'JGT'|'JLT'|'WD');
TIPODIRECTIVA: ('WORD'|'RESB'|'RESW');
BYTE :	'BYTE' SEP ('C\'' ID '\'' | 'X\'' NUMH '\'');
NUM : ('0'..'9' | ('A' .. 'F'))+ ('h' | 'H')?;	
NUMD : ('0'..'9')+;	
NL: '\n';
CR: '\r';
NUMH: (NUMD |'A' |'B' |'C' |'D' | 'E'| 'F' )+;
NUMHH : NUMH ('h' | 'H' );
ID : [a-zA-Z] [a-zA-Z0-9]*;
SEP	:(' ' |'\t')+; 

