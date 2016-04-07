using System;
// Decompiled with JetBrains decompiler
// Type: SBSPreferenceScore.Program
// Assembly: SBSPreferenceScore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17724365-D984-4FCD-910B-0545A5892707
// Assembly location: D:\demo\SBSPreferenceScoreV0.1.2\SBSPreferenceScore.exe
// Compiler-generated code is shown

using L3RuleSweepingUtil;
using Microsoft.TMSN.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SBSPreferenceScore
{
  internal class Program
  {
    private static SBSPairScoreGenerator generator;
    private static string querycolname;
    private static string judgecolname;
    private static string ratingcolformat;
    private static string urlcolformat;
    [CompilerGenerated]
    private static Func<string, string> CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate1;

    static Program()
    {
      Program.generator = new SBSPairScoreGenerator();
      Program.querycolname = "query";
      Program.judgecolname = "judgment";
      Program.ratingcolformat = "rating__{0}_";
      Program.urlcolformat = "{0}html";
    }

    public Program()
    {
      base.\u002Ector();
    }

    private static void ParseDataLine(string line)
    {
      SBSItem sbsItem = new SBSItem();
      sbsItem.ParseLine(line);
      Program.generator.GenerateScore(sbsItem);
    }

    private static bool ParseHeadLine(string line)
    {
      string[] strArray1 = line.Split("\t".ToCharArray());
      if (Program.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate1 == null)
      {
        // ISSUE: method pointer
        Program.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate1 = new Func<string, string>((object) null, __methodptr(\u003CParseHeadLine\u003Eb__0));
      }
      Func<string, string> selector = Program.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate1;
      string[] strArray2 = Enumerable.ToArray<string>(Enumerable.Select<string, string>((IEnumerable<string>) strArray1, selector));
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (strArray2[index] == Program.querycolname)
          SBSItem.queryidx = index;
        else if (strArray2[index] == Program.judgecolname)
          SBSItem.judgementidx = index;
        else if (strArray2[index].StartsWith(string.Format(Program.urlcolformat, (object) "left")))
          SBSItem.lefturlidx.Add(index);
        else if (strArray2[index].StartsWith(string.Format(Program.urlcolformat, (object) "right")))
          SBSItem.righturlidx.Add(index);
        else if (strArray2[index].StartsWith(string.Format(Program.ratingcolformat, (object) "left")))
          SBSItem.leftratingidx.Add(index);
        else if (strArray2[index].StartsWith(string.Format(Program.ratingcolformat, (object) "right")))
          SBSItem.rightratingidx.Add(index);
      }
      return SBSItem.judgementidx >= 0 && SBSItem.queryidx >= 0 && (SBSItem.lefturlidx.Count != 0 && SBSItem.righturlidx.Count != 0) && (SBSItem.leftratingidx.Count != 0 && SBSItem.rightratingidx.Count != 0);
    }

    private static void Main(string[] args)
    {
      Program.Args args1 = new Program.Args();
      if (!Parser.ParseArgumentsWithUsage(args, (object) args1))
        throw new Exception("Command line syntax error.");
      bool flag = true;
      Program.generator.Initialize(args1.TopN, args1.RankDiffWeightingString);
      using (StreamReader streamReader = new StreamReader(args1.InputData))
      {
        using (StreamWriter sw = new StreamWriter(args1.OutputScore))
        {
          using (StreamWriter summary = new StreamWriter(args1.OutputSummary))
          {
            while (!streamReader.EndOfStream)
            {
              string line = streamReader.ReadLine();
              if (flag)
              {
                flag = false;
                if (!Program.ParseHeadLine(line))
                  throw new Exception("Headline parsing error!");
              }
              else
                Program.ParseDataLine(line);
            }
            Program.generator.OutputScore(sw, summary);
          }
        }
      }
    }

    [CompilerGenerated]
    private static string \u003CParseHeadLine\u003Eb__0(string x)
    {
      return x.ToLower();
    }

    private class Args : CmdOptions
    {
      [Argument(ArgumentType.Required, ShortName = "i")]
      public string InputData;
      [Argument(ArgumentType.Required, ShortName = "o")]
      public string OutputScore;
      [Argument(ArgumentType.Required, ShortName = "s")]
      public string OutputSummary;
      [Argument(ArgumentType.AtMostOnce)]
      public int TopN;
      [Argument(ArgumentType.AtMostOnce)]
      public string RankDiffWeightingString;

      public Args()
      {
        this.InputData = string.Empty;
        this.OutputScore = string.Empty;
        this.OutputSummary = string.Empty;
        this.RankDiffWeightingString = string.Empty;
        base.\u002Ector();
      }
    }
  }
}
