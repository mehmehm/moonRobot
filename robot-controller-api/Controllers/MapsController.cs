using Microsoft.AspNetCore.Mvc;
using robot_controller_api.persistence;
namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    IMapDataAccess _mapRepo;
    //MapADO mapdata = new MapADO();

    public MapsController(IMapDataAccess mapRepo)
    {
        _mapRepo = mapRepo;
    }

    [HttpGet()]
    public IEnumerable<Map> getAllMaps()
    
    {
        return _mapRepo.GetMaps();
    }

    [HttpGet("square")]    
    public IEnumerable<Map> getAllSquareMaps()
    {
        return _mapRepo.GetSquareMaps();
    }

    [HttpGet("{id}", Name = "getMap")]
    public IActionResult getMapById(int id)
    {
        Map map = _mapRepo.getMapById(id);

        if (map == null)
        {
            return NotFound();
        }

        return Ok(map);
    }

    [HttpPost()]
    public IActionResult addMap(Map newMap)
    {
        if(newMap == null || newMap.columns <= 0 || newMap.rows <= 0)
        {
            return BadRequest();
        }

        for(int i = 0; i < _mapRepo.GetMaps().Count; i++)
        {
            if(newMap.columns == _mapRepo.GetMaps()[i].columns && _mapRepo.GetMaps()[i].rows == _mapRepo.GetMaps()[i].rows)
            {
                return Conflict();
            }
        }

        _mapRepo.AddMap(newMap);
        
        return CreatedAtRoute("GetMap", new { id = newMap.id }, newMap);
    }

    [HttpPut("{id}")]
    public IActionResult updateMap(int id, Map updatedMap)
    {            
        if(updatedMap.columns <= 0 || updatedMap.rows <= 0 || updatedMap == null)
        {
            return BadRequest();
        }
        Map map = _mapRepo.UpdateMap(updatedMap, id);

        if (map == null)
        {
            return NotFound();
        }
        
        return NoContent();    
    }

    [HttpDelete("{id}")]
    public IActionResult deleteMap(int id)
    {          
        List<Map> maps = _mapRepo.DeleteMap(id);
        
        if(maps == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{Id}/{x}-{y}")]
    public IActionResult checkMapCoordinates(int Id, int x, int y)
    {
        var map = _mapRepo.GetMaps().Find(a => a.id == Id);

        if(x > 0 && y > 0 && x < map.rows && y < map.columns)
        {
            return Ok(true);
        }

        return Ok(false);
    }
}
