﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bibim.Animation
{
    public abstract class EvalVariableTemplate<T> : Eval<T>
    {
        #region Fields
        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value ?? string.Empty;
            }
        }
        #endregion

        #region Constructors
        protected EvalVariableTemplate()
            : this(string.Empty)
        {
        }

        protected EvalVariableTemplate(string value)
        {
            Name = value;
        }
        #endregion

        public override T Evaluate(EvalContext context)
        {
            return default(T);
        }
    }
}
