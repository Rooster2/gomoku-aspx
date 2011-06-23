<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title>Gomoku - Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="labelErrorMsg" Visible="false" CssClass="loginerror" runat="server" />
        <br />
        <asp:LinkButton ID="linkLogout" runat="server" Visible="False" 
            onclick="linkLogout_Click">Logout/Change name</asp:LinkButton>
        <asp:Literal ID="linkSeperator" runat="server" Visible="False" >
         | </asp:Literal>
        <asp:LinkButton ID="linkContinue" runat="server" Visible="False" 
            onclick="linkContinue_Click">Take me to the Board List</asp:LinkButton>
        <br />
        <asp:Label ID="labelName" runat="server">Your name:</asp:Label>
        <asp:TextBox ID="textboxName" runat="server"></asp:TextBox>
        
        <!--
        <asp:Label ID="labelPassword" runat="server">Password:</asp:Label>
        <asp:TextBox ID="textboxPassword" TextMode="Password" runat="server"></asp:TextBox>
        <br />-->
        <asp:Button ID="buttonLogin" Text="OK" runat="server" 
            onclick="buttonLogin_Click" />
        <br />
    </div>
    </form>
</body>
</html>
