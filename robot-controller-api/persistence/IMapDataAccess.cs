namespace robot_controller_api.persistence
{
    public interface IMapDataAccess
    {
        List<Map> AddMap(Map newMap);
        List<Map> DeleteMap(int id);
        Map getMapById(int id);
        List<Map> GetMaps();
        List<Map> GetSquareMaps();
        Map UpdateMap(Map map, int id);
    }
}