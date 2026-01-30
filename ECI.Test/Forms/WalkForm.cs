using System;
using System.Linq;
using System.Windows.Forms;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;
using ECI.Test.Utilities;

namespace ECI.Test.Forms
{
    public partial class WalkForm : Form
    {
        private readonly IWalkService _walkService;
        private readonly IWalkValidator _walkValidator;
        private readonly ILogger _logger;
        private Client _currentClient;
        private Dog _currentDog;
        private Walk _currentWalk;
        private bool _isEditMode;

        public WalkForm(IWalkService walkService, IWalkValidator walkValidator, ILogger logger)
        {
            _walkService = walkService;
            _walkValidator = walkValidator;
            _logger = logger;
            InitializeComponent();
        }

        public void SetClientDog(Client client, Dog dog)
        {
            _currentClient = client;
            _currentDog = dog;
            lblClientName.Text = $"Client: {client.Name}";
            lblDogName.Text = $"Dog: {dog.Name}";
            LoadWalks();
            ClearEditor();
        }

        private void LoadWalks()
        {
            if (_currentClient != null && _currentDog != null)
            {
                try
                {
                    var walks = _walkService.GetByClientIdDogId(_currentClient.Id, _currentDog.Id).ToList();
                    dgvWalks.DataSource = null;
                    dgvWalks.DataSource = walks;
                    
                    dgvWalks.Columns["Id"].ReadOnly = true;
                    dgvWalks.Columns["Date"].ReadOnly = true;
                    dgvWalks.Columns["Duration"].ReadOnly = true;
                    
                    if (dgvWalks.Columns["ClientId"] != null)
                        dgvWalks.Columns["ClientId"].Visible = false;
                    if (dgvWalks.Columns["DogId"] != null)
                        dgvWalks.Columns["DogId"].Visible = false;
                    if (dgvWalks.Columns["Client"] != null)
                        dgvWalks.Columns["Client"].Visible = false;
                    if (dgvWalks.Columns["Dog"] != null)
                        dgvWalks.Columns["Dog"].Visible = false;
                        
                    dgvWalks.Columns["Id"].HeaderText = "Walk ID";
                    dgvWalks.Columns["Date"].HeaderText = "Date";
                    dgvWalks.Columns["Duration"].HeaderText = "Duration";
                }
                catch (Exception ex)
                {
                    _logger.LogError("WalkForm", "LoadWalks", ex);
                    MessageBox.Show("Unable to load walk history. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearEditor()
        {
            dateTimePicker1.Value = DateTime.Now;
            numDuration.Value = 0;
            _currentWalk = null;
            _isEditMode = false;
            btnSaveWalk.Text = "Add Walk";
        }

        private void btnEditWalk_Click(object sender, EventArgs e)
        {
            if (dgvWalks.SelectedRows.Count > 0)
            {
                try
                {
                    var walk = (Walk)dgvWalks.SelectedRows[0].DataBoundItem;
                    _currentWalk = walk;
                    _isEditMode = true;
                    dateTimePicker1.Value = new DateTime(walk.Date.Ticks);
                    numDuration.Value = walk.Duration;
                    btnSaveWalk.Text = "Update Walk";
                }
                catch (Exception ex)
                {
                    _logger.LogError("WalkForm", "EditWalk", ex);
                    MessageBox.Show("Unable to load walk details for editing. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteWalk_Click(object sender, EventArgs e)
        {
            if (dgvWalks.SelectedRows.Count > 0)
            {
                var walk = (Walk)dgvWalks.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"Are you sure you want to delete this walk from {walk.Date:MM/dd/yyyy}?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _walkService.Delete(walk.Id);
                        MessageBox.Show("Walk deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadWalks();
                        ClearEditor();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("WalkForm", "DeleteWalk", ex);
                        MessageBox.Show("Unable to delete the walk. Please try again.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveWalk_Click(object sender, EventArgs e)
        {
            try
            {
                var walk = new Walk
                {
                    ClientId = _currentClient.Id,
                    DogId = _currentDog.Id,
                    Date = dateTimePicker1.Value,
                    Duration = (int)numDuration.Value
                };

                var validationResult = _walkValidator.Validate(walk);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show(validationResult.ToString(), "Please check your input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_isEditMode && _currentWalk != null)
                {
                    walk.Id = _currentWalk.Id;
                    _walkService.Update(walk);
                    MessageBox.Show("Walk updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _walkService.Add(walk);
                    MessageBox.Show("Walk added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadWalks();
                ClearEditor();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Please check your input and try again.", "Invalid Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.LogError("WalkForm", "SaveWalk - Validation", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("WalkForm", "SaveWalk", ex);
                string message = _isEditMode ? "Unable to update the walk. Please try again." 
                                            : "Unable to save the walk. Please try again.";
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelWalk_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }
    }
}
