using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeSolverAPI.Templates
{
    public class Template : ITemplate
    {
        public string GenerateEmailVerifyTemplatePL(string url, string name)
        {
            return $"" +
                $"<h1>Witamy w RecipeSolver</h1>" +
                $"<h3>Witaj {name}</h3>" +
                $"<div>Dziękujemy za rejestrację konta w serwisie RecipeSolver. Jesteśmy niezmiernie wdzięczni za zaufanie i życzymy miłej współpracy.</div>" +
                $"<div>Prosimy kliknąć poniżej w celu aktywacji i weryfikacji adresu email.</div>" +
                $"<br> " +
                $"<a style=\"border-radius: 4px;background-color: rgb(0, 0, 0, .87);color: white; padding: 14px 25px; text-align: center; text-decoration: none; display: inline-block;\" href=\"{url}\" target=\"_blank\">Kliknij aby się zalogować.</a>" +
                $"<br>" +
                $"<h4>Pozdrawiamy</h4>" +
                $"<div>Zespół RecipeSolver</div>" +
                $"<br>" +
                $"<a href=\"https://www.recipesolver.pl\" style=\"color:#2F67F6\">recipesolver.pl</a>";
        }

        public string GenerateEmailResetPasswordTemplatePL(string url, string name)
        {
            return $"" +
                $"<h3>Dzień dobry {name}</h3>" +
                $"<div>otrzymaliśmy Twoją prośbę o zresetowanie hasła.</div>" +
                $"<br>" +
                $"<div>W celu zrestartowania hasła, kliknij w poniższy link</div>" +
                $"<a style=\"color: rgb(0, 0, 0, .87)\"  href=\"{url}\">{url}</a>     " +
                $"<br>" +
                $"<div>Jeśli jednak prośba o reset hasła <b>nie została wysłana przez Ciebie</b>, zignoruj tę wiadomość.</div>   " +
                $"<h4>Pozdrawiamy</h4>" +
                $"<div>Zespół RecipeSolver</div>";
        }
    }
}