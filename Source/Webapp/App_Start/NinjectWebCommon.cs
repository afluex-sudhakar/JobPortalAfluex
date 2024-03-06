[assembly: WebActivator.PreApplicationStartMethod(typeof(Webapp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Webapp.App_Start.NinjectWebCommon), "Stop")]

namespace Webapp.App_Start
{
    using Data.Interfaces.Repositories;
    using Data.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICourseRepository>().To<CourseRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<ICityRepository>().To<CityRepository>();
            kernel.Bind<IJobRepository>().To<JobRepository>();
            kernel.Bind<INotificationRepository>().To<NotificationRepository>();
            kernel.Bind<IJobRoleRepository>().To<JobRoleRepository>();
            kernel.Bind<ISkillRepository>().To<SkillRepository>();
            kernel.Bind<IStateRepository>().To<StateRepository>();
            kernel.Bind<IJobTypeRepository>().To<JobTypeRepository>();
            kernel.Bind<IEmpJobPostRespository>().To<EmpJobPostRespository>();
            kernel.Bind<IEnrollmentProgramRepository>().To<EnrollmentProgramRepository>();
            kernel.Bind<ITrainingMaterialRepository>().To<TrainingMaterialRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IUserJobRepository>().To<UserJobRepository>();
            kernel.Bind<IPincodeRepository>().To<PincodeRepository>();
            kernel.Bind<INewsLetterRepository>().To<NewsLetterRepository>();
            kernel.Bind<IDepartmentRepository>().To<DepartmentRepository>();
            kernel.Bind<IDepartmentCategoryRepository>().To<DepartmentCategoryRepository>();
            kernel.Bind<ICMSRepository>().To<CMSRepository>();
            kernel.Bind<IAdminDashboardRepository>().To<AdminDashboardRespository>();
            kernel.Bind<IChatRepository>().To<ChatRepository>();
            kernel.Bind<IFeedbackRepository>().To<FeedbackRepository>();
            kernel.Bind<IEmailMasterRepository>().To<EmailMasterRepository>();
            kernel.Bind<ISmslMasterRepository>().To<SmsMasterRepository>();
            kernel.Bind<IAudittrailRepository>().To<AudittrailRepository>();
        }
    }
}
