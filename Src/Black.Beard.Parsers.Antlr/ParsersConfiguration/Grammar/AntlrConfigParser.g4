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
grammar_spec
   : grammar_spec_list* 
     EOF
   ;

grammar_spec_list
   : default
   | template_selector
   | item_list
   | grammar_declaration
   ;

default
   : DEFAULT template_setting SEMI
   ;
template_selector
   : SELECT template_setting WHEN template_selector_expression SEMI
   ;

item_list
   : LIST identifier identifier+ SEMI
   ;

grammar_declaration
   : RULE identifier ruleConfig SEMI
   | TERM identifier ruleTermConfig SEMI
   ;




template_selector_expression
   : template_selector_expression_item ((OR|AND) template_selector_expression)?
   ;

template_selector_expression_item
   : template_selector_expression_item_has
   | template_selector_expression_item_is
   ;
template_selector_expression_item_has
   : RULE HAS  (ONE|ONLY|ANY|NO|MANY)  (BLOCK|RULE|TERM|ALTERNATIVE)
   | RULE HAS   ONE OUTPUT             (BLOCK|RULE|TERM|ALTERNATIVE)?
   ;

template_selector_expression_item_is
   : RULE IS NOT? (BLOCK|RULE|TERM|ALTERNATIVE)+
   | RULE IS NOT? IN identifier
   ;

ruleConfig
   : NO? GENERATE

     rule_tune_inherit?
     calculated_rule_tune_inherit?
     
     optional_template_setting?
     calculated_template_setting?
   ;

ruleTermConfig
   : KIND termKindEnum identifier?
   ;

termKindEnum
   : OTHER_
   | CONSTANT_
   | IDENTIFIER_
   | COMMENT_
   | BOOLEAN_
   | STRING_
   | DECIMAL_
   | INTEGER_
   | REAL_
   | HEXA_
   | BINARY_
   | PATTERN_
   | OPERATOR_
   | PONCTUATION_
   ;

calculated_rule_tune_inherit
   : CALCULATED rule_tune_inherit
   ;

rule_tune_inherit
   : INHERIT STRING_LITERAL?
   ;

calculated_template_setting
   : CALCULATED optional_template_setting
   ;

template_setting
   : TEMPLATE identifier additional_settings?
   ;

optional_template_setting
   : TEMPLATE identifier? additional_settings?
   ;

additional_settings
   : LPAREN value_item* RPAREN
   ;

value_item
   : identifier COLON identifier
   ;

identifier 
   : ID
   | STRING_LITERAL
   ;
