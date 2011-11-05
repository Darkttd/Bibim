﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bibim.Animation
{
    public abstract class EvalConstantTemplate<T> : Eval<T>
    {
        #region Properties
        public T Value
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        protected EvalConstantTemplate()
        {
            Value = default(T);
        }

        protected EvalConstantTemplate(T value)
        {
            Value = value;
        }
        #endregion

        public override T Evaluate(EvalContext context)
        {
            return Value;
        }
    }
}
