using System;
using System.Windows.Forms;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.DTOs;
using ECI.Test.Utilities;
using Unity;

namespace ECI.Test.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IUnityContainer _container;
        private readonly ILoginDtoValidator _loginValidator;
        private readonly ILogger _logger;

        public LoginForm(IAuthenticationService authService, ILoginDtoValidator loginValidator, IUnityContainer container, ILogger logger)
        {
            _authService = authService;
            _container = container;
            _loginValidator = loginValidator;
            _logger = logger;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var loginDto = new LoginDto
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text
                };

                // Validate the login data
                var validationResult = _loginValidator.Validate(loginDto);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show("Please enter both username and password.", "Login Required", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_authService.ValidateLogin(loginDto))
                {
                    this.Hide();
                    var mainForm = _container.Resolve<MainForm>();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("LoginForm", "Login", ex);
                MessageBox.Show("Unable to process login. Please try again.", "System Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
