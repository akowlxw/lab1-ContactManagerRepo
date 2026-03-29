using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactManagerProject
{
    public partial class ContactForm : Form
    {
        private ContactManager contactManager;
        private TextBox nameTextBox;
        private TextBox phoneNumberTextBox;
        private Button addContactButton;
        private Button removeContactButton;
        private TextBox searchTextBox;
        private Button searchButton;
        private ListBox contactsListBox;

        private readonly string _namePlaceholder = "Имя";
        private readonly string _phonePlaceholder = "Телефон";
        private readonly string _searchPlaceholder = "Поиск";

        public ContactForm()
        {
            this.Text = "Управление контактами";
            this.Width = 500;
            this.Height = 400;
            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,
                TabIndex = 2
            };
            nameTextBox.Text = _namePlaceholder;
            nameTextBox.Enter += TextBox_Enter;
            nameTextBox.Leave += TextBox_Leave;

            phoneNumberTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 150,
                TabIndex = 3
            };
            phoneNumberTextBox.Text = _phonePlaceholder;
            phoneNumberTextBox.Enter += TextBox_Enter;
            phoneNumberTextBox.Leave += TextBox_Leave;

            addContactButton = new Button
            {
                Location = new System.Drawing.Point(10, 40),
                Text = "Добавить",
                Width = 100,
                TabIndex = 0
            };
            addContactButton.Click += AddContactButton_Click;
            removeContactButton = new Button
            {
                Location = new System.Drawing.Point(120, 40),
                Text = "Удалить",
                Width = 100,
                TabIndex = 1
            };
            removeContactButton.Click += RemoveContactButton_Click;
            searchTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 70),
                Width = 200,
                TabIndex = 4
            };
            searchTextBox.Text = _searchPlaceholder;
            searchTextBox.Enter += TextBox_Enter;
            searchTextBox.Leave += TextBox_Leave;

            searchButton = new Button
            {
                Location = new System.Drawing.Point(220, 70),
                Text = "Искать",
                Width = 80,
                TabIndex = 5
            };
            searchButton.Click += SearchButton_Click;
            contactsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 450,
                Height = 200,
                TabIndex = 6
            };
            this.Controls.Add(nameTextBox);
            this.Controls.Add(phoneNumberTextBox);
            this.Controls.Add(addContactButton);
            this.Controls.Add(removeContactButton);
            this.Controls.Add(searchTextBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(contactsListBox);
            contactManager = new ContactManager();
            UpdateContactsList();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == GetPlaceholderForTextBox(textBox))
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = GetPlaceholderForTextBox(textBox);
                textBox.ForeColor = Color.Gray;
            }
        }
        private string GetPlaceholderForTextBox(TextBox textBox)
        {
            if (textBox == nameTextBox) return _namePlaceholder;
            if (textBox == phoneNumberTextBox) return _phonePlaceholder;
            if (textBox == searchTextBox) return _searchPlaceholder;
            return "";
        }

        private void UpdateContactsList()
        {
            contactsListBox.Items.Clear();
            foreach (var contact in contactManager.Contacts)
            {
                contactsListBox.Items.Add($"{contact.Name} - {contact.PhoneNumber}");
            }
        }
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) ||
            string.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            Contact newContact = new Contact(nameTextBox.Text, phoneNumberTextBox.Text);
            try
            {
                contactManager.AddContact(newContact);
                nameTextBox.Clear();
                phoneNumberTextBox.Clear();
                UpdateContactsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            if (contactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите контакт для удаления!");
                return;
            }
            string selectedItem = contactsListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string name = parts[0].Trim();
                string phoneNumber = parts[1].Trim();
                var contactToRemove = contactManager.Contacts.Find(c => c.Name == name &&
                c.PhoneNumber == phoneNumber);
                if (contactToRemove != null)
                {
                    try
                    {
                        contactManager.RemoveContact(contactToRemove);
                        UpdateContactsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                UpdateContactsList();
                return;
            }
            var searchResults = contactManager.SearchContacts(searchTextBox.Text);
            contactsListBox.Items.Clear();
            foreach (var contact in searchResults)
            {
                contactsListBox.Items.Add($"{contact.Name} - {contact.PhoneNumber}");
            }
        }
    }
}
