using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public sealed class DatabaseInfo
    {
        public string FileName { get; private set; }
        public SQLitePCL.ISQLiteConnection Connection { get; private set; }

        public DatabaseInfo(string fileName)
        {
            FileName = fileName;
            Connection = new SQLitePCL.SQLiteConnection(fileName);
        }
    }
}
