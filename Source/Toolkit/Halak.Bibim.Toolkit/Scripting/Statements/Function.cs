﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Halak.Bibim.Scripting.Statements
{
    public sealed class Function : Block
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
                if (name != value)
                {
                    name = value ?? string.Empty;
                }
            }
        }
        #endregion

        #region Constructors
        public Function()
            : this(string.Empty)
        {
        }

        public Function(string name)
        {
            this.name = name ?? string.Empty;
        }

        public Function(string name, IEnumerable<Statement> statements)
            : base(statements)
        {
            this.name = name ?? string.Empty;
        }
        #endregion

        #region Methods
        public override void Generate(BinaryScriptGenerator.Context context)
        {
            base.Generate(context);
        }
        #endregion
    }
}
