using Microsoft.AspNetCore.Mvc;
using robot_controller_api.persistence;
using Microsoft.Extensions.DependencyInjection;

namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private readonly IRobotCommandDataAccess _robotCommandsRepo;

    public RobotCommandsController(IRobotCommandDataAccess robotCommandsRepo)
    {
        _robotCommandsRepo = robotCommandsRepo;
    }

    //RobotCommandADO robotcommanddata = new RobotCommandADO();

    [HttpGet()]
    public IEnumerable<RobotCommand> GetAllRobotCommands()
    {
        
        return _robotCommandsRepo.GetRobotCommands();
    }

    [HttpGet("move")]
    public IEnumerable<RobotCommand> GetMoveCommandsOnly()
    {
        return _robotCommandsRepo.GetMoveCommandsOnly();
    }

    [HttpGet("{id}", Name = "GetRobotCommand")]
    public IActionResult GetRobotCommandById(int id)
    {
        RobotCommand command = _robotCommandsRepo.GetRobotCommandById(id);

        if(command == null)
        {
            return NotFound();
        }
        return Ok(command);
    }

    [HttpPost()]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {
        if(newCommand == null)
        {
            return BadRequest();
        }

        for(int i = 1; i < _robotCommandsRepo.GetRobotCommands().Count; i++)
        {
            if(newCommand.Name == _robotCommandsRepo.GetRobotCommands()[i].Name)
            {
                return Conflict();
            }
        }

        _robotCommandsRepo.AddRobotCommand(newCommand);

        return CreatedAtRoute("GetRobotCommand", new { id = newCommand.Id }, newCommand);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
    {      
        try
        {
            RobotCommand command = _robotCommandsRepo.UpdateRobotCommands(updatedCommand, id);

            if(command == null)
            {
                return NotFound();
            }
        }
        catch (Exception exception)
        {
            return BadRequest();
        }

        return NoContent();          
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {     
        List<RobotCommand> commands = _robotCommandsRepo.DeleteRobotCommand(id);

        if(commands == null)
        {
            return NotFound();
        }

        return NoContent(); 
    }

}
