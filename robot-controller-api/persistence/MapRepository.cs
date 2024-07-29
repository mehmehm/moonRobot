using System;
using robot_controller_api.persistence;
using robot_controller_api;
using Npgsql;
using NpgsqlTypes;
public class MapRepository : IMapDataAccess, IRepository
{

	private IRepository _repo => this;
	public List<Map> GetMaps()
	{
		var map = _repo.ExecuteReader<Map>("SELECT * FROM public.map");
		return map;
	}

	public List<Map> GetSquareMaps()
	{
		var maps = _repo.ExecuteReader<Map>("SELECT FROM public.map WHERE rows = columns");
		return maps;
	}

	public Map getMapById(int id)
	{
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", id)
		};
	
		var map = _repo.ExecuteReader<Map>("SELECT FROM map WHERE id = @id", sqlParams).Single();
		return map;
	}

	public Map UpdateMap(Map updatedMap, int id)
	{
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", updatedMap.id),
			new("name", updatedMap.name),
			new("description", updatedMap.description ?? (object)DBNull.Value),
			new("columns", updatedMap.columns),
			new("rows", updatedMap.rows)
		};

		var result = _repo.ExecuteReader<Map>("UPDATE robotcommand \nSET name=@name, description=@description, ismovecommand = @ismovecommand, modifieddate = current_timestamp \nWHERE id = @id RETURNING *; ", sqlParams).Single();
		return result;
	}

	public List<Map> AddMap(Map newMap)
	{
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", newMap.id),
			new("name", newMap.name),
			new("description", newMap.description ?? (object)DBNull.Value),
			new("columns", newMap.columns),
			new("rows", newMap.rows),
			new("createddate", newMap.createdDate),
			new("modifieddate", newMap.modifiedDate)
		};

		var result = _repo.ExecuteReader<Map>("INSERT INTO robotcommands\n(\"Name\", Description, IsMoveCommand, CreatedDate, ModifiedDate)\nVALUES(@name, @description, @ismovecommand, current_timestamp, current_timestamp)", sqlParams).Single();
		var maps = GetMaps();
		maps.Add(result);
		return maps;
	}

	public List<Map> DeleteMap(int id)
	{
		var sqlParams = new NpgsqlParameter[]
		{
			new("id", id),
		};

		var result = _repo.ExecuteReader<Map>("DELETE FROM robotcommand WHERE id = @id", sqlParams);
		return result;
	}

}
