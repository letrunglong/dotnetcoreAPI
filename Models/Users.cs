namespace CmdApi.Models
{
    public class TblUsers
    {

        //Comment set after pulling azure-pipelines.yaml

        public int Id {get; set;}
        public string Name {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public string Address {get; set;}
    }
    public class Login
    {
        public string UserName {get; set;}
        public string Password {get; set;}
    }
}

