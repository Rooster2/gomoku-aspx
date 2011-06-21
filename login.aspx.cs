using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlServerCe;
using System.Configuration;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CommonState.PersonName != null)
            {
                textboxName.Text = CommonState.PersonName;
            }
        }
    }

    protected void buttonLogin_Click(object sender, EventArgs e)
    {
        // TODO: field length and type check
        string errorMsg = String.Empty;
        string name = textboxName.Text;
        if (String.IsNullOrEmpty(name))
        {
            errorMsg = "please supply a name!";
            goto ShowError;
        }

        // check person existence / kill it if session out
        if (Person.IsExisted(name))
        {
            if (Person.IsOnline(name))
            {
                errorMsg = String.Format("The name \"{0}\" is already in use.", name);
                goto ShowError;
            }
            else
            {
                if (Person.KillByName(name) == false)
                {
                    errorMsg = "looks like the man escaped (sql delete error?)";
                    goto ShowError;
                }
            }
        }

        // add a person
        string id = Guid.NewGuid().ToString();
        if (Person.SignUp(name, id) == true)
        {
            CommonState.PersonName = name;
            CommonState.PersonId = id;
            Response.Redirect("~/boardlist.aspx");
            return;
        }
        else
        {
            errorMsg = "cannot sign you up (sql insert error?)";
            goto ShowError;
        }

        // we love GOTO xD
    ShowError:
        labelErrorMsg.Text = errorMsg;
        labelErrorMsg.BackColor = System.Drawing.Color.Yellow;
    }
}