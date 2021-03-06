﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bibim.Scripting.Statements.Operators
{
    [OperatorPrecedence(4)]
    public sealed class AdditionOperator : BinaryOperator
    {
        #region Constructors
        public AdditionOperator()
        {
        }

        public AdditionOperator(Expression leftOperand, Expression rightOperand)
            : base(leftOperand, rightOperand)
        {
        }
        #endregion

        #region Methods
        public override void Generate(ScriptCompiler.Context context)
        {
            Generate(context, ScriptInstruction.AddInt);
        }

        public override string ToString()
        {
            return string.Format("{0} + {1}", ToString(LeftOperand), ToString(RightOperand));
        }
        #endregion
    }
}
