﻿using RobotController.Commands;
using RobotController;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace RobotController.CommandProviders
{
    /// <summary>
    /// <see cref="FileCommandProvider"/> provides continuous input of robot commands from the file.
    /// </summary>
    internal class FileCommandProvider : ICommandProvider
    {
        IFileSystem _fileSystem;

        public FileCommandProvider(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public FileCommandProvider() : this(new FileSystem())
        {

        }

        const string PLACE_REGEX = @"^PLACE[\s\u3000-[\r\n]][0-4],[0-4],(NORTH|EAST|WEST|SOUTH)$";

        /// <summary>
        /// Gets continuous input of robot commands from the user in the console.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICommand> GetCommands(string[] args)
        {
            var fileName = args[0];

            var lines = _fileSystem.File.ReadLines(fileName);
            foreach (var line in lines)
            {
                string command = line;

                if (command == "MOVE")
                    yield return new MoveCommand();
                else if (command == "LEFT")
                    yield return new LeftCommand();
                else if (command == "RIGHT")
                    yield return new RightCommand();
                else if (command == "REPORT")
                    yield return new ReportCommand();
                else if (Regex.IsMatch(command, PLACE_REGEX))
                    yield return Deserialize(command);

            }
        }

        /// <summary>
        /// Deserializes <see cref="PlaceCommand" from the <see cref="string"/>/>
        /// </summary>
        /// <param name="command">Serialized <see cref="PlaceCommand"/> presentation.</param>
        /// <returns><see cref="PlaceCommand"/></returns>
        public static PlaceCommand Deserialize(string command)
        {
            string[] placeParams = command.Substring(6).Split(',');
            var x = int.Parse(placeParams[0]);
            var y = int.Parse(placeParams[1]);
            Direction facing = Direction.North;

            switch (placeParams[2])
            {
                case "NORTH": facing = Direction.North; break;
                case "EAST": facing = Direction.East; break;
                case "SOUTH": facing = Direction.South; break;
                case "WEST": facing = Direction.West; break;
                default: return null;
            }

            return new PlaceCommand(x, y, facing, command);
        }
    }
}
