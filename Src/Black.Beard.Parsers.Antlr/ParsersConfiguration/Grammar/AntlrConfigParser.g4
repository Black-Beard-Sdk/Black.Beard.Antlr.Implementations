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
parser grammar AntlrConfigParser;


options { tokenVocab = AntlrConfigLexer; }


// The main entry point for parsing a v4 grammar.
grammarSpec
   : default_values? 
     template_selector* 
     grammarDeclaration* 
     EOF
   ;

template_selector
   : SET TEMPLATE identifier additional_settings WHEN template_selector_expression SEMI
   ;

additional_settings
   : LPAREN default_value_item* RPAREN
   ;

template_selector_expression
   : template_selector_expression_item ((OR|AND) template_selector_expression_item)*
   ;

template_selector_expression_item
   : template_selector_expression_item_1
   | template_selector_expression_item_2
   ;
template_selector_expression_item_1
   : RULE HAS (ONE|ONLY|ANY|NONE) (BLOCK|RULE|TERM|ALTERNATIVE)
   ;

template_selector_expression_item_2
   : RULE IS (BLOCK|RULE|TERM|ALTERNATIVE)
   ;

default_values
   : WITH DEFAULT default_value_item+ SEMI
   ;

default_value_item
   : identifier COLON constant
   ;

grammarDeclaration
   : RULE identifier COLON ruleConfig SEMI
   ;

ruleConfig
   : NO? GENERATE
     TEMPLATE COLON (templateIdentifier=identifier)?
     (CALCULATED TEMPLATE COLON calculatedTemplateIdentifier=identifier)?
   ;

identifier 
   : ID
   ;

constant 
   : STRING_LITERAL
   | identifier
   ;