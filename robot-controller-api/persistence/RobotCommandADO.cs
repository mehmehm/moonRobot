using Npgsql;

namespace robot_controller_api.persistence;

public class RobotCommandADO : IRobotCommandDataAccess
{
    // Connection string is usually set in a config file for the ease of change.
    private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=Jubal@2309;Database=postgres";
    public List<RobotCommand> GetRobotCommands()
    {
        var robotcommands = new List<RobotCommand>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn);

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            var id = (int)dr["Id"];
            var desc = dr["Description"] as string;
            var name = (string)dr["Name"];
            var ismovecommand = (bool)dr["IsMoveCommand"];
            var createddate = (DateTime)dr["CreatedDate"];
            var modifieddate = (DateTime)dr["ModifiedDate"];

            RobotCommand newcommand = new RobotCommand(id, name, ismovecommand, createddate, modifieddate, desc);
            robotcommands.Add(newcommand);
        }

        return robotcommands;
    }

    public List<RobotCommand> GetMoveCommandsOnly()
    {
        List<RobotCommand> moveCommands = new List<RobotCommand>();
        List<RobotCommand> allCommands = GetRobotCommands();
        for (int i = 1; i < allCommands.Count; i++)
        {
            if (allCommands[i].IsMoveCommand)
            {
                moveCommands.Add(allCommands[i]);
            }
        }

        return moveCommands;
    }

    public RobotCommand GetRobotCommandById(int id)
    {
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE Id = " + id, conn);

        using var dr = cmd.ExecuteReader();

        while(dr.Read())
        {
            var Id = (int)dr["Id"];
            var desc = dr["Description"] as string;
            var name = (string)dr["Name"];
            var ismovecommand = (bool)dr["IsMoveCommand"];
            var createddate = (DateTime)dr["CreatedDate"];
            var modifieddate = (DateTime)dr["ModifiedDate"];

            RobotCommand newcommand = new RobotCommand(Id, name, ismovecommand, createddate, modifieddate, desc);
            return newcommand;
        }
        return null;
    }

    public RobotCommand UpdateRobotCommands(RobotCommand updatedCommand, int id)
    {
        List<RobotCommand> commands = GetRobotCommands();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("UPDATE RobotCommand" +
            "\nSET \"Name\" = " + $"'{updatedCommand.Name}'," + "Description = " + $"'{updatedCommand.Description}', " + "IsMoveCommand = " + $"{updatedCommand.IsMoveCommand}, "
            + "CreatedDate = " + $"TO_TIMESTAMP('{updatedCommand.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'), "
            + "ModifiedDate = " + $"TO_TIMESTAMP('{updatedCommand.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS')"
            + "\nWHERE id = " + $"{updatedCommand.Id}", conn);

        using var dr = cmd.ExecuteReader();

        if (id < 1 || id > commands.Count)
        {
            throw new Exception();
        }

        commands[id - 1].Name = updatedCommand.Name;
        commands[id - 1].IsMoveCommand = updatedCommand.IsMoveCommand;
        commands[id - 1].ModifiedDate = updatedCommand.ModifiedDate;
        return commands[id - 1];
    }

    public List<RobotCommand> AddRobotCommand(RobotCommand addCommand)
    {
        List<RobotCommand> commands = GetRobotCommands();

        var robotCommands = new List<RobotCommand>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO robotcommand" + " \n( " + "\"Name\"" + ", Description, IsMoveCommand, CreatedDate, ModifiedDate)" + "\nVALUES(" +
        $"'{addCommand.Name}', " +
        $"'{addCommand.Description}', " + $"{addCommand.IsMoveCommand}, " +
        $"TO_TIMESTAMP('{addCommand.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'), " +
        $"TO_TIMESTAMP('{addCommand.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'))", conn);

        using var dr = cmd.ExecuteReader();

        commands.Add(addCommand);

        return commands;
    }

    public List<RobotCommand> DeleteRobotCommand(int id)
    {
        List<RobotCommand> commands = GetRobotCommands();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM robotcommand WHERE id = " + id, conn);

        using var dr = cmd.ExecuteReader();

        if (id < 1)
        {
            return null;
        }

        int i = 0;
        for (i = 0; i < commands.Count; i++)
        {
            if (commands[i].Id == id)
            {
                commands.RemoveAt(i);
                return commands;
            }
        }

        return null;
    }
}


