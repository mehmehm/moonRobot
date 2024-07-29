using Npgsql;

namespace robot_controller_api.persistence;

public class MapADO : IMapDataAccess
{

    private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=Jubal@2309;Database=postgres";
    public List<Map> GetMaps()
    {
        var map = new List<Map>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM map", conn);

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            var id = (int)dr["id"];
            var name = (string)dr["Name"];
            var desc = dr["description"] as string;
            var columns = (int)dr["columns"];
            var rows = (int)dr["rows"];
            var createddate = (DateTime)dr["CreatedDate"];
            var modifieddate = (DateTime)dr["ModifiedDate"];

            Map newMap = new Map(id, columns, rows, name, createddate, modifieddate, desc);
            map.Add(newMap);
        }

        return map;
    }

    public List<Map> GetSquareMaps()
    {
        List<Map> maps = GetMaps();
        List<Map> mapSquare = new List<Map>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM map WHERE rows = columns", conn);
        using var dr = cmd.ExecuteReader();

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].rows == maps[i].columns)
            {
                mapSquare.Add(maps[i]);
            }
        }

        return mapSquare;
    }

    public Map getMapById(int id)
    {
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT from map WHERE id = " + id, conn);

        using var dr = cmd.ExecuteReader();

        Map map = GetMaps().Find(a => a.id == id);

        if (map == null)
        {
            return null;
        }

        return map;
    }
    public Map UpdateMap(Map map, int id)
    {
        List<Map> mapList = GetMaps();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("UPDATE map" +
            "\nSET \"name\" = " + $"'{map.name}'," + "description = " + $"'{map.description}', " + "rows = " + $"'{map.rows}', " + "columns = " + $"'{map.columns}', "
            + "createddate = " + $"TO_TIMESTAMP('{map.createdDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'), "
            + "modifieddate = " + $"TO_TIMESTAMP('{map.modifiedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS')"
            + "\nWHERE id = " + $"{map.id}", conn);

        using var dr = cmd.ExecuteReader();

        int i = 0;

        for (i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].id == id)
            {
                mapList[i].name = map.name;
                mapList[i].description = map.description;
                mapList[i].columns = map.columns;
                mapList[i].rows = map.rows;
                mapList[i].modifiedDate = map.modifiedDate;
                return mapList[i];
            }
        }

        return null;
    }

    public List<Map> DeleteMap(int id)
    {
        List<Map> maps = GetMaps();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM map WHERE id = " + id, conn);

        using var dr = cmd.ExecuteReader();

        int i = 0;

        for (i = 0; i < maps.Count; i++)
        {
            if (maps[i].id == id)
            {
                maps.RemoveAt(i);
                return maps;
            }
        }

        return null;
    }

    public List<Map> AddMap(Map newMap)
    {
        List<Map> maps = GetMaps();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO maps" + "\n(\"name\"" + ", description, rows, columns, createddate, modifieddate)" + "\nVALUES(" +
        $"'{newMap.name}', " +
        $"'{newMap.description}', " + $"{newMap.rows}, " + $"{newMap.columns}, " +
        $"TO_TIMESTAMP('{newMap.createdDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'), " +
        $"TO_TIMESTAMP('{newMap.createdDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 'YYYY-MM-DD HH24:MI:SS.MS'))", conn);

        using var dr = cmd.ExecuteReader();

        maps.Add(newMap);

        return maps;
    }

}

