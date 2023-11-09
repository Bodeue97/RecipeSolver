using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeSolverAPI.Templates
{
    public interface ITemplate
    {
         string GenerateEmailVerifyTemplatePL(string url, string name);
        string GenerateEmailResetPasswordTemplatePL(string url, string name);   
    }
}