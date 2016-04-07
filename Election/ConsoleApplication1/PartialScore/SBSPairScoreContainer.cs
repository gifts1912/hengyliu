// Decompiled with JetBrains decompiler
// Type: L3RuleSweepingUtil.SBSPairScoreContainer
// Assembly: SBSPreferenceScore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17724365-D984-4FCD-910B-0545A5892707
// Assembly location: D:\demo\SBSPreferenceScoreV0.1.2\SBSPreferenceScore.exe
// Compiler-generated code is shown

using System.Collections;
using System.Collections.Generic;

namespace L3RuleSweepingUtil
{
  public class SBSPairScoreContainer : IEnumerable
  {
    private Dictionary<string, List<SBSPairScoreItem>> container;

    public int Count
    {
      get
      {
        return this.container.Count;
      }
    }

    public SBSPairScoreContainer()
    {
      this.container = new Dictionary<string, List<SBSPairScoreItem>>();
      base.\u002Ector();
    }

    public IEnumerator<KeyValuePair<string, List<SBSPairScoreItem>>> _GetEnum()
    {
      return (IEnumerator<KeyValuePair<string, List<SBSPairScoreItem>>>) this.container.GetEnumerator();
    }

    public IEnumerator<KeyValuePair<string, List<SBSPairScoreItem>>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, List<SBSPairScoreItem>>>) new SBSPairScoreContainerEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new SBSPairScoreContainerEnumerator(this);
    }

    public void Add(string query, SBSPairScoreItem item)
    {
      if (!this.container.ContainsKey(query))
        this.container.Add(query, new List<SBSPairScoreItem>());
      this.container[query].Add(item);
    }
  }
}
