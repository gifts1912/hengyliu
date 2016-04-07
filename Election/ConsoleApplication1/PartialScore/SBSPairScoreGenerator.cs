// Decompiled with JetBrains decompiler
// Type: L3RuleSweepingUtil.SBSPairScoreGenerator
// Assembly: SBSPreferenceScore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17724365-D984-4FCD-910B-0545A5892707
// Assembly location: D:\demo\SBSPreferenceScoreV0.1.2\SBSPreferenceScore.exe
// Compiler-generated code is shown

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace L3RuleSweepingUtil
{
  public class SBSPairScoreGenerator
  {
    private int topN;
    private SBSPairScoreContainer container;
    private RankDiffWeighter rdweighter;
    private int querycount;
    private int usefulquerycount;
    [CompilerGenerated]
    private static Func<KeyValuePair<string, int>, int> CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate2;
    [CompilerGenerated]
    private static Func<KeyValuePair<string, int>, int> CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate3;
    [CompilerGenerated]
    private static Func<UrlItem, bool> CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate8;
    [CompilerGenerated]
    private static Func<int, bool> CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate9;

    public SBSPairScoreGenerator()
    {
      this.topN = 3;
      this.container = new SBSPairScoreContainer();
      this.rdweighter = new RankDiffWeighter();
      base.\u002Ector();
    }

    public void Initialize(int n, string rdweight)
    {
      this.topN = n;
      this.rdweighter.Initialize(rdweight);
    }

    public void OutputScore(StreamWriter sw, StreamWriter summary)
    {
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      foreach (KeyValuePair<string, List<SBSPairScoreItem>> keyValuePair in this.container)
      {
        foreach (SBSPairScoreItem sbsPairScoreItem in keyValuePair.Value)
        {
          string key = keyValuePair.Key.ToString();
          sw.WriteLine(key + "\t" + sbsPairScoreItem.url0 + "\t" + sbsPairScoreItem.url1 + "\t" + sbsPairScoreItem.score.ToString("0.0000"));
          if (!dictionary1.ContainsKey(key))
            dictionary1.Add(key, 1);
          else
            dictionary1[key] = dictionary1[key] + 1;
          if (sbsPairScoreItem.url0 == "*" || sbsPairScoreItem.url1 == "*")
          {
            if (!dictionary2.ContainsKey(key))
              dictionary2.Add(key, 1);
            else
              dictionary2[key] = dictionary2[key] + 1;
          }
        }
      }
      Dictionary<string, int> dictionary3 = dictionary1;
      if (SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate2 == null)
      {
        // ISSUE: method pointer
        SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate2 = new Func<KeyValuePair<string, int>, int>((object) null, __methodptr(\u003COutputScore\u003Eb__0));
      }
      Func<KeyValuePair<string, int>, int> selector1 = SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate2;
      int num1 = Enumerable.Sum(Enumerable.Select<KeyValuePair<string, int>, int>((IEnumerable<KeyValuePair<string, int>>) dictionary3, selector1));
      int count1 = dictionary1.Count;
      Dictionary<string, int> dictionary4 = dictionary2;
      if (SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate3 == null)
      {
        // ISSUE: method pointer
        SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate3 = new Func<KeyValuePair<string, int>, int>((object) null, __methodptr(\u003COutputScore\u003Eb__1));
      }
      Func<KeyValuePair<string, int>, int> selector2 = SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate3;
      int num2 = Enumerable.Sum(Enumerable.Select<KeyValuePair<string, int>, int>((IEnumerable<KeyValuePair<string, int>>) dictionary4, selector2));
      int count2 = dictionary2.Count;
      summary.WriteLine("UsefulQuery%: {0} ({1}/{2})", (object) ((float) ((double) this.usefulquerycount / (double) this.querycount * 100.0)).ToString("0.00"), (object) this.usefulquerycount, (object) this.querycount);
      summary.WriteLine("ScoredQuery%: {0} ({1}/{2})", (object) ((float) ((double) count1 / (double) this.usefulquerycount * 100.0)).ToString("0.00"), (object) count1, (object) this.usefulquerycount);
      summary.WriteLine();
      summary.WriteLine("QueryWithDefaultScore%: {0} ({1}/{2})", (object) ((float) ((double) count2 / (double) count1 * 100.0)).ToString("0.00"), (object) count2, (object) count1);
      summary.WriteLine("DefaultScore%: {0} ({1}/{2})", (object) ((float) ((double) num2 / (double) num1 * 100.0)).ToString("0.00"), (object) num2, (object) num1);
      summary.WriteLine("AvgDefaultScorePerQuery: {0} ({1}/{2})", (object) ((float) num2 / (float) count1).ToString("0.00"), (object) num2, (object) count1);
      summary.WriteLine("AvgScorePerQuery: {0} ({1}/{2})", (object) ((float) num1 / (float) count1).ToString("0.00"), (object) num1, (object) count1);
      summary.WriteLine();
      foreach (KeyValuePair<string, int> keyValuePair in dictionary1)
      {
        int num3 = 0;
        if (dictionary2.ContainsKey(keyValuePair.Key))
          num3 = dictionary2[keyValuePair.Key];
        if (num3 > 0)
          summary.WriteLine(keyValuePair.Key + (object) "\t" + (string) (object) keyValuePair.Value + " (" + (string) (object) num3 + ")");
        else
          summary.WriteLine(keyValuePair.Key + (object) "\t" + (string) (object) keyValuePair.Value);
      }
    }

    public void GenerateScore(SBSItem item)
    {
      ++this.querycount;
      if (item.judgement == 0.0)
        return;
      ++this.usefulquerycount;
      string query = item.query;
      UrlItem[] urlItemArray1 = Enumerable.ToArray<UrlItem>(Enumerable.Take<UrlItem>((IEnumerable<UrlItem>) item.urllistgood, this.topN));
      UrlItem[] urlItemArray2 = Enumerable.ToArray<UrlItem>(Enumerable.Take<UrlItem>((IEnumerable<UrlItem>) item.urllistbad, this.topN));
      HashSet<string> hashSet = new HashSet<string>();
      IEnumerable<UrlItem> source1 = Enumerable.Concat<UrlItem>((IEnumerable<UrlItem>) urlItemArray1, (IEnumerable<UrlItem>) urlItemArray2);
      if (SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate8 == null)
      {
        // ISSUE: method pointer
        SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate8 = new Func<UrlItem, bool>((object) null, __methodptr(\u003CGenerateScore\u003Eb__4));
      }
      Func<UrlItem, bool> predicate1 = SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate8;
      foreach (UrlItem urlItem in Enumerable.ToArray<UrlItem>(Enumerable.Distinct<UrlItem>(Enumerable.Where<UrlItem>(source1, predicate1))))
      {
        this.container.Add(query, new SBSPairScoreItem(urlItem.url, "*", item.judgement * (double) urlItem.rating));
        this.container.Add(query, new SBSPairScoreItem("*", urlItem.url, item.judgement * (double) urlItem.rating * -1.0));
      }
      foreach (UrlItem urlItem1 in urlItemArray1)
      {
        foreach (UrlItem urlItem2 in urlItemArray2)
        {
          Func<UrlItem, bool> func1 = (Func<UrlItem, bool>) null;
          Func<UrlItem, int, int> func2 = (Func<UrlItem, int, int>) null;
          SBSPairScoreGenerator.\u003C\u003Ec__DisplayClassc cDisplayClassc = new SBSPairScoreGenerator.\u003C\u003Ec__DisplayClassc();
          cDisplayClassc.u1 = urlItem2;
          UrlItem[] urlItemArray3 = urlItemArray2;
          if (func1 == null)
          {
            // ISSUE: method pointer
            func1 = new Func<UrlItem, bool>((object) cDisplayClassc, __methodptr(\u003CGenerateScore\u003Eb__5));
          }
          Func<UrlItem, bool> predicate2 = func1;
          int num1 = Enumerable.FirstOrDefault<UrlItem>((IEnumerable<UrlItem>) urlItemArray3, predicate2).pos;
          if (!(urlItem1.url == cDisplayClassc.u1.url) && !(urlItem1.url == "") && !(cDisplayClassc.u1.url == ""))
          {
            UrlItem[] urlItemArray4 = urlItemArray1;
            if (func2 == null)
            {
              // ISSUE: method pointer
              func2 = new Func<UrlItem, int, int>((object) cDisplayClassc, __methodptr(\u003CGenerateScore\u003Eb__6));
            }
            Func<UrlItem, int, int> selector = func2;
            IEnumerable<int> source2 = Enumerable.Select<UrlItem, int>((IEnumerable<UrlItem>) urlItemArray4, selector);
            if (SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate9 == null)
            {
              // ISSUE: method pointer
              SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate9 = new Func<int, bool>((object) null, __methodptr(\u003CGenerateScore\u003Eb__7));
            }
            Func<int, bool> predicate3 = SBSPairScoreGenerator.CS\u0024\u003C\u003E9__CachedAnonymousMethodDelegate9;
            int[] numArray = Enumerable.ToArray<int>(Enumerable.Where<int>(source2, predicate3));
            int num2 = num1 + 1;
            if (numArray.Length != 0)
              num2 = Math.Min(num2, numArray[0]);
            if (num1 >= urlItem1.pos && urlItem1.rating >= cDisplayClassc.u1.rating && (!hashSet.Contains(cDisplayClassc.u1.url) && !hashSet.Contains(urlItem1.url)))
            {
              float weight = this.rdweighter.GetWeight(urlItem1.pos, num2);
              this.container.Add(query, new SBSPairScoreItem(urlItem1.url, cDisplayClassc.u1.url, item.judgement * (double) weight));
            }
          }
          else
            break;
        }
        hashSet.Add(urlItem1.url);
      }
    }

    [CompilerGenerated]
    private static int \u003COutputScore\u003Eb__0(KeyValuePair<string, int> n)
    {
      return n.Value;
    }

    [CompilerGenerated]
    private static int \u003COutputScore\u003Eb__1(KeyValuePair<string, int> n)
    {
      return n.Value;
    }

    [CompilerGenerated]
    private static bool \u003CGenerateScore\u003Eb__4(UrlItem n)
    {
      if (n.rating != 0)
        return n.url != "";
      return false;
    }

    [CompilerGenerated]
    private static bool \u003CGenerateScore\u003Eb__7(int n)
    {
      return n >= 0;
    }

    [CompilerGenerated]
    private sealed class \u003C\u003Ec__DisplayClassc
    {
      public UrlItem u1;

      public \u003C\u003Ec__DisplayClassc()
      {
        base.\u002Ector();
      }

      public bool \u003CGenerateScore\u003Eb__5(UrlItem n)
      {
        return n.url == this.u1.url;
      }

      public int \u003CGenerateScore\u003Eb__6(UrlItem u, int i)
      {
        if (u == this.u1)
          return i;
        return -1;
      }
    }
  }
}
