﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halak.Bibim.Scripting.Statements.Operators
{
    // () [] . -> ++ --
    // ++ -- + - ! ~ &
    // * % /
    // + -
    // << >>
    // < <=
    // > >=
    // == !=
    // &
    // ^
    // |
    // &&
    // ||
    // ?:
    // =
    // ,
    public abstract class BinaryOperator : Expression
    {
        #region Properties
        public Expression LeftOperand
        {
            get;
            set;
        }

        public Expression RightOperand
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public BinaryOperator()
        {
        }

        public BinaryOperator(Expression leftOperand, Expression rightOperand)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }
        #endregion

        #region Methods
        protected static string ToString(Expression operand)
        {
            if (operand != null)
                return operand.ToString();
            else
                return "(null)";
        }
        #endregion
    }
}
