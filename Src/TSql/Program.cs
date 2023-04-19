//// See https://aka.ms/new-console-template for more information

using Bb.SqlServer;
using Bb.SqlServer.Asts;
using Bb.SqlServer.Parser;
using System.Text;

var sb = new StringBuilder("SELECT * FROM [a]");

var parser = SqlServerScriptParser.ParseString(sb);
var result = parser.GetModel();


AstTRoot.New
(
    AstBatchs.New
    (
        AstBatch.New
        (
            AstSqlClauses.New
            (
                AstSqlClause.New
                (
                    AstSelectStatementStandalone.New
                    (
                        AstWithExpression.Null(), AstSelectStatement.New
                        (
                            AstQueryExpression.New
                            (

                                AstQuerySpecification.New
                                (
                                      AstAllDistinct.Null()
                                    , AstTopClause.Null()
                                    , AstSelectList.New
                                    (
                                        AstSelectListElem.New
                                        (
                                            AstAsterisk.New(AstStarAsterisk.Star())
                                        )
                                    )
                                    , null
                                    , AstTableSources.New
                                    (
                                        AstTableSource.New
                                        (
                                            AstTableSourceItemJoined.New
                                                (
                                                    AstTableSourceItem.New
                                                    (
                                                          AstCompleteTableRef.New(AstSchemaIdentifier.New(""), AstTableId.New(""))
                                                        , AstAsTableAlias.Null()
                                                        , AstTableHints.Null()

                                                    ),null
                                                )
                                        )
                                    )
                                    , AstWhereCondition.Null()
                                    , AstGroupbysList.Null()
                                    , AstSearchCondition.Null()

                                )
                                , AstSelectOrderByClause.Null()
                                , AstSqlUnions.Null()
                            ), AstSelectOrderByClause.Null()
                            , AstForClause.Null()
                        )
                    )
                )
            )
        )
    )
);

Console.WriteLine(result.ToString());
