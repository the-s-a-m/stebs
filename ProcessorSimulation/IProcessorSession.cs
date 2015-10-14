﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorSimulation
{
    public interface IProcessorSession : IDisposable
    {
        /// <summary>Method to set a register value.</summary>
        /// <param name="register">Register type</param>
        /// <param name="value">New value</param>
        void SetRegister(Registers type, byte value);
    }
}