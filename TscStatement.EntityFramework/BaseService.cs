using System.Data.OleDb;
using TscStatement.Abstract.IService;

namespace TscStatement.EntityFramework
{
    public class BaseService : IBaseService
    {
        public string DataBase { get; set; }
        public string Provider { get; set; }
        public OleDbConnection Connection { get; set; }

        public BaseService(string dataBase)
        {
            Provider = "Provider=Microsoft.Ace.OleDb.12.0;Extended Properties=Excel 12.0;Data Source=";
            DataBase = dataBase;
            Connection = new OleDbConnection(Provider + DataBase);
        }
    }
}