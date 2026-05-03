using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

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

        //
        // тестирование функционала групп контактов
        //

        // AddContact без указания группы присваивает группу по умолчанию
        [TestMethod]
        public void AddContact_WithoutGroup_UsesDefaultGroup()
        {
            var manager = new ContactManager();

            manager.AddContact(new Contact("ТестИмя", "12345", ""));

            Assert.AreEqual(ContactManager.DefaultGroup, manager.Contacts[0].Group);
        }

        // добавление новой группы приводит к её появлению в списке групп
        [TestMethod]
        public void AddGroup_AddsNewGroupToList()
        {
            var manager = new ContactManager();

            manager.AddGroup("ТестГруппа");

            Assert.IsTrue(manager.GetGroups().Contains("ТестГруппа"));
        }

        // GetContactsByGroup возвращает только контакты выбранной группы
        [TestMethod]
        public void GetContactsByGroup_ReturnsOnlySelectedGroup()
        {
            var manager = new ContactManager();
            manager.AddGroup("Семья");
            manager.AddGroup("Друзья");
            manager.AddContact(new Contact("ТестИмя1", "12345", "Семья"));
            manager.AddContact(new Contact("ТестИмя2", "67890", "Друзья"));

            var result = manager.GetContactsByGroup("Семья");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ТестИмя1", result[0].Name);
        }
        // LoadContacts Загрузка данных сохраняет группы, даже если в них нет контактов
        [TestMethod]
        public void LoadContacts_LoadsSavedGroupWithoutContacts()
        {
            var manager = new ContactManager();
            manager.AddGroup("ТестГруппа");

            var loadedManager = new ContactManager();

            Assert.IsTrue(loadedManager.GetGroups().Contains("ТестГруппа"));
        }

        // SearchContacts по названию группы возвращает соответствующие контакты
        [TestMethod]
        public void SearchContacts_FindsByGroup()
        {
            var manager = new ContactManager();
            manager.AddGroup("Семья");
            manager.AddContact(new Contact("ТестИмя", "12345", "Семья"));

            var result = manager.SearchContacts("Семья");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ТестИмя", result.First().Name);
        }
    }
}
