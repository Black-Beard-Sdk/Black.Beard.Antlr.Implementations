
# https://www.antlr.org/download.html
# http://www.antlr.org/download/antlr-4.11.1-complete.jar


java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp ANTLRv4Lexer.g4 -visitor -no-listener -package Bb.Parsers.Antlr
java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp ANTLRv4Parser.g4 -visitor -no-listener -package Bb.Parsers.Antlr