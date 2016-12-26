/* 
    作者：Sheh伟伟
    博客：http://www.jianshu.com/users/fd3eec0ab0f2/latest_articles
    Github：https://github.com/DavidSheh
*/

using PureMVC.Patterns;
using PureMVC.Interfaces;
using Demo.PureMVC.EmployeeAdmin.Controller;

namespace Demo.PureMVC.EmployeeAdmin
{
    public class ApplicationFacade : Facade
    {
        #region Accessors

        /// <summary>
        /// Facade Singleton Factory method.  This method is thread safe.
        /// </summary>
        public new static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new ApplicationFacade();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start the application
        /// </summary>
        /// <param name="app"></param>
        public void Startup(object app)
        {
            SendNotification(NotiConst.STARTUP, app);
        }

        #endregion

        #region Protected & Internal Methods

        protected ApplicationFacade()
        {
            // Protected constructor.
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        ///</summary>
        static ApplicationFacade()
        {
        }

        /// <summary>
        /// Register Commands with the Controller
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(NotiConst.STARTUP, typeof(StartupCommand));
            RegisterCommand(NotiConst.DELETE_USER, typeof(DeleteUserCommand));
            RegisterCommand(NotiConst.ADD_ROLE_RESULT, typeof(AddRoleResultCommand));
        }

        #endregion
    }
}
