using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ProjectSystems.ViewModel
{
    public class AuthorizationVM : ViewModelBase
    {
        public AuthorizationVM()
        {
            Log.Information("Вызов конструктора меню авторизации");
        }
    }
}
