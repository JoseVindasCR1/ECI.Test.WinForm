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
    public partial class MainForm : Form
    {
        private readonly IClientService _clientService;
        private readonly IUnityContainer _container;
        private readonly IClientValidator _clientValidator;
        private readonly ILogger _logger;
        private Client _currentClient;
        private bool _isEditMode;

        public MainForm(IClientService clientService, IClientValidator clientValidator, IUnityContainer container, ILogger logger)
        {
            _clientService = clientService;
            _container = container;
            _clientValidator = clientValidator;
            _logger = logger;
            InitializeComponent();
            LoadClients();
            ClearEditor();
        }

        private void LoadClients()
        {
            var clients = _clientService.GetAll().ToList();
            dgvClients.DataSource = null;
            dgvClients.DataSource = clients;
            dgvClients.Columns["Id"].ReadOnly = true;
            dgvClients.Columns["Name"].ReadOnly = true;
            dgvClients.Columns["Phone"].ReadOnly = true;
        }

        private void ClearEditor()
        {
            txtName.Clear();
            txtPhone.Clear();
            _currentClient = null;
            _isEditMode = false;
            btnSave.Text = "Add Client";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new Client
                {
                    Name = txtName.Text,
                    Phone = txtPhone.Text
                };

                // Validate the client before saving
                var validationResult = _clientValidator.Validate(client);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show(validationResult.ToString(), "Please check your input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_isEditMode && _currentClient != null)
                {
                    client.Id = _currentClient.Id;
                    _clientService.Update(client);
                    MessageBox.Show("Client updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _clientService.Add(client);
                    MessageBox.Show("Client added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadClients();
                ClearEditor();
            }
            catch (ArgumentException ex)
            {
                // Validation errors from the service layer
                MessageBox.Show("Please check your input and try again.", "Invalid Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.LogError("MainForm", "SaveClient - Validation", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("MainForm", "SaveClient", ex);
                string message = _isEditMode ? "Unable to update the client. Please try again." 
                                            : "Unable to save the client. Please try again.";
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                var client = (Client)dgvClients.SelectedRows[0].DataBoundItem;
                _currentClient = client;
                _isEditMode = true;
                txtName.Text = client.Name;
                txtPhone.Text = client.Phone;
                btnSave.Text = "Update Client";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                var client = (Client)dgvClients.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"Are you sure you want to delete '{client.Name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _clientService.Delete(client.Id);
                        MessageBox.Show("Client deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadClients();
                        ClearEditor();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("MainForm", "DeleteClient", ex);
                        MessageBox.Show("Unable to delete the client. Please try again.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnViewDogs_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                var client = (Client)dgvClients.SelectedRows[0].DataBoundItem;
                var detailForm = _container.Resolve<DetailForm>();
                detailForm.SetClient(client);
                detailForm.ShowDialog();
                LoadClients();
            }
        }

        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
