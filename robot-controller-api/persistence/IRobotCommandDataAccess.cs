namespace robot_controller_api.persistence
{
    public interface IRobotCommandDataAccess
    {
        List<RobotCommand> AddRobotCommand(RobotCommand addCommand);
        List<RobotCommand> DeleteRobotCommand(int id);
        List<RobotCommand> GetMoveCommandsOnly();
        RobotCommand GetRobotCommandById(int id);
        List<RobotCommand> GetRobotCommands();
        RobotCommand UpdateRobotCommands(RobotCommand updatedCommand, int id);
    }
}