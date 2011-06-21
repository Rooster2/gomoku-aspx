<%@ Import Namespace="System.Collections.Generic" %>
<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
        // Code that runs on application startup
		//Application.Add("users", users);
        Application.RemoveAll();
        Application.Clear();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

        Application.RemoveAll();
        Application.Clear();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
		//Session.RemoveAll();
    }
       
</script>
