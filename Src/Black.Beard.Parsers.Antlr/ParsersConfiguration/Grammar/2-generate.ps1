
# https://www.antlr.org/download.html
# http://www.antlr.org/download/antlr-4.11.1-complete.jar


java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp AntlrConfigLexer.g4 -visitor -no-listener -package Bb.ParserConfigurations.Antlr
java.exe -jar antlr-4.11.1-complete.jar -Dlanguage=CSharp AntlrConfigParser.g4 -visitor -no-listener -package Bb.ParserConfigurations.Antlr