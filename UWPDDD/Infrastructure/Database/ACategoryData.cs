using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public sealed class ACategoryData
    {
        ISQLiteConnection con;

        public void Delete(int id)
        {
            const string DELETE_SQL = @"DELETE FROM ACategory WHERE ID=?";
            using (var statement = con.Prepare(DELETE_SQL))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public IEnumerable<Dictionary<string,object>> GetAll()
        {
            const string GETALL_SQL = @"SELECT ID,Category 
                                                    FROM ACategory";
            using (var statement = con.Prepare(GETALL_SQL))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    var dict = new Dictionary<string, object>();
                    for (int i = 0; i < statement.ColumnCount; i++)
                    {
                        dict.Add(statement.ColumnName(i), statement[i]);
                    }
                    yield return dict;
                }
            }
        }

        public void Insert(string category)
        {
            const string INSERT_SQL = @"INSERT INTO ACategory(Category) VALUES(?)";
            using (var statement = con.Prepare(INSERT_SQL))
            {
                statement.Bind(1, category);
                statement.Step();
            }
        }

        public void Update(int id,string category)
        {
            const string UPDATE_SQL = @"UPDATE ACategory 
                                                         SET Category=? 
                                                         WHERE ID=?";
            using (var statement = con.Prepare(UPDATE_SQL))
            {
                statement.Bind(1, id);
                statement.Bind(2, category);
            }
        }
    }
}
