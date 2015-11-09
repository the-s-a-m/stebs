﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorSimulation
{
    public interface IProcessor
    {
        /// <summary>Event that is fired, when a register changed.</summary>
        event Action<IProcessor, IRegister> RegisterChanged;
        /// <summary>Event, that is fired, when a step is started/stopped by the simulator.</summary>
        event Action<IProcessor, SimulationState, SimulationStepSize> SimulationStateChanged;

        IAlu Alu { get; }
        IReadOnlyRam Ram { get; }
        IDictionary<Registers, IRegister> Registers { get; }

        /// <summary>Create session, with which the processor state can be modified.</summary>
        /// <returns>Session instance</returns>
        /// <remarks>This method can block, because only one session should exist and it should be used by one thread only.</remarks>
        IProcessorSession createSession();

        /// <summary>Notifies, that the simulator started/stopped a simulation step.</summary>
        /// <param name="state">State in which the current simulation is.</param>
        /// <param name="stepSize">Step size which is beeing simulated.</param>
        void NotifySimulationStateChanged(SimulationState state, SimulationStepSize stepSize);
    }
}
