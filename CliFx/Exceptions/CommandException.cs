﻿using System;

namespace CliFx.Exceptions
{
    /// <summary>
    /// Thrown when a command cannot proceed with normal execution due to an error.
    /// Use this exception if you want to report an error that occured during execution of a command.
    /// This exception also allows specifying exit code which will be returned to the calling process.
    /// </summary>
    public class CommandException : CliFxException
    {
        /// <summary>
        /// Initializes an instance of <see cref="CommandException"/>.
        /// </summary>
        public CommandException(string? message, Exception? innerException, 
            int exitCode = DefaultExitCode, bool showHelp = false)
                : base(message, innerException, exitCode, showHelp)
        {
            
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandException"/>.
        /// </summary>
        public CommandException(string? message, int exitCode = DefaultExitCode, bool showHelp = false)
                : this(message, null, exitCode, showHelp)
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandException"/>.
        /// </summary>
        public CommandException(int exitCode = DefaultExitCode, bool showHelp = false)
            : this(null, exitCode, showHelp)
        {
        }
    }
}