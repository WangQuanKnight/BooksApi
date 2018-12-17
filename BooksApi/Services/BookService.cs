using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        
        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            ObjectId docId = new ObjectId(id);
            return _books.Find(book => book.id == docId).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id,Book bookIn)
        {
            ObjectId docId = new ObjectId(id);
            _books.ReplaceOne(book => book.id == docId, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.id == bookIn.id);
        }

        public void Remove(ObjectId id)
        {
            _books.DeleteOne(book => book.id == id);
        }
    }
}
