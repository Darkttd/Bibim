﻿using System;
using System.Collections.Generic;
using System.Text;
using Bibim.Reflection;

namespace Bibim.Scripting
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ScriptNativeFunctionAttribute : Attribute
    {
        #region Properties
        public int FunctionID
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public ScriptNativeFunctionAttribute(char a, char b, char c, char d)
            : this(FOURCC.Make(a, b, c, d))
        {
        }

        public ScriptNativeFunctionAttribute(int functionID)
        {
            FunctionID = functionID;
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ScriptNativeFunctionReturnAttribute : Attribute
    {
        #region Properties
        public ScriptObjectType Type
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public ScriptNativeFunctionReturnAttribute(ScriptObjectType type)
        {
            Type = type;
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ScriptNativeFunctionParameterAttribute : Attribute
    {
        #region Properties
        public ScriptObjectType Type
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public ScriptNativeFunctionParameterAttribute(ScriptObjectType type)
        {
            Type = type;
            Name = string.Empty;
        }

        public ScriptNativeFunctionParameterAttribute(ScriptObjectType type, string name)
        {
            Type = type;
            Name = name ?? string.Empty;
        }
        #endregion
    }
}
