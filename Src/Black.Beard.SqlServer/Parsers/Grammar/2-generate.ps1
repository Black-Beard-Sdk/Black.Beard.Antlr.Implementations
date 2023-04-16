
# https://www.antlr.org/download.html
# http://www.antlr.org/download/antlr-4.11.1-complete.jar


java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp TSqlLexer.g4 -visitor -no-listener -package Bb.SqlServer.Parser
java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp TSqlParser.g4 -visitor -no-listener -package Bb.SqlServer.Parser