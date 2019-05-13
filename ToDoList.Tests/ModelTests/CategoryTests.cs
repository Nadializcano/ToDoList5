using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using ToDoList.Models;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
  {
  //   public CategoryTest()
  //   {
  //     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ToDoListTest";
  //   }


    // public void Dispose()
    // {
    //   Item.ClearAll();
    //   Category.ClearAll();
    // }
    // [TestMethod]
    // public void CategoryConstructor_CreatesInstanceOfCategory_Category()
    // {
    //     string name = "Test Category";
    //     Category newCategory = new Category(name);
    //     Assert.AreEqual(typeof(Category), newCategory.GetType());
    // }
    // [TestMethod]
    // public void GetName_ReturnsName_String()
    // {
    //     string name = "Test Category";
    //     Category newCategory = new Category(name);
    //     string actualResult = newCategory.GetName();
    //     Assert.AreEqual(name, actualResult);
    // }
    // // [TestMethod]
    // // public void GetId_ReturnsCategoryId_Int()
    // // {
    // //     string name = "Test Category";
    // //     Category newCategory = new Category(name);
    // //     int actualResult = newCategory.GetId();
    // //     Assert.AreEqual(1, actualResult);
    // // }
    // [TestMethod]
    // public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    // {
    //     string name01 = "Work";
    //     string name02 = "School";
    //     Category newCategory1 = new Category(name01);
    //     newCategory1.Save();
    //     Category newCategory2 = new Category(name02);
    //     newCategory2.Save();
    //     List<Category> newList = new List<Category> { newCategory1, newCategory2 };
    //
    //     List<Category> actualResult = Category.GetAll();
    //     CollectionAssert.AreEqual(newList, actualResult);
    // }
    // [TestMethod]
    // public void Find_ReturnsCategoryInDatabase_Category()
    // {
    //   //Arrange
    //   Category testCategory = new Category("Household chores");
    //   testCategory.Save();
    //
    //   //Act
    //   Category foundCategory = Category.Find(testCategory.GetId());
    //
    //   //Assert
    //   Assert.AreEqual(testCategory, foundCategory);
    // }
    //
    // [TestMethod]
    // public void GetItems_ReturnsEmptyItemList_ItemList()
    // {
    //     string name = "Work";
    //     Category newCategory = new Category(name);
    //     List<Item> newList = new List<Item> { };
    //     List<Item> actualResult = newCategory.GetItems();
    //     CollectionAssert.AreEqual(newList, actualResult);
    // }
    [TestMethod]
    public void GetItems_ReturnsAllCategoryItems_ItemList()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      Item testItem1 = new Item("Mow the lawn");
      testItem1.Save();
      Item testItem2 = new Item("Buy plane ticket");
      testItem2.Save();

      //Act
      testCategory.AddItem(testItem1);
      List<Item> savedItems = testCategory.GetItems();
      List<Item> testList = new List<Item> {testItem1};

      //Assert
      CollectionAssert.AreEqual(testList, savedItems);
    }
    // [TestMethod]
    // public void GetAll_CategoriesEmptyAtFirst_List()
    // {
    //   //Arrange, Act
    //   int result = Category.GetAll().Count;
    //
    //   //Assert
    //   Assert.AreEqual(0, result);
    // }
    //
    // [TestMethod]
    // public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    // {
    //   //Arrange, Act
    //   Category firstCategory = new Category("Household chores");
    //   Category secondCategory = new Category("Household chores");
    //
    //   //Assert
    //   Assert.AreEqual(firstCategory, secondCategory);
    // }
    // [TestMethod]
    // public void Save_SavesCategoryToDatabase_CategoryList()
    // {
    //   //Arrange
    //   Category testCategory = new Category("Household chores");
    //   testCategory.Save();
    //
    //   //Act
    //   List<Category> result = Category.GetAll();
    //   List<Category> testList = new List<Category>{testCategory};
    //
    //   //Assert
    //   CollectionAssert.AreEqual(testList, result);
    // }
    // [TestMethod]
    // public void Save_DatabaseAssignsIdToCategory_Id()
    // {
    //   //Arrange
    //   Category testCategory = new Category("Household chores");
    //   testCategory.Save();
    //
    //   //Act
    //   Category savedCategory = Category.GetAll()[0];
    //
    //   int result = savedCategory.GetId();
    //   int testId = testCategory.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(testId, result);
    // }

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

    [TestMethod]
    public void Test_AddItem_AddsItemToCategory()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Item testItem2 = new Item("Water the garden");
      testItem2.Save();

      //Act
      testCategory.AddItem(testItem);
      testCategory.AddItem(testItem2);
      List<Item> result = testCategory.GetItems();
      List<Item> testList = new List<Item>{testItem, testItem2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


  }
}
