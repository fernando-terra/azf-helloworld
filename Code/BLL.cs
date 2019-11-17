using System;
using System.Text;

public class BLL {

    public static string HelloWorld(string name, string age = ""){
        
        var str = new StringBuilder();

        str.Append(string.Format("Olá {0}.", name));        
        str.Append((!string.IsNullOrEmpty(age) ? 
                     string.Format("Você tem {0} anos.", age) : 
                     string.Format("Não sabemos sua idade.")));
        
        return str.ToString();
    }
}