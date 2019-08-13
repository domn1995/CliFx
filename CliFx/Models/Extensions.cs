﻿using System;
using System.Collections.Generic;
using System.Linq;
using CliFx.Internal;

namespace CliFx.Models
{
    public static class Extensions
    {
        public static bool IsCommandSpecified(this CommandInput commandInput) => !commandInput.CommandName.IsNullOrWhiteSpace();

        public static bool IsHelpRequested(this CommandInput commandInput)
        {
            var firstOptionAlias = commandInput.Options.FirstOrDefault()?.Alias;

            return string.Equals(firstOptionAlias, "help", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(firstOptionAlias, "h", StringComparison.Ordinal) ||
                   string.Equals(firstOptionAlias, "?", StringComparison.Ordinal);
        }

        public static bool IsVersionRequested(this CommandInput commandInput)
        {
            var firstOptionAlias = commandInput.Options.FirstOrDefault()?.Alias;

            return string.Equals(firstOptionAlias, "version", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsDefault(this CommandSchema commandSchema) => commandSchema.Name.IsNullOrWhiteSpace();

        public static CommandSchema FindByName(this IReadOnlyList<CommandSchema> commandSchemas, string commandName) =>
            commandSchemas.FirstOrDefault(c => string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase));

        public static CommandSchema FindParent(this IReadOnlyList<CommandSchema> commandSchemas, string commandName)
        {
            // If command has no name, it's the default command so it doesn't have a parent
            if (commandName.IsNullOrWhiteSpace())
                return null;

            // Repeatedly cut off individual words from the name until we find a command with that name
            var temp = commandName;
            while (temp.Contains(" "))
            {
                temp = temp.SubstringUntilLast(" ");

                var parent = commandSchemas.FindByName(temp);
                if (parent != null)
                    return parent;
            }

            // If no parent is matched by name, then the parent is the default command
            return commandSchemas.FirstOrDefault(c => c.IsDefault());
        }

        public static CommandOptionSchema FindByAlias(this IReadOnlyList<CommandOptionSchema> optionSchemas, string alias)
        {
            foreach (var optionSchema in optionSchemas)
            {
                // Compare against name. Case is ignored.
                var matchesByName =
                    !optionSchema.Name.IsNullOrWhiteSpace() &&
                    string.Equals(optionSchema.Name, alias, StringComparison.OrdinalIgnoreCase);

                // Compare against short name. Case is NOT ignored.
                var matchesByShortName =
                    optionSchema.ShortName != null &&
                    alias.Length == 1 &&
                    alias[0] == optionSchema.ShortName;

                if (matchesByName || matchesByShortName)
                    return optionSchema;
            }

            return null;
        }
    }
}