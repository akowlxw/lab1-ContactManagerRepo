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
        private ComboBox groupComboBox;
        private TextBox newGroupTextBox;
        private Button addGroupButton;
        private Button addContactButton;
        private Button removeContactButton;
        private TextBox searchTextBox;
        private Button searchButton;
        private ComboBox filterGroupComboBox;
        private ListBox contactsListBox;

        private readonly string _namePlaceholder = "Имя";
        private readonly string _phonePlaceholder = "Телефон";
        private readonly string _searchPlaceholder = "Поиск";
<<<<<<< Updated upstream
=======
        private readonly string _newGroupPlaceholder = "Новая группа";

>>>>>>> Stashed changes

        public ContactForm()
        {
            this.Text = "Управление контактами";
            this.Width = 700;
            this.Height = 430;

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

<<<<<<< Updated upstream
=======
            groupComboBox = new ComboBox
            {
                Location = new Point(330, 10),
                Width = 140,
                TabIndex = 4,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

>>>>>>> Stashed changes
            addContactButton = new Button
            {
                Location = new System.Drawing.Point(480, 10),
                Text = "Добавить",
                Width = 100,
                TabIndex = 0
            };
            addContactButton.Click += AddContactButton_Click;

            newGroupTextBox = new TextBox
            {
                Location = new Point(10, 45),
                Width = 200,
                TabIndex = 5
            };
            newGroupTextBox.Text = _newGroupPlaceholder;
            newGroupTextBox.Enter += TextBox_Enter;
            newGroupTextBox.Leave += TextBox_Leave;

            addGroupButton = new Button
            {
                Location = new Point(220, 45),
                Text = "Добавить группу",
                Width = 130,
                TabIndex = 1
            };
            addGroupButton.Click += AddGroupButton_Click;

            removeContactButton = new Button
            {
                Location = new System.Drawing.Point(360, 45),
                Text = "Удалить",
                Width = 100,
<<<<<<< Updated upstream
                TabIndex = 1
=======
                TabIndex = 6
>>>>>>> Stashed changes
            };
            removeContactButton.Click += RemoveContactButton_Click;

            searchTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 80),
                Width = 200,
<<<<<<< Updated upstream
                TabIndex = 4
=======
                TabIndex = 7
>>>>>>> Stashed changes
            };
            searchTextBox.Text = _searchPlaceholder;
            searchTextBox.Enter += TextBox_Enter;
            searchTextBox.Leave += TextBox_Leave;

            searchButton = new Button
            {
                Location = new System.Drawing.Point(220, 80),
                Text = "Искать",
                Width = 80,
<<<<<<< Updated upstream
                TabIndex = 5
=======
                TabIndex = 8
>>>>>>> Stashed changes
            };
            searchButton.Click += SearchButton_Click;


            filterGroupComboBox = new ComboBox
            {
                Location = new Point(310, 80),
                Width = 160,
                TabIndex = 9,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            filterGroupComboBox.SelectedIndexChanged += FilterGroupComboBox_SelectedIndexChanged;

            contactsListBox = new ListBox
            {
<<<<<<< Updated upstream
                Location = new System.Drawing.Point(10, 100),
                Width = 450,
                Height = 200,
                TabIndex = 6
=======
                Location = new System.Drawing.Point(10, 115),
                Width = 660,
                Height = 240,
                TabIndex = 10
>>>>>>> Stashed changes
            };
            this.Controls.Add(nameTextBox);
            this.Controls.Add(phoneNumberTextBox);
            this.Controls.Add(groupComboBox);
            this.Controls.Add(addContactButton);
            this.Controls.Add(newGroupTextBox);
            this.Controls.Add(addGroupButton);
            this.Controls.Add(removeContactButton);
            this.Controls.Add(searchTextBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(filterGroupComboBox);
            this.Controls.Add(contactsListBox);

            contactManager = new ContactManager();
            UpdateGroups();
            UpdateContactsList();
        }

        private void TextBox_Enter(object sender, EventArgs e)
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
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
            if (textBox == newGroupTextBox) return _newGroupPlaceholder;
            return "";
        }

        private void UpdateGroups()
        {
            string selectedGroup = ContactManager.DefaultGroup;
            string selectedFilterGroup = ContactManager.AllGroups;

            if (groupComboBox.SelectedItem != null)
            {
                selectedGroup = groupComboBox.SelectedItem.ToString();
            }

            if (filterGroupComboBox.SelectedItem != null)
            {
                selectedFilterGroup = filterGroupComboBox.SelectedItem.ToString();
            }

            groupComboBox.Items.Clear();
            filterGroupComboBox.Items.Clear();
            filterGroupComboBox.Items.Add(ContactManager.AllGroups);

            foreach (string group in contactManager.GetGroups())
            {
                groupComboBox.Items.Add(group);
                filterGroupComboBox.Items.Add(group);
            }

            if (groupComboBox.Items.Contains(selectedGroup))
            {
                groupComboBox.SelectedItem = selectedGroup;
            }
            else
            {
                groupComboBox.SelectedItem = ContactManager.DefaultGroup;
            }

            if (filterGroupComboBox.Items.Contains(selectedFilterGroup))
            {
                filterGroupComboBox.SelectedItem = selectedFilterGroup;
            }
            else
            {
                filterGroupComboBox.SelectedItem = ContactManager.AllGroups;
            }
        }

        private void UpdateContactsList()
        {
            List<Contact> contacts;

            if (filterGroupComboBox.SelectedItem == null ||
                filterGroupComboBox.SelectedItem.ToString() == ContactManager.AllGroups)
            {
                contacts = contactManager.Contacts;
            }
            else
            {
                contacts = contactManager.GetContactsByGroup(filterGroupComboBox.SelectedItem.ToString());
            }

            contactsListBox.Items.Clear();
            foreach (Contact contact in contacts)
            {
                contactsListBox.Items.Add(contact);
            }
        }

        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newGroupTextBox.Text) ||
                newGroupTextBox.Text == _newGroupPlaceholder)
            {
                MessageBox.Show("Введите название группы!");
                return;
            }

            try
            {
                contactManager.AddGroup(newGroupTextBox.Text);
                newGroupTextBox.Clear();
                UpdateGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            string group = ContactManager.DefaultGroup;
            if (groupComboBox.SelectedItem != null)
            {
                group = groupComboBox.SelectedItem.ToString();
            }

            Contact newContact = new Contact(nameTextBox.Text, phoneNumberTextBox.Text, group);
            try
            {
                contactManager.AddContact(newContact);
                nameTextBox.Clear();
                phoneNumberTextBox.Clear();
                groupComboBox.SelectedItem = ContactManager.DefaultGroup;
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

            Contact contactToRemove = contactsListBox.SelectedItem as Contact;
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text) ||
                searchTextBox.Text == _searchPlaceholder)
            {
                UpdateContactsList();
                return;
            }

            List<Contact> searchResults = contactManager.SearchContacts(searchTextBox.Text);
            contactsListBox.Items.Clear();

            foreach (Contact contact in searchResults)
            {
                if (filterGroupComboBox.SelectedItem == null ||
                    filterGroupComboBox.SelectedItem.ToString() == ContactManager.AllGroups ||
                    contact.Group == filterGroupComboBox.SelectedItem.ToString())
                {
                    contactsListBox.Items.Add(contact);
                }
            }
        }

        private void FilterGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text) ||
                searchTextBox.Text == _searchPlaceholder)
            {
                UpdateContactsList();
                return;
            }

            SearchButton_Click(sender, e);
        }
    }
}
