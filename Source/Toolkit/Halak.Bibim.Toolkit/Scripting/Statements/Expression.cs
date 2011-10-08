﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halak.Bibim.Scripting.Statements
{
    public abstract class Expression : Statement
    {
        public virtual ScriptObjectType GuessResultType(BinaryScriptGenerator.Context context) { return ScriptObjectType.Null; }
    }
}