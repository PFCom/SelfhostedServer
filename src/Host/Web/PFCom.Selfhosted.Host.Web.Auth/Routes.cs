namespace PFCom.Selfhosted.Host.Web.Auth
{
    public static class Routes
    {
        private const string AREA = "auth";
        
        private const string PREFIX = "v{version:apiVersion}/auth";
        
        public static class Register
        {
            public const string AREA = Routes.AREA + "_register";
            
            private const string PREFIX = Routes.PREFIX + "/register";
            
            public static class Local
            {
                public const string POST = Register.PREFIX + "/local";
            }
        }
    }
}
