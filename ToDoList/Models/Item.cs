using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
      private string _description;
      private int _id;

      public Item (string description, int id = 0)
      {
          _description = description;
          _id = id;
      }
      public string GetDescription()
      {
          return _description;
      }
      public int GetId()
      {
        return _id;
      }

      public void SetDescription(string newDescription)
      {
        _description = newDescription;
      }

      public static List<Item> GetAll()
      {
          List<Item> allItems = new List<Item> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM items;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int itemId = rdr.GetInt32(0);
            string itemDescription = rdr.GetString(1);
            Item newItem = new Item(itemDescription, itemId);
            allItems.Add(newItem);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allItems;
      }

      public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM items;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static Item Find(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM items WHERE id = (@searchId);";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int itemId = 0;
          string itemName = "";
          while(rdr.Read())
          {
            itemId = rdr.GetInt32(0);
            itemName = rdr.GetString(1);
          }
          Item newItem = new Item(itemName, itemId);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newItem;
        }

      public override bool Equals(System.Object otherItem)
      {
        if (!(otherItem is Item))
        {
          return false;
        }
        else
        {
          Item newItem = (Item) otherItem;
          bool idEquality = this.GetId() == newItem.GetId();
          bool descriptionEquality = this.GetDescription() == newItem.GetDescription();
          return (idEquality && descriptionEquality);
        }
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO items (description) VALUES (@description;";
        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@description";
        description.Value = this._description;
        cmd.Parameters.Add(description);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
      public void Edit(string newDescription)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@newDescription";
        description.Value = newDescription;
        cmd.Parameters.Add(description);
        cmd.ExecuteNonQuery();
        _description = newDescription;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

      }

      public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @ItemId; DELETE FROM categories_items WHERE item_id = @ItemId;";
      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@ItemId";
      itemIdParameter.Value = this.GetId();
      cmd.Parameters.Add(itemIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

     public List<Category> GetCategories()
   {
     MySqlConnection conn = DB.Connection();
      conn.Open();//opening database
      var cmd = conn.CreateCommand() as MySqlCommand;
      //This locates IDs for all Categorys present on the join table whose entries are associated with the ID for the Item object this method was called upon.
      cmd.CommandText = @"SELECT category_id FROM categories_items WHERE item_id = @itemId;";
      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@itemId";
      itemIdParameter.Value = _id;
      cmd.Parameters.Add(itemIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader; //use a reader to gather each of these category_ids into a List named categoryIds
      List<int> categoryIds = new List<int> {};//categoryIds will be a list of IDs for all Category objects associated with the Item this method was called upon.
      while(rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        categoryIds.Add(categoryId);
      }
      rdr.Dispose();
      List<Category> categories = new List<Category> {};
      foreach (int categoryId in categoryIds)
      {
        var categoryQuery = conn.CreateCommand() as MySqlCommand;
        categoryQuery.CommandText = @"SELECT * FROM categories WHERE id = @CategoryId;";
        MySqlParameter categoryIdParameter = new MySqlParameter();
        categoryIdParameter.ParameterName = "@CategoryId";
        categoryIdParameter.Value = categoryId;
        categoryQuery.Parameters.Add(categoryIdParameter);
        var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
        while(categoryQueryRdr.Read())
        {
          int thisCategoryId = categoryQueryRdr.GetInt32(0);
          string categoryName = categoryQueryRdr.GetString(1);
          Category foundCategory = new Category(categoryName, thisCategoryId);
          categories.Add(foundCategory);
        }
        categoryQueryRdr.Dispose();
      }

      conn.Close();//closing database
      if (conn != null)
      {
        conn.Dispose();
      }
      return categories;
   }
  //This method will be called on an Item, and takes an entire Category object as an argument.
   public void AddCategory(Category newCategory)
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     //This adds an entry to the categories_items join table containing IDs for relevant Item and Category objects.
     cmd.CommandText = @"INSERT INTO categories_items (category_id, item_id) VALUES (@CategoryId, @ItemId);";
           MySqlParameter category_id = new MySqlParameter();
           category_id.ParameterName = "@CategoryId";
           category_id.Value = newCategory.GetId();
           cmd.Parameters.Add(category_id);
           MySqlParameter item_id = new MySqlParameter();
           item_id.ParameterName = "@ItemId";
           item_id.Value = _id;
           cmd.Parameters.Add(item_id);
           cmd.ExecuteNonQuery();
           conn.Close();
           if (conn != null)
     {
       conn.Dispose();
     }
    }

  }
}
