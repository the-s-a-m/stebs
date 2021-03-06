﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorSimulation
{
    /// <summary>
    /// A wrapper around IRegister instances with type Registers.Status.
    /// This allows easier access on the different defined flags.
    /// </summary>
    public class StatusRegister
    {
        private const uint InterruptBit = 16;
        private const uint SignedBit = 8;
        private const uint OverflowBit = 4;
        private const uint ZeroBit = 2;
        public IRegister Register { get; }
        public uint Value => Register.Value;
        public Registers Type => Registers.Status;
        /// <summary>Returns if interrupts are enabled. This is changed to true wit the command STI and to false with command CLI.</summary>
        public bool Interrupt => (Value & InterruptBit) != 0;
        /// <summary>Returns if the last ALU result was negative in a 8-bit two complement representation.</summary>
        public bool Signed => (Value & SignedBit) != 0;
        /// <summary>Returns if the last ALU operation caused an overflow.</summary>
        public bool Overflow => (Value & OverflowBit) != 0;
        /// <summary>Returns if the last ALU result was 0.</summary>
        public bool Zero => (Value & ZeroBit) != 0;
        public StatusRegister(IRegister register)
        {
            if(register.Type != Registers.Status)
            {
                throw new ArgumentException("Only IRegister instances with the type Registers.Status are allowed in the class StatusRegister");
            }
            this.Register = register;
        }
        private StatusRegister SetBit(bool flag, uint bit, RegisterFactory registerFactory) => new StatusRegister(registerFactory(Registers.Status, flag ? (Value | bit) : (Value & ~bit)));
        /// <summary>Creates new IRegister, which only differs in the interrupt flag, which is set to the given value.</summary>
        public StatusRegister SetInterrupt(bool flag, RegisterFactory registerFactory) => SetBit(flag, InterruptBit, registerFactory);
        /// <summary>Creates new IRegister, which only differs in the signed flag, which is set to the given value.</summary>
        public StatusRegister SetSigned(bool flag, RegisterFactory registerFactory) => SetBit(flag, SignedBit, registerFactory);
        /// <summary>Creates new IRegister, which only differs in the overflow flag, which is set to the given value.</summary>
        public StatusRegister SetOverflow(bool flag, RegisterFactory registerFactory) => SetBit(flag, OverflowBit, registerFactory);
        /// <summary>Creates new IRegister, which only differs in the zero flag, which is set to the given value.</summary>
        public StatusRegister SetZero(bool flag, RegisterFactory registerFactory) => SetBit(flag, ZeroBit, registerFactory);
    }
}
