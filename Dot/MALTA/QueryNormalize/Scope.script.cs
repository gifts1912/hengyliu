﻿using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public class MyTrim
{
    public static String RemoveDoubleQuote(String s)
    {
        return s.Replace("\"", "");
    }
}