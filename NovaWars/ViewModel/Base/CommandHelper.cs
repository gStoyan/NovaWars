

using System.Linq;

namespace NovaWars.ViewModel.Base
{
   public static class CommandHelper
    {
        public static bool CheckParameter(object parameter)
        {
            if (parameter == null) {

                return false;
            }
            string text = parameter.ToString();

            if (text.Split('-').Any(t=>t=="" || t == " "))
            {
                return false;
            }
            return true;
        }

        public static bool CheckParameterForNull(object parameter)
            => parameter != null;
    }
}
