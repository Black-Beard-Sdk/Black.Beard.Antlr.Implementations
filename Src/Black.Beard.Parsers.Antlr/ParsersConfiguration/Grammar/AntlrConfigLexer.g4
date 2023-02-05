/*
 * [The "BSD license"]
 *  Copyright (c) 2023 Black beard
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 *  IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 *  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 *  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 *  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
// ======================================================
// Lexer specification
// ======================================================

lexer grammar AntlrConfigLexer;

options {  }
import LexBasic;

// -------------------------
// Comments
channels { OFF_CHANNEL , COMMENT }

DOC_COMMENT
   : DocComment -> channel (COMMENT)
   ;

BLOCK_COMMENT
   : BlockComment -> channel (COMMENT)
   ;

LINE_COMMENT
   : LineComment -> channel (COMMENT)
   ;

WS
   : Ws+ -> channel (OFF_CHANNEL)
   ;

// -------------------------
// Integer

INT
   : Int
   ;

// -------------------------
// Literal string
//
// ANTLR makes no distinction between a single character literal and a
// multi-character string. All literals are single quote delimited and
// may contain unicode escape sequences of the form \uxxxx, where x
// is a valid hexadecimal number (per Unicode standard).
STRING_LITERAL
   : SQuoteLiteral
   ;

fragment WSNLCHARS : ' ' | '\t' | '\f' | '\n' | '\r' ;

NO : 'NO';

GENERATE : 'GENERATE';

CALCULATED : 'CALCULATED';
WITH : 'WITH';
DEFAULT : 'DEFAULT';
RULE : 'RULE';
TEMPLATE : 'TEMPLATE';
SELECT : 'SELECT';
WHEN : 'WHEN';
ONE : 'ONE';
ONLY : 'ONLY';
ANY : 'ANY';
MANY : 'MANY';
BLOCK : 'BLOCK';
TERM : 'TERM';
ALTERNATIVE : 'ALTERNATIVE';
HAS : 'HAS';
IS : 'IS';
NOT : 'NOT';
IN : 'IN';
LIST : 'LIST';


OR : '|';
AND : '&';
OUTPUT : 'OUTPUT';
SEMI    : Semi   ;
COLON   : Colon   ;
LPAREN   : LParen   ;
RPAREN   : RParen   ;
ASSIGN   : Equal   ;
ID   : Id   ;
   // -------------------------
   // Whitespace

// ------------------------------------------------------------------------------
// Grammar specific Keywords, Punctuation, etc.
fragment Id
   : NameStartChar NameChar*
   ;
   
