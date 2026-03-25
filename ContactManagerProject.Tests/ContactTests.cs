using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactManagerProject.Tests
{
    [TestClass]
    public class ContactTests
    {
        // Конструктор корректно устанавливает имя и номер телефона
        [TestMethod]
        public void Constructor_SetsCorrectly()
        {
            var name = "Тест";
            var phone = "+79891234567";

            var contact = new Contact(name, phone);

            Assert.AreEqual(name, contact.Name);
            Assert.AreEqual(phone, contact.PhoneNumber);
        }
        // Конструктор разрешает пустую строку в имени
        [TestMethod]
        public void Constructor_WithEmptyName_Allowed()
        {
            var contact = new Contact("", "+79891234567");

            Assert.AreEqual("", contact.Name);
        }
        // Конструктор разрешает пустую строку в номере телефона
        [TestMethod]
        public void Constructor_WithEmptyPhone_Allowed()
        {
            var contact = new Contact("Тест", "");

            Assert.AreEqual("", contact.PhoneNumber);
        }
        // Конструктор разрешает null в имени
        [TestMethod]
        public void Constructor_WithNullName_Allowed()
        {
            var contact = new Contact(null, "+79891234567");

            Assert.IsNull(contact.Name);
        }

        // Конструктор разрешает null в номере телефона
        [TestMethod]
        public void Constructor_WithNullPhone_Allowed()
        {
            var contact = new Contact("Тест", null);

            Assert.IsNull(contact.PhoneNumber);
        }
        // Свойство Name можно изменить после создания контакта
        [TestMethod]
        public void Name_CanBeChanged()
        {
            var contact = new Contact("Имя1", "+79891234567");

            contact.Name = "Имя2";

            Assert.AreEqual("Имя2", contact.Name);
        }

        // Свойство PhoneNumber можно изменить после создания контакта
        [TestMethod]
        public void PhoneNumber_CanBeChanged()
        {
            var contact = new Contact("Тест", "+7 000 000 00 00");

            contact.PhoneNumber = "+7 999 999 99 99";

            Assert.AreEqual("+7 999 999 99 99", contact.PhoneNumber);
        }
    }
}
