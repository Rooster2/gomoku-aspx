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
        /*
        if (!IsPostBack)
        {
            if (CommonState.PersonName != null)
            {
                textboxName.Text = CommonState.PersonName;
            }
        }*/
        labelErrorMsg.Text = "Please tell me your name:";
        labelErrorMsg.Visible = true;
        if (CommonState.PersonId != null)
        {
            Person p = Person.FindPersonById(CommonState.PersonId);
            if (Person.IsSessionAlive(p.LastActivity))
            {
                labelName.Visible = false;
                textboxName.Visible = false;
                buttonLogin.Visible = false;
                labelErrorMsg.Text = "Hello " + p.Name + ", You are already logged in";
                labelErrorMsg.Visible = true;
                linkLogout.Visible = true;
                linkSeperator.Visible = true;
                //linkLogout.Text = "Change name";
                linkContinue.Visible = true;
                //linkContinue.Text = "Take me to the Board List";
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
        Person p = Person.FindPersonByName(name);
        if (p != null)
        {
            Debug.WriteLine("there is a person named: " + name);
            // so username existed, we should dig further
            Debug.WriteLine(p.LastActivity.ToString() + " - " + CommonState.EpochTime + " = " +
                (p.LastActivity - CommonState.EpochTime).ToString());
            if (Person.IsSessionAlive(p.LastActivity))
            {
                Debug.WriteLine("and it is online");
                errorMsg = String.Format("The name \"{0}\" is already in use.", name);
                goto ShowError;
            }
            else
            {
                Debug.WriteLine("and it is sessioned out");
                if (Person.DeletePersonById(p.Id) == false)
                {
                    errorMsg = "looks like the man escaped (sql delete error?)";
                    goto ShowError;
                }
            }
        }

        // add a person
        Debug.WriteLine("no person named " + name + ", here we go!");
        string id = Guid.NewGuid().ToString();
        if (Person.SignUp(name, id) == true)
        {
            //CommonState.PersonName = name;
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
        labelErrorMsg.Visible = true;
        labelErrorMsg.Text = errorMsg;
    }
    protected void linkContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/boardlist.aspx");
    }
    protected void linkLogout_Click(object sender, EventArgs e)
    {
        string personId = CommonState.PersonId;
        if (!String.IsNullOrEmpty(personId) &&
            Person.FindPersonById(personId) != null)
        {
            Person.DeletePersonById(personId);
        }
        CommonState.PersonId = null;
        
        Response.Redirect("~/");
    }
}