using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Util
{
    public class UploadFoto : ValidationAttribute
    {
        //sobrescrever o método IsValid
        public override bool IsValid(object value)
        {
            //converter o object para HttpPostedFileBase
            HttpPostedFileBase foto = (HttpPostedFileBase)value;

            //Regra: Arquivos JPG (MIME TYPE) de até 2MB
            return foto != null && foto.ContentType.Equals("image/jpeg")
                     && foto.ContentLength <= (2 * Math.Pow(1024, 2));
        }
    }
}