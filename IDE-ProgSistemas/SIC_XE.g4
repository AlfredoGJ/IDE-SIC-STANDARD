grammar SIC_XE;


options {							
    language= CSharp2;
}


//------------------------PARSER RULES------------------------------------------------------------------------------------------------//

programa: inicio (proposicion)+  fin;
inicio: ID SEP 'START' SEP NUM endl;
fin: SEP 'END' (SEP ID)? endl?;
proposicion: (instruccion | directiva | RSUB) SEP? endl;
instruccion: ID? SEP (format1 | format2 | (EXT? format3));
format1: INST1;
format2: (INST2RR SEP REG SEP ',' SEP REG) | (INST2RN SEP REG SEP ',' SEP NUM) | ('SVC' SEP NUM) | (INST2R SEP REG);
format3: INST3 SEP MODIR? (ID | NUM) INDEX?;
RSUB: ((ID SEP) | SEP)? 'RSUB' SEP?;
directiva: ID? SEP (bytedir | (TIPODIRECTIVA SEP NUM) | BASE SEP ID);
endl : CR NL;
bytedir: BYTE SEP BYTEOP;

// ---------------------- LEXER RULES-----------------------------------------------------------------------------------------------//

// Directivas
TIPODIRECTIVA: ('WORD'|'RESB'|'RESW');
BYTE :	'BYTE';
BYTEOP: (BYTECHAR | BYTENUM);
BYTECHAR: ('C\'' ID '\'') ;
BYTENUM: ('X\'' NUMH '\'');
BASE:'BASE' ;
// Indexado 
INDEX : (',' ' '* 'X'); 

//Instrucciones
INST1: ('FIX' | 'FLOAT' | 'HIO' | 'NORM' | 'SIO' | 'TIO'); 
INST2RN: ('SHIFTR' | 'SHIFTL');
INST2R: ('CLEAR' | 'TIXR');
INST2RR: ('ADDR' | 'COMPR' | 'DIVR' | 'MULR' | 'RMO' | 'SUBR');
INST3:('ADD'|'ADDF'|'AND'|'COMP'|'COMPF'|'DIV'|'DIVF'|'J'|'JEQ'|'JGT'|'JLT'|'JSUB'|'LDA'|'LDB'|'LDCH'|'LDF'|'LDL'|'LDS'|'LDT'|'LDX'|'LPS'|'MUL'|'MULF'|'OR'|'RD'|'SSK'|'STA'|'STB'|'STCH'|'STF'|'STI'|'STL'|'STS'|'STSW'|'STT'|'STX'|'SUB'|'SUBF'|'TD'|'TIX'|'WD');

// Registros
REG: 'A'|'X'|'L'|'B'|'S'|'T'|'F';

// Modos de direccionamiento
MODIR: ('#' | '@');

// Generales
NUM : ('0'..'9' | ('A' .. 'F'))+ ('h' | 'H')?;	
NUMH: ('0'..'9' | ('A' .. 'F'))+;
NUMHH : NUMH ('h' | 'H' );
ID :('a'..'z'|'A'..'Z') ('a'..'z'|'A'..'Z'| '0'..'9' )+ ;
EXT:'+';

// Separadores
SEP:(' ' |'\t')+; 
NL: '\n';
CR: '\r';