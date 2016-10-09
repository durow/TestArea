using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public sealed class ANoteData
    {
        ISQLiteConnection con;

        public ANoteData(DatabaseInfo info)
        {
            con = info.Connection;
        }

        public void DeleteRowByID(int id)
        {
            const string DELETE_SQL = @"DELETE FROM ANote WHERE ID=?";
            using (var statement = con.Prepare(DELETE_SQL))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public IEnumerable<Dictionary<string,object>> GetAll()
        {
            const string GETALL_SQL = @"SELECT ID,AddDateTime,EditDateTime,Title,Content, Category 
                                                    FROM ANote,ACategory 
                                                    WHERE Anote.CategoryID=Acategory.ID";
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

        public string GetContentByID(int id)
        {
            const string GETCONTENT_SQL = @"SELECT Content FROM ANote WHERE ID=?";
            using (var statement = con.Prepare(GETCONTENT_SQL))
            {
                var result = statement.Step();
                if (result == SQLiteResult.ROW)
                    return statement[0].ToString();
                else
                    return string.Empty;
            }
        }

        public void Insert(string addDateTime,
                                  string editDateTime,
                                  string title,
                                  string content,
                                  int categoryID)
        {
            const string INSERT_SQL = @"INSERT INTO ANote(AddDateTime,EditDateTime,Title,Content,CategoryID) VALUES(?,?,?,?,?)";
            using (var statement = con.Prepare(INSERT_SQL))
            {
                statement.Bind(1, addDateTime);
                statement.Bind(2, editDateTime);
                statement.Bind(3, title);
                statement.Bind(4, content);
                statement.Bind(5, categoryID);
                statement.Step();
            }
        }

        public void Update(int id, 
                                    string editDateTime,
                                    string title,
                                    string content,
                                    int categoryID)
        {
            const string UPDATE_SQL = @"UPDATE ANote 
                                                     SET EditDateTime=?,Title=?,Content=?,CategoryID=? 
                                                     WHERE ID=?";
            using (var statement = con.Prepare(UPDATE_SQL))
            {
                statement.Bind(1, editDateTime);
                statement.Bind(2, title);
                statement.Bind(3, content);
                statement.Bind(4, categoryID);
                statement.Bind(5, id);
            }
        }
    }
}
