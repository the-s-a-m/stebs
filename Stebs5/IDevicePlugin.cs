﻿using ProcessorSimulation.Device;

namespace Stebs5
{
    /// <summary>
    /// A device plugin is a stebs plugin, which adds devices.
    /// Devices are sensors or actors for processors. (See <see cref="IDevice" />)
    /// </summary>
    public interface IDevicePlugin
    {
        /// <summary>
        /// Name of the plugin, which is displayed to the user.
        /// </summary>
        string Name { get; }
        string PluginId { get; }
        /// <summary>
        /// Html template of the device.
        /// </summary>
        string DeviceTemplate { get; }
        IDevice CreateDevice();
    }
}