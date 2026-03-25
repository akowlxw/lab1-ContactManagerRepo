using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ContactManagerProject.Tests
{
    [TestClass]
    public class ContactManagerTests
    {
        [TestInitialize]
        public void SetUp()
        {
            if (File.Exists("contacts.txt"))
                File.Delete("contacts.txt");
        }
        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists("contacts.txt"))
                File.Delete("contacts.txt");
        }

        // Constructor создаёт пустой список контактов
        [TestMethod]
        public void Constructor_InitEmptyList()
        {
            var manager = new ContactManager();

            Assert.IsNotNull(manager.Contacts);
            Assert.AreEqual(0, manager.Contacts.Count);
        }

        // AddContact успешно добавляет контакт в коллекцию
        [TestMethod]
        public void AddContact_Successfull()
        {
            var manager = new ContactManager();
            var contact = new Contact("Тест", "+79891234567");

            manager.AddContact(contact);

            Assert.AreEqual(1, manager.Contacts.Count);
            CollectionAssert.Contains(manager.Contacts, contact);
        }

        // AddContact выбрасывает ArgumentNullException при передаче null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddContact_Null_ThrowsException()
        {
            var manager = new ContactManager();

            manager.AddContact(null);
        }

        // RemoveContact успешно удаляет контакт из коллекции
        [TestMethod]
        public void RemoveContact_Successfull()
        {
            var manager = new ContactManager();
            var contact = new Contact("Тест", "+79891234567");
            manager.AddContact(contact);

            manager.RemoveContact(contact);

            Assert.AreEqual(0, manager.Contacts.Count);
        }

        // RemoveContact выбрасывает ArgumentNullException при передаче null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveContact_Null_ThrowsException()
        {
            var manager = new ContactManager();

            manager.RemoveContact(null);
        }

        // SearchContacts находит контакт по имени
        [TestMethod]
        public void SearchContacts_NameMatch_FindsContact()
        {
            var manager = new ContactManager();
            manager.AddContact(new Contact("Имя1", "+7991111111"));
            manager.AddContact(new Contact("Имя2", "+79992222222"));

            var results = manager.SearchContacts("Имя1");

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Имя1", results[0].Name);
        }
        // SearchContacts находит контакт по номеру телефона
        [TestMethod]
        public void SearchContacts_PhoneMatch_FindsContact()
        {
            var manager = new ContactManager();
            manager.AddContact(new Contact("Имя1", "+79991111111"));
            manager.AddContact(new Contact("Имя2", "+79992222222"));

            var results = manager.SearchContacts("+79992222222");

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Имя2", results[0].Name);
        }

        // SearchContacts возвращает пустой список при отсутствии совпадений
        [TestMethod]
        public void SearchContacts_NoMatches_ReturnEmptyList()
        {
            var manager = new ContactManager();
            manager.AddContact(new Contact("Имя1", "+79991111111"));

            var results = manager.SearchContacts("Тест");

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }
        // SearchContacts с пустой строкой возвращает все контакты
        [TestMethod]
        public void SearchContacts_EmptyString_ReturnsAllContacts()
        {
            var manager = new ContactManager();
            manager.AddContact(new Contact("Имя1", "+79991111111"));
            manager.AddContact(new Contact("Имя2", "+79992222222"));

            var results = manager.SearchContacts("");

            Assert.AreEqual(2, results.Count);
        }
    }
}
