using BookManagement.Model;
using BookManagement.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Repository.Implementation
{
    public class BookExchangeReposirory : IBookExchangeReposirory
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
       // private readonly IBookExchangeReposirory _bookExchangeReposirory;
        public BookExchangeReposirory(ISqlConnectionFactory sqlConnectionFactory
           // , IBookExchangeReposirory bookExchangeReposirory
            )
        {
            _sqlConnectionFactory = sqlConnectionFactory;
           // _bookExchangeReposirory= bookExchangeReposirory;

        }

        public async void AddExchangedBook(BookExchange usersdetails)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string insertQuery = @"INSERT INTO Books ([book_id],[requester_id],[owner_id],[status],[delivery_method],[exchange_date],[updated_at],[request_message]) VALUES (@book_id,@requester_id,@owner_id,@status,@delivery_method,@exchange_date,@updated_at,@request_message)";
                _sqlConnectionFactory.OpenConnection(connection);
                try
                {
                    var results = await connection.ExecuteAsync(insertQuery, new
                    {
                        book_id = usersdetails.book_id,
                        requester_id = usersdetails.requester_id,
                        owner_id = usersdetails.owner_id,
                        status = usersdetails.status,
                        delivery_method = usersdetails.delivery_method,
                        exchange_date = usersdetails.exchange_date,
                        updated_at = usersdetails.updated_at,
                        request_message = usersdetails.request_message
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + "\n" + ex.InnerException.ToString());
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public async Task<List<BookExchange>> ExchangedBook(BookExchange searchbook)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string sql_query = @"SELECT * FROM BookExchange WHERE 1=1 And requester_id = @requester_id ";
                var parameters = new DynamicParameters();

                //if (!string.IsNullOrEmpty(searchbook.requester_id))
                //{
                //    sql_query += " AND requester_id = @requester_id";
                //    parameters.Add("requester_id", $"%{searchbook.requester_id}%");
                //}
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<BookExchange>(sql_query, transaction: transaction);
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

        public async Task<List<BookExchange>> GetAllExchangedBook()
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<BookExchange>("SELECT * FROM BookExchange ", transaction: transaction);
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

        public async Task<List<BookExchangeTX>> GetAllexchangedBookTrx()
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<BookExchangeTX>(" Select bx.exchange_id,usr.username as book_Owner,usrbx.username as Book_exchanged_To, bs.book_id,bs.title,bx.status Requested_status,bx.request_message,bx.delivery_method,bx.exchange_date from\r\n    BookExchange bx LEFT Join Books bs ON bx.book_id = bs.book_id\r\n\tLEFT join Users usr on bx.owner_id = usr.user_id\r\n\tLeft Join Users usrbx on bx.exchanged_to = usrbx.user_id", transaction: transaction);
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

        public List<BookExchange> GetAllExchangedBookUser(int? owner_id)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string selectbooks = @"SELECT * FROM BookExchange WHERE owner_id = @owner_id ";
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        return connection.QueryAsync<List<BookExchange>>(selectbooks, new { user_id = owner_id }, transaction: transaction).Result.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

       
    }
}
