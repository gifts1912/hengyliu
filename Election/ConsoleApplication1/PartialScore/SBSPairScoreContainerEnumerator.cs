// Decompiled with JetBrains decompiler
// Type: L3RuleSweepingUtil.SBSPairScoreContainerEnumerator
// Assembly: SBSPreferenceScore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17724365-D984-4FCD-910B-0545A5892707
// Assembly location: D:\demo\SBSPreferenceScoreV0.1.2\SBSPreferenceScore.exe
// Compiler-generated code is shown

using System;
using System.Collections;
using System.Collections.Generic;

namespace L3RuleSweepingUtil
{
  public class SBSPairScoreContainerEnumerator : IEnumerator<KeyValuePair<string, List<SBSPairScoreItem>>>, IDisposable, IEnumerator
  {
    private SBSPairScoreContainer _collection;
    private IEnumerator cur;

    public KeyValuePair<string, List<SBSPairScoreItem>> Current
    {
      get
      {
        return (KeyValuePair<string, List<SBSPairScoreItem>>) this.cur.Current;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public SBSPairScoreContainerEnumerator(SBSPairScoreContainer collection)
    {
      base.\u002Ector();
      this._collection = collection;
      this.cur = (IEnumerator) this._collection._GetEnum();
    }

    public bool MoveNext()
    {
      return this.cur.MoveNext();
    }

    public void Reset()
    {
      this.cur = (IEnumerator) null;
    }

    void IDisposable.Dispose()
    {
    }
  }
}
