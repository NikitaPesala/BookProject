using ConceptArchitect.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    

    public class BookManager
    {
        DbManager db;
        public BookManager(DbManager db)
        {
            this.db = db;
        }


        private BookVariantInfo VariantExtractor(IDataReader reader)
        {
            var info = new BookVariantInfo()
            {
                Id = (string)reader["book_id"],
                Isbn = (string)reader["isbn"],
                Cover = (string)reader["cover_photo"],
                Title = (string)reader["Title"],
                Price = (int)reader["price"],
                PublisherId = (string)reader["publisher_id"]
            };
            string format = (string)reader["book_format"];

            info.Format= Enum.Parse<BookFormat>(format);

            return info;
        }


        public List<BookVariantInfo> GetBookVariant(string bookId)
        {

            return db.ExecuteCommand(command =>
            {
                List<BookVariantInfo> bookVariants = new List<BookVariantInfo>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FindBookVariants";
                
                var parameter = command.CreateParameter();
                parameter.ParameterName = "bookId";
                parameter.DbType = DbType.String;
                parameter.Value = bookId;
                command.Parameters.Add(parameter);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {   
                    bookVariants.Add(VariantExtractor(reader));
                }

                return bookVariants;

            });

        }
    }
}
