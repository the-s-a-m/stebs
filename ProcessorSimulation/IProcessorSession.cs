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
        void SetRegister(Registers type, uint value);

        /// <summary>Method to set a register value.</summary>
        /// <param name="register">New register to be set.</param>
        void SetRegister(IRegister register);

        /// <summary>
        /// Session instance, that allows write access to the ram, used by the accessed processor.
        /// </summary>
        IRamSession RamSession { get; }

        /// <summary>
        /// Access to the Processor encasuled in the session for read operations.
        /// </summary>
        IProcessor Processor { get; }
    }
}
