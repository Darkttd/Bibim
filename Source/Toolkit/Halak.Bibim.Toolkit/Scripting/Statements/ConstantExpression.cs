﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halak.Bibim.Scripting.Statements
{
    public sealed class ConstantExpression : Expression
    {
        #region Properties
        public object Value
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public ConstantExpression()
            : this(null)
        {
        }

        public ConstantExpression(object value)
        {
            Value = value;
        }
        #endregion

        #region Methods
        public override void Generate(BinaryScriptGenerator.Context context)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();
            else
                return "(null)";
        }
        #endregion
    }
}
