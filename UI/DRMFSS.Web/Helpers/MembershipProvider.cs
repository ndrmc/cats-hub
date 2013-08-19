using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using DRMFSS.BLL;
using DRMFSS.Shared;
using DRMFSS.BLL.Services;

namespace DRMFSS.Web.Helpers
{
    public class MembershipProvider : System.Web.Security.MembershipProvider
    {
        
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private string passwordStrengthRegularExpression;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        IUnitOfWork repository = new UnitOfWork();

        private readonly IUserProfileService _userProfileService;

        public MembershipProvider(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("SSS.MemberProviders.Providers Initialize: config is null.");
            }

            if (name == null || name.Length == 0)
            {
                name = "SSS.MemberProviders.Providers.SSSMembershipProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description",
                           "Entity Framework MVC3 Custom Membership Provider by Nannette Thacker http://www.shiningstar.net Shining Star Services LLC");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], "/");
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "false"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            passwordStrengthRegularExpression =
                Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters =
                Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "0"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));


            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            // NKT: I'm only supporting the Hashed format, you may need to alter this for your legacy database
            switch (temp_format)
            {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                var context = new CTSContext();
                UserProfile user = context.UserProfiles.Where(p => p.UserName == username).SingleOrDefault();
                if (user != null)
                {
                    user.Password = MD5Hashing.MD5Hash(newPassword);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
                                                  string passwordQuestion, string passwordAnswer, bool isApproved,
                                                  object providerUserKey, out MembershipCreateStatus status)
        {
            var user = new UserProfile();
            user.ActiveInd = true;
            user.UserName = username;
            user.Email = email;
            user.Password = MD5Hashing.MD5Hash(password);
            user.FirstName = " ";
            user.LastName = " ";
            user.LockedInInd = false;
            user.LanguageCode = "en";
            user.DefaultTheme = "vista";
            user.DatePreference = "en";
            user.PreferedWeightMeasurment = "MT";


            IUnitOfWork repository = new UnitOfWork();
            try
            {
                _userProfileService.AddUserProfile(user);
                status = MembershipCreateStatus.Success;
                return GetMembershipUser(user);
            }
            catch (Exception e)
            {
                status = MembershipCreateStatus.ProviderError;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            //BLL.MembershipEntities context = new BLL.MembershipEntities();
            //MembershipUserCollection collection = new MembershipUserCollection();
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var entities = new CTSContext();
            UserProfile user = (from u in entities.UserProfiles
                                where u.UserName == username
                                select u).SingleOrDefault();
            return (user != null) ? GetMembershipUser(user) : null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {

            UserProfile user = _userProfileService.FindById(Convert.ToInt32(providerUserKey));
            return (user != null) ? GetMembershipUser(user) : null;
        }

        //TODO: remove the context here.
        public override string GetUserNameByEmail(string email)
        {
            var entities = new CTSContext() ;
            UserProfile user = (from u in entities.UserProfiles
                                where u.Email == email
                                select u).SingleOrDefault();
            return user.UserName;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        // TODO: Remove the context here.
        public override bool ValidateUser(string username, string password)
        {
            var entities = new CTSContext();
            string pass = MD5Hashing.MD5Hash(password);
            UserProfile user = (from u in entities.UserProfiles
                                where u.UserName == username && u.Password == pass &&
                                      !u.LockedInInd && u.ActiveInd
                                select u).SingleOrDefault();
            return (user != null) ? true : false;
        }

        private MembershipUser GetMembershipUser(UserProfile user)
        {
            //BLL.Membership membership = user.Membership;
            var mUser = new MembershipUser("MemberProvider", user.UserName, user.UserProfileID, user.Email,
                                           string.Empty, string.Empty, false, false, DateTime.Now, DateTime.Now,
                                           DateTime.Now, DateTime.Now, DateTime.Now);
            return mUser;
        }

        public UserProfile getUser(MembershipUser use, IUnitOfWork repo)
        {
            if(use == null)
            {
                return null;
            }
            return _userProfileService.GetUser(use.UserName);
        }
    }
}