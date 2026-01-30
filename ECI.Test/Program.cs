using System;
using System.Windows.Forms;
using ECI.Test.BL.Services;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA;
using ECI.Test.DA.Repositories;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Forms;
using ECI.Test.Utilities;
using Unity;

namespace ECI.Test
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = new UnityContainer();

            container.RegisterSingleton<TestDbContext>();

            var dbContext = container.Resolve<TestDbContext>();
            DataSeeder.Seed(dbContext);

            // Register repositories
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IDogRepository, DogRepository>();
            container.RegisterType<IClientDogRepository, ClientDogRepository>();
            container.RegisterType<IWalkRepository, WalkRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            // Register validators
            container.RegisterType<IClientValidator, ClientValidator>();
            container.RegisterType<IDogValidator, DogValidator>();
            container.RegisterType<IWalkValidator, WalkValidator>();
            container.RegisterType<IUserValidator, UserValidator>();
            container.RegisterType<ILoginDtoValidator, LoginDtoValidator>();

            // Register services
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<IDogService, DogService>();
            container.RegisterType<IWalkService, WalkService>();
            container.RegisterType<IUserService, UserService>();

            // Register utilities
            container.RegisterType<ILogger, Logger>();

            container.RegisterInstance<IUnityContainer>(container);

            var loginForm = container.Resolve<LoginForm>();
            Application.Run(loginForm);
        }
    }
}
