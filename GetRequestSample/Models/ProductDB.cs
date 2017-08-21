namespace GetRequestSample.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductDB : DbContext
    {
        // Your context has been configured to use a 'ProductDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GetRequestSample.Models.ProductDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProductDB' 
        // connection string in the application configuration file.
        public ProductDB()
            : base("name=ProductDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}