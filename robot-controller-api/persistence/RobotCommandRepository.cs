using System;
using robot_controller_api.persistence;
using robot_controller_api;
using Npgsql;
using NpgsqlTypes;
public class RobotCommandRepository: IRobotCommandDataAccess, IRepository
{
	
	private IRepository _repo => this;
	public List<RobotCommand> GetRobotCommands()
	{
		var commands = _repo.ExecuteReader<RobotCommand>("SELECT * FROM public.robotcommand");
		return commands;
	}

	public List<RobotCommand> GetMoveCommandsOnly()
    {
		var commands = _repo.ExecuteReader<RobotCommand>("SELECT * FROM robotcommand WHERE IsMoveCommand = true");
		return commands;
    }

	public RobotCommand GetRobotCommandById(int id)
    {
		var command = _repo.ExecuteReader<RobotCommand>("SELECT * FROM robotcommand WHERE id = " + id).Single();
		return command;
    }

	public RobotCommand UpdateRobotCommands(RobotCommand updatedCommand, int id)
	{
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", updatedCommand.Id),
			new("name", updatedCommand.Name),
			new("description", updatedCommand.Description ?? (object)DBNull.Value),
			new("ismovecommand", updatedCommand.IsMoveCommand)
		};

		var result = _repo.ExecuteReader<RobotCommand>("UPDATE robotcommand \nSET name=@name, description=@description, ismovecommand = @ismovecommand, modifieddate = current_timestamp \nWHERE id = @id RETURNING *; ", sqlParams).Single();
		return result;
	}

	public List<RobotCommand> AddRobotCommand(RobotCommand newCommand)
    {
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", newCommand.Id),
			new("name", newCommand.Name),
			new("description", newCommand.Description ?? (object)DBNull.Value),
			new("ismovecommand", newCommand.IsMoveCommand),
			new("createddate", newCommand.CreatedDate),
			new("modifieddate", newCommand.ModifiedDate)
		};

		var result = _repo.ExecuteReader<RobotCommand>("INSERT INTO robotcommands\n(\"Name\", Description, IsMoveCommand, CreatedDate, ModifiedDate)\nVALUES(@name, @description, @ismovecommand, current_timestamp, current_timestamp)", sqlParams).Single();
		var commands = GetRobotCommands();
		commands.Add(result);
		return commands;
    }

	public List<RobotCommand> DeleteRobotCommand(int id)
    {
		var result = _repo.ExecuteReader<RobotCommand>("DELETE FROM robotcommand WHERE id = " + id);
		var commands = GetRobotCommands();
		
		for(int i = 0; i < commands.Count; i++)
        {
			if(commands[i].Id == id)
            {
				commands.RemoveAt(i);
				return commands;
            }
        }

		return null;
    }


}
