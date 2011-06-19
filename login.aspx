<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="labelUsername" runat="server">Username:</asp:Label>
        <asp:TextBox ID="textboxUsername" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="labelPassword" runat="server">Password:</asp:Label>
        <asp:TextBox ID="textboxPassword" TextMode="Password" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="buttonLogin" Text="OK" runat="server" 
            onclick="buttonLogin_Click" />
    </div>
    </form>
</body>
</html>
