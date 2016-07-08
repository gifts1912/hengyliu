using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using MS.Internal.Bing.DataMining.SearchLogApi;

public class PreviousQueryNumberReducer : Reducer
{
    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        return new Schema("SessionId,PageViewCount:int,PageViewSequenceNumber:int,RawQuery,PreviousQuerySequenceNumber:int");
    }
    public override IEnumerable<Row> Reduce(RowSet input, Row outputRow, string[] args)
    {
        int queryCount = 0;
        int tempSequenceNumber = -1;
        foreach (Row row in input.Rows)
        {
            outputRow["PageViewCount"].Set(row["Session_PageViewCount"].Integer);
            outputRow["PageViewSequenceNumber"].Set(row["Session_PageViewSequenceNumber"].Integer);
            outputRow["RawQuery"].Set(row["Query_RawQuery"].String);
            outputRow["SessionId"].Set(row["SessionId"].String);
            if (queryCount != 0)
                outputRow["PreviousQuerySequenceNumber"].Set(tempSequenceNumber);
            queryCount++;
            tempSequenceNumber = row["Session_PageViewSequenceNumber"].Integer;
            yield return outputRow;
        }
    }
}

public class AnswerProcessor : Processor
{
    public override Schema Produces(string[] requestedColumns, string[] args, Schema input)
    {
        return new Schema("SessionId,PageViewSequenceNumber:int,RawQuery,ClickCount:int,Url,WebVerticalClickCount:int,AlgoClickCount:int,WebIndexClickCount:int");
    }
    public override IEnumerable<Row> Process(RowSet input, Row outputRow, string[] args)
    {
        foreach (Row row in input.Rows)
        {
            AnswerList answers = (AnswerList)row["Page_Entities_Answers"].Value;
            //string RawQuery = row["Query_RawQuery"].String;
            //string SessionId = row["SessionId"].String;
            outputRow["PageViewSequenceNumber"].Set(row["Session_PageViewSequenceNumber"].Integer);
            outputRow["RawQuery"].Set(row["Query_RawQuery"].String);
            outputRow["SessionId"].Set(row["SessionId"].String);
            outputRow["WebVerticalClickCount"].Set(row["Metrics_WebVerticalClickCount"].Integer);
            outputRow["AlgoClickCount"].Set(row["Metrics_AlgoClickCount"].Integer);
            //outputRow["CoreWebIndexClickCount"].Set(row["Metrics_CoreWebIndexClickCount"].Integer);
            //outputRow["CoreResultClickCount"].Set(row["Metrics_CoreResultClickCount"].Integer);
            //outputRow["CoreClickCount"].Set(row["Metrics_CoreClickCount"].Integer);
            outputRow["WebIndexClickCount"].Set(row["Metrics_WebIndexClickCount"].Integer);
            string child2TitleUrl;
            string child2DataSource;
            foreach (Answer answer in answers)
            {
                if (answer.Service != "Reference") continue;
                if (answer.Scenario != "QnAMalta") continue;
                //outputRow["PositionOfEntityInTopLevelRegion"].Set(answer.PositionOfEntityInTopLevelRegion);
                outputRow["ClickCount"].Set(answer.Clicks.Count);
                foreach (PageElement child in answer.Children)
                {
                    child2TitleUrl = "";
                    //child2DataSource = "";
                    foreach (PageElement child2 in child.Children)
                    {
                        if (!string.IsNullOrWhiteSpace(child2.TitleUrl))
                            child2TitleUrl = child2.TitleUrl;
                        /*foreach (KeyValuePair<string, string> p in child2.DataSource.Properties)
                        {
                            child2DataSource = child2DataSource + "\"" + p.Key + "\":" + p.Value + ",";
                        }
                        child2DataSource = "{" + child2DataSource + "},";*/
                    }
                    outputRow["Url"].Set(child2TitleUrl);
                    //outputRow["DataSource"].Set(child2DataSource);
                    yield return outputRow;

                }
            }
        }
    }
}
