#r "AcademicKnowledgeUtility.dll"

using System;
using System.Diagnostics;
using AcademicKnowledgeUtility;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    string cognitiveKey = System.Configuration.ConfigurationManager.ConnectionStrings["academicApiKey"].ConnectionString;
    string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connStringSql"].ConnectionString;

    Stopwatch sw = new Stopwatch();
    var journalCount = KesUtility.GetExprEntityCount(MainETL.JournalExpr, cognitiveKey).Result;
    sw.Restart();
    for (Int64 offset = 0; offset < journalCount; offset += 1000)
    {
        log.Info("Insert " + offset + " : " + sw.Elapsed);
        var fResult = MainETL.PopulateJournals(sqlConnectionString, cognitiveKey, offset).Result;
    }
    log.Info("Spent " + sw.Elapsed + " moving " + journalCount + " journals");

    var conferenceCount = KesUtility.GetExprEntityCount(MainETL.ConferenceExpr, cognitiveKey).Result;
    sw.Restart();
    for (Int64 offset = 0; offset < conferenceCount; offset += 1000)
    {
        log.Info("Insert " + offset + " : " + sw.Elapsed);
        var fResult = MainETL.PopulateConference(sqlConnectionString, cognitiveKey, offset).Result;
    }
    log.Info("Spent " + sw.Elapsed + " moving " + conferenceCount + " conferences");



    var affiliationCount = KesUtility.GetExprEntityCount(MainETL.AffiliationExpr, cognitiveKey).Result;
    sw.Restart();
    for (Int64 offset = 0; offset < affiliationCount; offset += 1000)
    {
        log.Info("Insert " + offset + " : " + sw.Elapsed);
        var fResult = MainETL.PopulateAffiliation(sqlConnectionString, cognitiveKey, offset).Result;
    }
    log.Info("Spent " + sw.Elapsed + " moving " + affiliationCount + " affiliations");



    var fosCount = KesUtility.GetExprEntityCount(MainETL.FieldsOfStudyExpr, cognitiveKey).Result;
    sw.Restart();
    for (Int64 offset = 0; offset < fosCount; offset += 1000)
    {
        log.Info("Insert " + offset + " : " + sw.Elapsed);
        var fResult = MainETL.PopulateFieldsOfStudy(sqlConnectionString, cognitiveKey, offset).Result;
    }
    log.Info("Spent " + sw.Elapsed + " moving " + fosCount + " fos");
}


