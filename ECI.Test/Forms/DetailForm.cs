using System;
using System.Linq;
using System.Windows.Forms;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;
using ECI.Test.Utilities;
using Unity;

namespace ECI.Test.Forms
{
    public partial class DetailForm : Form
    {
        private readonly IDogService _dogService;
        private readonly IDogValidator _dogValidator;
        private readonly IUnityContainer _container;
        private readonly ILogger _logger;
        private Client _currentClient;
        private Dog _currentDog;
        private bool _isEditMode;

        public DetailForm(IDogService dogService, IDogValidator dogValidator, IUnityContainer container, ILogger logger)
        {
            _container = container;
            _dogService = dogService;
            _dogValidator = dogValidator;
            _logger = logger;
            InitializeComponent();
        }

        public void SetClient(Client client)
        {
            _currentClient = client;
            lblClientName.Text = $"Client: {client.Name}";
            LoadDogs();
            ClearEditor();
        }

        private void LoadDogs()
        {
            if (_currentClient != null)
            {
                var dogs = _dogService.GetByClientId(_currentClient.Id).ToList();
                dgvDogs.DataSource = null;
                dgvDogs.DataSource = dogs;
                dgvDogs.Columns["Id"].ReadOnly = true;
                dgvDogs.Columns["Name"].ReadOnly = true;
                dgvDogs.Columns["Breed"].ReadOnly = true;
                dgvDogs.Columns["Age"].ReadOnly = true;
            }
        }

        private void ClearEditor()
        {
            txtDogName.Clear();
            txtBreed.Clear();
            numAge.Value = 0;
            _currentDog = null;
            _isEditMode = false;
            btnSaveDog.Text = "Add Dog";
        }

        private void btnSaveDog_Click(object sender, EventArgs e)
        {
            try
            {
                var dog = new Dog
                {
                    Name = txtDogName.Text,
                    Breed = txtBreed.Text,
                    Age = (int)numAge.Value
                };

                var validationResult = _dogValidator.Validate(dog);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show(validationResult.ToString(), "Please check your input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_isEditMode && _currentDog != null)
                {
                    dog.Id = _currentDog.Id;
                    _dogService.Update(dog);
                    MessageBox.Show("Dog updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _dogService.Add(dog);
                    _dogService.AssignToClient(_currentClient.Id, dog.Id);
                    MessageBox.Show("Dog added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDogs();
                ClearEditor();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Please check your input and try again.", "Invalid Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.LogError("DetailForm", "SaveDog - Validation", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("DetailForm", "SaveDog", ex);
                string message = _isEditMode ? "Unable to update the dog. Please try again." 
                                            : "Unable to save the dog. Please try again.";
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelDog_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }

        private void btnEditDog_Click(object sender, EventArgs e)
        {
            if (dgvDogs.SelectedRows.Count > 0)
            {
                var dog = (Dog)dgvDogs.SelectedRows[0].DataBoundItem;
                _currentDog = dog;
                _isEditMode = true;
                txtDogName.Text = dog.Name;
                txtBreed.Text = dog.Breed;
                numAge.Value = dog.Age;
                btnSaveDog.Text = "Update Dog";
            }
        }

        private void btnDeleteDog_Click(object sender, EventArgs e)
        {
            if (dgvDogs.SelectedRows.Count > 0)
            {
                var dog = (Dog)dgvDogs.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"Are you sure you want to delete '{dog.Name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _dogService.Delete(dog.Id);
                        MessageBox.Show("Dog deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDogs();
                        ClearEditor();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("DetailForm", "DeleteDog", ex);
                        MessageBox.Show("Unable to delete the dog. Please try again.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewWalks_Click(object sender, EventArgs e)
        {
            if (dgvDogs.SelectedRows.Count > 0)
            {
                var dog = (Dog)dgvDogs.SelectedRows[0].DataBoundItem;
                var walkForm = _container.Resolve<WalkForm>();
                walkForm.SetClientDog(_currentClient, dog);
                walkForm.ShowDialog();
                LoadDogs();
            }
        }
    }
}
