﻿using System.Composition;
using Ultramarine.Generators.Tasks.Library.Contracts;

namespace Ultramarine.Generators.Tasks.Library
{
    [Export(typeof(Task))]
    public class Generator : CompositeTask
    {
        
    }
}
