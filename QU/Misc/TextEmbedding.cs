using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc
{
    public class TextEmbedding
    {
        public static float[] ConvertBase64String2Vector(string v)
        {
            byte[] bs = Convert.FromBase64String(v);
            float[] fa = new float[bs.Length / 4];
            Buffer.BlockCopy(bs, 0, fa, 0, bs.Length);

            return fa;
        }

        public virtual float[] GetVector(string phrase)
        {
            return new float[0];
        }

        public virtual List<string> GetKeys()
        {
            return new List<string>();
        }

        public virtual void Dispose()
        {
        }
    }

    public class SQLiteTextEmbedding : TextEmbedding
    {
        private SQLiteConnection _sqlConn;
        private string _tableName;
        private string _phraseField;
        private string _vectorField;

        public SQLiteTextEmbedding(string dbName, string tableName, string phraseField = "word", string vectorField = "vector")
        {
            _sqlConn = new SQLiteConnection(string.Format("Data Source={0}.sqlite;Version=3;", dbName));
            _tableName = tableName;
            _phraseField = phraseField;
            _vectorField = vectorField;
            _sqlConn.Open();
        }

        public override float[] GetVector(string phrase)
        {
            string sql = string.Format("select {2} from {0} where word == '{1}'", this._tableName, phrase, this._vectorField);

            float[] vector = new float[0];
            var command = new SQLiteCommand(sql, this._sqlConn);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                    vector = ConvertBase64String2Vector(reader["vector"].ToString());
            }

            return vector;
        }

        public override List<string> GetKeys()
        {
            throw new Exception("Not supported operation for SQLiteTextEmbedding");
        }

        public override void Dispose()
        {
            base.Dispose();
            _sqlConn.Close();
        }
    }

    public class InMemoryTextEmbedding : TextEmbedding
    {
        Dictionary<string, float[]> _dict;

        public InMemoryTextEmbedding(string file, int phraseCol, int vectorCol)
        {
            _dict = ReadVectorDictionaryFromFile(file, phraseCol, vectorCol);
        }

        public static Dictionary<string, float[]> ReadVectorDictionaryFromFile(string file, int qCol, int vCol)
        {
            Dictionary<string, float[]> dict = new Dictionary<string, float[]>();
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] items = line.Split('\t');
                    string q = items[qCol];
                    string v = items[vCol];
                    dict[q] = ConvertBase64String2Vector(v);
                }
            }

            return dict;
        }

        public override float[] GetVector(string phrase)
        {
            float[] vector;
            if (!_dict.TryGetValue(phrase, out vector))
            {
                return new float[0];
            }

            return vector;
        }

        public override List<string> GetKeys()
        {
            return _dict.Keys.ToList();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
