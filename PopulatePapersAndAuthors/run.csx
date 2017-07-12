#r "AcademicKnowledgeUtility.dll"

using System;
using AcademicKnowledgeUtility;
using System.Diagnostics;

public static void Run(TimerInfo myTimer,  TraceWriter log)
{
    string cognitiveKey = System.Configuration.ConfigurationManager.ConnectionStrings["academicApiKey"].ConnectionString;
    string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connStringSql"].ConnectionString;
    string schema = System.Configuration.ConfigurationManager.ConnectionStrings["schema"].ConnectionString;

    var paperCount = KesUtility.GetExprEntityCount(SqlUtility.GetUserDatasetExpr(sqlConnectionString, schema), cognitiveKey).Result;

    Stopwatch sw = new Stopwatch();
    sw.Start();
    for (Int64 offset = 0; offset < paperCount; offset += 1000)
    {
        log.Info("Insert " + offset + " : " + sw.Elapsed);
        var fResult = MainETL.PopulatePapersAndAuthors(sqlConnectionString, cognitiveKey, offset).Result;
    }
    sw.Stop();

}
