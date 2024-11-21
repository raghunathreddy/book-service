using BookManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using BookManagement.Model;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;


namespace BookManagement.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public BookRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<Book>> GetAllBooksUser(int? user_id)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string selectbooks = @"SELECT * FROM Books WHERE user_id = @user_id ";
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<Book>(selectbooks, new { user_id = user_id }, transaction: transaction);
                        return results?.AsList();
                    }
                    catch (Exception ex)
                    {

                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<Book>("SELECT * FROM Books", transaction: transaction);
                        return results?.AsList();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        public async Task<List<Book>> SearchBook(Book searchbook)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string sql_query = @"SELECT * FROM Books WHERE 1=1 ";
                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(searchbook.author))
                {
                    sql_query += " AND author LIKE @author";
                    parameters.Add("author", $"%{searchbook.author}%");
                }
                if (!string.IsNullOrEmpty(searchbook.title))
                {
                    sql_query += " AND title LIKE @title";
                    parameters.Add("title", $"%{searchbook.title}%");
                }
                if (!string.IsNullOrEmpty(searchbook.genre))
                {
                    sql_query += " AND genre LIKE @genre";
                    parameters.Add("genre", $"%{searchbook.genre}%");
                }

                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        // db.Query<Product>(sql, parameters).ToList();
                        var result = await connection.QueryAsync<Book>(sql_query, parameters, transaction: transaction);
                        return result?.AsList();
                    }
                    catch (Exception ex)
                    {

                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        //public async Task<List<User>> GetUsersByEmail(string email)
        //{
        //    using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
        //    {
        //        _sqlConnectionFactory.OpenConnection(connection);
        //        using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
        //        {
        //            try
        //            {
        //                var results = await connection.QueryAsync<User>(SqlQueries.UserQueries.GetByEmail, new { email = email }, transaction: transaction);
        //                return results?.AsList();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw new InvalidOperationException(ex.Message);
        //            }
        //        }
        //    }
        //}

        public async void AddBooks(Book booketails)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string insertQuery = @"INSERT INTO Books ([user_id],[title],[author],[genre],[condition],[bookAvaliable],[AvaliableExchange],[created_at],[updated_at]) VALUES (@user_id,@title,@author,@genre,@condition,@bookAvaliable,@AvaliableExchange,@created_at,@updated_at)";
                _sqlConnectionFactory.OpenConnection(connection);
                try
                {
                    var results = await connection.ExecuteAsync(insertQuery, new
                    {
                        user_id = booketails.user_id,
                        title = booketails.title,
                        author = booketails.author,
                        genre = booketails.genre,
                        condition = booketails.condition,
                        bookAvaliable = booketails.bookAvaliable,
                        AvaliableExchange = booketails.AvaliableExchange,
                        created_at = booketails.created_at,
                        updated_at = booketails.updated_at
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + "\n" + ex.InnerException.ToString());
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        
    }
}
