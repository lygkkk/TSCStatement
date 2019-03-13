using System.Data.OleDb;

namespace TscStatement.Abstract.IService
{
    public interface IBaseService
    {
        string DataBase { get; set; }
        string Provider { get; set; }
        OleDbConnection Connection { get; set; }
    }
}